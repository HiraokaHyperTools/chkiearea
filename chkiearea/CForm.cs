﻿using System;
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
using System.Diagnostics;

namespace ChkIEArea {
    public partial class CForm : Form {
        public CForm() {
            InitializeComponent();
        }

        string lastfp = null;

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

            #region IDisposable メンバ

            public void Dispose() {
                if (pb != null) pb.Dispose();
            }

            #endregion
        }

        private void buttonPDF_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.pdf"); }
        private void buttonDOC_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.doc"); }
        private void buttonHTM_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.htm"); }
        private void buttonHTML_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.html"); }
        private void buttonEML_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.eml"); }
        private void buttonMHT_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.mht"); }
        private void buttontxt_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.txt"); }
        private void buttonPPT_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.ppt"); }
        private void buttonPPTX_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.pptx"); }
        private void bDOCX_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.docx"); }
        private void bXLS_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.xls"); }
        private void bXLSX_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.xlsx"); }
        private void bDXF_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.dxf"); }
        private void bTIF_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.tif"); }
        private void bTIFF_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.tiff"); }
        private void bMP4_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.mp4"); }
        private void bWMV_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.wmv"); }

        private void view2(ToolStripItem sender, String fn) {
            String fp = Path.Combine(Path.Combine(Application.StartupPath, "f"), fn);

            if (0 != (ModifierKeys & Keys.Control)) {
                Process.Start(fp);
            }
            else if (0 == (ModifierKeys & Keys.Shift)) {
                foreach (ToolStripItem tsi in toolStrip1.Items) {
                    ToolStripButton tsb = tsi as ToolStripButton;
                    if (tsb != null) {
                        bool f = (sender == tsb);
                        if (tsb.Checked != f)
                            tsb.Checked = f;
                    }
                }
                using (WIP wip = new WIP(this)) {
                    wb.Navigate(lastfp = fp);
                }
            }
            else {
                ViewForm form = new ViewForm(fp);
                form.Show();
            }
        }

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

