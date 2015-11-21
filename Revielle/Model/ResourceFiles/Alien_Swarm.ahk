#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; Run, http://www.google.com
run %comspec% /k Steam.lnk
run %comspec% /k Alien_Swarm.lnk
