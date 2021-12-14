# GTA V Music Event Player
GTA V Music Event Player is a script using Script Hook V .NET that can play any Music Event in GTA V. Music Events are what play background music for missions in GTA V. They are played by calling the native function AUDIO::TRIGGER_MUSIC_EVENT, however I could not find an easy way to test them out and see which one sounds good for your project, so I made a script that does if for you.
## Requirements 
- [Script Hook V](http://www.dev-c.com/gtav/scripthookv/)
- [Script Hook V .NET](https://github.com/crosire/scripthookvdotnet)
- [LemonUI](https://github.com/justalemon/LemonUI)
- [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)
## Installation
- Download and install the latest versions of Script Hook V, Script Hook V .NET, and LemonUI
- Download the latest version of Json.NET and place **Newtonsoft.Json.dll** as well as **Newtonsoft.Json.xml** in your scripts folder
- Download the latest versions of **Music Event Player.dll**, **musicEventNames.json**, and **musicStopEventNames.json** from Releases and place them in your scripts folder
- .pdb files are optional and are only used for giving error reports if the script were to crash
## Useage
- The key to open the menu is F3
- Open menu and start and stop any Music Event you want
- It is not required to stop the current event before playing another
## Notes
- Some Music Events do not work, if an event is not functioning correctly there will be no music playing when you start it
- All music event names are sourced from [GTA V Data Dumps](https://github.com/DurtyFree/gta-v-data-dumps) by Alexander Schmid
