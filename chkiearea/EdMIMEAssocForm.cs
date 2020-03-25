using ChkIEArea.Interfaces;
using ChkIEArea.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChkIEArea {
    public partial class EdMIMEAssocForm : Form, IEater {
        public AppChoice appChoice;

        public EdMIMEAssocForm() {
            InitializeComponent();
        }

        public void AddCLSID(RegistryKey rk, string ext, AppChoice choice) {
            throw new NotImplementedException();
        }

        public void AddCLSIDSimple(RegistryKey rk, string ext, AppChoice choice) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + "CLSID" + "\\" + choice.clsid, "", "") + "\n"
                + "---\n"
                + "ProgID: " + choice.progid + "\n"
                + " CLSID: " + choice.clsid + "\n"
                ;
            b.Tag = choice;
            b.Click += new EventHandler(b_Click);
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Font = lBaseFont.Font;
            flp1.Controls.Add(b);
        }

        public void AddDE(RegistryKey rkrootclsid, AppChoice choice) {
            throw new NotImplementedException();
        }

        public void AddDESimple(RegistryKey rk, AppChoice choice) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + choice.clsid, "", "") + "\n"
                + "---\n"
                + "ProgID: " + choice.progid + "\n"
                + " CLSID: " + choice.clsid + "\n"
                ;
            b.Tag = choice;
            b.Click += new EventHandler(b_Click);
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Font = lBaseFont.Font;
            flp1.Controls.Add(b);
        }

        public void AddEFP(RegistryKey rkrootclsid, AppChoice choice) {
            throw new NotImplementedException();
        }

        public void AddEFPSimple(RegistryKey rk, AppChoice choice) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + choice.clsid, "", "") + "\n"
                + "---\n"
                + "ProgID: " + choice.progid + "\n"
                + " CLSID: " + choice.clsid + "\n"
                ;
            b.Tag = choice;
            b.Click += new EventHandler(b_Click);
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Font = lBaseFont.Font;
            flp1.Controls.Add(b);
        }

        public void SetContentType(string contentType) {
            lMime.Text = contentType;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey($@"Software\Microsoft\Windows\Shell\Associations\MIMEAssociations\{contentType}\UserChoice", false);
            lProgid.Text = "";

            if (rk != null) {
                lProgid.Text = "" + rk.GetValue("Progid");
            }
        }

        void b_Click(object sender, EventArgs e) {
            appChoice = (AppChoice)((Button)sender).Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bRemove_Click(object sender, EventArgs e) {
            appChoice = AppChoice.Remove;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
