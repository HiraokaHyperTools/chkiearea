using System;
using System.Collections.Generic;
using System.Text;

namespace ChkIEArea.Models {
    public class AppChoice {
        public static readonly AppChoice Remove = new AppChoice("", "");

        public String clsid;
        public String progid;

        public AppChoice() {

        }

        public AppChoice(string oleId, string clsid) {
            this.progid = oleId;
            this.clsid = clsid;
        }
    }
}
