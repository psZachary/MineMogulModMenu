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

copy "!GAME_PATH!\BepInEx\core\BepInEx.dll" GameAssemblies\
copy "!GAME_PATH!\BepInEx\core\0Harmony.dll" GameAssemblies\
copy "!GAME_PATH!\MineMogul_Data\Managed\UnityEngine.dll" GameAssemblies\
copy "!GAME_PATH!\MineMogul_Data\Managed\UnityEngine.CoreModule.dll" GameAssemblies\
copy "!GAME_PATH!\MineMogul_Data\Managed\UnityEngine.IMGUIModule.dll" GameAssemblies\
copy "!GAME_PATH!\MineMogul_Data\Managed\UnityEngine.InputLegacyModule.dll" GameAssemblies\
copy "!GAME_PATH!\MineMogul_Data\Managed\UnityEngine.PhysicsModule.dll" GameAssemblies\
copy "!GAME_PATH!\MineMogul_Data\Managed\Assembly-CSharp.dll" GameAssemblies\


dotnet build

copy /Y "bin\Debug\netstandard2.1\MineMogulModMenu.dll" "!GAME_PATH!\BepInEx\plugins\"

echo Done!
pause