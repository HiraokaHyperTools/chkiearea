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
using System.Diagnostics;
using ChkIEArea.Interfaces;
using ChkIEArea.Models;

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
        private void bDWG_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.dwg"); }
        private void bTIF_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.tif"); }
        private void bTIFF_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.tiff"); }
        private void bMP4_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.mp4"); }
        private void bWMV_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.wmv"); }
        private void bXDW_Click(object sender, EventArgs e) { view2(sender as ToolStripItem, "動作チェック用テストデータ.xdw"); }

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

            this.Text += " -- " + Application.ProductVersion + " (" + ((IntPtr.Size == 4 ? "x86" : "x64")) + ")";
        }

        interface IValueModifier {
            ModifyResult Modify(Object current);
        }

        class IntEditor : IValueModifier {
            public IntEditor(uint bitReset, uint bitSet, bool preferBytea) {
                this.bitReset = bitReset;
                this.bitSet = bitSet;
                this.preferBytea = preferBytea;
            }

            uint bitReset;
            uint bitSet;
            bool preferBytea;

            #region IValueModifier メンバ

            public ModifyResult Modify(object curObj) {
                int? curIntVal = null;
                if (curObj is int) {
                    curIntVal = (int)curObj;
                }
                else if (curObj is byte[]) {
                    byte[] curBytes = (byte[])curObj;
                    if (curBytes.Length == 4) {
                        curIntVal = BitConverter.ToInt32(curBytes, 0);
                    }
                }

                int newIntVal = (int)((uint)(curIntVal ?? 0) & (uint)(~bitReset) | (uint)bitSet);
                String change = ((curIntVal.HasValue) ? "0x" + curIntVal.Value.ToString("X8") : "") + " → " + "0x" + newIntVal.ToString("X8");

                if (!curIntVal.HasValue || curIntVal.Value != newIntVal) {
                    return new ModifyResult(true, preferBytea ? (Object)BitConverter.GetBytes(newIntVal) : newIntVal, change);
                }

                return new ModifyResult(false, curObj, change);
            }

            #endregion
        }

        class StrEditor : IValueModifier {
            public StrEditor(String newData) {
                this.newData = newData;
            }

            String newData;

            #region IValueModifier メンバ

            public ModifyResult Modify(object curObj) {
                String curStrVal = null;
                if (curObj is string) {
                    curStrVal = (String)curObj;
                }

                String change = curStrVal + " → " + newData;
                if (curStrVal == null || curStrVal != newData) {
                    return new ModifyResult(true, newData, change);
                }

                return new ModifyResult(false, newData, change);
            }

            #endregion
        }

        class ModifyResult {
            public ModifyResult(bool updated, object newValue, string diffInfo) {
                this.updated = updated;
                this.newValue = newValue;
                this.diffInfo = diffInfo;
            }

            public bool updated;
            public object newValue;
            public string diffInfo;
        }

        private void ModifyValue(string leftName, IValueModifier valueModifier) {
            // HKEY_CLASSES_ROOT\.pdf → AcroExch.Document.DC
            String progId = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + Path.GetExtension(lastfp), "", null) as String;
            if (String.IsNullOrEmpty(progId)) {
                MessageBox.Show(this, "アプリの関連付けがありませんので、設定できません。中止します。", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // HKEY_CLASSES_ROOT\.pdf\Content Type → application/pdf
            String contentType = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + Path.GetExtension(lastfp), "Content Type", null) as String;
            if (String.IsNullOrEmpty(contentType)) {
                MessageBox.Show(this, "Content Type が未設定です。設定できません。中止します。", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // HKEY_CLASSES_ROOT\MIME\Database\Content Type\application/pdf\CLSID → {FE687896-F410-4D10-8740-D584DA23C74D}
            String clsid = Registry.GetValue(@"HKEY_CLASSES_ROOT\MIME\Database\Content Type\" + contentType, "CLSID", null) as String;
            String realProgId;
            if (!String.IsNullOrEmpty(clsid)) {
                // ActiveX
                // HKEY_CLASSES_ROOT\CLSID\{FE687896-F410-4D10-8740-D584DA23C74D}\ProgID → PDF4Ax.PDFVw.1
                realProgId = Registry.GetValue(@"HKEY_CLASSES_ROOT\CLSID\" + clsid + @"\ProgID", "", null) as String;
                if (String.IsNullOrEmpty(realProgId)) {
                    MessageBox.Show(this, "ProgID が未設定です。設定できません。中止します。", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else {
                // Non ActiveX ... Word など
                realProgId = progId;
            }

            String keyName = @"HKEY_CLASSES_ROOT\" + realProgId;

            ModifyResult modifyResult = valueModifier.Modify(Registry.GetValue(keyName, leftName, null));

            if (modifyResult.updated) {
                if (MessageBox.Show(this, String.Join("\n\n", new String[] {
                        "修正します。" + keyName + "\\" + leftName,
                        modifyResult.diffInfo,
                    }), Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK
                ) {
                    RegistryAlt.SetValue(keyName, leftName, modifyResult.newValue);
                    MessageBox.Show(this, "修正しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else {
                MessageBox.Show(this, String.Join("\n\n", new String[] {
                    "修正は不要です。" + keyName + "\\" + leftName,
                    modifyResult.diffInfo,
                }), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        bool UseSimple { get { return 0 == (ModifierKeys & Keys.Control); } }

        private void buttonEditFlags_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "先に調査してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (UseSimple) {
                ModifyValue("EditFlags", new IntEditor(0, 0x10000, true));
            }
            else {
                EdREGForm form = new EdREGForm();
                form.Modify(lastfp, new IntMod("EditFlags", 0, 0x10000, true));
                form.ShowDialog(this);
            }
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

            if (UseSimple) {
                ModifyValue("BrowserFlags", new IntEditor(8, 0, false));
            }
            else {
                EdREGForm form = new EdREGForm();
                form.Modify(lastfp, new IntMod("BrowserFlags", 8, 0, false));
                form.ShowDialog(this);
            }
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

        delegate RetType Func<RetType>();

        private void bMIME_Click(object sender, EventArgs e) {
            Repairty ty;
            if (sender == useClsid) ty = Repairty.UseCLSID;
            else if (sender == useEFP || sender == useEFP2) ty = Repairty.UseEFP;
            else if (sender == useDE) ty = Repairty.UseDE;
            else throw new NotSupportedException();

            EdAppForm formClsid = new EdAppForm();
            EdMIMEAssocForm formMimeAssoc = new EdMIMEAssocForm();

            var isMIMEAssociations = sender == useEFP2;

            Form form = isMIMEAssociations ? (Form)formMimeAssoc : formClsid;
            IEater eater = isMIMEAssociations ? (IEater)formMimeAssoc : formClsid;

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
                if (UseSimple) {
                    eater.AddCLSIDSimple(Registry.ClassesRoot, ext, new AppChoice(oleId, clsid));
                }
                else {
                    eater.AddCLSID(Registry.ClassesRoot, ext, new AppChoice(oleId, clsid));
                }

                eater.SetContentType(contentType);
                {
                    if (form.ShowDialog() != DialogResult.OK) {
                        return;
                    }
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

                    String pid = null;
                    RegistryKey rkProgid = rkApp.OpenSubKey("ProgID", false);
                    if (rkProgid != null) {
                        pid = "" + rkProgid.GetValue("");
                    }

                    dict[appname] = new Guid(s);
                    if (UseSimple) {
                        eater.AddDESimple(rkrootclsid, new AppChoice(pid, s));
                    }
                    else {
                        eater.AddDE(rkrootclsid, new AppChoice(pid, s));
                    }
                }
                if (dict.Count == 0) {
                    MessageBox.Show(this, "DefaultExtensionから有効なアプリを発見できませんでした。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                eater.SetContentType(contentType);
                {
                    if (form.ShowDialog() != DialogResult.OK) {
                        return;
                    }
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

                    String pid = null;
                    RegistryKey rkProgid = rkApp.OpenSubKey("ProgID", false);
                    if (rkProgid != null) {
                        pid = "" + rkProgid.GetValue("");
                    }

                    foreach (String appext in rkEFP.GetSubKeyNames()) {
                        if (String.Compare(appext, ext, true) == 0) {
                            String appname = rkApp.GetValue("") as String;
                            if (appname == null || appname.Length == 0)
                                appname = s;

                            dict[appname] = new Guid(s);
                            if (UseSimple) {
                                eater.AddEFPSimple(rkrootclsid, new AppChoice(pid, s));
                            }
                            else {
                                eater.AddEFP(rkrootclsid, new AppChoice(pid, s));
                            }
                        }
                    }
                }
                if (dict.Count == 0) {
                    MessageBox.Show(this, "EFPから有効なアプリを発見できませんでした。設定できません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                eater.SetContentType(contentType);
                {
                    if (form.ShowDialog() != DialogResult.OK) {
                        return;
                    }
                }
            }
            else throw new NotSupportedException("不明な方法：" + ty);

            if (isMIMEAssociations) {
                var choice = formMimeAssoc.appChoice;
                if (choice == null) {
                    return;
                }
                RegistryKey rk1 = Registry.CurrentUser.OpenSubKey($@"Software\Microsoft\Windows\Shell\Associations\MIMEAssociations\{contentType}\UserChoice", false);
                if (rk1 == null) {
                    if (!alert.Confirm())
                        return;
                }
                else {
                    String Progid = rk1.GetValue("Progid") as String;
                    if (Progid == null || String.Compare(Progid, ext, true) != 0) {
                        if (!alert.Confirm())
                            return;
                    }
                    else {
                        MessageBox.Show(this, "対策済みです。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                {
                    RegistryAlt.SetValue($@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Associations\MIMEAssociations\{contentType}\UserChoice", "Progid", choice.progid);

                    MessageBox.Show(this, "設定しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else {
                var choice = formClsid.appChoice;
                if (choice == null) {
                    return;
                }
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
                        if (curclsid == null || String.Compare(curclsid, choice.clsid, true) != 0) {
                            if (!alert.Confirm())
                                return;
                        }
                        else {
                            MessageBox.Show(this, "対策済みです。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                {
                    RegistryAlt.SetValue(@"HKEY_CLASSES_ROOT\Mime\Database\Content Type\" + contentType, "Extension", ext);
                    RegistryAlt.SetValue(@"HKEY_CLASSES_ROOT\Mime\Database\Content Type\" + contentType, "CLSID", choice.clsid);

                    MessageBox.Show(this, "設定しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void bBrowserFlags2_Click(object sender, EventArgs e) {
            if (lastfp == null) {
                MessageBox.Show(this, "先に調査してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (UseSimple) {
                ModifyValue("BrowserFlags", new IntEditor(0xffffffffU, 0x80000024U, false));
            }
            else {
                EdREGForm form = new EdREGForm();
                form.Modify(lastfp, new IntMod("BrowserFlags", 0xffffffffU, 0x80000024U, false));
                form.ShowDialog(this);
            }
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

            public static string AcrobatDCExe { // 動作は未確認
                get {
                    RegistryKey rkExe = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Adobe Acrobat\DC\Installer", false);
                    if (rkExe != null) {
                        return rkExe.GetValue("Acrobat.exe") as String;
                    }
                    return null;
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

            public static string AcroRd32_DCExe { // 動作は未確認：表示までは確認
                get {
                    RegistryKey rkDir = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Adobe\Acrobat Reader\DC\InstallPath", false);
                    if (rkDir != null) {
                        String dir = rkDir.GetValue("") as String;
                        if (!String.IsNullOrEmpty(dir))
                            return Path.Combine(dir, "AcroRd32.exe");
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
            alExe.Add(AcroPUt.AcrobatDCExe);
            alExe.Add(AcroPUt.Acrobat10Exe);
            alExe.Add(AcroPUt.Acrobat9Exe);
            alExe.Add(AcroPUt.Acrobat8Exe);
            alExe.Add(AcroPUt.Acrobat7Exe);
            alExe.Add(AcroPUt.AcroRd32_DCExe);
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

            if (UseSimple) {
                ModifyValue("BrowseInPlace", new StrEditor("1"));
            }
            else {
                EdREGForm form = new EdREGForm();
                form.Modify(lastfp, new StrMod("BrowseInPlace", "1"));
                form.ShowDialog(this);
            }
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
                ToolStripItem tsiEFP = bOOo.DropDownItems.Add("③ 「Use EFP」で、「SOActiveX Class」を選択");
                tsiEFP.Click += new EventHandler(tsiOOoEFP_Click);
            }
        }

        void tsiOOoEFP_Click(object sender, EventArgs e) {
            String clsid = "{67F2A879-82D5-4A6D-8CC5-FFB3C114B69D}";

            using (SelExtForm form = new SelExtForm()) {
                if (form.ShowDialog() == DialogResult.OK) {
                    foreach (String fext in form.fexts) {
                        String ContentType = "" + Registry.GetValue("HKEY_CLASSES_ROOT\\" + fext, "Content Type", "");
                        if (ContentType.Length != 0) {
                            Registry.SetValue("HKEY_CLASSES_ROOT\\MIME\\Database\\Content Type\\" + ContentType, "CLSID", clsid);
                            Registry.SetValue("HKEY_CLASSES_ROOT\\MIME\\Database\\Content Type\\" + ContentType, "Extension", fext);
                            using (RegistryKey rkExt = Registry.ClassesRoot.CreateSubKey("CLSID\\" + clsid + "\\EnableFullPage\\" + fext)) {
                            }
                        }
                    }
                    MessageBox.Show(this, "設定しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        class Utm {
            public static object Modify(object val, uint remove, uint add) {
                if (val is byte[] && ((byte[])val).Length == 4) val = BitConverter.ToUInt32((byte[])val, 0);
                if (val is uint) val = (int)(uint)val;
                if (val is int) {
                    val = (int)((((uint)(int)val) | add) & (~remove));
                }
                else {
                    val = (int)add;
                }
                return val;
            }
        }

        void Mso(String fexts) {
            foreach (String fext in fexts.Split(',')) {
                String ContentType = "" + Registry.GetValue("HKEY_CLASSES_ROOT\\" + fext, "Content Type", "");
                if (!String.IsNullOrEmpty(ContentType)) {
                    using (RegistryKey rkMime = Registry.ClassesRoot.CreateSubKey("MIME\\Database\\Content Type\\" + ContentType)) {
                        rkMime.DeleteValue("CLSID", false);
                        rkMime.SetValue("Extension", fext);
                    }
                }
                String ProgID = "" + Registry.GetValue("HKEY_CLASSES_ROOT\\" + fext, "", "");
                if (!String.IsNullOrEmpty(ProgID)) {
                    {
                        String a = "EditFlags";
                        Registry.SetValue("HKEY_CLASSES_ROOT\\" + ProgID, a, Utm.Modify(Registry.GetValue("HKEY_CLASSES_ROOT\\" + ProgID, a, 0), 0, 0x10000));
                    }
                    {
                        // https://support.microsoft.com/ja-jp/kb/982995
                        uint BrowserFlags = 0x80000024;
                        if (fext.StartsWith(".do")) BrowserFlags = 0x80000024;
                        //if (fext.StartsWith(".xl")) BrowserFlags = 0x80000A00;
                        if (fext.StartsWith(".pp")) BrowserFlags = 0x800000A0;
                        if (fext.StartsWith(".po")) BrowserFlags = 0x800000A0;

                        String a = "BrowserFlags";
                        Registry.SetValue("HKEY_CLASSES_ROOT\\" + ProgID, a, (int)BrowserFlags);
                    }
                }
            }

            MessageBox.Show(this, "設定しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bMso_Click(object sender, EventArgs e) {
            using (SelExtForm form = new SelExtForm()) {
                if (form.ShowDialog() == DialogResult.OK) {
                    Mso(String.Join(",", form.fexts.ToArray()));
                }
            }
        }

        private void bAdobePlan_Click(object sender, EventArgs e) {
            // https://www.adobe.com/devnet-docs/acrobatetk/tools/PrefRef/Windows/Originals.html
            foreach (String ver in "10.0/11.0/DC".Split('/')) {
                String left = @"HKEY_CURRENT_USER\Software\Adobe\Acrobat Reader\" + ver + @"\Originals";
                String right = "bBrowserIntegration";

                Object curData = Registry.GetValue(left, right, null);
                if (curData != null) {
                    switch (MessageBox.Show(this, "つぎのレジストリの値を削除します。\n\n" + left + "\n" + right, Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)) {
                        case DialogResult.Yes:
                            RegDelValue(left, right);
                            MessageBox.Show(this, "削除しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case DialogResult.No:
                            // スキップ
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
            }

            {
                String left = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Ext\Settings\{CA8A9780-280D-11CF-A24D-444553540000}";
                Object curData = Registry.GetValue(left, "Flags", null);
                if (curData != null) {
                    switch (MessageBox.Show(this, "つぎのレジストリキーを削除します。\n\n" + left, Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)) {
                        case DialogResult.Yes:
                            RegDelKey(left);
                            MessageBox.Show(this, "削除しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case DialogResult.No:
                            // スキップ
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
            }

            MessageBox.Show(this, "確認は完了しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RegDelKey(string left) {
            if (left.StartsWith(@"HKEY_CURRENT_USER\")) {
                left = left.Substring(18);
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(Path.GetDirectoryName(left), true);
                if (rk != null) {
                    rk.DeleteSubKey(Path.GetFileName(left));
                }
            }
            else {
                throw new NotSupportedException(left);
            }
        }

        private void RegDelValue(string left, string right) {
            if (left.StartsWith(@"HKEY_CURRENT_USER\")) {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(left.Substring(18), true);
                if (rk != null) {
                    rk.DeleteValue(right);
                }
            }
            else {
                throw new NotSupportedException(left);
            }
        }

        private void bJustPDFContentType_Click(object sender, EventArgs e) {
            Registry.SetValue(@"HKEY_CLASSES_ROOT\.pdf", "Content Type", "application/pdf");
            MessageBox.Show(this, "設定しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bJustPDFMemo_Click(object sender, EventArgs e) {
            Process.Start(Path.Combine(Application.StartupPath, "JustPDFSettei.png"));
        }

    }
}
