#define AppName "ChatBot"
#define AppExeName "ChatBot.exe"
#define AppVersion "3.2.7"

[Setup]
AppId={{B7F0D9B6-9C5F-4C3E-9F01-CHATBOT2026}
AppName={#AppName}
AppVersion={#AppVersion}

; --- CONFIGURAÇÃO DO NOME DO ARQUIVO DE SAÍDA ---
; O resultado será: ChatBot_3.2.7.exe
OutputBaseFilename={#AppName}_{#AppVersion}
; -----------------------------------------------

DefaultDirName={localappdata}\{#AppName}
DefaultGroupName={#AppName}
PrivilegesRequired=lowest
SetupIconFile="C:\Users\gusta\source\repos\ChatBot\ChatBot.ico"
UninstallDisplayIcon={app}\ChatBot.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
CloseApplications=yes

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Origem dos arquivos compilados em Release no VS
Source: "C:\Users\gusta\source\repos\ChatBot\ChatBot\bin\Release\net10.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\gusta\source\repos\ChatBot\ChatBot.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{userdesktop}\{#AppName}"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\ChatBot.ico"; Tasks: desktopicon
Name: "{userprograms}\{#AppName}"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\ChatBot.ico"

[Run]
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
procedure CurUninstallStepChanged(UninstallStep: TUninstallStep);
begin
  if UninstallStep = usPostUninstall then
  begin
    DelTree(ExpandConstant('{app}'), True, True, True);
  end;
end;