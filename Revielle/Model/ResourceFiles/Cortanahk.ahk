

; Test Clipboard: cortana execute Cole Protocol

; Open Cortana Search Box
Send, {LWin} ;

; Pass Clipboard to Cortana Search Box
SendInput, ^v ;
Sleep, 2000 ;
Send, {enter} ;
