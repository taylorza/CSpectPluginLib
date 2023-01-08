namespace Plugins.ExecutionProfiler
{
    partial class FormProfiler
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.histogramViewer = new Plugins.ExecutionProfiler.Controls.HistogramViewer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvAsm = new System.Windows.Forms.ListView();
            this.Address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Disassembly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvSource = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Line = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerProfileUpdate = new System.Windows.Forms.Timer(this.components);
            this.txtSldFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSldFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProfileMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(93, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(696, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(13, 41);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.histogramViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(759, 388);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.TabIndex = 4;
            // 
            // histogramViewer
            // 
            this.histogramViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histogramViewer.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.histogramViewer.Location = new System.Drawing.Point(0, 0);
            this.histogramViewer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.histogramViewer.Name = "histogramViewer";
            this.histogramViewer.Size = new System.Drawing.Size(759, 120);
            this.histogramViewer.TabIndex = 9;
            this.histogramViewer.Locate += new System.EventHandler<string>(this.histogramViewer_Locate);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvAsm);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvSource);
            this.splitContainer2.Size = new System.Drawing.Size(753, 258);
            this.splitContainer2.SplitterDistance = 244;
            this.splitContainer2.TabIndex = 1;
            // 
            // lvAsm
            // 
            this.lvAsm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Address,
            this.Disassembly});
            this.lvAsm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAsm.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvAsm.FullRowSelect = true;
            this.lvAsm.HideSelection = false;
            this.lvAsm.Location = new System.Drawing.Point(0, 0);
            this.lvAsm.MultiSelect = false;
            this.lvAsm.Name = "lvAsm";
            this.lvAsm.Size = new System.Drawing.Size(244, 258);
            this.lvAsm.TabIndex = 10;
            this.lvAsm.UseCompatibleStateImageBehavior = false;
            this.lvAsm.View = System.Windows.Forms.View.Details;
            this.lvAsm.VirtualMode = true;
            this.lvAsm.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.LvAsm_RetrieveVirtualItem);
            this.lvAsm.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.LvAsm_SearchForVirtualItem);
            // 
            // Address
            // 
            this.Address.Text = "Address";
            this.Address.Width = 73;
            // 
            // Disassembly
            // 
            this.Disassembly.Text = "Disassembly";
            this.Disassembly.Width = 185;
            // 
            // lvSource
            // 
            this.lvSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.Line,
            this.Source});
            this.lvSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSource.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSource.FullRowSelect = true;
            this.lvSource.HideSelection = false;
            this.lvSource.Location = new System.Drawing.Point(0, 0);
            this.lvSource.MultiSelect = false;
            this.lvSource.Name = "lvSource";
            this.lvSource.ShowItemToolTips = true;
            this.lvSource.Size = new System.Drawing.Size(505, 258);
            this.lvSource.TabIndex = 11;
            this.lvSource.UseCompatibleStateImageBehavior = false;
            this.lvSource.View = System.Windows.Forms.View.Details;
            this.lvSource.VirtualMode = true;
            this.lvSource.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.LvSource_RetrieveVirtualItem);
            this.lvSource.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.lvSource_SearchForVirtualItem);
            this.lvSource.SelectedIndexChanged += new System.EventHandler(this.lvSource_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File";
            // 
            // Line
            // 
            this.Line.Text = "Line";
            this.Line.Width = 43;
            // 
            // Source
            // 
            this.Source.Text = "Source";
            this.Source.Width = 388;
            // 
            // timerProfileUpdate
            // 
            this.timerProfileUpdate.Interval = 1000;
            this.timerProfileUpdate.Tick += new System.EventHandler(this.timerProfileUpdate_Tick);
            // 
            // txtSldFile
            // 
            this.txtSldFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSldFile.Location = new System.Drawing.Point(421, 13);
            this.txtSldFile.Name = "txtSldFile";
            this.txtSldFile.ReadOnly = true;
            this.txtSldFile.Size = new System.Drawing.Size(232, 20);
            this.txtSldFile.TabIndex = 6;
            this.txtSldFile.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "SLD File";
            // 
            // btnSldFile
            // 
            this.btnSldFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSldFile.Location = new System.Drawing.Point(652, 13);
            this.btnSldFile.Name = "btnSldFile";
            this.btnSldFile.Size = new System.Drawing.Size(24, 21);
            this.btnSldFile.TabIndex = 7;
            this.btnSldFile.Text = "...";
            this.btnSldFile.UseVisualStyleBackColor = true;
            this.btnSldFile.Click += new System.EventHandler(this.btnSldFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mode";
            // 
            // cbProfileMode
            // 
            this.cbProfileMode.FormattingEnabled = true;
            this.cbProfileMode.Items.AddRange(new object[] {
            "Per Frame",
            "Every Execution"});
            this.cbProfileMode.Location = new System.Drawing.Point(230, 13);
            this.cbProfileMode.Name = "cbProfileMode";
            this.cbProfileMode.Size = new System.Drawing.Size(106, 21);
            this.cbProfileMode.TabIndex = 4;
            // 
            // FormProfiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.cbProfileMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSldFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSldFile);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "FormProfiler";
            this.Text = "Execution Profiler";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormProfiler_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timerProfileUpdate;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lvAsm;
        private System.Windows.Forms.ListView lvSource;
        private System.Windows.Forms.ColumnHeader Source;
        private System.Windows.Forms.ColumnHeader Disassembly;
        private System.Windows.Forms.ColumnHeader Address;
        private System.Windows.Forms.ColumnHeader Line;
        private System.Windows.Forms.TextBox txtSldFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSldFile;
        private Controls.HistogramViewer histogramViewer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbProfileMode;
    }
}