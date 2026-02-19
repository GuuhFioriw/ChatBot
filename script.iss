#define AppName "ChatBot"
#define AppExeName "ChatBot.exe"
#define AppVersion "3.2.9"

[Setup]
AppId={{B7F0D9B6-9C5F-4C3E-9F01-CHATBOT2026}
AppName={#AppName}
AppVersion={#AppVersion}
OutputBaseFilename={#AppName}_{#AppVersion}

DefaultDirName={localappdata}\{#AppName}
DefaultGroupName={#AppName}
PrivilegesRequired=lowest

DisableDirPage=yes
DisableProgramGroupPage=yes

SetupIconFile="C:\Users\gusta\source\repos\ChatBot\ChatBot.ico"
UninstallDisplayIcon={app}\ChatBot.ico

Compression=lzma
SolidCompression=yes
WizardStyle=modern
CloseApplications=yes

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\gusta\source\repos\ChatBot\ChatBot\bin\Release\net10.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\gusta\source\repos\ChatBot\ChatBot.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{userdesktop}\{#AppName}"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\ChatBot.ico"; Tasks: desktopicon
Name: "{userprograms}\{#AppName}"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\ChatBot.ico"

[Run]
Filename: "{app}\{#AppExeName}"; Flags: nowait postinstall skipifsilent

[Code]
procedure CurUninstallStepChanged(UninstallStep: TUninstallStep);
begin
  if UninstallStep = usPostUninstall then
  begin
    DelTree(ExpandConstant('{app}'), True, True, True);
  end;
end;
