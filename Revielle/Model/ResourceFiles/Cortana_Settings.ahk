; /* Open up the settings */
run %comspec% 
Click
SendInput, cd C:\Users\comcc_000\Documents\Visual Studio 2015\Projects\Reveille\Reveille\Model\ResourceFiles
Send, {enter} ;
SendInput, start Cortana_Settings.lnk
Send, {enter} ;

sleep, 1000

; /* Toggle "Hey Cortana" */
CoordMode, Mouse, Screen ; make mousemove relative to desktop (not active window)
x := 80
y := 800

MouseMove, x, y
Sleep, 1000
Click x, y

Sleep, 500

; /* Close Command prompt */
Send, {ALTDOWN}{TAB}{ALTUP}
Sleep, 500
SendInput, Exit
Send, {enter} ;

