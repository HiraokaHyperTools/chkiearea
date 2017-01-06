using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace ChkIEArea {
    class RegistryAlt {
        public static void SetValue(string keyName, string leftName, object newData) {
            String hkcrPrefix = @"HKEY_CLASSES_ROOT\";
            String hkcrAltPrefix = @"HKEY_CURRENT_USER\Software\Classes\";

            try {
                Registry.SetValue(keyName, leftName, newData);
            }
            catch (UnauthorizedAccessException) {
                Registry.SetValue(keyName.Replace(hkcrPrefix, hkcrAltPrefix), leftName, newData);
            }
        }

    }
}
