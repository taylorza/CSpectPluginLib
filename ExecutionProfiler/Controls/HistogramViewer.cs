using System;
using System.Drawing;
using System.Windows.Forms;

namespace Plugins.ExecutionProfiler.Controls
{
    public partial class HistogramViewer : UserControl
    {
        private readonly BufferedGraphicsContext _ctx;
        private BufferedGraphics _gfx;

        private int _labelCount;
        private int[] _buckets = null;
        private int _minBucket = int.MaxValue;
        private int _maxBucket = int.MinValue;
        private int _viewMinBucket;
        private int _viewMaxBucket;
        private int _viewMinX;
        private int _viewMaxX;

        private int _labelWidth;
        private int _labelHeight;

        private int _histogramViewHeight;
        private int _histogramViewWidth;

        public HistogramViewer()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            _ctx = BufferedGraphicsManager.Current;
            _ctx.MaximumBuffer = new Size(Width + 1, Height + 1);
            MouseWheel += HistogramViewer_MouseWheel;
            InitializeComponent();
        }

        private void HistogramViewer_Load(object sender, EventArgs e)
        {
            CalculateLabels();
        }

        private void CalculateLabels()
        {
            using (var g = this.CreateGraphics())
            {
                var size = g.MeasureString("00:0000", Font);
                _labelWidth = (int)size.Width;
                _labelHeight = (int)size.Height;
            }
            _labelCount = Math.Max(Width / (_labelWidth * 4), 1);
            _viewMinX = _labelWidth / 2;
            _viewMaxX = Width - _labelWidth / 2;
            _histogramViewHeight = Height - _labelHeight - 5;
            _histogramViewWidth = Width - _labelWidth;
        }

        public void Clear()
        {
            _minBucket = int.MaxValue;
            _maxBucket = int.MinValue;
            _viewMinBucket = 0;
            _viewMaxBucket = 0;

            ForceUpdate();
        }

        public void Reset()
        {
            _viewMinBucket = _minBucket;
            _viewMaxBucket = _maxBucket;
            ForceUpdate();
        }

        public void SetData(int minBucket, int maxBucket, int[] values)
        {
            _buckets = values;

            if (_minBucket > minBucket) _minBucket = minBucket;
            if (_maxBucket < maxBucket) _maxBucket = maxBucket;
            
            _viewMinBucket = _minBucket;
            _viewMaxBucket = _maxBucket;

            ForceUpdate();
        }

        public void ForceUpdate()
        {
            if (_gfx != null) DrawHistogram(_gfx.Graphics);
            Refresh();
        }

        private void HistogramViewer_Paint(object sender, PaintEventArgs e)
        {
            _gfx.Render(e.Graphics);    
        }

        private void DrawHistogram(Graphics g)
        {
            if (_gfx == null) return;
            _gfx.Graphics.FillRectangle(Brushes.White, 0, 0, Width, Height);
            _gfx.Graphics.DrawLine(Pens.Black, 0, Height - _labelHeight, Width, Height - _labelHeight);

            var range = _viewMaxBucket - _viewMinBucket;
            if (range <= 0) return;

            DrawXAxisLabels(g, range);

            var maxValue = 0;
            for (int i = _viewMinBucket; i <= _viewMaxBucket; i++)
            {
                if (maxValue < _buckets[i]) maxValue = _buckets[i];
            }
            if (maxValue <= 0) return;

            var yscale = (float)_histogramViewHeight / maxValue;
            var xscale = (float)_histogramViewWidth / range;

            float x = _viewMinX;
            for (int i = _viewMinBucket; i <= _viewMaxBucket; i++)
            {
                var barHeight = yscale * _buckets[i];
                if (_buckets[i] > 0) barHeight = Math.Max(barHeight, 2); // Ensure that low values are not completely drowned out

                var y = _histogramViewHeight - barHeight;
                g.FillRectangle(Brushes.Black, new RectangleF(x, y, Math.Max(1, xscale), barHeight));
                x += xscale;
            }
        }

        private void DrawXAxisLabels(Graphics g, int range)
        {
            var addressStep = (float)(range / _labelCount);
            var labelStep = _histogramViewWidth / _labelCount;
            int address;
            int bank;
            int offset;
            for (var label = 0; label < _labelCount; label++)
            {
                address = (int)(_viewMinBucket + (label * addressStep));
                bank = address >> 16;
                offset = address & 0xffff;
                g.DrawString($"{bank:x2}:{offset:x4}", Font, Brushes.Black, (label * labelStep), Height - _labelHeight);
            }

            address = _viewMaxBucket;
            bank = address >> 16;
            offset = address & 0xffff;
            g.DrawString($"{bank:x2}:{offset:x4}", Font, Brushes.Black, _histogramViewWidth, Height - _labelHeight);
        }

        private void HistogramViewer_FontChanged(object sender, EventArgs e)
        {
            CalculateLabels();
            ForceUpdate();
        }

        private void HistogramViewer_Resize(object sender, EventArgs e)
        {
            _ctx.MaximumBuffer = new Size(Width + 1, Height + 1);
            _gfx?.Dispose();
            _gfx = _ctx.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));

            CalculateLabels();
            ForceUpdate();
        }

        private void HistogramViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            var range = _viewMaxBucket - _viewMinBucket;
            var histogramViewWidth = Width - _labelWidth;
            var address = _viewMinBucket + (int)(((float)range / histogramViewWidth) * (e.X - _viewMinX));

            var deltaMin = e.Delta * (float)(address - _viewMinBucket) / histogramViewWidth;
            var deltaMax = e.Delta * (float)(_viewMaxBucket - address) / histogramViewWidth;

            var start = (int)(_viewMinBucket + deltaMin);
            var end = (int)(_viewMaxBucket - deltaMax);

            if (end - start < 32) return;
            if (start < end && start >= _minBucket) _viewMinBucket = start;
            if (end > start && end <= _maxBucket) _viewMaxBucket = end;

            CalculateLabels();
            ForceUpdate();
        }

        private void HistogramViewer_MouseMove(object sender, MouseEventArgs e)
        {
            var range = _viewMaxBucket - _viewMinBucket;
            var histogramViewWidth = Width - _labelWidth;
            var address = _viewMinBucket + (int)(((float)range / histogramViewWidth) * (e.X - _viewMinX));
            var bank = address >> 16;
            var offset = address & 0xffff;
            toolTip.SetToolTip(this, $"{bank:x2}:{offset:x4}");
        }

        private void HistogramViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (_viewMinBucket >= _viewMaxBucket) return;

            var range = _viewMaxBucket - _viewMinBucket;
            var histogramViewWidth = Width - _labelWidth;
            var address = (int)(_viewMinBucket + ((float)range / histogramViewWidth) * (e.X - _viewMinX));
            if (address < _viewMinBucket || address > _viewMaxBucket) return;
            OnLocate(address);
        }

        protected void OnLocate(int address)
        {
            var bank = address >> 16;
            var offset = address & 0xffff;            
            Locate?.Invoke(this, $"{bank:x2}:{offset:x4}");
        }

        public event EventHandler<string> Locate;

        
    }
}

