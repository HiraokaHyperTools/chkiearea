; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "ChkIEArea"
!system 'DefineAsmVer.exe "chkiearea\bin\x86\release\${APP}.exe" "!define VER ""[SVER]"" " > Tmpver.nsh'
!include "Tmpver.nsh"

!searchreplace APV ${VER} "." "_"

; The name of the installer
Name "${APP} ${VER}"

; The file to write
OutFile "Setup_${APP}_${APV}.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!define DOTNET_VERSION "2.0"

!include "DotNET.nsh"
!include LogicLib.nsh

AutoCloseWindow true

LicenseData "License.rtf"

;--------------------------------

; Pages

Page license
Page directory
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ; Put file there
  File "chkiearea\bin\x86\release\ChkIEArea.exe"
  File "chkiearea\bin\x86\release\ChkIEArea.exe.manifest"
  File "chkiearea\bin\x86\release\ChkIEArea.pdb"
  
  SetOutPath $INSTDIR\f
  File "chkiearea\bin\x86\release\f\*.*"

  Exec "$INSTDIR\ChkIEArea.exe"
SectionEnd ; end the section
