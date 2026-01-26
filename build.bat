@echo off
setlocal enabledelayedexpansion

if not exist "game_path.txt" (
    set /p GAME_PATH="Enter game path: "
    echo !GAME_PATH!> game_path.txt
) else (
    set /p GAME_PATH=<game_path.txt
)

if not exist "GameAssemblies" (
    mkdir GameAssemblies
)

xcopy "!GAME_PATH!\BepInEx\core\*.dll" "GameAssemblies\" /Y
xcopy "!GAME_PATH!\MineMogul_Data\Managed\*.dll" "GameAssemblies\" /Y


dotnet build

copy /Y "bin\Debug\netstandard2.1\MineMogulModMenu.dll" "!GAME_PATH!\BepInEx\plugins\"

echo Done!
pause