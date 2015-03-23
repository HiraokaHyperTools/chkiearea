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
            lExt.Text = ext;
            RegistryKey extKey = Registry.ClassesRoot.OpenSubKey(ext, false);

            lExtDef.Text = lPid.Text = "";
            lExtCT.Text = lMime.Text = "";
            lPidClsidDef.Text = "";
            lPidBip.Text = "";
            lPidBf.Text = "";
            lPidEf.Text = "";
            lMimeClsid.Text = lClsid.Text = "";
            lMimeExt.Text = "";
            lClsidDef.Text = "";
            lClsidDe.Text = "";
            lClsidEfp.Text = "";
            lClsidIps.Text = "";
            lClsidPid.Text = "";
            lPid2Clsid.Text = "";
            lPid2.Text = "";
            lPid2Bip.Text = "";
            lPid2Bf.Text = "";
            lPid2Ef.Text = "";

            alrv.Clear();

            if (extKey != null) {
                String extDef = "" + extKey.GetValue("");
                lExtDef.Text = lPid.Text = extDef;
                String extCt = "" + extKey.GetValue("Content Type");
                lExtCT.Text = lMime.Text = extCt;

                if (!String.IsNullOrEmpty(extDef)) {
                    RegistryKey pidKey = Registry.ClassesRoot.OpenSubKey(extDef, false);
                    if (pidKey != null) {
                        RegistryKey pidClsidKey = pidKey.OpenSubKey("CLSID", false);
                        if (pidClsidKey != null) {
                            lPidClsidDef.Text = "" + pidClsidKey.GetValue("");
                        }
                        lPidBip.Text = "" + pidKey.GetValue("BrowseInPlace");
                        new RV(new ValRef(pidKey, "BrowseInPlace"), lPidBf, alrv);
                        lPidBf.Text = FUt.Hex(pidKey.GetValue("BrowserFlags"));
                        new RV(new ValRef(pidKey, "BrowserFlags"), lPidBf, alrv);
                        lPidEf.Text = FUt.Hex(pidKey.GetValue("EditFlags"));
                        new RV(new ValRef(pidKey, "EditFlags"), lPidEf, alrv);
                    }
                }
                if (!String.IsNullOrEmpty(extCt)) {
                    RegistryKey mimeKey = Registry.ClassesRoot.OpenSubKey("MIME\\Database\\Content Type\\" + extCt, false);
                    if (mimeKey != null) {
                        String mimeClsid = "" + mimeKey.GetValue("CLSID");
                        lMimeClsid.Text = lClsid.Text = mimeClsid;
                        lMimeExt.Text = "" + mimeKey.GetValue("Extension");

                        if (!String.IsNullOrEmpty(mimeClsid)) {
                            RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID\\" + mimeClsid, false);
                            if (clsidKey != null) {
                                lClsidDef.Text = "" + clsidKey.GetValue("");
                                RegistryKey clsidDeKey = clsidKey.OpenSubKey("DefaultExtension", false);
                                if (clsidDeKey != null) {
                                    lClsidDe.Text = "" + clsidDeKey.GetValue("");
                                }
                                RegistryKey clsidEfpKey = clsidKey.OpenSubKey("EnableFullPage", false);
                                if (clsidEfpKey != null) {
                                    lClsidEfp.Text = String.Join(" ", clsidEfpKey.GetSubKeyNames());
                                }
                                RegistryKey clsidIpsKey = clsidKey.OpenSubKey("InprocServer32", false);
                                if (clsidIpsKey != null) {
                                    lClsidIps.Text = "" + clsidIpsKey.GetValue("");
                                }
                                RegistryKey clsidPidKey = clsidKey.OpenSubKey("ProgID", false);
                                if (clsidPidKey != null) {
                                    String clsidPid = "" + clsidPidKey.GetValue("");
                                    lClsidPid.Text = lPid2.Text = clsidPid;

                                    if (!String.IsNullOrEmpty(clsidPid)) {
                                        RegistryKey pidKey = Registry.ClassesRoot.OpenSubKey(clsidPid, false);
                                        if (pidKey != null) {
                                            RegistryKey pidClsidKey = pidKey.OpenSubKey("CLSID", false);
                                            if (pidClsidKey != null) {
                                                lPid2Clsid.Text = "" + pidClsidKey.GetValue("");
                                            }
                                            lPid2Bip.Text = "" + pidKey.GetValue("BrowseInPlace");
                                            new RV(new ValRef(pidKey, "BrowseInPlace"), lPid2Bip, alrv);
                                            lPid2Bf.Text = FUt.Hex(pidKey.GetValue("BrowserFlags"));
                                            new RV(new ValRef(pidKey, "BrowserFlags"), lPid2Bf, alrv);
                                            lPid2Ef.Text = FUt.Hex(pidKey.GetValue("EditFlags"));
                                            new RV(new ValRef(pidKey, "EditFlags"), lPid2Ef, alrv);
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
            b.Left = la.Right + la.Margin.Right;
            b.Top = (la.Top + la.Bottom) / 2 - b.Height / 2;
            b.Parent = la.Parent;
            b.Click += delegate {
                ae.Apply();
                MessageBox.Show(this, "設定しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        #endregion
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
            isSet = true;
        }

        bool isSet = false;
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
            String o = "" + vr.GetValue();
            if (!o.Equals(s)) {
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