            this.Text += " " + Application.ProductVersion + " (" + ((IntPtr.Size == 4 ? "x86" : "x64")) + ")";
        }

        private void buttonEditFlags_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "先に調査してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            EdREGForm form = new EdREGForm();
            form.Modify(lastfp, new IntMod("EditFlags", 0, 0x10000, true));
            form.ShowDialog(this);
            return;
        }

        private void buttonPDFNotGood_Click(object sender, EventArgs e) {
            MessageBox.Show("Adobe PDF製品の再インストールをお願い致します。");
        }

        private void buttonBrowserFlags_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "先に調査してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            EdREGForm form = new EdREGForm();
            form.Modify(lastfp, new IntMod("BrowserFlags", 8, 0, false));
            form.ShowDialog(this);
            return;
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
                                if (MessageBox.Show(parent, "対策します。", parent.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                                    if (preferBytea) {
                                        appkey.SetValue(keyName, BitConverter.GetBytes(valo), RegistryValueKind.Binary);
                                    }
                                    else {
                                        appkey.SetValue(keyName, (int)valo, RegistryValueKind.DWord);
                                    }
                                    MessageBox.Show(parent, "対策しました。", parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else {
                                MessageBox.Show(parent, "対策済みです。", parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (MessageBox.Show(parent, "対策します。", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
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
            UseCLSID, UseEFP, UseDE,
        }

        private void bMIME_Click(object sender, EventArgs e) {
            Repairty ty;
            if (sender == bMIME) ty = Repairty.UseCLSID;
            else if (sender == bMIMEefp) ty = Repairty.UseEFP;
            else if (sender == bMIMEde) ty = Repairty.UseDE;
            else throw new NotSupportedException();

            EdAppForm form2 = new EdAppForm();

            if (lastfp == null) {
                MessageBox.Show(this, "先に調査してください。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (ty == Repairty.UseCLSID && String.Compare(".pdf", Path.GetExtension(lastfp), true) == 0) {
                if (MessageBox.Show(this, "PDFにつきましては、Adobe社様の製品をご利用の場合、EFPで検出した方が良いと思います。\n\nこのまま、続行しますか。", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
            }

            Alertut alert = new Alertut(this);

            String ext = Path.GetExtension(lastfp);
            RegistryKey rkext = Registry.ClassesRoot.OpenSubKey(ext, false);
            if (rkext == null) {
                MessageBox.Show(this, "拡張子が登録されていません。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            String contentType = rkext.GetValue("Content Type") as String;
            if (contentType == null) {
                MessageBox.Show(this, "Content Typeが有りません。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RegistryKey rkct = Registry.ClassesRoot.OpenSubKey(@"Mime\Database\Content Type", false);
            if (rkct == null) {
                MessageBox.Show(this, "MIME DBが無いようです。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            String oleId = rkext.GetValue("") as String;
            if (oleId == null) {
                MessageBox.Show(this, "拡張子の関連アプリが登録されていません。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            String newclsid = "";
            if (ty == Repairty.UseCLSID) {
                RegistryKey rkole = Registry.ClassesRoot.OpenSubKey(oleId, false);
                if (rkole == null) {
                    MessageBox.Show(this, "拡張子の関連アプリが見つかりません。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                RegistryKey rkclsid = rkole.OpenSubKey("CLSID", false);
                if (rkclsid == null) {
                    MessageBox.Show(this, "CLSIDが見つかりません。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Guid clsidGuid;
                String clsid = rkclsid.GetValue("") as String;
                if (clsid == null || !UtGuid.TryParse(clsid, out clsidGuid)) {
                    MessageBox.Show(this, "CLSIDが無いか正しく在りません。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                form2.AddCLSID(Registry.ClassesRoot, ext, oleId, clsid);

                form2.SetContentType(contentType);
                {
                    if (form2.ShowDialog() != DialogResult.OK)
                        return;
                    String sel = form2.Sel;
                    if (sel == null)
                        return;
                    newclsid = sel;
                }
            }
            else if (ty == Repairty.UseDE) {
                SortedDictionary<String, Guid> dict = new SortedDictionary<string, Guid>();
                RegistryKey rkrootclsid = Registry.ClassesRoot.OpenSubKey(@"CLSID", false);
                foreach (String s in rkrootclsid.GetSubKeyNames()) {
                    if (s == null || !Regex.IsMatch(s, "^\\{[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\\}$", RegexOptions.IgnoreCase))
                        continue;
                    RegistryKey rkApp = rkrootclsid.OpenSubKey(s, false);
                    if (rkApp == null)
                        continue;
                    RegistryKey rkDE = rkApp.OpenSubKey("DefaultExtension", false);
                    if (rkDE == null)
                        continue;
                    String targets = rkDE.GetValue("") as String;
                    if (targets == null)
                        continue;
                    String[] cols = targets.Split(',');
                    String appext = cols[0].Trim();
                    if (String.Compare(appext, ext, true) != 0)
                        continue;

                    String appname = rkApp.GetValue("") as String;
                    if (appname == null || appname.Length == 0)
                        appname = s;

                    dict[appname] = new Guid(s);
                    form2.AddDE(rkrootclsid, s);
                }
                if (dict.Count == 0) {
                    MessageBox.Show(this, "DefaultExtensionから有効なアプリを発見できませんでした。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                form2.SetContentType(contentType);
                {
                    if (form2.ShowDialog() != DialogResult.OK)
                        return;
                    String sel = form2.Sel;
                    if (sel == null)
                        return;
                    newclsid = sel;
                }
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
                            form2.AddEFP(rkrootclsid, s);
                        }
                    }
                }
                if (dict.Count == 0) {
                    MessageBox.Show(this, "EFPから有効なアプリを発見できませんでした。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                form2.SetContentType(contentType);
                {
                    if (form2.ShowDialog() != DialogResult.OK)
                        return;
                    String sel = form2.Sel;
                    if (sel == null)
                        return;
                    newclsid = sel;
                }
            }
            else throw new NotSupportedException("不明な方法：" + ty);

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
                            MessageBox.Show(this, "対策済みです。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }

            {
                RegistryKey rk1 = Registry.ClassesRoot.CreateSubKey(@"Mime\Database\Content Type\" + contentType);
                rk1.SetValue("Extension", ext);
                rk1.SetValue("CLSID", newclsid);

                MessageBox.Show(this, "設定しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bBrowserFlags2_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "先に調査してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            EdREGForm form = new EdREGForm();
            form.Modify(lastfp, new IntMod("BrowserFlags", 0xffffffffU, 0x80000024U, false));
            form.ShowDialog(this);
            return;
        }

        private void bAcro5_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "Adobe Acrobat 5.xのみを導入している環境で、PDFが飛び出るのを直す機能です。\n\n"
                + "かなり実験的な品質です。動作確認できる環境がなく、効果や影響を確かめていません。\n\n"
                + "続行しますか。", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            String ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            {
                if (MessageBox.Show(this, "対策1を実行します。(pdf.ocxを登録)", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                String fpocx = Path.Combine(ProgramFiles, @"Adobe\Acrobat 5.0\Acrobat\ActiveX\pdf.ocx");

                if (File.Exists(fpocx)) {
                    ProcessStartInfo psi = new ProcessStartInfo("regsvr32.exe", " \"" + fpocx + "\"");
                    Process p = Process.Start(psi);
                    p.WaitForExit();
                }
                else {
                    MessageBox.Show(this, "次のファイルが見付かりませんので、対策を実行できません。\n\n" + fpocx, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                MessageBox.Show(this, "対策1の実行を終了しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            {
                if (MessageBox.Show(this, @"対策2を実行します。(レジストリ Software\Adobe\Acrobat\Exe を、設定)", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                String fpexe = Path.Combine(ProgramFiles, @"Adobe\Acrobat 5.0\Acrobat\Acrobat.exe");

                if (File.Exists(fpexe)) {
                    RegistryKey rk = Registry.ClassesRoot.CreateSubKey(@"Software\Adobe\Acrobat\Exe");
                    rk.SetValue("", FPUt.Encap(fpexe));
                }
                else {
                    MessageBox.Show(this, "次のファイルが見付かりませんので、対策を実行できません。\n\n" + fpexe, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                MessageBox.Show(this, "対策2の実行を終了しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bAcro6_Click(object sender, EventArgs e) {
            // VER 6
            if (MessageBox.Show(this, "Adobe Acrobat 6.xのみを導入している環境で、PDFが飛び出るのを直す機能です。\n\n"
                + "かなり実験的な品質です。動作確認できる環境がなく、効果や影響を確かめていません。\n\n"
                + "続行しますか。", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            String ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            {
                if (MessageBox.Show(this, "対策1を実行します。(pdf.ocxを登録)", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                String fpocx = Path.Combine(ProgramFiles, @"Adobe\Acrobat 6.0\Acrobat\ActiveX\pdf.ocx");

                if (File.Exists(fpocx)) {
                    ProcessStartInfo psi = new ProcessStartInfo("regsvr32.exe", " \"" + fpocx + "\"");
                    Process p = Process.Start(psi);
                    p.WaitForExit();
                }
                else {
                    MessageBox.Show(this, "次のファイルが見付かりませんので、対策を実行できません。\n\n" + fpocx, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                MessageBox.Show(this, "対策1の実行を終了しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            {
                if (MessageBox.Show(this, @"対策2を実行します。(レジストリ Software\Adobe\Acrobat\Exe を、設定)", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                String fpexe = Path.Combine(ProgramFiles, @"Adobe\Acrobat 6.0\Acrobat\Acrobat.exe");

                if (File.Exists(fpexe)) {
                    RegistryKey rk = Registry.ClassesRoot.CreateSubKey(@"Software\Adobe\Acrobat\Exe");
                    rk.SetValue("", FPUt.Encap(fpexe));
                }
                else {
                    MessageBox.Show(this, "次のファイルが見付かりませんので、対策を実行できません。\n\n" + fpexe, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                MessageBox.Show(this, "対策2の実行を終了しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        class FPUt {
            public static string Encap(String fp) {
                if (fp.Contains(" "))
                    return "\"" + fp + "\"";
                return fp;
            }
        }

        class AcroPUt {
            public static string SoftwareAdobeAcrobatExe {
                get {
                    RegistryKey rkExe = Registry.ClassesRoot.OpenSubKey(@"Software\Adobe\Acrobat\Exe", false);
                    if (rkExe != null) {
                        return rkExe.GetValue("") as String;
                    }
                    return null;
                }

                set {
                    RegistryKey rkExe = Registry.ClassesRoot.CreateSubKey(@"Software\Adobe\Acrobat\Exe");
                    rkExe.SetValue("", FPUt.Encap(value));
                }
            }

            public static string Acrobat10Exe { // 動作は未確認
                get {
                    RegistryKey rkExe = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Adobe Acrobat\10.0\Installer", false);
                    if (rkExe != null) {
                        return rkExe.GetValue("Acrobat.exe") as String;
                    }
                    return null;
                }
            }

            public static string Acrobat9Exe { // 動作は未確認
                get {
                    RegistryKey rkExe = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Adobe Acrobat\9.0\Installer", false);
                    if (rkExe != null) {
                        return rkExe.GetValue("Acrobat.exe") as String;
                    }
                    return null;
                }
            }

            public static string Acrobat8Exe {
                get {
                    RegistryKey rkExe = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Adobe Acrobat\8.0\Installer", false);
                    if (rkExe != null) {
                        return rkExe.GetValue("Acrobat.exe") as String;
                    }
                    return null;
                }
            }

            public static string Acrobat7Exe { // 動作は未確認
                get {
                    RegistryKey rkExe = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Adobe Acrobat\7.0\Installer", false);
                    if (rkExe != null) {
                        return rkExe.GetValue("Acrobat.exe") as String;
                    }
                    return null;
                }
            }

            public static string AcroRd32_11Exe { // 動作は未確認：表示までは確認
                get {
                    RegistryKey rkDir = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Acrobat Reader\11.0\InstallPath", false);
                    if (rkDir != null) {
                        String dir = rkDir.GetValue("") as String;
                        if (!String.IsNullOrEmpty(dir))
                            return Path.Combine(dir, "AcroRd32.exe");
                    }
                    return null;
                }
            }

            public static string AcroRd32_10Exe { // 動作は未確認：表示までは確認
                get {
                    RegistryKey rkDir = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Acrobat Reader\10.0\InstallPath", false);
                    if (rkDir != null) {
                        String dir = rkDir.GetValue("") as String;
                        if (!String.IsNullOrEmpty(dir))
                            return Path.Combine(dir, "AcroRd32.exe");
                    }
                    return null;
                }
            }

            public static string AcroRd32_9Exe { // 動作は未確認：表示までは確認
                get {
                    RegistryKey rkDir = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Acrobat Reader\9.0\InstallPath", false);
                    if (rkDir != null) {
                        String dir = rkDir.GetValue("") as String;
                        if (!String.IsNullOrEmpty(dir))
                            return Path.Combine(dir, "AcroRd32.exe");
                    }
                    return null;
                }
            }

            public static string AcroRd32_8Exe { // 動作は未確認
                get {
                    RegistryKey rkDir = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Acrobat Reader\8.0\InstallPath", false);
                    if (rkDir != null) {
                        String dir = rkDir.GetValue("") as String;
                        if (!String.IsNullOrEmpty(dir))
                            return Path.Combine(dir, "AcroRd32.exe");
                    }
                    return null;
                }
            }

            public static string AcroRd32_7Exe { // 動作は未確認
                get {
                    RegistryKey rkDir = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Acrobat Reader\7.0\InstallPath", false);
                    if (rkDir != null) {
                        String dir = rkDir.GetValue("") as String;
                        if (!String.IsNullOrEmpty(dir))
                            return Path.Combine(dir, "AcroRd32.exe");
                    }
                    return null;
                }
            }

        }

        private void bAcroExe_DropDownOpening(object sender, EventArgs e) {
            bAcroExe.DropDownItems.Clear();

            {
                ToolStripItem tsiConfirm = bAcroExe.DropDownItems.Add("現在の設定は?");
                tsiConfirm.Click += new EventHandler(tsiConfirm_Click);
            }

            List<String> alExe = new List<String>();
            alExe.Add(AcroPUt.Acrobat10Exe);
            alExe.Add(AcroPUt.Acrobat9Exe);
            alExe.Add(AcroPUt.Acrobat8Exe);
            alExe.Add(AcroPUt.Acrobat7Exe);
            alExe.Add(AcroPUt.AcroRd32_11Exe);
            alExe.Add(AcroPUt.AcroRd32_10Exe);
            alExe.Add(AcroPUt.AcroRd32_9Exe);
            alExe.Add(AcroPUt.AcroRd32_8Exe);
            alExe.Add(AcroPUt.AcroRd32_7Exe);

            foreach (String fp in alExe) {
                if (!String.IsNullOrEmpty(fp) && File.Exists(fp)) {
                    if (bAcroExe.DropDownItems.Count == 1)
                        bAcroExe.DropDownItems.Add(new ToolStripSeparator());
                    ToolStripItem tsi = bAcroExe.DropDownItems.Add(fp);
                    tsi.Click += new EventHandler(tsi_Click);
                }
            }
        }

        void tsiConfirm_Click(object sender, EventArgs e) {
            String fp = AcroPUt.SoftwareAdobeAcrobatExe;
            MessageBox.Show(this, String.IsNullOrEmpty(fp) ? "設定されていません。" : ("現在の設定は次の通りです。\n\n" + fp), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void tsi_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "レジストリを編集します。\n\nよろしいですか。", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                AcroPUt.SoftwareAdobeAcrobatExe = ((ToolStripItem)sender).Text;
                MessageBox.Show(this, "設定しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bRestart_Click(object sender, EventArgs e) {
            Application.Restart();
        }

        private void bBrowseInPlace_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "先に調査してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            EdREGForm form = new EdREGForm();
            form.Modify(lastfp, new StrMod("BrowseInPlace", "1"));
            form.ShowDialog(this);
            return;
        }

        private void bOOo_DropDownOpening(object sender, EventArgs e) {
            bOOo.DropDownItems.Clear();

            List<string> extrapaths = new List<string>();
            foreach (String dir1 in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "LibreOffice*")) {
                extrapaths.Add(dir1);
            }
            foreach (String dir1 in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "OpenOffice.org*")) {
                extrapaths.Add(dir1);
            }
            foreach (String dir1 in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "OpenOffice 4*")) {
                extrapaths.Add(dir1);
            }

            foreach (String dir1 in extrapaths) {
                if (Directory.Exists(dir1)) {
                    String fp1 = Path.Combine(dir1, "program" + "\\" + "so_activex.dll");
                    if (File.Exists(fp1)) {
                        ToolStripItem tsiReg = bOOo.DropDownItems.Add("① so_activex.dll登録 from " + fp1);
                        tsiReg.Tag = fp1;
                        tsiReg.Click += new EventHandler(tsiReg_Click);
                    }
                }
            }

            bOOo.DropDownItems.Add(new ToolStripSeparator());
            {
                ToolStripItem tsirk = bOOo.DropDownItems.Add("② ooo341.regを登録");
                tsirk.Click += new EventHandler(tsirk_Click);
            }
            bOOo.DropDownItems.Add(new ToolStripSeparator());
            {
                ToolStripItem tsi = bOOo.DropDownItems.Add("③ 「Use EFP」で、「SOActiveX Class」を選択");
                tsi.Enabled = false;
            }
        }

        void tsirk_Click(object sender, EventArgs e) {
            String fp1 = Path.Combine(Application.StartupPath, "ooo341.reg");
            Process.Start("regedit.exe", " \"" + fp1 + "\"");
        }

        void tsiReg_Click(object sender, EventArgs e) {
            String fp1 = (String)((ToolStripItem)sender).Tag;
            Process.Start("regsvr32.exe", " \"" + fp1 + "\"");
        }
    }
}