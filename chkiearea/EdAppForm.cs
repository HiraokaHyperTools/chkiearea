using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ChkIEArea {
    public partial class EdAppForm : Form {
        public EdAppForm() {
            InitializeComponent();
        }

        public String Sel = null;

        public void AddCLSID(RegistryKey rk, String ext, String pid, String clsid) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + "CLSID" + "\\" + clsid, "", "");
            b.Tag = clsid;
            b.Click += new EventHandler(b_Click);
            flp1.Controls.Add(b);

            AddRKey(0, "HKEY_CLASSES_ROOT");
            AddRKey(1, ext);
            AddVKey(2, "(Šù’è) = " + Registry.GetValue(rk.Name + "\\" + ext, "", ""));
            AddRKey(1, pid);
            AddVKey(2, "(Šù’è) = " + Registry.GetValue(rk.Name + "\\" + pid, "", ""));
            AddRKey(2, "CLSID");
            AddVKey(3, "(Šù’è) = " + Registry.GetValue(rk.Name + "\\" + pid + "\\" + "CLSID", "", ""));
        }

        public void AddEFP(RegistryKey rk, String clsid) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + clsid, "", "");
            b.Tag = clsid;
            b.Click += new EventHandler(b_Click);
            flp1.Controls.Add(b);

            AddRKey(0, "HKEY_CLASSES_ROOT");
            AddRKey(1, "CLSID");
            AddRKey(2, clsid);
            AddRKey(3, "EnableFullPage");
            RegistryKey rkEFP = rk.OpenSubKey(clsid + "\\" + "EnableFullPage", false);
            if (rkEFP != null) {
                foreach (String sk in rkEFP.GetSubKeyNames()) {
                    AddRKey(4, sk);
                }
            }
        }

        public void AddDE(RegistryKey rk, String clsid) {
            Button b = new Button();
            b.AutoSize = true;
            b.Text = "" + Registry.GetValue(rk.Name + "\\" + clsid, "", "");
            b.Tag = clsid;
            b.Click += new EventHandler(b_Click);
            flp1.Controls.Add(b);

            AddRKey(0, "HKEY_CLASSES_ROOT");
            AddRKey(1, "CLSID");
            AddRKey(2, clsid);
            AddRKey(3, "DefaultExtension");
            AddVKey(4, "(Šù’è) = " + Registry.GetValue(rk.Name + "\\" + clsid + "\\" + "DefaultExtension", "", ""));
        }

        void b_Click(object sender, EventArgs e) {
            Sel = (String)((Button)sender).Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        Bitmap _Folder = ChkIEArea.Properties.Resources.Folder_16x16;
        Bitmap _Prop = ChkIEArea.Properties.Resources.PropertiesHS;

        void AddVKey(int indent, String name) {
            AddKey(indent, name, _Prop);
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