# ImpostorHUD
A BepInEx mod for Among Us that displays evil roles (Impostor, Shapeshifter, Phantom, Viper) on a HUD panel in the top right corner.

## Installation
1. Install [r2modman](https://thunderstore.io/package/ebkr/r2modman/)
2. Install BepInEx via r2modman
3. Drop `ImpostorHUD.dll` into your BepInEx plugins folder

## Building from source
1. Install [.NET 6 SDK](https://dotnet.microsoft.com/download)
2. Clone the repo
3. Run `dotnet build Plugin.csproj`
4. The dll will be copied to your plugins folder automatically (edit the path in Plugin.csproj first)

## Features
- Shows all Impostor roles in a panel in the top right
- Color coded by role (red = Impostor, orange = Shapeshifter, purple = Phantom, green = Viper)
- Only shows when evil roles are present

## Notes
- For use in freeplay/private games only
- Your fault if you're banned from public servers for cheating. This is not for public.
