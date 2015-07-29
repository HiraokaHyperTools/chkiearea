using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChkIEArea {
    public partial class SelExtForm : Form {
        public SelExtForm() {
            InitializeComponent();
        }

        private void SelExtForm_Load(object sender, EventArgs e) {
            int cx = lb1.Items.Count;
            for (int x = 0; x < cx; x++) lb1.SetSelected(x,true);
        }

        public List<String> fexts = new List<string>();

        private void button1_Click(object sender, EventArgs e) {
            int cx = lb1.Items.Count;
            for (int x = 0; x < cx; x++)
                if (lb1.GetSelected(x))
                    fexts.Add(lb1.Items[x] + "");
        }
    }
}