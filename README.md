# MineMogulModMenu
A bloat-free mod menu written in C# using BepinEx for the game MineMogul. 
## Requirements
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download)
- [BepinEx](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.4/BepInEx_win_x64_5.4.23.4.zip)
- [MineMogul](https://minemogul.com)
## Usage
1. Download and extract [BepinEx](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.4/BepInEx_win_x64_5.4.23.4.zip) to your MineMogul game folder
2. Run the game once to generate BepInEx folders
3. [Download](https://github.com/psZachary/MineMogulModMenu/releases) or [Build](#12) the plugin (see below)
4. Copy the file from `MineMogulModMenu\bin\Debug\netstandard2.1\MineMogulModMenu.dll` to `MineMogul\MineMogul_Data\Managed\`
5. Press **F1** to open the menu in-game.
## Build
1. Install [BepinEx](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.4/BepInEx_win_x64_5.4.23.4.zip) and run the game once to generate BepinEx folders.
2. `git clone https://github.com/psZachary/MineMogulModMenu.git`
3. `cd .\MineMogulModMenu`
4. `.\build.bat`
   
On first run, you'll be prompted for your game path (e.g., `D:\steam\steamapps\common\MineMogul`). It will automatically copy required game assemblies & place the output assembly in the BepinEx folder. 
## Preview
![Preview](https://i.ibb.co/d0Xq12Wf/Screen-Recording-2026-01-26-001402.gif)
## License
MIT
