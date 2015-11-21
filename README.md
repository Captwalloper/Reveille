# Reveille
Demo for creating custom Cortana commands.

OVERVIEW:

Revielle is a Windows 10 Univeral App which demonstrates MVVM and Cortana Integration. <br/>
In-progress, the VoiceCommandService will allow the app to function even when it's not in the foreground. <br/>

USE:

1) Execute a custom autohotkey script with your voice! (No recompilation needed!) <br/>
----- a) Place your autohotkey script(s) in the app's ResourceFiles directory (under Model). <br/>
----- b) Rename the scripts so each word is capitalized, and separated by underscore (ex "My_Custom_Script.ahk"). <br/>
----- c) Activate Cortana by clicking the mic icon next to the "ask me anything" textbox on the taskbar, or pressing WIN+C. <br/>
----- d) Say "Cortana, execute [filename]" <br/>
----- e) Cortana will launch your script; diagnostics will be displayed by the app. <br/>

2) Create an entirely custom command. <br/>
----- a) Modify the vcd file appropriately (https://msdn.microsoft.com/en-us/library/windows/apps/mt185609.aspx) <br/>
----- b) Modify the ProcessCommand method in the Cortana class. <br/>
----- b-2) Create the appropriate subclass of CortanaCommand. You'll need to instantiate it in the step above. <br/>

3) What on Earth does "unseal the hushed casket" do? <br/>
----- Ignore that if it bothers you. Eventually, that may turn into an interactive tutorial. For now it's an easter egg. <br/>

FINAL COMMENTS:

Cortana is an extremely cool feature of Windows 10. Hopefully, you'll be able to leverage this simple application to better exploit it. 
