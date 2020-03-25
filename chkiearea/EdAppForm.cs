using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ChkIEArea.Interfaces;
using ChkIEArea.Models;
using Microsoft.Win32;

namespace ChkIEArea {
    public partial class EdAppForm : Form, IEater {
        public EdAppForm() {
            InitializeComponent();
        }

        public AppChoice appChoice = null;

        public void AddCLSID(RegistryKey rk, String ext, AppChoice choice) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + "CLSID" + "\\" + choice.clsid, "", "");
            b.Tag = choice;
            b.Click += new EventHandler(b_Click);
            flp1.Controls.Add(b);

            AddRKey(0, "HKEY_CLASSES_ROOT");
            AddRKey(1, ext);
            AddVKey(2, "", "" + Registry.GetValue(rk.Name + "\\" + ext, "", ""));
            AddRKey(1, choice.progid);
            AddVKey(2, "", "" + Registry.GetValue(rk.Name + "\\" + choice.progid, "", ""));
            AddRKey(2, "CLSID");
            AddVKey(3, "", "" + Registry.GetValue(rk.Name + "\\" + choice.progid + "\\" + "CLSID", "", ""));
        }

        public void AddEFP(RegistryKey rk, AppChoice choice) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + choice.clsid, "", "");
            b.Tag = choice;
            b.Click += new EventHandler(b_Click);
            flp1.Controls.Add(b);

            AddRKey(0, "HKEY_CLASSES_ROOT");
            AddRKey(1, "CLSID");
            AddRKey(2, choice.clsid);
            AddVKey(3, "", "" + Registry.GetValue(rk.Name + "\\" + choice.clsid, "", ""));
            AddRKey(3, "EnableFullPage");
            RegistryKey rkEFP = rk.OpenSubKey(choice.clsid + "\\" + "EnableFullPage", false);
            if (rkEFP != null) {
                foreach (String sk in rkEFP.GetSubKeyNames()) {
                    AddRKey(4, sk);
                }
            }
        }

        public void AddDE(RegistryKey rk, AppChoice choice) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + choice.clsid, "", "");
            b.Tag = choice;
            b.Click += new EventHandler(b_Click);
            flp1.Controls.Add(b);

            AddRKey(0, "HKEY_CLASSES_ROOT");
            AddRKey(1, "CLSID");
            AddRKey(2, choice.clsid);
            AddVKey(3, "", "" + Registry.GetValue(rk.Name + "\\" + choice.clsid, "", ""));
            AddRKey(3, "DefaultExtension");
            AddVKey(4, "", "" + Registry.GetValue(rk.Name + "\\" + choice.clsid + "\\" + "DefaultExtension", "", ""));
        }

        public void AddCLSIDSimple(RegistryKey rk, String ext, AppChoice choice) {
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

        void b_Click(object sender, EventArgs e) {
            appChoice = (AppChoice)((Button)sender).Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        Bitmap _Folder = ChkIEArea.Properties.Resources.Folder_16x16;
        Bitmap _Prop = ChkIEArea.Properties.Resources.PropertiesHS;

        void AddVKey(int indent, String name, String val) {
            if (String.IsNullOrEmpty(name)) name = "(既定)";
            AddKey(indent, name + " = " + val, _Prop);
        }
        void AddRKey(int indent, String name) {
            AddKey(indent, name, _Folder);
        }

        void AddKey(int indent, String name, Bitmap p) {
            FlowLayoutPanel fx = new FlowLayoutPanel();
            fx.AutoSize = true;
            fx.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fx.Margin = new Padding(8 + 16 * indent, 0, 0, 1);
            fx.Padding = new Padding(0);
            {
                PictureBox pb = new PictureBox();
                pb.Image = p;
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.Margin = new Padding(0);
                fx.Controls.Add(pb);
            }
            {
                Label la = new Label();
                la.MinimumSize = new Size(0, 16);
                la.Text = name;
                la.AutoSize = true;
                la.TextAlign = ContentAlignment.MiddleLeft;
                la.Margin = new Padding(3, 0, 0, 0);
                la.Font = lMimeClsid.Font;
                fx.Controls.Add(la);
            }
            flp1.Controls.Add(fx);
        }

        private void EdAppForm_Load(object sender, EventArgs e) {

        }

        public void SetContentType(String contentType) {
            lMime.Text = contentType;
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(@"Mime\Database\Content Type\" + contentType, false);
            lMimeClsid.Text = "";
            lMimeExt.Text = "";

            if (rk != null) {
                lMimeClsid.Text = "" + rk.GetValue("CLSID");
                lMimeExt.Text = "" + rk.GetValue("Extension");
            }
        }
    }
}
