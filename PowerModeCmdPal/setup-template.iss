#define AppVersion "1.0.0.0"

[Setup]
AppId={{GUID-HERE}}
AppName=Power Mode for Command Palette
AppVersion={#AppVersion}
AppPublisher=JesterMeijin
DefaultDirName={autopf}\PowerModeCmdPal
OutputDir=bin\Release\installer
OutputBaseFilename=PowerModeCmdPal-Setup-{#AppVersion}
Compression=lzma
SolidCompression=yes
MinVersion=10.0.19041

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "bin\Release\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\Power Mode for Command Palette"; Filename: "{app}\PowerModeCmdPal.exe"

[Registry]
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{71065ad6-14c0-44df-81ab-bd88bd9d7bfb}}"; ValueData: "PowerModeCmdPal"
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{71065ad6-14c0-44df-81ab-bd88bd9d7bfb}}\LocalServer32"; ValueData: "{app}\PowerModeCmdPal.exe -RegisterProcessAsComServer"