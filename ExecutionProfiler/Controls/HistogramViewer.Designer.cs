namespace Plugins.ExecutionProfiler.Controls
{
    partial class HistogramViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // HistogramViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "HistogramViewer";
            this.Size = new System.Drawing.Size(175, 162);
            this.Load += new System.EventHandler(this.HistogramViewer_Load);
            this.FontChanged += new System.EventHandler(this.HistogramViewer_FontChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HistogramViewer_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HistogramViewer_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HistogramViewer_MouseMove);
            this.Resize += new System.EventHandler(this.HistogramViewer_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
    }
}
