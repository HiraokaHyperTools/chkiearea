using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using ChkIEArea.Properties;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ChkIEArea {
    public partial class CForm : Form {
        public CForm() {
            InitializeComponent();
        }

        string lastfp = null;

        void view(string fn) {
            using (WIP wip = new WIP(this)) {
                wb.Navigate(lastfp = Path.Combine(Path.Combine(Application.StartupPath, "f"), fn));
            }
        }

        class WIP : IDisposable {
            PictureBox pb;

            public WIP(Control form) {
                pb = new PictureBox();
                pb.Parent = form;
                pb.BackgroundImage = Resources.wip;
                pb.BackgroundImageLayout = ImageLayout.Tile;
                pb.Dock = DockStyle.Fill;
                pb.Show();
                pb.BringToFront();
                form.Update();
            }

            #region IDisposable �����o

            public void Dispose() {
                if (pb != null) pb.Dispose();
            }

            #endregion
        }

        private void buttonPDF_Click(object sender, EventArgs e) { Chk(bPDF); view("����`�F�b�N�p�e�X�g�f�[�^.pdf"); }
        private void buttonDOC_Click(object sender, EventArgs e) { Chk(bDOC); view("����`�F�b�N�p�e�X�g�f�[�^.doc"); }
        private void buttonHTM_Click(object sender, EventArgs e) { Chk(bHTM); view("����`�F�b�N�p�e�X�g�f�[�^.htm"); }
        private void buttonHTML_Click(object sender, EventArgs e) { Chk(bHTML); view("����`�F�b�N�p�e�X�g�f�[�^.html"); }
        private void buttonEML_Click(object sender, EventArgs e) { Chk(bEML); view("����`�F�b�N�p�e�X�g�f�[�^.eml"); }
        private void buttonMHT_Click(object sender, EventArgs e) { Chk(bMHT); view("����`�F�b�N�p�e�X�g�f�[�^.mht"); }
        private void buttontxt_Click(object sender, EventArgs e) { Chk(bTXT); view("����`�F�b�N�p�e�X�g�f�[�^.txt"); }
        private void buttonPPT_Click(object sender, EventArgs e) { Chk(bPPT); view("����`�F�b�N�p�e�X�g�f�[�^.ppt"); }

        private void Chk(ToolStripItem b) {
            foreach (ToolStripItem tsi in toolStrip1.Items) {
                ToolStripButton tsb = tsi as ToolStripButton;
                if (tsb != null) {
                    bool f = (b == tsb);
                    if (tsb.Checked != f)
                        tsb.Checked = f;
                }
            }
        }

        private void CForm_Load(object sender, EventArgs e) {
            // http://forum.mozilla.gr.jp/cbbs.cgi?mode=al2&namber=8265&rev=&&KLOG=55
        }

        private void buttonEditFlags_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "��ɒ������Ă��������B", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
#if true
            RFUt.Modify(this, lastfp, "EditFlags", 0, 0x10000, true);
#else
            String ex = Path.GetExtension(lastfp);
            RegistryKey exkey = Registry.ClassesRoot.OpenSubKey(ex, false);
            if (exkey != null) {
                string exname = (string)exkey.GetValue("");
                if (exname != null) {
                    RegistryKey appkey = Registry.ClassesRoot.OpenSubKey(exname, true);
                    if (appkey != null) {
                        object editFlags = appkey.GetValue("EditFlags");
                        if (editFlags == null) {
                            byte[] b4 = new byte[] { 0, 0, 1, 0 };
                            editFlags = b4;

                            if (MessageBox.Show(this, "�΍􂵂܂��B(0)", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                                appkey.SetValue("EditFlags", editFlags);
                                MessageBox.Show(this, "�΍􂵂܂����B", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else if (editFlags is byte[]) {
                            byte[] b4 = (byte[])editFlags;
                            if (0 == (b4[2] & 1)) {
                                if (MessageBox.Show(this, "�΍􂵂܂��B(B)", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                                    b4[2] |= 0x01;

                                    appkey.SetValue("EditFlags", editFlags);
                                    MessageBox.Show(this, "�΍􂵂܂����B", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else {
                                MessageBox.Show(this, "�΍�ς݂ł��B", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else if (editFlags is int) {
                            int v = (int)editFlags;
                            if (0 == (v & 0x10000)) {
                                if (MessageBox.Show(this, "�΍􂵂܂��B(DW)", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                                    v |= 0x10000;

                                    appkey.SetValue("EditFlags", v);
                                    MessageBox.Show(this, "�΍􂵂܂����B", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else {
                                MessageBox.Show(this, "�΍�ς݂ł��B", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
#endif
        }

        private void buttonPDFNotGood_Click(object sender, EventArgs e) {
            MessageBox.Show("Adobe PDF���i�̍ăC���X�g�[�������肢�v���܂��B");
        }

        private void buttonBrowserFlags_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "��ɒ������Ă��������B", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            RFUt.Modify(this, lastfp, "BrowserFlags", 8, 0, false);
        }

        class RFUt {
            public static void Modify(Form parent, string lastfp, string keyName, uint fRemove, uint fAdd, bool preferBytea) {
                String ex = Path.GetExtension(lastfp);
                RegistryKey exkey = Registry.ClassesRoot.OpenSubKey(ex, false);
                if (exkey != null) {
                    string exname = (string)exkey.GetValue("");
                    if (exname != null) {
                        RegistryKey appkey = Registry.ClassesRoot.OpenSubKey(exname, true);
                        if (appkey != null) {
                            object editFlags = appkey.GetValue(keyName);

                            uint vali = 0;
                            if (editFlags is byte[]) {
                                byte[] b4 = (byte[])editFlags;
                                if (b4.Length == 4) vali = BitConverter.ToUInt32(b4, 0);
                            }
                            else if (editFlags is int) {
                                vali = (uint)((int)editFlags);
                            }

                            uint valo = (vali & (~fRemove)) | fAdd;

                            if (vali != valo) {
                                if (MessageBox.Show(parent, "�΍􂵂܂��B", parent.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                                    if (preferBytea) {
                                        appkey.SetValue(keyName, BitConverter.GetBytes(valo));
                                    }
                                    else {
                                        appkey.SetValue(keyName, valo);
                                    }
                                    MessageBox.Show(parent, "�΍􂵂܂����B", parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else {
                                MessageBox.Show(parent, "�΍�ς݂ł��B", parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        [DllImport("ole32.dll")]
        static extern void CoFreeUnusedLibraries();

        private void CForm_FormClosing(object sender, FormClosingEventArgs e) {
            wb.Dispose();
            CoFreeUnusedLibraries();
        }

        class Alertut {
            IWin32Window parent;
            bool confirmed = false;

            public Alertut(IWin32Window parent) {
                this.parent = parent;
            }

            public bool Confirm() {
                if (!confirmed)
                    if (MessageBox.Show(parent, "�΍􂵂܂��B", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        confirmed = true;
                return confirmed;
            }
        }

        class UtGuid {
            public static bool TryParse(String s, out Guid o) {
                try {
                    o = new Guid(s);
                    return true;
                }
                catch (Exception) {
                    o = Guid.Empty;
                    return false;
                }
            }
        }

        enum Repairty {
            UseCLSID, UseEFP,
        }

        private void bMIME_Click(object sender, EventArgs e) {
            Repairty ty = (sender == bMIME) ? Repairty.UseCLSID : Repairty.UseEFP;

            if (lastfp == null) {
                MessageBox.Show(this, "��ɒ������Ă��������B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (ty == Repairty.UseCLSID && String.Compare(".pdf", Path.GetExtension(lastfp), true) == 0) {
                if (MessageBox.Show(this, "PDF�ɂ��܂��ẮAAdobe�Зl�̐��i�������p�̏ꍇ�AEFP�Ō��o���������ǂ��Ǝv���܂��B\n\n���̂܂܁A���s���܂����B", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
            }

            Alertut alert = new Alertut(this);

            String ext = Path.GetExtension(lastfp);
            RegistryKey rkext = Registry.ClassesRoot.OpenSubKey(ext, false);
            if (rkext == null) {
                MessageBox.Show(this, "�g���q���o�^����Ă��܂���B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            String oleId = rkext.GetValue("") as String;
            if (oleId == null) {
                MessageBox.Show(this, "�g���q�̊֘A�A�v�����o�^����Ă��܂���B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            String newclsid = "";
            if (ty == Repairty.UseCLSID) {
                RegistryKey rkole = Registry.ClassesRoot.OpenSubKey(oleId, false);
                if (rkole == null) {
                    MessageBox.Show(this, "�g���q�̊֘A�A�v����������܂���B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                RegistryKey rkclsid = rkole.OpenSubKey("CLSID", false);
                if (rkclsid == null) {
                    MessageBox.Show(this, "CLSID��������܂���B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Guid clsidGuid;
                String clsid = rkclsid.GetValue("") as String;
                if (clsid == null || !UtGuid.TryParse(clsid, out clsidGuid)) {
                    MessageBox.Show(this, "CLSID���������������݂�܂���B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                newclsid = clsidGuid.ToString("B");
            }
            else if (ty == Repairty.UseEFP) {
                SortedDictionary<String, Guid> dict = new SortedDictionary<string, Guid>();
                RegistryKey rkrootclsid = Registry.ClassesRoot.OpenSubKey(@"CLSID", false);
                foreach (String s in rkrootclsid.GetSubKeyNames()) {
                    if (s == null || !Regex.IsMatch(s, "^\\{[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\\}$", RegexOptions.IgnoreCase))
                        continue;
                    RegistryKey rkApp = rkrootclsid.OpenSubKey(s, false);
                    if (rkApp == null)
                        continue;
                    RegistryKey rkEFP = rkApp.OpenSubKey("EnableFullPage", false);
                    if (rkEFP == null)
                        continue;
                    foreach (String appext in rkEFP.GetSubKeyNames()) {
                        if (String.Compare(appext, ext, true) == 0) {
                            String appname = rkApp.GetValue("") as String;
                            if (appname == null || appname.Length == 0)
                                appname = s;

                            dict[appname] = new Guid(s);
                        }
                    }
                }
                if (dict.Count == 0) {
                    MessageBox.Show(this, "EFP����L���ȃA�v���𔭌��ł��܂���ł����B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                using (SelAppForm form = new SelAppForm(dict)) {
                    if (form.ShowDialog(this) != DialogResult.OK)
                        return;
                    Guid? sel = form.Sel;
                    if (sel == null)
                        return;
                    newclsid = sel.Value.ToString("B");
                }
            }
            else throw new NotSupportedException("�s���ȕ��@�F" + ty);

            String contentType = rkext.GetValue("Content Type") as String;
            if (contentType == null) {
                MessageBox.Show(this, "Content Type���L��܂���B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RegistryKey rkct = Registry.ClassesRoot.OpenSubKey(@"Mime\Database\Content Type", false);
            if (rkct == null) {
                MessageBox.Show(this, "MIME DB�������悤�ł��B�ݒ�ł��܂���B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            {
                RegistryKey rk1 = Registry.ClassesRoot.OpenSubKey(@"Mime\Database\Content Type\" + contentType, false);
                if (rk1 == null) {
                    if (!alert.Confirm())
                        return;
                }
                else {
                    String curext = rk1.GetValue("Extension") as String;
                    if (curext == null || String.Compare(curext, ext, true) != 0) {
                        if (!alert.Confirm())
                            return;
                    }
                    else {
                        String curclsid = rk1.GetValue("CLSID") as String;
                        if (curclsid == null || String.Compare(curclsid, newclsid, true) != 0) {
                            if (!alert.Confirm())
                                return;
                        }
                        else {
                            MessageBox.Show(this, "�΍�ς݂ł��B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }

            {
                RegistryKey rk1 = Registry.ClassesRoot.CreateSubKey(@"Mime\Database\Content Type\" + contentType);
                rk1.SetValue("Extension", ext);
                rk1.SetValue("CLSID", newclsid);

                MessageBox.Show(this, "�ݒ肵�܂����B", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}