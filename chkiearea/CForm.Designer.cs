namespace ChkIEArea {
    partial class CForm {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CForm));
            this.lt = new System.Windows.Forms.Label();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.l1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bPDF = new System.Windows.Forms.ToolStripButton();
            this.bDOC = new System.Windows.Forms.ToolStripButton();
            this.bDOCX = new System.Windows.Forms.ToolStripButton();
            this.bXLS = new System.Windows.Forms.ToolStripButton();
            this.bXLSX = new System.Windows.Forms.ToolStripButton();
            this.bPPT = new System.Windows.Forms.ToolStripButton();
            this.bPPTX = new System.Windows.Forms.ToolStripButton();
            this.bDXF = new System.Windows.Forms.ToolStripButton();
            this.bTIF = new System.Windows.Forms.ToolStripButton();
            this.bTIFF = new System.Windows.Forms.ToolStripButton();
            this.bHTM = new System.Windows.Forms.ToolStripButton();
            this.bHTML = new System.Windows.Forms.ToolStripButton();
            this.bEML = new System.Windows.Forms.ToolStripButton();
            this.bMHT = new System.Windows.Forms.ToolStripButton();
            this.bTXT = new System.Windows.Forms.ToolStripButton();
            this.bMP4 = new System.Windows.Forms.ToolStripButton();
            this.bWMV = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.bEditFlags = new System.Windows.Forms.ToolStripButton();
            this.bBrowserFlags = new System.Windows.Forms.ToolStripButton();
            this.bBrowserFlags2 = new System.Windows.Forms.ToolStripButton();
            this.bBrowseInPlace = new System.Windows.Forms.ToolStripButton();
            this.bMIME = new System.Windows.Forms.ToolStripButton();
            this.bMIMEefp = new System.Windows.Forms.ToolStripButton();
            this.bMIMEde = new System.Windows.Forms.ToolStripButton();
            this.bAcro5 = new System.Windows.Forms.ToolStripButton();
            this.bAcro6 = new System.Windows.Forms.ToolStripButton();
            this.bAcroExe = new System.Windows.Forms.ToolStripDropDownButton();
            this.bAdobePlan = new System.Windows.Forms.ToolStripButton();
            this.l2 = new System.Windows.Forms.Label();
            this.l3 = new System.Windows.Forms.Label();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.bRestart = new System.Windows.Forms.ToolStripButton();
            this.l4 = new System.Windows.Forms.Label();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.bOOo = new System.Windows.Forms.ToolStripDropDownButton();
            this.bMso = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lt
            // 
            this.lt.AutoSize = true;
            this.lt.Dock = System.Windows.Forms.DockStyle.Top;
            this.lt.Location = new System.Drawing.Point(0, 0);
            this.lt.Name = "lt";
            this.lt.Size = new System.Drawing.Size(92, 12);
            this.lt.TabIndex = 0;
            this.lt.Text = "展開されるIEエリア";
            // 
            // wb
            // 
            this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb.Location = new System.Drawing.Point(0, 0);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(674, 132);
            this.wb.TabIndex = 0;
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.l1.Location = new System.Drawing.Point(0, 148);
            this.l1.Margin = new System.Windows.Forms.Padding(3);
            this.l1.Name = "l1";
            this.l1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.l1.Size = new System.Drawing.Size(433, 22);
            this.l1.TabIndex = 2;
            this.l1.Text = "調査項目 (Shift+ボタン押下で、新しい画面に表示。Ctrl+ボタン押下で、シェル操作で開く)";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.wb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 136);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bPDF,
            this.bDOC,
            this.bDOCX,
            this.bXLS,
            this.bXLSX,
            this.bPPT,
            this.bPPTX,
            this.bDXF,
            this.bTIF,
            this.bTIFF,
            this.bHTM,
            this.bHTML,
            this.bEML,
            this.bMHT,
            this.bTXT,
            this.bMP4,
            this.bWMV});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 170);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(678, 46);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.TabStop = true;
            // 
            // bPDF
            // 
            this.bPDF.BackColor = System.Drawing.SystemColors.Control;
            this.bPDF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bPDF.Image = global::ChkIEArea.Properties.Resources.PlayHS;
            this.bPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bPDF.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bPDF.Name = "bPDF";
            this.bPDF.Size = new System.Drawing.Size(53, 20);
            this.bPDF.Text = "PDF";
            this.bPDF.Click += new System.EventHandler(this.buttonPDF_Click);
            // 
            // bDOC
            // 
            this.bDOC.BackColor = System.Drawing.SystemColors.Control;
            this.bDOC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bDOC.Image = ((System.Drawing.Image)(resources.GetObject("bDOC.Image")));
            this.bDOC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bDOC.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bDOC.Name = "bDOC";
            this.bDOC.Size = new System.Drawing.Size(59, 20);
            this.bDOC.Text = "Word";
            this.bDOC.Click += new System.EventHandler(this.buttonDOC_Click);
            // 
            // bDOCX
            // 
            this.bDOCX.BackColor = System.Drawing.SystemColors.Control;
            this.bDOCX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bDOCX.Image = ((System.Drawing.Image)(resources.GetObject("bDOCX.Image")));
            this.bDOCX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bDOCX.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bDOCX.Name = "bDOCX";
            this.bDOCX.Size = new System.Drawing.Size(91, 20);
            this.bDOCX.Text = "Word2007";
            this.bDOCX.Click += new System.EventHandler(this.bDOCX_Click);
            // 
            // bXLS
            // 
            this.bXLS.BackColor = System.Drawing.SystemColors.Control;
            this.bXLS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bXLS.Image = ((System.Drawing.Image)(resources.GetObject("bXLS.Image")));
            this.bXLS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bXLS.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bXLS.Name = "bXLS";
            this.bXLS.Size = new System.Drawing.Size(62, 20);
            this.bXLS.Text = "Excel";
            this.bXLS.Click += new System.EventHandler(this.bXLS_Click);
            // 
            // bXLSX
            // 
            this.bXLSX.BackColor = System.Drawing.SystemColors.Control;
            this.bXLSX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bXLSX.Image = ((System.Drawing.Image)(resources.GetObject("bXLSX.Image")));
            this.bXLSX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bXLSX.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bXLSX.Name = "bXLSX";
            this.bXLSX.Size = new System.Drawing.Size(94, 20);
            this.bXLSX.Text = "Excel2007";
            this.bXLSX.Click += new System.EventHandler(this.bXLSX_Click);
            // 
            // bPPT
            // 
            this.bPPT.BackColor = System.Drawing.SystemColors.Control;
            this.bPPT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bPPT.Image = ((System.Drawing.Image)(resources.GetObject("bPPT.Image")));
            this.bPPT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bPPT.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bPPT.Name = "bPPT";
            this.bPPT.Size = new System.Drawing.Size(47, 20);
            this.bPPT.Text = "ppt";
            this.bPPT.Click += new System.EventHandler(this.buttonPPT_Click);
            // 
            // bPPTX
            // 
            this.bPPTX.BackColor = System.Drawing.SystemColors.Control;
            this.bPPTX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bPPTX.Image = ((System.Drawing.Image)(resources.GetObject("bPPTX.Image")));
            this.bPPTX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bPPTX.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bPPTX.Name = "bPPTX";
            this.bPPTX.Size = new System.Drawing.Size(54, 20);
            this.bPPTX.Text = "pptx";
            this.bPPTX.Click += new System.EventHandler(this.buttonPPTX_Click);
            // 
            // bDXF
            // 
            this.bDXF.BackColor = System.Drawing.SystemColors.Control;
            this.bDXF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bDXF.Image = ((System.Drawing.Image)(resources.GetObject("bDXF.Image")));
            this.bDXF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bDXF.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bDXF.Name = "bDXF";
            this.bDXF.Size = new System.Drawing.Size(91, 20);
            this.bDXF.Text = "DXF(CAD)";
            this.bDXF.Click += new System.EventHandler(this.bDXF_Click);
            // 
            // bTIF
            // 
            this.bTIF.BackColor = System.Drawing.SystemColors.Control;
            this.bTIF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bTIF.Image = ((System.Drawing.Image)(resources.GetObject("bTIF.Image")));
            this.bTIF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bTIF.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bTIF.Name = "bTIF";
            this.bTIF.Size = new System.Drawing.Size(48, 20);
            this.bTIF.Text = "TIF";
            this.bTIF.Click += new System.EventHandler(this.bTIF_Click);
            // 
            // bTIFF
            // 
            this.bTIFF.BackColor = System.Drawing.SystemColors.Control;
            this.bTIFF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bTIFF.Image = ((System.Drawing.Image)(resources.GetObject("bTIFF.Image")));
            this.bTIFF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bTIFF.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bTIFF.Name = "bTIFF";
            this.bTIFF.Size = new System.Drawing.Size(56, 20);
            this.bTIFF.Text = "TIFF";
            this.bTIFF.Click += new System.EventHandler(this.bTIFF_Click);
            // 
            // bHTM
            // 
            this.bHTM.BackColor = System.Drawing.SystemColors.Control;
            this.bHTM.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bHTM.Image = ((System.Drawing.Image)(resources.GetObject("bHTM.Image")));
            this.bHTM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bHTM.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bHTM.Name = "bHTM";
            this.bHTM.Size = new System.Drawing.Size(56, 20);
            this.bHTM.Text = "HTM";
            this.bHTM.Click += new System.EventHandler(this.buttonHTM_Click);
            // 
            // bHTML
            // 
            this.bHTML.BackColor = System.Drawing.SystemColors.Control;
            this.bHTML.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bHTML.Image = ((System.Drawing.Image)(resources.GetObject("bHTML.Image")));
            this.bHTML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bHTML.Name = "bHTML";
            this.bHTML.Size = new System.Drawing.Size(64, 20);
            this.bHTML.Text = "HTML";
            this.bHTML.Click += new System.EventHandler(this.buttonHTML_Click);
            // 
            // bEML
            // 
            this.bEML.BackColor = System.Drawing.SystemColors.Control;
            this.bEML.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bEML.Image = ((System.Drawing.Image)(resources.GetObject("bEML.Image")));
            this.bEML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bEML.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.bEML.Name = "bEML";
            this.bEML.Size = new System.Drawing.Size(54, 20);
            this.bEML.Text = "EML";
            this.bEML.Click += new System.EventHandler(this.buttonEML_Click);
            // 
            // bMHT
            // 
            this.bMHT.BackColor = System.Drawing.SystemColors.Control;
            this.bMHT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bMHT.Image = ((System.Drawing.Image)(resources.GetObject("bMHT.Image")));
            this.bMHT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMHT.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.bMHT.Name = "bMHT";
            this.bMHT.Size = new System.Drawing.Size(56, 20);
            this.bMHT.Text = "MHT";
            this.bMHT.Click += new System.EventHandler(this.buttonMHT_Click);
            // 
            // bTXT
            // 
            this.bTXT.BackColor = System.Drawing.SystemColors.Control;
            this.bTXT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bTXT.Image = ((System.Drawing.Image)(resources.GetObject("bTXT.Image")));
            this.bTXT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bTXT.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.bTXT.Name = "bTXT";
            this.bTXT.Size = new System.Drawing.Size(46, 20);
            this.bTXT.Text = "txt";
            this.bTXT.Click += new System.EventHandler(this.buttontxt_Click);
            // 
            // bMP4
            // 
            this.bMP4.BackColor = System.Drawing.SystemColors.Control;
            this.bMP4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bMP4.Image = ((System.Drawing.Image)(resources.GetObject("bMP4.Image")));
            this.bMP4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMP4.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.bMP4.Name = "bMP4";
            this.bMP4.Size = new System.Drawing.Size(55, 20);
            this.bMP4.Text = "MP4";
            this.bMP4.Click += new System.EventHandler(this.bMP4_Click);
            // 
            // bWMV
            // 
            this.bWMV.BackColor = System.Drawing.SystemColors.Control;
            this.bWMV.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bWMV.Image = ((System.Drawing.Image)(resources.GetObject("bWMV.Image")));
            this.bWMV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bWMV.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.bWMV.Name = "bWMV";
            this.bWMV.Size = new System.Drawing.Size(58, 20);
            this.bWMV.Text = "WMV";
            this.bWMV.Click += new System.EventHandler(this.bWMV_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bEditFlags,
            this.bBrowserFlags,
            this.bBrowserFlags2,
            this.bBrowseInPlace,
            this.bMIME,
            this.bMIMEefp,
            this.bMIMEde,
            this.bAcro5,
            this.bAcro6,
            this.bAcroExe,
            this.bAdobePlan});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 283);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(678, 138);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.TabStop = true;
            // 
            // bEditFlags
            // 
            this.bEditFlags.BackColor = System.Drawing.SystemColors.Control;
            this.bEditFlags.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bEditFlags.Image = global::ChkIEArea.Properties.Resources.ZoomHS;
            this.bEditFlags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bEditFlags.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bEditFlags.Name = "bEditFlags";
            this.bEditFlags.Size = new System.Drawing.Size(270, 20);
            this.bEditFlags.Text = "保存確認が出る場合は、EditFlags修正案";
            this.bEditFlags.Click += new System.EventHandler(this.buttonEditFlags_Click);
            // 
            // bBrowserFlags
            // 
            this.bBrowserFlags.BackColor = System.Drawing.SystemColors.Control;
            this.bBrowserFlags.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bBrowserFlags.Image = ((System.Drawing.Image)(resources.GetObject("bBrowserFlags.Image")));
            this.bBrowserFlags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bBrowserFlags.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bBrowserFlags.Name = "bBrowserFlags";
            this.bBrowserFlags.Size = new System.Drawing.Size(319, 20);
            this.bBrowserFlags.Text = "IEでインライン表示するには、BrowserFlags修正案!";
            this.bBrowserFlags.Click += new System.EventHandler(this.buttonBrowserFlags_Click);
            // 
            // bBrowserFlags2
            // 
            this.bBrowserFlags2.Image = global::ChkIEArea.Properties.Resources.ZoomHS;
            this.bBrowserFlags2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bBrowserFlags2.Name = "bBrowserFlags2";
            this.bBrowserFlags2.Size = new System.Drawing.Size(545, 20);
            this.bBrowserFlags2.Text = "Word/PowerPointが飛び出る場合は、BrowserFlags修正案(Office2007/2010/2013)";
            this.bBrowserFlags2.Click += new System.EventHandler(this.bBrowserFlags2_Click);
            // 
            // bBrowseInPlace
            // 
            this.bBrowseInPlace.Image = global::ChkIEArea.Properties.Resources.ZoomHS;
            this.bBrowseInPlace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bBrowseInPlace.Name = "bBrowseInPlace";
            this.bBrowseInPlace.Size = new System.Drawing.Size(164, 20);
            this.bBrowseInPlace.Text = "BrowseInPlace修正案";
            this.bBrowseInPlace.Click += new System.EventHandler(this.bBrowseInPlace_Click);
            // 
            // bMIME
            // 
            this.bMIME.BackColor = System.Drawing.SystemColors.Control;
            this.bMIME.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bMIME.Image = ((System.Drawing.Image)(resources.GetObject("bMIME.Image")));
            this.bMIME.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMIME.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bMIME.Name = "bMIME";
            this.bMIME.Size = new System.Drawing.Size(186, 20);
            this.bMIME.Text = "MIME修正案｢Use CLSID｣";
            this.bMIME.ToolTipText = "OLE用CLSIDを、IE用CLSIDにコピーします";
            this.bMIME.Click += new System.EventHandler(this.bMIME_Click);
            // 
            // bMIMEefp
            // 
            this.bMIMEefp.BackColor = System.Drawing.SystemColors.Control;
            this.bMIMEefp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bMIMEefp.Image = ((System.Drawing.Image)(resources.GetObject("bMIMEefp.Image")));
            this.bMIMEefp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMIMEefp.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.bMIMEefp.Name = "bMIMEefp";
            this.bMIMEefp.Size = new System.Drawing.Size(342, 20);
            this.bMIMEefp.Text = "MIME修正案｢Use EFP｣(CLSID by EnableFullPage)";
            this.bMIMEefp.ToolTipText = "OLE用CLSIDのEnableFullPageを検索し、IE用CLSIDにコピーします";
            this.bMIMEefp.Click += new System.EventHandler(this.bMIME_Click);
            // 
            // bMIMEde
            // 
            this.bMIMEde.Image = global::ChkIEArea.Properties.Resources.ZoomHS;
            this.bMIMEde.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMIMEde.Name = "bMIMEde";
            this.bMIMEde.Size = new System.Drawing.Size(350, 20);
            this.bMIMEde.Text = "MIME修正案「Use DE」(CLSID by DefaultExtension)";
            this.bMIMEde.ToolTipText = "OLE用CLSIDのDefaultExtensionを検索し、IE用CLSIDにコピーします";
            this.bMIMEde.Click += new System.EventHandler(this.bMIME_Click);
            // 
            // bAcro5
            // 
            this.bAcro5.Image = global::ChkIEArea.Properties.Resources.PrintPreviewHS;
            this.bAcro5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bAcro5.Name = "bAcro5";
            this.bAcro5.Size = new System.Drawing.Size(149, 20);
            this.bAcro5.Text = "Acrobat 5.x 修正案";
            this.bAcro5.Click += new System.EventHandler(this.bAcro5_Click);
            // 
            // bAcro6
            // 
            this.bAcro6.Image = global::ChkIEArea.Properties.Resources.PrintPreviewHS;
            this.bAcro6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bAcro6.Name = "bAcro6";
            this.bAcro6.Size = new System.Drawing.Size(149, 20);
            this.bAcro6.Text = "Acrobat 6.x 修正案";
            this.bAcro6.Click += new System.EventHandler(this.bAcro6_Click);
            // 
            // bAcroExe
            // 
            this.bAcroExe.Image = global::ChkIEArea.Properties.Resources.PrintPreviewHS;
            this.bAcroExe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bAcroExe.Name = "bAcroExe";
            this.bAcroExe.Size = new System.Drawing.Size(323, 20);
            this.bAcroExe.Text = "HKCR\\Software\\Adobe\\Acrobat\\Exe 修正案";
            this.bAcroExe.DropDownOpening += new System.EventHandler(this.bAcroExe_DropDownOpening);
            // 
            // bAdobePlan
            // 
            this.bAdobePlan.Image = global::ChkIEArea.Properties.Resources.ZoomHS;
            this.bAdobePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bAdobePlan.Name = "bAdobePlan";
            this.bAdobePlan.Size = new System.Drawing.Size(300, 20);
            this.bAdobePlan.Text = "Adobe Reader bBrowserIntegration 修正案";
            this.bAdobePlan.Click += new System.EventHandler(this.bAdobePlan_Click);
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.l2.Location = new System.Drawing.Point(0, 261);
            this.l2.Margin = new System.Windows.Forms.Padding(3);
            this.l2.Name = "l2";
            this.l2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.l2.Size = new System.Drawing.Size(41, 22);
            this.l2.TabIndex = 4;
            this.l2.Text = "修正者";
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.l3.Location = new System.Drawing.Point(0, 421);
            this.l3.Margin = new System.Windows.Forms.Padding(3);
            this.l3.Name = "l3";
            this.l3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.l3.Size = new System.Drawing.Size(29, 22);
            this.l3.TabIndex = 6;
            this.l3.Text = "補助";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bRestart});
            this.toolStrip3.Location = new System.Drawing.Point(0, 443);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(678, 25);
            this.toolStrip3.TabIndex = 7;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // bRestart
            // 
            this.bRestart.Image = ((System.Drawing.Image)(resources.GetObject("bRestart.Image")));
            this.bRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bRestart.Name = "bRestart";
            this.bRestart.Size = new System.Drawing.Size(136, 22);
            this.bRestart.Text = "プログラムを再起動する";
            this.bRestart.Click += new System.EventHandler(this.bRestart_Click);
            // 
            // l4
            // 
            this.l4.AutoSize = true;
            this.l4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.l4.Location = new System.Drawing.Point(0, 216);
            this.l4.Margin = new System.Windows.Forms.Padding(3);
            this.l4.Name = "l4";
            this.l4.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.l4.Size = new System.Drawing.Size(53, 22);
            this.l4.TabIndex = 8;
            this.l4.Text = "簡単設定";
            // 
            // toolStrip4
            // 
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bOOo,
            this.bMso});
            this.toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip4.Location = new System.Drawing.Point(0, 238);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(678, 23);
            this.toolStrip4.TabIndex = 9;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // bOOo
            // 
            this.bOOo.Image = global::ChkIEArea.Properties.Resources.PrintPreviewHS;
            this.bOOo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bOOo.Name = "bOOo";
            this.bOOo.Size = new System.Drawing.Size(223, 20);
            this.bOOo.Text = "OpenOffice ActiveX を有効化";
            this.bOOo.DropDownOpening += new System.EventHandler(this.bOOo_DropDownOpening);
            // 
            // bMso
            // 
            this.bMso.Image = global::ChkIEArea.Properties.Resources.PrintPreviewHS;
            this.bMso.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMso.Name = "bMso";
            this.bMso.Size = new System.Drawing.Size(193, 20);
            this.bMso.Text = "Microsoft Office を有効化";
            this.bMso.Click += new System.EventHandler(this.bMso_Click);
            // 
            // CForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 468);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.l1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.l4);
            this.Controls.Add(this.toolStrip4);
            this.Controls.Add(this.l2);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.l3);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.lt);
            this.Name = "CForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chk IE Area";
            this.Load += new System.EventHandler(this.CForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lt;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bPDF;
        private System.Windows.Forms.ToolStripButton bDOC;
        private System.Windows.Forms.ToolStripButton bPPT;
        private System.Windows.Forms.ToolStripButton bHTM;
        private System.Windows.Forms.ToolStripButton bHTML;
        private System.Windows.Forms.ToolStripButton bMHT;
        private System.Windows.Forms.ToolStripButton bTXT;
        private System.Windows.Forms.ToolStripButton bEML;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton bEditFlags;
        private System.Windows.Forms.ToolStripButton bBrowserFlags;
        private System.Windows.Forms.ToolStripButton bMIME;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.ToolStripButton bMIMEefp;
        private System.Windows.Forms.ToolStripButton bDOCX;
        private System.Windows.Forms.ToolStripButton bBrowserFlags2;
        private System.Windows.Forms.ToolStripButton bMIMEde;
        private System.Windows.Forms.ToolStripButton bAcro5;
        private System.Windows.Forms.ToolStripButton bAcro6;
        private System.Windows.Forms.ToolStripDropDownButton bAcroExe;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton bRestart;
        private System.Windows.Forms.ToolStripButton bXLS;
        private System.Windows.Forms.ToolStripButton bXLSX;
        private System.Windows.Forms.ToolStripButton bDXF;
        private System.Windows.Forms.ToolStripButton bTIF;
        private System.Windows.Forms.ToolStripButton bTIFF;
        private System.Windows.Forms.ToolStripButton bPPTX;
        private System.Windows.Forms.ToolStripButton bMP4;
        private System.Windows.Forms.ToolStripButton bWMV;
        private System.Windows.Forms.ToolStripButton bBrowseInPlace;
        private System.Windows.Forms.Label l4;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripDropDownButton bOOo;
        private System.Windows.Forms.ToolStripButton bMso;
        private System.Windows.Forms.ToolStripButton bAdobePlan;
    }
}

