#define AppName "ChatBot"
#define AppExeName "ChatBot.exe"
#define AppVersion "3.2.7"

[Setup]
AppId={{B7F0D9B6-9C5F-4C3E-9F01-CHATBOT2026}
AppName={#AppName}
AppVersion={#AppVersion}
; INSTALAÇÃO LOCAL: Evita conflitos com o motor do Edge/WebView2
DefaultDirName={localappdata}\{#AppName}
DefaultGroupName={#AppName}
; PrivilegesRequired=lowest garante que o instalador rode sem pedir senha de admin
PrivilegesRequired=lowest
SetupIconFile="C:\Users\gusta\source\repos\ChatBot\ChatBot.ico"
UninstallDisplayIcon={app}\ChatBot.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Copia todos os arquivos da pasta Release
Source: "C:\Users\gusta\source\repos\ChatBot\ChatBot\bin\Release\net10.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

; Copia o ícone para a pasta do app
Source: "C:\Users\gusta\source\repos\ChatBot\ChatBot.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
; CORREÇÃO: Usando {userdesktop} em vez de {commondesktop} para evitar erro de permissão negada
Name: "{userdesktop}\{#AppName}"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\ChatBot.ico"; Tasks: desktopicon
Name: "{userprograms}\{#AppName}"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\ChatBot.ico"

[Run]
; Executa o bot como usuário comum após a instalação
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
// Limpeza completa da pasta na desinstalação (incluindo caches do Edge)
procedure CurUninstallStepChanged(UninstallStep: TUninstallStep);
begin
  if UninstallStep = usPostUninstall then
  begin
    DelTree(ExpandConstant('{app}'), True, True, True);
  end;
end;