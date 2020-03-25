; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

Unicode true

!define APP "ChkIEArea"
!system 'DefineAsmVer.exe "chkiearea\bin\x86\release\${APP}.exe" "!define VER ""[SFVER]"" " > Tmpver.nsh'
!include "Tmpver.nsh"

!system 'MySign "chkiearea\bin\x86\release\${APP}.exe"  "chkiearea\bin\release\${APP}.exe" '
!finalize 'MySign "%1"'

!searchreplace APV ${VER} "." "_"

; The name of the installer
Name "${APP} ${VER}"

; The file to write
!ifndef USER
OutFile "Setup_${APP}_${APV}_admin.exe"
!else
OutFile "Setup_${APP}_${APV}_user.exe"
!endif

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
!ifndef USER
RequestExecutionLevel admin
!else
RequestExecutionLevel user
!endif

!include LogicLib.nsh

AutoCloseWindow true

LicenseData "License.rtf"

XPStyle on

;--------------------------------

; Pages

Page license
Page directory
Page components
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

SectionEnd ; end the section

Section "x86"
  ; Put file there
  SetOutPath $INSTDIR
  File "chkiearea\bin\x86\release\ChkIEArea.exe"
  File "chkiearea\bin\x86\release\ChkIEArea.exe.config"
  File "chkiearea\bin\x86\release\ChkIEArea.pdb"

  File "chkiearea\bin\x86\release\JustPDFSettei.png"
  File "chkiearea\bin\x86\release\ooo341.reg"

  SetOutPath $INSTDIR\f
  File "chkiearea\bin\x86\release\f\*.*"

  Exec "$INSTDIR\ChkIEArea.exe"
SectionEnd

Section /o "Any CPU"
  ; Put file there
  SetOutPath $INSTDIR\AnyCPU
  File "chkiearea\bin\release\ChkIEArea.exe"
  File "chkiearea\bin\release\ChkIEArea.exe.config"
  File "chkiearea\bin\release\ChkIEArea.pdb"

  File "chkiearea\bin\release\JustPDFSettei.png"
  File "chkiearea\bin\release\ooo341.reg"

  SetOutPath $INSTDIR\AnyCPU\f
  File "chkiearea\bin\release\f\*.*"

  Exec "$INSTDIR\AnyCPU\ChkIEArea.exe"
SectionEnd
