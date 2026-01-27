@echo off
setlocal enabledelayedexpansion
for /f %%a in ('echo prompt $E ^| cmd') do set "ESC=%%a"

where dotnet > nul 2>&1
if errorlevel 1 (
    echo %ESC%[91mERROR: dotnet is not installed%ESC%[0m
    exit /b 1
)

if not exist "game_path.txt" (
    set /p GAME_PATH="Enter game path: "
    echo !GAME_PATH!> game_path.txt
) else (
    set /p GAME_PATH=<game_path.txt
)
if not exist "!GAME_PATH!\BepInEx" (
    echo %ESC%[91mERROR: BepInEx not installed at !GAME_PATH!%ESC%[0m
    exit /b 1
)
if not exist "!GAME_PATH!\BepInEx\plugins" (
    echo %ESC%[91mERROR: Please run your game at least once after installing BepInEx%ESC%[0m
    exit /b 1
)
if not exist "GameAssemblies" (
    mkdir GameAssemblies
)
echo Copying BepInEx assemblies to GameAssemblies\
xcopy "!GAME_PATH!\BepInEx\core\*.dll" "GameAssemblies\" /Y > nul
if errorlevel 1 (
    echo %ESC%[91mERROR: Failed to copy BepInEx assemblies%ESC%[0m
    exit /b 1
)
echo Copying game assemblies to GameAssemblies\
xcopy "!GAME_PATH!\MineMogul_Data\Managed\*.dll" "GameAssemblies\" /Y > nul
if errorlevel 1 (
    echo %ESC%[91mERROR: Failed to copy game assemblies%ESC%[0m
    exit /b 1
)
dotnet build > nul 2>&1 || (
    dotnet build
    echo %ESC%[91mERROR: Build failed%ESC%[0m
    exit /b 1
)
echo Build output: bin\Debug\netstandard2.1\MineMogulModMenu.dll
echo Copying plugin to !GAME_PATH!\BepInEx\plugins\
copy /Y "bin\Debug\netstandard2.1\MineMogulModMenu.dll" "!GAME_PATH!\BepInEx\plugins\" > nul
if errorlevel 1 (
    echo %ESC%[91mERROR: Failed to copy plugin to BepInEx%ESC%[0m
    exit /b 1
)
echo %ESC%[92mSUCCESS: Build Complete!%ESC%[0m