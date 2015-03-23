using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace ChkIEArea {
    public partial class EdREGForm : Form, IEdMarker {
        public EdREGForm() {
            InitializeComponent();
        }

        class RV {
            public RV(ValRef vr, Label la, IList<RV> al) {
                this.vr = vr;
                this.la = la;
                al.Add(this);
            }

            public ValRef vr;
            public Label la;

            public bool Mark(IValMod vm, IEdMarker em) {
                if (vm.ModifyVal(vr)) {
                    em.MarkEditable(la, new EV(vr));
                    return true;
                }
                return false;
            }
        }

        class EV : IApplyEd {
            public EV(ValRef vr) {
                this.vr = vr;
            }

            public override string ToString() {
                return "→" + vr.GetMod();
            }

            ValRef vr;

            #region IApplyEd メンバ

            public void Apply() {
                vr.Apply();
            }

            #endregion
        }

        List<RV> alrv = new List<RV>();

        public void Modify(string lastfp, IValMod vm) {
            String ext = Path.GetExtension(lastfp);
            RegistryKey extKey = Registry.ClassesRoot.OpenSubKey(ext, false);

            alrv.Clear();

            AddHR();

            AddRKey(0, "HKEY_CLASSES_ROOT");
            AddRKey(1, ext, extKey != null);
            if (extKey != null) {
                String extDef = "" + extKey.GetValue("");
                String extCt = "" + extKey.GetValue("Content Type");
                AddVKey(2, "(既定)", extDef);
                AddVKey(2, "Content Type", extCt);
                AddSpc();

                AddRKey(0, "HKEY_CLASSES_ROOT");
                if (!String.IsNullOrEmpty(extDef)) {
                    RegistryKey pidKey = Registry.ClassesRoot.OpenSubKey(extDef, false);
                    AddRKey(1, extDef, pidKey != null);
                    if (pidKey != null) {
                        RegistryKey pidClsidKey = pidKey.OpenSubKey("CLSID", false);
                        if (pidClsidKey != null) {
                            AddVKey(2, "CLSID", "" + pidClsidKey.GetValue(""));
                        }
                        Ctls cs;
                        cs = AddVKey(2, "BrowseInPlace", "" + pidKey.GetValue("BrowseInPlace"));
                        new RV(new ValRef(pidKey, "BrowseInPlace"), cs.la, alrv);
                        cs = AddVKey(2, "BrowserFlags", FUt.Hex(pidKey.GetValue("BrowserFlags")));
                        new RV(new ValRef(pidKey, "BrowserFlags"), cs.la, alrv);
                        cs = AddVKey(2, "EditFlags", FUt.Hex(pidKey.GetValue("EditFlags")));
                        new RV(new ValRef(pidKey, "EditFlags"), cs.la, alrv);
                    }
                }
                AddSpc();

                AddRKey(0, "HKEY_CLASSES_ROOT");
                AddRKey(1, "MIME");
                AddRKey(2, "Database");
                AddRKey(3, "Content Type");
                if (!String.IsNullOrEmpty(extCt)) {
                    RegistryKey mimeKey = Registry.ClassesRoot.OpenSubKey("MIME\\Database\\Content Type\\" + extCt, false);
                    AddRKey(4, extCt, mimeKey != null);
                    if (mimeKey != null) {
                        String mimeClsid = "" + mimeKey.GetValue("CLSID");
                        AddVKey(5, "CLSID", mimeClsid);
                        AddVKey(5, "Extension", "" + mimeKey.GetValue("Extension"));
                        AddSpc();

                        AddRKey(0, "HKEY_CLASSES_ROOT");
                        AddRKey(1, "CLSID");
                        if (!String.IsNullOrEmpty(mimeClsid)) {
                            RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID\\" + mimeClsid, false);
                            AddRKey(2, mimeClsid, clsidKey != null);
                            if (clsidKey != null) {
                                AddVKey(3, "", "" + clsidKey.GetValue(""));
                                RegistryKey clsidDeKey = clsidKey.OpenSubKey("DefaultExtension", false);
                                AddRKey(3, "DefaultExtension", clsidDeKey != null);
                                if (clsidDeKey != null) {
                                    AddVKey(4, "", "" + clsidDeKey.GetValue(""));
                                }
                                RegistryKey clsidEfpKey = clsidKey.OpenSubKey("EnableFullPage", false);
                                AddRKey(3, "EnableFullPage", clsidEfpKey != null);
                                if (clsidEfpKey != null) {
                                    foreach (String sk in clsidEfpKey.GetSubKeyNames()) {
                                        AddRKey(4, sk);
                                    }
                                }
                                RegistryKey clsidIpsKey = clsidKey.OpenSubKey("InprocServer32", false);
                                AddRKey(3, "InprocServer32", clsidIpsKey != null);
                                if (clsidIpsKey != null) {
                                    AddVKey(4, "", "" + clsidIpsKey.GetValue(""));
                                }
                                RegistryKey clsidPidKey = clsidKey.OpenSubKey("ProgID", false);
                                AddRKey(3, "ProgID", clsidPidKey != null);
                                if (clsidPidKey != null) {
                                    String clsidPid = "" + clsidPidKey.GetValue("");
                                    AddVKey(4, "", clsidPid);
                                    AddSpc();

                                    AddRKey(0, "HKEY_CLASSES_ROOT");
                                    if (!String.IsNullOrEmpty(clsidPid)) {
                                        RegistryKey pidKey = Registry.ClassesRoot.OpenSubKey(clsidPid, false);
                                        AddRKey(1, clsidPid, pidKey != null);
                                        if (pidKey != null) {
                                            RegistryKey pidClsidKey = pidKey.OpenSubKey("CLSID", false);
                                            AddRKey(2, "CLSID", pidClsidKey != null);
                                            if (pidClsidKey != null) {
                                                AddVKey(3, "", "" + pidClsidKey.GetValue(""));
                                            }
                                            Ctls cs;
                                            cs = AddVKey(2, "BrowseInPlace", "" + pidKey.GetValue("BrowseInPlace"));
                                            new RV(new ValRef(pidKey, "BrowseInPlace"), cs.la, alrv);
                                            cs = AddVKey(2, "BrowserFlags", FUt.Hex(pidKey.GetValue("BrowserFlags")));
                                            new RV(new ValRef(pidKey, "BrowserFlags"), cs.la, alrv);
                                            cs = AddVKey(2, "EditFlags", FUt.Hex(pidKey.GetValue("EditFlags")));
                                            new RV(new ValRef(pidKey, "EditFlags"), cs.la, alrv);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            bool any = false;
            foreach (RV rv in alrv) {
                if (vm.Can(rv.vr) && rv.Mark(vm, this)) {
                    any |= true;
                }
            }
            if (!any) {
                lTodo.Text = "変更は不要です。";
            }
        }

        class FUt {
            internal static String Hex(Object v) {
                if (v is byte[]) {
                    byte[] bin = (byte[])v;
                    String s = "0x";
                    s += (bin.Length >= 4) ? bin[3].ToString("X2") : "00";
                    s += (bin.Length >= 3) ? bin[2].ToString("X2") : "00";
                    s += (bin.Length >= 2) ? bin[1].ToString("X2") : "00";
                    s += (bin.Length >= 1) ? bin[0].ToString("X2") : "00";
                    return s;
                }
                else if (v == null) return "";
                else if (v is String) return "";
                else return String.Format("0x{0:X8}", v);
            }
        }

        #region IEdMarker メンバ

        public void MarkEditable(Label la, IApplyEd ae) {
            Button b = new Button();
            b.Text = "" + ae;
            b.AutoSize = true;
            //b.Left = la.Right + la.Margin.Right;
            //b.Top = (la.Top + la.Bottom) / 2 - b.Height / 2;
            //b.Parent = la.Parent;
            b.Anchor = AnchorStyles.Left;
            b.Parent = la.Parent;
            la.Parent.Controls.SetChildIndex(b, la.Parent.Controls.IndexOf(la) + 1);
            b.Click += delegate {
                ae.Apply();
                MessageBox.Show(this, "設定しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        #endregion

        private void EdREGForm_Load(object sender, EventArgs e) {

        }

        Bitmap _Folder = ChkIEArea.Properties.Resources.Folder_16x16;
        Bitmap _FolderDeleted = ChkIEArea.Properties.Resources.DeleteFolderHS;
        Bitmap _Prop = ChkIEArea.Properties.Resources.PropertiesHS;

        Ctls AddVKey(int indent, String name, String val) {
            if (String.IsNullOrEmpty(name)) name = "(既定)";
            return AddKey(indent, name + " = " + val, _Prop);
        }
        Ctls AddRKey(int indent, String name) {
            return AddKey(indent, name, _Folder);
        }
        Ctls AddRKey(int indent, String name, bool exists) {
            return AddKey(indent, name, exists ? _Folder : _FolderDeleted);
        }
        void AddSpc() {
            Label la = new Label();
            la.AutoSize = false;
            la.Text = "";
            la.Size = new Size(16, 16);
            flp1.Controls.Add(la);
        }
        void AddHR() {
            Label la = new Label();
            la.AutoSize = false;
            la.Text = "";
            la.Size = new Size(flp1.Width - 16, 2);
            la.BorderStyle = BorderStyle.Fixed3D;
            la.Margin = new Padding(0, 5, 0, 5);
            la.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            flp1.Controls.Add(la);
        }

        class Ctls {
            public Label la;
            public FlowLayoutPanel fx;
        }

        Ctls AddKey(int indent, String name, Bitmap p) {
            Ctls ctls = new Ctls();

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
                pb.Anchor = AnchorStyles.Left;
                fx.Controls.Add(pb);
            }
            {
                Label la = new Label();
                la.MinimumSize = new Size(0, 16);
                la.Text = name;
                la.AutoSize = true;
                la.TextAlign = ContentAlignment.MiddleLeft;
                la.Margin = new Padding(3, 0, 0, 0);
                la.Font = _Font;
                la.Anchor = AnchorStyles.Left;
                fx.Controls.Add(la);
                ctls.la = la;
            }
            flp1.Controls.Add(fx);
            ctls.fx = fx;

            return ctls;
        }

        Font _Font = new Font("ＭＳ ゴシック", 9, FontStyle.Bold);
    }

    public interface IEdMarker {
        void MarkEditable(Label la, IApplyEd ae);
    }

    public interface IApplyEd {
        void Apply();
    }

    public interface IValMod {
        bool ModifyVal(ValRef vr);

        bool Can(ValRef vr);
    }

    public class ValRef {
        public ValRef(RegistryKey rk, String valName) {
            this.rk = rk;
            this.valName = valName;
        }

        public Object GetValue() {
            return rk.GetValue(valName);
        }
        public void SetValue(Object nv) {
            this.nv = nv;
        }

        Object nv;

        public RegistryKey rk;
        public String valName;

        public string GetMod() {
            return Utrv.Format(nv);
        }

        public void Apply() {
            Registry.SetValue(rk.Name, valName, nv);
        }
    }

    class Utrv {
        public static String Format(Object v) {
            String s = "";
            if (v is byte[]) {
                foreach (byte b in (byte[])v) {
                    s += b.ToString("X2") + " ";
                }
                return s.TrimEnd();
            }
            else if (v == null) return "";
            else if (v is String) return "" + v;
            else if (v is int || v is uint) return String.Format("0x{0:X8}", v);
            return "";
        }
    }

    class StrMod : IValMod {
        public StrMod(String valName, String s) {
            this.valName = valName;
            this.s = s;
        }

        String valName;
        String s;

        #region IValMod メンバ

        public bool ModifyVal(ValRef vr) {
            String o = vr.GetValue() as String;
            if (o == null || !o.Equals(s)) {
                vr.SetValue(s);
                return true;
            }
            return false;
        }

        public bool Can(ValRef vr) {
            return (vr.valName.Equals(valName));
        }

        #endregion
    }

    class IntMod : IValMod {
        public IntMod(String valName, uint fRemove, uint fAdd, bool preferBytea) {
            this.valName = valName;
            this.fRemove = fRemove;
            this.fAdd = fAdd;
            this.preferBytea = preferBytea;
        }

        String valName;
        uint fRemove;
        uint fAdd;
        bool preferBytea;

        #region IValMod メンバ

        public bool ModifyVal(ValRef vr) {
            Object o = vr.GetValue();
            uint v = 0;
            if (o is int) { v = (uint)((int)o); }
            else if (o is Int64) { v = (uint)((Int64)o); }
            else if (o is byte[]) {
                byte[] bin = (byte[])o;
                if (bin.Length >= 1) v |= bin[0];
                if (bin.Length >= 2) v |= (uint)(bin[1] << 8);
                if (bin.Length >= 3) v |= (uint)(bin[2] << 16);
                if (bin.Length >= 4) v |= (uint)(bin[3] << 24);
            }
            {
                uint nv = (v & (~fRemove)) | fAdd;
                if (v != nv) {
                    vr.SetValue(preferBytea ? (Object)Ut.Bytea(nv) : nv);
                    return true;
                }
            }
            return false;
        }

        public bool Can(ValRef vr) {
            return (vr.valName.Equals(valName));
        }

        #endregion

        class Ut {
            internal static byte[] Bytea(uint nv) {
                MemoryStream os = new MemoryStream(4);
                new BinaryWriter(os).Write(nv);
                return os.ToArray();
            }
        }
    }
    // uint fRemove, uint fAdd, bool preferBytea
}