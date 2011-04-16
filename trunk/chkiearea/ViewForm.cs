using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChkIEArea {
    public partial class ViewForm : Form {
        String navi;

        public ViewForm(String navi) {
            this.navi = navi;

            InitializeComponent();
        }

        private void ViewForm_Load(object sender, EventArgs e) {
            this.Text = this.Text.Replace("*", Path.GetFileName(navi));

            wb.Navigate(navi);
        }
    }
}