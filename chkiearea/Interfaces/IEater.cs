using System;
using System.Collections.Generic;
using System.Text;
using ChkIEArea.Models;
using Microsoft.Win32;

namespace ChkIEArea.Interfaces {
    public interface IEater {
        void AddCLSIDSimple(RegistryKey classesRoot, string ext, AppChoice choice);
        void AddCLSID(RegistryKey classesRoot, string ext, AppChoice choice);
        void SetContentType(string contentType);
        void AddDESimple(RegistryKey rkrootclsid, AppChoice choice);
        void AddDE(RegistryKey rkrootclsid, AppChoice choice);
        void AddEFPSimple(RegistryKey rkrootclsid, AppChoice choice);
        void AddEFP(RegistryKey rkrootclsid, AppChoice choice);
    }
}
