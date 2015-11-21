; /* Search YouTube from Clipboard */

/*
Run, https://www.youtube.com/
Sleep, 3000
SendInput, ^v
Send, {enter} ;

; /* Play first result */
; First result location
x := 1000
y := 550

MouseMove, x, y
Sleep, 2000
Click x, y


; Play Button (seems unnecessary now)
; x := 700
; y := 440

; MouseMove, x, y
; Sleep, 1000
; Click x, y
*/

Run, https://goosh.org/
Sleep, 2000
youtube := "youtube.com "
SendInput, %youtube%
SendInput, ^v
Send, {enter}
Sleep, 1000

open := "open 1"
SendInput, %open%
Send, {enter}
Sleep, 3000

; /* Cast to TV */

; screen cast button
x := 1850
y := 50

MouseMove, x, y
Sleep, 1000
Click x, y

; first listed cast device
x := 1825
y := 120

MouseMove, x, y
Sleep, 1000
Click x, y