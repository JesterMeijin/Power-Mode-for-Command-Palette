#define AppVersion "1.0.0.0"

[Setup]
AppId={{47691d48-7642-4ad2-9682-f02d460bf671}}
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
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{47691d48-7642-4ad2-9682-f02d460bf671}}"; ValueData: "PowerModeCmdPal"
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{47691d48-7642-4ad2-9682-f02d460bf671}}\LocalServer32"; ValueData: "{app}\PowerModeCmdPal.exe -RegisterProcessAsComServer"