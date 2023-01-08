using Plugin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plugins.ExecutionProfiler
{
    public partial class FormProfiler : Form
    {
        class SldEntry
        {
            public int Address;
            public string File;
            public int Line;

            public override int GetHashCode()
            {
                return File.GetHashCode() * 31 + Line;
            }

            public override bool Equals(object obj)
            {
                if (obj== null) return false;
                if (obj is SldEntry s)
                {
                    return string.Compare(s.File, File, true) == 0 && s.Line == Line;
                }
                return false;
            }
        }

        private readonly iCSpect _cspect;

        private readonly Dictionary<int, SldEntry> _sld = new Dictionary<int, SldEntry>();
        private readonly Dictionary<string, string[]> _srcFiles = new Dictionary<string, string[]>();
        private readonly Dictionary<SldEntry, int> _sourceHits = new Dictionary<SldEntry, int>();
        private readonly List<ListViewItem> _asmItems = new List<ListViewItem>();
        private readonly List<ListViewItem> _srcItems = new List<ListViewItem>();
        
        public FormProfiler(iCSpect cspect)
        {
            Application.EnableVisualStyles();
            _cspect = cspect;
            InitializeComponent();
        }

        private void LvAsm_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            for(var i = 0; i < _asmItems.Count; i++)
            {
                if (_asmItems[i].Text == e.Text)
                {
                    e.Index = i;
                    break;
                }
            }
        }

        private void LvAsm_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= 0 && e.ItemIndex < _asmItems.Count) e.Item = _asmItems[e.ItemIndex];
        }

        private void LvSource_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= 0 && e.ItemIndex < _srcItems.Count) e.Item = _srcItems[e.ItemIndex];
        }

        private void lvSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSource.SelectedIndices.Count > 0)
            {
                var index = lvSource.SelectedIndices[0];
                var item = _srcItems[index];
                if (item != null) histogramViewer_Locate(lvSource, item.Text);
            }
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnReset.Enabled = false;
            ExecutionProfilerPlugin.Profile = true;
            timerProfileUpdate.Enabled = true;

            lvAsm.Items.Clear();
            lvSource.Items.Clear();            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timerProfileUpdate.Enabled = false;
            ExecutionProfilerPlugin.Profile = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnReset.Enabled = true;
            UpdateViews();
        }

        private void UpdateViews()
        {
            if (ExecutionProfilerPlugin.MaxAddress < ExecutionProfilerPlugin.MinAddress) return;
            StringBuilder sbAsm = new StringBuilder();
            StringBuilder sbSrc = new StringBuilder();

            var from = ExecutionProfilerPlugin.MinAddress;
            var to = ExecutionProfilerPlugin.MaxAddress;
            var data = ExecutionProfilerPlugin.Executed;
            //var data = (int[])_cspect.GetGlobal(eGlobal.profile_exe);

            var maxSourceHits = 0;

            _asmItems.Clear();
            _srcItems.Clear();
            for (int i = from; i < to;)
            {
                var l = _cspect.DissasembleMemory(i, true);
                var hits = data[i];
                var item = new ListViewItem($"{i >> 16:x2}:{(i & 0x0000ffff):x4}");
                item.SubItems.Add(l.line);
                item.BackColor = GetColor(hits, ExecutionProfilerPlugin.MaxValue);

                _asmItems.Add(item);
                if (_sld.TryGetValue(i, out SldEntry s))
                {
                    if (_sourceHits.TryGetValue(s, out int h))
                    {
                        hits += h;
                    }
                    _sourceHits[s] = hits;

                    if (hits > maxSourceHits) maxSourceHits = hits;
                }

                i += l.bytes;
            }

            lvSource.VirtualMode = false;
            foreach (var s in _sourceHits.OrderBy(f => f.Key.File + f.Key.Line.ToString("00000000")))
            {

                var item = new ListViewItem($"{s.Key.Address >> 16:x2}:{(s.Key.Address & 0x0000ffff):x4}");
                item.SubItems.Add(s.Key.File);
                item.SubItems.Add(s.Key.Line.ToString());
                item.SubItems.Add(_srcFiles[s.Key.File][s.Key.Line - 1]);
                item.BackColor = GetColor(s.Value, maxSourceHits);
                _srcItems.Add(item);
            }
            lvSource.VirtualMode = true;
            lvAsm.VirtualListSize = _asmItems.Count;
            lvSource.VirtualListSize = _srcItems.Count;
        }

        private static Color GetColor(int hits, int maxValue)
        {
            if (hits < 0 || maxValue < 1) return Color.White;

            var start = Color.White;
            var end = Color.Red;
            var ratio = (float)hits / maxValue;
            if (ratio < 0) ratio = 0;
            if (ratio > 1) ratio = 1;
            return Color.FromArgb(
                (int)(start.R + ((end.R - start.R) * ratio)),
                (int)(start.G + ((end.G - start.G) * ratio)),
                (int)(start.B + ((end.B - start.B) * ratio)));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _asmItems.Clear();
            _srcItems.Clear();

            lvAsm.VirtualListSize = 0;
            lvSource.VirtualListSize = 0;

            ExecutionProfilerPlugin.MaxValue = 0;
            ExecutionProfilerPlugin.MinAddress = int.MaxValue;
            ExecutionProfilerPlugin.MaxAddress = 0;
            Array.Clear(ExecutionProfilerPlugin.Executed, 0, ExecutionProfilerPlugin.Executed.Length);
            histogramViewer.Clear();
        }

        private void timerProfileUpdate_Tick(object sender, EventArgs e)
        {
            if (ExecutionProfilerPlugin.MaxAddress <= ExecutionProfilerPlugin.MinAddress || ExecutionProfilerPlugin.MaxValue == 0) return;
            histogramViewer.SetData(ExecutionProfilerPlugin.MinAddress, ExecutionProfilerPlugin.MaxAddress, ExecutionProfilerPlugin.Executed);
        }

        private void FormProfiler_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExecutionProfilerPlugin.Profile = false;
            ExecutionProfilerPlugin.Form = null;
            ExecutionProfilerPlugin.Active= false;
        }

        private void histogramView_Paint(object sender, PaintEventArgs e)
        {
            if (ExecutionProfilerPlugin.MaxAddress <= ExecutionProfilerPlugin.MinAddress || ExecutionProfilerPlugin.MaxValue == 0) return;
            histogramViewer.ForceUpdate();
        }

        private void btnSldFile_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "sjasmplus sld (*.sld)|*.sld",
                CheckFileExists = true
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtSldFile.Text = dlg.FileName;
                LoadSld(dlg.FileName);
                if (!ExecutionProfilerPlugin.Profile) UpdateViews();
            }
        }

        void LoadSld(string filename)
        {
            using (var f = new StreamReader(filename))
            {
                SldEntry lastSldEntry = null;
                string sldLine;
                while ((sldLine = f.ReadLine()) != null)
                {
                    var sldParts = sldLine.Split('|');
                    if (sldParts.Length < 6 || string.IsNullOrEmpty(sldParts[0]) || string.IsNullOrEmpty(sldParts[1])) continue;

                    var srcFile = sldParts[0];
                    var srcLine = int.Parse(sldParts[1]);
                    //var bank = int.Parse(sldParts[4]);
                    var address = int.Parse(sldParts[5]);
                    var type = sldParts[6];
                    var comment = sldParts[7];

                    if (_sld.ContainsKey(address) && lastSldEntry != null) continue;

                    if (type == "K")
                    {
                        var zxndbgstart = comment.IndexOf("#ZXNDBG,START");
                        var zxndbgend = comment.IndexOf("#ZXNDBG,END");
                        
                        if (zxndbgstart >= 0)
                        {
                            var commentParts = sldLine.Substring(zxndbgstart).Split(',');
                            if (commentParts.Length >= 4)
                            {
                                srcFile = commentParts[2];
                                srcLine = int.Parse(commentParts[3]);
                                lastSldEntry = new SldEntry() {Address = address, File = srcFile, Line = srcLine };
                            }
                        }
                        else if (zxndbgend >= 0)
                        {
                            lastSldEntry = null;
                            continue;
                        }
                    }

                    if (lastSldEntry != null)
                    {
                        _sld.Add(address, lastSldEntry);
                    }
                    else 
                    {
                        _sld[address] = new SldEntry() {Address = address, File = srcFile, Line = srcLine };
                    }

                    if (!_srcFiles.ContainsKey(srcFile))
                    {
                        var lines = File.ReadAllLines(srcFile);
                        _srcFiles.Add(srcFile, lines);
                    }
                }
            }
        }

        private void histogramViewer_Locate(object _, string e)
        {
            var item = lvAsm.FindItemWithText(e);
            if (item != null)
            {
                lvAsm.EnsureVisible(item.Index);
            }
        }
    }
}
