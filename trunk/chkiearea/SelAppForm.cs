using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ChkIEArea.Properties;

namespace ChkIEArea {
    public partial class SelAppForm : Form {
        public SelAppForm(IDictionary<String, Guid> dict) {
            this.dict = dict;

            InitializeComponent();
        }

        IDictionary<String, Guid> dict;

        private void SelAppForm_Load(object sender, EventArgs e) {
            foreach (KeyValuePair<String, Guid> kv in dict) {
                Button b = new Button();
                b.Text = kv.Key + "\n" + kv.Value.ToString("B");
                b.AutoSize = true;
                b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                b.TextAlign = ContentAlignment.TopLeft;
                b.Image = Resources.PlayHS;
                b.TextImageRelation = TextImageRelation.ImageBeforeText;
                flp.Controls.Add(b);
                flp.SetFlowBreak(b, true);
                b.Click += new EventHandler(b_Click);
                b.Tag = kv.Value;
            }
        }

        public Guid? Sel = null;

        void b_Click(object sender, EventArgs e) {
            Sel = (Guid)((Control)sender).Tag;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}