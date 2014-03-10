.subsections_via_symbols
.section __DWARF, __debug_abbrev,regular,debug

	.byte 1,17,1,37,8,3,8,27,8,19,11,17,1,18,1,16,6,0,0,2,46,1,3,8,17,1,18,1,64,10,0,0
	.byte 3,5,0,3,8,73,19,2,10,0,0,15,5,0,3,8,73,19,2,6,0,0,4,36,0,11,11,62,11,3,8,0
	.byte 0,5,2,1,3,8,11,15,0,0,17,2,0,3,8,11,15,0,0,6,13,0,3,8,73,19,56,10,0,0,7,22
	.byte 0,3,8,73,19,0,0,8,4,1,3,8,11,15,73,19,0,0,9,40,0,3,8,28,13,0,0,10,57,1,3,8
	.byte 0,0,11,52,0,3,8,73,19,2,10,0,0,12,52,0,3,8,73,19,2,6,0,0,13,15,0,73,19,0,0,14
	.byte 16,0,73,19,0,0,16,28,0,73,19,56,10,0,0,18,46,0,3,8,17,1,18,1,0,0,0
.section __DWARF, __debug_info,regular,debug
Ldebug_info_start:

LDIFF_SYM0=Ldebug_info_end - Ldebug_info_begin
	.long LDIFF_SYM0
Ldebug_info_begin:

	.short 2
	.long 0
	.byte 4,1
	.asciz "Mono AOT Compiler 3.2.7 (monotouch-7.0.7-hotfix-branch/2d13830 Mon Feb 17 17:33:59 EST 2014)"
	.asciz "BeBabby.exe"
	.asciz ""

	.byte 2,0,0,0,0,0,0,0,0
LDIFF_SYM1=Ldebug_line_start - Ldebug_line_section_start
	.long LDIFF_SYM1
LDIE_I1:

	.byte 4,1,5
	.asciz "sbyte"
LDIE_U1:

	.byte 4,1,7
	.asciz "byte"
LDIE_I2:

	.byte 4,2,5
	.asciz "short"
LDIE_U2:

	.byte 4,2,7
	.asciz "ushort"
LDIE_I4:

	.byte 4,4,5
	.asciz "int"
LDIE_U4:

	.byte 4,4,7
	.asciz "uint"
LDIE_I8:

	.byte 4,8,5
	.asciz "long"
LDIE_U8:

	.byte 4,8,7
	.asciz "ulong"
LDIE_I:

	.byte 4,4,5
	.asciz "intptr"
LDIE_U:

	.byte 4,4,7
	.asciz "uintptr"
LDIE_R4:

	.byte 4,4,4
	.asciz "float"
LDIE_R8:

	.byte 4,8,4
	.asciz "double"
LDIE_BOOLEAN:

	.byte 4,1,2
	.asciz "boolean"
LDIE_CHAR:

	.byte 4,2,8
	.asciz "char"
LDIE_STRING:

	.byte 4,4,1
	.asciz "string"
LDIE_OBJECT:

	.byte 4,4,1
	.asciz "object"
LDIE_SZARRAY:

	.byte 4,4,1
	.asciz "object"
.section __DWARF, __debug_loc,regular,debug
Ldebug_loc_start:
.section __DWARF, __debug_frame,regular,debug
	.align 3

LDIFF_SYM2=Lcie0_end - Lcie0_start
	.long LDIFF_SYM2
Lcie0_start:

	.long -1
	.byte 3
	.asciz ""

	.byte 1,124,14
	.align 2
Lcie0_end:
.text
	.align 3
methods:
	.space 16
.text
	.align 2
	.no_dead_strip _BeBabby_Application__ctor
_BeBabby_Application__ctor:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 4
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229
	.byte 0,224,157,229,104,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,120,224,158,229,0,0,94,227,0,224,158,21
	.byte 20,208,141,226,0,1,189,232,128,128,189,232

Lme_0:
.text
	.align 2
	.no_dead_strip _BeBabby_Application_Main_string__
_BeBabby_Application_Main_string__:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 8
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 100,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,0,0,160,227,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 12
	.byte 0,0,159,231,20,0,141,229,0,224,157,229,148,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,32,157,229
	.byte 0,16,160,227
bl _p_1

	.byte 0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,196,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_1:
.text
	.align 2
	.no_dead_strip _BeBabby_AppDelegate_get_Window
_BeBabby_AppDelegate_get_Window:

	.byte 128,64,45,233,13,112,160,225,64,1,45,233,16,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 16
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,96,160,227,0,224,157,229,64,224,158,229
	.byte 0,0,94,227,0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21
	.byte 8,0,157,229,20,0,144,229,0,96,160,225,0,224,157,229,116,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 132,224,158,229,0,0,94,227,0,224,158,21,6,0,160,225,6,0,160,225,0,224,157,229,156,224,158,229,0,0,94,227
	.byte 0,224,158,21,16,208,141,226,64,1,189,232,128,128,189,232

Lme_2:
.text
	.align 2
	.no_dead_strip _BeBabby_AppDelegate_set_Window_MonoTouch_UIKit_UIWindow
_BeBabby_AppDelegate_set_Window_MonoTouch_UIKit_UIWindow:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 20
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229
	.byte 12,16,157,229,20,16,128,229,0,224,157,229,116,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,132,224,158,229
	.byte 0,0,94,227,0,224,158,21,20,208,141,226,0,1,189,232,128,128,189,232

Lme_3:
.text
	.align 2
	.no_dead_strip _BeBabby_AppDelegate__ctor
_BeBabby_AppDelegate__ctor:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 24
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229
bl _p_2

	.byte 0,224,157,229,108,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,124,224,158,229,0,0,94,227,0,224,158,21
	.byte 20,208,141,226,0,1,189,232,128,128,189,232

Lme_4:
.text
	.align 2
	.no_dead_strip _BeBabby_AppDelegate_OnResignActivation_MonoTouch_UIKit_UIApplication
_BeBabby_AppDelegate_OnResignActivation_MonoTouch_UIKit_UIApplication:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 28
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,120,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_5:
.text
	.align 2
	.no_dead_strip _BeBabby_AppDelegate_DidEnterBackground_MonoTouch_UIKit_UIApplication
_BeBabby_AppDelegate_DidEnterBackground_MonoTouch_UIKit_UIApplication:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 32
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,120,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_6:
.text
	.align 2
	.no_dead_strip _BeBabby_AppDelegate_WillEnterForeground_MonoTouch_UIKit_UIApplication
_BeBabby_AppDelegate_WillEnterForeground_MonoTouch_UIKit_UIApplication:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 36
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,120,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_7:
.text
	.align 2
	.no_dead_strip _BeBabby_AppDelegate_WillTerminate_MonoTouch_UIKit_UIApplication
_BeBabby_AppDelegate_WillTerminate_MonoTouch_UIKit_UIApplication:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 40
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,120,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_8:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController__ctor_intptr
_BeBabby_MainViewController__ctor_intptr:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 44
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229
	.byte 12,16,157,229
bl _p_3

	.byte 0,224,157,229,116,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,132,224,158,229,0,0,94,227,0,224,158,21
	.byte 0,224,157,229,148,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226,0,1,189,232,128,128,189,232

Lme_9:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_DidReceiveMemoryWarning
_BeBabby_MainViewController_DidReceiveMemoryWarning:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 48
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 100,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,0,224,157,229,124,224,158,229,0,0,94,227
	.byte 0,224,158,21,16,0,157,229
bl _p_4

	.byte 0,224,157,229,148,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_a:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_ViewDidLoad
_BeBabby_MainViewController_ViewDidLoad:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 52
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 100,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,0,224,157,229,124,224,158,229,0,0,94,227
	.byte 0,224,158,21,16,0,157,229
bl _p_5

	.byte 0,224,157,229,148,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_b:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_btnStartCamera_MonoTouch_UIKit_UIButton
_BeBabby_MainViewController_btnStartCamera_MonoTouch_UIKit_UIButton:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 56
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,120,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_c:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_ViewWillAppear_bool
_BeBabby_MainViewController_ViewWillAppear_bool:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,12,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 60
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,12,0,221,229,20,0,141,229,0,224,157,229
	.byte 136,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,16,157,229
bl _p_6

	.byte 0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_d:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_ViewDidAppear_bool
_BeBabby_MainViewController_ViewDidAppear_bool:

	.byte 128,64,45,233,13,112,160,225,112,1,45,233,64,208,77,226,24,0,141,229,28,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 64
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,96,160,227,0,80,160,227,8,0,141,226
	.byte 0,0,160,227,0,0,160,227,8,0,141,229,0,0,160,227,12,0,141,229,0,0,160,227,16,0,141,229,0,0,160,227
	.byte 20,0,141,229,0,64,160,227,0,224,157,229,116,224,158,229,0,0,94,227,0,224,158,21,4,224,157,229,0,224,158,229
	.byte 0,224,157,229,140,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,156,224,158,229,0,0,94,227,0,224,158,21
	.byte 24,0,157,229,40,0,141,229,28,0,221,229,44,0,141,229,0,224,157,229,188,224,158,229,0,0,94,227,0,224,158,21
	.byte 40,0,157,229,44,16,157,229
bl _p_7

	.byte 0,224,157,229,216,224,158,229,0,0,94,227,0,224,158,21,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 68
	.byte 0,0,159,231
bl _p_8

	.byte 36,0,141,229
bl _p_9

	.byte 36,0,157,229,0,96,160,225,0,224,157,229,12,225,158,229,0,0,94,227,0,224,158,21,6,0,160,225,0,224,157,229
	.byte 32,225,158,229,0,0,94,227,0,224,158,21,6,0,160,225,0,224,214,229
bl _p_10

	.byte 255,0,0,226,32,0,141,229,0,224,157,229,68,225,158,229,0,0,94,227,0,224,158,21,32,0,157,229,0,0,80,227
	.byte 19,0,0,26,0,224,157,229,96,225,158,229,0,0,94,227,0,224,158,21,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 72
	.byte 0,0,159,231,32,0,141,229,0,224,157,229,132,225,158,229,0,0,94,227,0,224,158,21,32,0,157,229
bl _p_11

	.byte 0,224,157,229,156,225,158,229,0,0,94,227,0,224,158,21,110,0,0,234,0,224,157,229,176,225,158,229,0,0,94,227
	.byte 0,224,158,21,0,224,157,229,192,225,158,229,0,0,94,227,0,224,158,21,8,0,141,226,0,0,160,227,0,0,160,227
	.byte 8,0,141,229,0,0,160,227,12,0,141,229,0,0,160,227,16,0,141,229,0,0,160,227,20,0,141,229,0,224,157,229
	.byte 248,225,158,229,0,0,94,227,0,224,158,21,8,0,141,226,60,0,141,229,0,224,157,229,16,226,158,229,0,0,94,227
	.byte 0,224,158,21,60,0,157,229
bl _p_12

	.byte 56,0,141,229,0,224,157,229,44,226,158,229,0,0,94,227,0,224,158,21,56,0,157,229,0,80,160,225,0,224,157,229
	.byte 68,226,158,229,0,0,94,227,0,224,158,21,6,0,160,225,0,224,157,229,88,226,158,229,0,0,94,227,0,224,158,21
	.byte 0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 76
	.byte 0,0,159,231
bl _p_8

	.byte 52,0,141,229
bl _p_13

	.byte 52,0,157,229,0,64,160,225,0,224,157,229,140,226,158,229,0,0,94,227,0,224,158,21,4,0,160,225,48,0,141,229
	.byte 5,0,160,225,0,16,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 80
	.byte 1,16,159,231
bl _p_14

	.byte 44,0,141,229,0,224,157,229,192,226,158,229,0,0,94,227,0,224,158,21,44,16,157,229,48,32,157,229,2,0,160,225
	.byte 0,224,210,229
bl _p_15

	.byte 0,224,157,229,228,226,158,229,0,0,94,227,0,224,158,21,4,0,160,225,40,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 84
	.byte 0,0,159,231,36,0,141,229,0,224,157,229,16,227,158,229,0,0,94,227,0,224,158,21,36,16,157,229,40,32,157,229
	.byte 2,0,160,225,0,224,210,229
bl _p_16

	.byte 4,0,160,225,32,0,141,229,0,224,157,229,60,227,158,229,0,0,94,227,0,224,158,21,32,16,157,229,6,0,160,225
	.byte 0,224,214,229
bl _p_17

	.byte 0,224,157,229,92,227,158,229,0,0,94,227,0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,116,227,158,229
	.byte 0,0,94,227,0,224,158,21,0,224,157,229,132,227,158,229,0,0,94,227,0,224,158,21,64,208,141,226,112,1,189,232
	.byte 128,128,189,232

Lme_e:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_ViewWillDisappear_bool
_BeBabby_MainViewController_ViewWillDisappear_bool:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,12,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 88
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,12,0,221,229,20,0,141,229,0,224,157,229
	.byte 136,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,16,157,229
bl _p_18

	.byte 0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_f:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_ViewDidDisappear_bool
_BeBabby_MainViewController_ViewDidDisappear_bool:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,12,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 92
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,12,0,221,229,20,0,141,229,0,224,157,229
	.byte 136,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,16,157,229
bl _p_19

	.byte 0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_10:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_showInfo_MonoTouch_Foundation_NSObject
_BeBabby_MainViewController_showInfo_MonoTouch_Foundation_NSObject:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 96
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,120,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_11:
.text
	.align 2
	.no_dead_strip _BeBabby_MainViewController_ReleaseDesignerOutlets
_BeBabby_MainViewController_ReleaseDesignerOutlets:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 100
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 100,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,116,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_12:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController__ctor_intptr
_BeBabby_FlipsideViewController__ctor_intptr:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,12,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 104
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229
	.byte 12,16,157,229
bl _p_3

	.byte 0,224,157,229,116,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,132,224,158,229,0,0,94,227,0,224,158,21
	.byte 0,224,157,229,148,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226,0,1,189,232,128,128,189,232

Lme_13:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_add_Done_System_EventHandler
_BeBabby_FlipsideViewController_add_Done_System_EventHandler:

	.byte 128,64,45,233,13,112,160,225,112,13,45,233,24,208,77,226,0,96,160,225,8,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 108
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,80,160,227,0,64,160,227,0,224,157,229
	.byte 72,224,158,229,0,0,94,227,0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,96,224,158,229,0,0,94,227
	.byte 0,224,158,21,6,0,160,225,28,0,150,229,0,80,160,225,4,224,157,229,0,224,158,229,0,224,157,229,132,224,158,229
	.byte 0,0,94,227,0,224,158,21,5,0,160,225,5,64,160,225,0,224,157,229,156,224,158,229,0,0,94,227,0,224,158,21
	.byte 6,0,160,225,0,0,86,227,63,0,0,11,28,176,134,226,5,0,160,225,8,16,157,229,5,0,160,225
bl _p_20

	.byte 0,160,160,225,0,224,157,229,208,224,158,229,0,0,94,227,0,224,158,21,0,0,90,227,9,0,0,10,0,0,154,229
	.byte 0,0,144,229,8,0,144,229,12,0,144,229,0,16,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 112
	.byte 1,16,159,231,1,0,80,225,37,0,0,27,5,0,160,225,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 116
	.byte 0,0,159,231,0,128,160,225,11,0,160,225,10,16,160,225,5,32,160,225
bl _p_21

	.byte 20,0,141,229,0,224,157,229,60,225,158,229,0,0,94,227,0,224,158,21,20,0,157,229,16,0,141,229,0,80,160,225
	.byte 0,224,157,229,88,225,158,229,0,0,94,227,0,224,158,21,16,0,157,229,0,16,160,225,4,16,160,225,4,0,80,225
	.byte 191,255,255,26,0,224,157,229,124,225,158,229,0,0,94,227,0,224,158,21,0,224,157,229,140,225,158,229,0,0,94,227
	.byte 0,224,158,21,24,208,141,226,112,13,189,232,128,128,189,232,14,16,160,225,0,0,159,229
bl _p_22

	.byte 221,1,0,2,14,16,160,225,0,0,159,229
bl _p_22

	.byte 245,1,0,2

Lme_14:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_remove_Done_System_EventHandler
_BeBabby_FlipsideViewController_remove_Done_System_EventHandler:

	.byte 128,64,45,233,13,112,160,225,112,13,45,233,24,208,77,226,0,96,160,225,8,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 120
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,80,160,227,0,64,160,227,0,224,157,229
	.byte 72,224,158,229,0,0,94,227,0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,96,224,158,229,0,0,94,227
	.byte 0,224,158,21,6,0,160,225,28,0,150,229,0,80,160,225,4,224,157,229,0,224,158,229,0,224,157,229,132,224,158,229
	.byte 0,0,94,227,0,224,158,21,5,0,160,225,5,64,160,225,0,224,157,229,156,224,158,229,0,0,94,227,0,224,158,21
	.byte 6,0,160,225,0,0,86,227,63,0,0,11,28,176,134,226,5,0,160,225,8,16,157,229,5,0,160,225
bl _p_23

	.byte 0,160,160,225,0,224,157,229,208,224,158,229,0,0,94,227,0,224,158,21,0,0,90,227,9,0,0,10,0,0,154,229
	.byte 0,0,144,229,8,0,144,229,12,0,144,229,0,16,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 112
	.byte 1,16,159,231,1,0,80,225,37,0,0,27,5,0,160,225,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 116
	.byte 0,0,159,231,0,128,160,225,11,0,160,225,10,16,160,225,5,32,160,225
bl _p_21

	.byte 20,0,141,229,0,224,157,229,60,225,158,229,0,0,94,227,0,224,158,21,20,0,157,229,16,0,141,229,0,80,160,225
	.byte 0,224,157,229,88,225,158,229,0,0,94,227,0,224,158,21,16,0,157,229,0,16,160,225,4,16,160,225,4,0,80,225
	.byte 191,255,255,26,0,224,157,229,124,225,158,229,0,0,94,227,0,224,158,21,0,224,157,229,140,225,158,229,0,0,94,227
	.byte 0,224,158,21,24,208,141,226,112,13,189,232,128,128,189,232,14,16,160,225,0,0,159,229
bl _p_22

	.byte 221,1,0,2,14,16,160,225,0,0,159,229
bl _p_22

	.byte 245,1,0,2

Lme_15:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_DidReceiveMemoryWarning
_BeBabby_FlipsideViewController_DidReceiveMemoryWarning:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 124
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 100,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,0,224,157,229,124,224,158,229,0,0,94,227
	.byte 0,224,158,21,16,0,157,229
bl _p_4

	.byte 0,224,157,229,148,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_16:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_ViewDidLoad
_BeBabby_FlipsideViewController_ViewDidLoad:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 128
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 100,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,0,224,157,229,124,224,158,229,0,0,94,227
	.byte 0,224,158,21,16,0,157,229
bl _p_5

	.byte 0,224,157,229,148,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_17:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_ViewWillAppear_bool
_BeBabby_FlipsideViewController_ViewWillAppear_bool:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,12,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 132
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,12,0,221,229,20,0,141,229,0,224,157,229
	.byte 136,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,16,157,229
bl _p_6

	.byte 0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_18:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_ViewDidAppear_bool
_BeBabby_FlipsideViewController_ViewDidAppear_bool:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,12,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 136
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,12,0,221,229,20,0,141,229,0,224,157,229
	.byte 136,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,16,157,229
bl _p_7

	.byte 0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_19:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_ViewWillDisappear_bool
_BeBabby_FlipsideViewController_ViewWillDisappear_bool:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,12,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 140
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,12,0,221,229,20,0,141,229,0,224,157,229
	.byte 136,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,16,157,229
bl _p_18

	.byte 0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_1a:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_ViewDidDisappear_bool
_BeBabby_FlipsideViewController_ViewDidDisappear_bool:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,28,208,77,226,8,0,141,229,12,16,205,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 144
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,8,0,157,229,16,0,141,229,12,0,221,229,20,0,141,229,0,224,157,229
	.byte 136,224,158,229,0,0,94,227,0,224,158,21,16,0,157,229,20,16,157,229
bl _p_19

	.byte 0,224,157,229,164,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,180,224,158,229,0,0,94,227,0,224,158,21
	.byte 28,208,141,226,0,1,189,232,128,128,189,232

Lme_1b:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_done_MonoTouch_UIKit_UIBarButtonItem
_BeBabby_FlipsideViewController_done_MonoTouch_UIKit_UIBarButtonItem:

	.byte 128,64,45,233,13,112,160,225,0,5,45,233,32,208,77,226,0,160,160,225,8,16,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 148
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,64,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,88,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 104,224,158,229,0,0,94,227,0,224,158,21,10,0,160,225,1,0,160,227,0,224,157,229,128,224,158,229,0,0,94,227
	.byte 0,224,158,21,10,0,160,225,1,16,160,227,0,32,154,229,15,224,160,225,100,240,146,229,0,224,157,229,164,224,158,229
	.byte 0,0,94,227,0,224,158,21,10,0,160,225,28,0,154,229,0,0,80,227,25,0,0,10,0,224,157,229,196,224,158,229
	.byte 0,0,94,227,0,224,158,21,10,0,160,225,28,0,154,229,24,0,141,229,10,0,160,225,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 152
	.byte 0,0,159,231,0,0,144,229,20,0,141,229,0,224,157,229,252,224,158,229,0,0,94,227,0,224,158,21,20,32,157,229
	.byte 24,48,157,229,3,0,160,225,10,16,160,225,16,48,141,229,15,224,160,225,12,240,147,229,16,0,157,229,4,224,157,229
	.byte 0,224,158,229,0,224,157,229,52,225,158,229,0,0,94,227,0,224,158,21,0,224,157,229,68,225,158,229,0,0,94,227
	.byte 0,224,158,21,32,208,141,226,0,5,189,232,128,128,189,232

Lme_1c:
.text
	.align 2
	.no_dead_strip _BeBabby_FlipsideViewController_ReleaseDesignerOutlets
_BeBabby_FlipsideViewController_ReleaseDesignerOutlets:

	.byte 128,64,45,233,13,112,160,225,0,1,45,233,20,208,77,226,8,0,141,229,0,0,159,229,0,0,0,234
	.long _mono_aot_BeBabby_got - . + 156
	.byte 0,0,159,231,0,0,141,229,0,224,157,229,0,224,158,229,4,224,141,229,0,224,157,229,60,224,158,229,0,0,94,227
	.byte 0,224,158,21,4,224,157,229,0,224,158,229,0,224,157,229,84,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229
	.byte 100,224,158,229,0,0,94,227,0,224,158,21,0,224,157,229,116,224,158,229,0,0,94,227,0,224,158,21,20,208,141,226
	.byte 0,1,189,232,128,128,189,232

Lme_1d:
.text
	.align 3
methods_end:

	.long 0
.text
	.align 3
method_addresses:
	.no_dead_strip method_addresses
	bl _BeBabby_Application__ctor
	bl _BeBabby_Application_Main_string__
	bl _BeBabby_AppDelegate_get_Window
	bl _BeBabby_AppDelegate_set_Window_MonoTouch_UIKit_UIWindow
	bl _BeBabby_AppDelegate__ctor
	bl _BeBabby_AppDelegate_OnResignActivation_MonoTouch_UIKit_UIApplication
	bl _BeBabby_AppDelegate_DidEnterBackground_MonoTouch_UIKit_UIApplication
	bl _BeBabby_AppDelegate_WillEnterForeground_MonoTouch_UIKit_UIApplication
	bl _BeBabby_AppDelegate_WillTerminate_MonoTouch_UIKit_UIApplication
	bl _BeBabby_MainViewController__ctor_intptr
	bl _BeBabby_MainViewController_DidReceiveMemoryWarning
	bl _BeBabby_MainViewController_ViewDidLoad
	bl _BeBabby_MainViewController_btnStartCamera_MonoTouch_UIKit_UIButton
	bl _BeBabby_MainViewController_ViewWillAppear_bool
	bl _BeBabby_MainViewController_ViewDidAppear_bool
	bl _BeBabby_MainViewController_ViewWillDisappear_bool
	bl _BeBabby_MainViewController_ViewDidDisappear_bool
	bl _BeBabby_MainViewController_showInfo_MonoTouch_Foundation_NSObject
	bl _BeBabby_MainViewController_ReleaseDesignerOutlets
	bl _BeBabby_FlipsideViewController__ctor_intptr
	bl _BeBabby_FlipsideViewController_add_Done_System_EventHandler
	bl _BeBabby_FlipsideViewController_remove_Done_System_EventHandler
	bl _BeBabby_FlipsideViewController_DidReceiveMemoryWarning
	bl _BeBabby_FlipsideViewController_ViewDidLoad
	bl _BeBabby_FlipsideViewController_ViewWillAppear_bool
	bl _BeBabby_FlipsideViewController_ViewDidAppear_bool
	bl _BeBabby_FlipsideViewController_ViewWillDisappear_bool
	bl _BeBabby_FlipsideViewController_ViewDidDisappear_bool
	bl _BeBabby_FlipsideViewController_done_MonoTouch_UIKit_UIBarButtonItem
	bl _BeBabby_FlipsideViewController_ReleaseDesignerOutlets
	bl method_addresses
method_addresses_end:
.section __TEXT, __const
	.align 3
code_offsets:

	.long 0

.text
	.align 3
unbox_trampolines:
unbox_trampolines_end:

	.long 0
.section __TEXT, __const
	.align 3
method_info_offsets:

	.long 31,10,4,2
	.short 0, 10, 20, 30
	.byte 1,3,4,3,3,3,3,3,3,3,32,3,3,3,3,8,3,3,3,3,67,5,5,3,3,3,3,3,3,4,0
.section __TEXT, __const
	.align 3
extra_method_table:

	.long 11,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0
.section __TEXT, __const
	.align 3
extra_method_info_offsets:

	.long 0
.section __TEXT, __const
	.align 3
class_name_table:

	.short 11, 1, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 4, 0, 0
	.short 0, 2, 0, 3, 0, 5, 0
.section __TEXT, __const
	.align 3
got_info_offsets:

	.long 43,10,5,2
	.short 0, 10, 20, 30, 41
	.byte 102,2,1,1,1,1,1,3,1,1,115,1,1,1,1,1,1,1,1,1,125,4,3,4,3,3,1,1,1,1,128,147
	.byte 1,5,12,1,1,1,1,1,1,128,172,1,7
.section __TEXT, __const
	.align 3
ex_info_offsets:

	.long 31,10,4,2
	.short 0, 11, 23, 36
	.byte 129,95,46,68,62,53,48,51,51,51,51,131,124,62,62,51,67,129,49,67,67,51,45,134,193,128,153,128,153,62,62,67
	.byte 67,67,67,121,0
.section __TEXT, __const
	.align 3
unwind_info:

	.byte 18,12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32,18,12,13,0,72,14,8,135,2,68,14,12,136
	.byte 3,142,1,68,14,40,20,12,13,0,72,14,8,135,2,68,14,16,134,4,136,3,142,1,68,14,32,24,12,13,0,72
	.byte 14,8,135,2,68,14,24,132,6,133,5,134,4,136,3,142,1,68,14,88,28,12,13,0,72,14,8,135,2,68,14,32
	.byte 132,8,133,7,134,6,136,5,138,4,139,3,142,1,68,14,56,20,12,13,0,72,14,8,135,2,68,14,16,136,4,138
	.byte 3,142,1,68,14,48
.section __TEXT, __const
	.align 3
class_info_offsets:

	.long 5,10,1,2
	.short 0
	.byte 138,33,7,23,61,101

.text
	.align 4
plt:
_mono_aot_BeBabby_plt:
	.no_dead_strip plt_MonoTouch_UIKit_UIApplication_Main_string___string_string
plt_MonoTouch_UIKit_UIApplication_Main_string___string_string:
_p_1:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 172,181
	.no_dead_strip plt_MonoTouch_UIKit_UIApplicationDelegate__ctor
plt_MonoTouch_UIKit_UIApplicationDelegate__ctor:
_p_2:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 176,186
	.no_dead_strip plt_MonoTouch_UIKit_UIViewController__ctor_intptr
plt_MonoTouch_UIKit_UIViewController__ctor_intptr:
_p_3:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 180,191
	.no_dead_strip plt_MonoTouch_UIKit_UIViewController_DidReceiveMemoryWarning
plt_MonoTouch_UIKit_UIViewController_DidReceiveMemoryWarning:
_p_4:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 184,196
	.no_dead_strip plt_MonoTouch_UIKit_UIViewController_ViewDidLoad
plt_MonoTouch_UIKit_UIViewController_ViewDidLoad:
_p_5:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 188,201
	.no_dead_strip plt_MonoTouch_UIKit_UIViewController_ViewWillAppear_bool
plt_MonoTouch_UIKit_UIViewController_ViewWillAppear_bool:
_p_6:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 192,206
	.no_dead_strip plt_MonoTouch_UIKit_UIViewController_ViewDidAppear_bool
plt_MonoTouch_UIKit_UIViewController_ViewDidAppear_bool:
_p_7:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 196,211
	.no_dead_strip plt__jit_icall_mono_object_new_fast
plt__jit_icall_mono_object_new_fast:
_p_8:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 200,216
	.no_dead_strip plt_Xamarin_Media_MediaPicker__ctor
plt_Xamarin_Media_MediaPicker__ctor:
_p_9:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 204,239
	.no_dead_strip plt_Xamarin_Media_MediaPicker_get_IsCameraAvailable
plt_Xamarin_Media_MediaPicker_get_IsCameraAvailable:
_p_10:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 208,244
	.no_dead_strip plt_System_Console_WriteLine_string
plt_System_Console_WriteLine_string:
_p_11:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 212,249
	.no_dead_strip plt_System_Guid_ToString
plt_System_Guid_ToString:
_p_12:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 216,254
	.no_dead_strip plt_Xamarin_Media_StoreCameraMediaOptions__ctor
plt_Xamarin_Media_StoreCameraMediaOptions__ctor:
_p_13:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 220,259
	.no_dead_strip plt_string_Concat_string_string
plt_string_Concat_string_string:
_p_14:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 224,264
	.no_dead_strip plt_Xamarin_Media_StoreMediaOptions_set_Name_string
plt_Xamarin_Media_StoreMediaOptions_set_Name_string:
_p_15:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 228,269
	.no_dead_strip plt_Xamarin_Media_StoreMediaOptions_set_Directory_string
plt_Xamarin_Media_StoreMediaOptions_set_Directory_string:
_p_16:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 232,274
	.no_dead_strip plt_Xamarin_Media_MediaPicker_TakePhotoAsync_Xamarin_Media_StoreCameraMediaOptions
plt_Xamarin_Media_MediaPicker_TakePhotoAsync_Xamarin_Media_StoreCameraMediaOptions:
_p_17:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 236,279
	.no_dead_strip plt_MonoTouch_UIKit_UIViewController_ViewWillDisappear_bool
plt_MonoTouch_UIKit_UIViewController_ViewWillDisappear_bool:
_p_18:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 240,284
	.no_dead_strip plt_MonoTouch_UIKit_UIViewController_ViewDidDisappear_bool
plt_MonoTouch_UIKit_UIViewController_ViewDidDisappear_bool:
_p_19:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 244,289
	.no_dead_strip plt_System_Delegate_Combine_System_Delegate_System_Delegate
plt_System_Delegate_Combine_System_Delegate_System_Delegate:
_p_20:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 248,294
	.no_dead_strip plt_System_Threading_Interlocked_CompareExchange_System_EventHandler_System_EventHandler__System_EventHandler_System_EventHandler
plt_System_Threading_Interlocked_CompareExchange_System_EventHandler_System_EventHandler__System_EventHandler_System_EventHandler:
_p_21:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 252,299
	.no_dead_strip plt__jit_icall_mono_arch_throw_corlib_exception
plt__jit_icall_mono_arch_throw_corlib_exception:
_p_22:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 256,311
	.no_dead_strip plt_System_Delegate_Remove_System_Delegate_System_Delegate
plt_System_Delegate_Remove_System_Delegate_System_Delegate:
_p_23:

	.byte 0,192,159,229,12,240,159,231
	.long _mono_aot_BeBabby_got - . + 260,346
plt_end:
.section __TEXT, __const
	.align 3
image_table:

	.long 4
	.asciz "BeBabby"
	.asciz "0097A5B2-AE59-413A-969D-11BBD3EE8C8E"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,0,0,0,0
	.asciz "Xamarin.Mobile"
	.asciz "FFD342CE-5997-46A8-BE92-80BB617DA404"
	.asciz ""
	.asciz ""
	.align 3

	.long 0,0,7,1,0
	.asciz "mscorlib"
	.asciz "17F16E72-A3DC-42CF-A044-666CF723BEA0"
	.asciz ""
	.asciz "7cec85d7bea7798e"
	.align 3

	.long 1,2,0,5,0
	.asciz "monotouch"
	.asciz "8CAA0B37-C277-4F55-B309-A1D0848FCA15"
	.asciz ""
	.asciz "84e04ff9cfb79065"
	.align 3

	.long 1,0,0,0,0
.data
	.align 3
_mono_aot_BeBabby_got:
	.space 268
got_end:
.section __TEXT, __const
	.align 2
assembly_guid:
	.asciz "0097A5B2-AE59-413A-969D-11BBD3EE8C8E"
.section __TEXT, __const
	.align 2
runtime_version:
	.asciz ""
.section __TEXT, __const
	.align 2
assembly_name:
	.asciz "BeBabby"
.data
	.align 3
_mono_aot_file_info:

	.long 97,0
	.align 2
	.long _mono_aot_BeBabby_got
	.align 2
	.long methods
	.align 2
	.long 0
	.align 2
	.long blob
	.align 2
	.long class_name_table
	.align 2
	.long class_info_offsets
	.align 2
	.long method_info_offsets
	.align 2
	.long ex_info_offsets
	.align 2
	.long code_offsets
	.align 2
	.long method_addresses
	.align 2
	.long extra_method_info_offsets
	.align 2
	.long extra_method_table
	.align 2
	.long got_info_offsets
	.align 2
	.long methods_end
	.align 2
	.long unwind_info
	.align 2
	.long mem_end
	.align 2
	.long image_table
	.align 2
	.long plt
	.align 2
	.long plt_end
	.align 2
	.long assembly_guid
	.align 2
	.long runtime_version
	.align 2
	.long 0
	.align 2
	.long 0
	.align 2
	.long 0
	.align 2
	.long 0
	.align 2
	.long 0
	.align 2
	.long globals
	.align 2
	.long assembly_name
	.align 2
	.long unbox_trampolines
	.align 2
	.long unbox_trampolines_end

	.long 43,268,24,31,14,387000831,0,2886
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,128,4,4,14
	.long 0,0,0,0,0
	.globl _mono_aot_module_BeBabby_info
	.align 2
_mono_aot_module_BeBabby_info:
	.align 2
	.long _mono_aot_file_info
.section __TEXT, __const
	.align 3
blob:

	.byte 0,0,1,4,0,2,5,6,0,1,7,0,1,8,0,1,9,0,1,10,0,1,11,0,1,12,0,1,13,0,1,14
	.byte 0,1,15,0,1,16,0,1,17,0,1,18,0,6,19,20,21,22,23,24,0,1,25,0,1,26,0,1,27,0,1,28
	.byte 0,1,29,0,3,30,31,32,0,3,33,31,32,0,1,34,0,1,35,0,1,36,0,1,37,0,1,38,0,1,39,0
	.byte 2,40,41,0,1,42,12,0,39,42,47,40,40,17,0,1,40,40,40,40,40,40,40,40,40,40,40,40,40,14,2,37
	.byte 1,17,0,25,14,2,34,1,17,0,45,17,0,55,40,40,40,40,40,40,11,2,129,191,2,34,255,254,0,0,0,0
	.byte 255,43,0,0,1,40,40,40,40,40,40,40,40,16,2,129,189,2,134,142,40,3,195,0,3,27,3,195,0,3,166,3
	.byte 195,0,3,128,3,195,0,3,140,3,195,0,3,135,3,195,0,3,136,3,195,0,3,137,7,20,109,111,110,111,95,111
	.byte 98,106,101,99,116,95,110,101,119,95,102,97,115,116,0,3,193,0,0,231,3,193,0,0,232,3,194,0,8,45,3,194
	.byte 0,10,155,3,193,0,0,225,3,194,0,12,186,3,193,0,0,222,3,193,0,0,220,3,193,0,0,241,3,195,0,3
	.byte 138,3,195,0,3,139,3,194,0,9,238,3,255,254,0,0,0,0,255,43,0,0,1,7,32,109,111,110,111,95,97,114
	.byte 99,104,95,116,104,114,111,119,95,99,111,114,108,105,98,95,101,120,99,101,112,116,105,111,110,0,3,194,0,9,240,10
	.byte 0,4,255,255,255,255,255,52,0,0,1,24,0,1,2,6,20,0,0,192,255,255,249,16,0,0,18,128,128,68,128,140
	.byte 208,0,0,13,8,0,3,0,68,6,28,1,32,10,19,6,255,255,255,255,255,52,0,0,1,24,0,1,2,1,16,0
	.byte 1,3,7,48,1,1,4,5,32,0,0,192,255,255,242,16,0,0,30,128,204,68,128,216,208,0,0,13,8,0,9,0
	.byte 68,1,24,1,24,1,4,5,20,0,24,0,4,5,4,1,32,10,38,5,255,255,255,255,255,56,0,0,1,24,0,1
	.byte 2,7,28,0,1,3,5,16,0,0,192,255,255,243,24,0,0,29,128,164,72,128,176,208,0,0,13,8,6,0,8,0
	.byte 72,1,28,5,4,1,4,5,16,0,16,1,4,1,20,10,0,4,255,255,255,255,255,56,0,0,1,24,0,1,2,7
	.byte 28,0,0,192,255,255,248,16,0,0,25,128,140,72,128,152,208,0,0,13,12,208,0,0,13,8,0,4,0,72,2,32
	.byte 5,4,1,32,10,0,4,255,255,255,255,255,52,0,0,1,24,0,1,2,6,24,0,0,192,255,255,249,16,0,0,20
	.byte 128,132,68,128,144,208,0,0,13,8,0,4,0,68,1,28,5,4,1,32,10,0,4,255,255,255,255,255,56,0,0,1
	.byte 24,0,1,2,1,16,0,0,192,255,255,254,16,0,0,23,128,128,72,128,140,208,0,0,13,12,208,0,0,13,8,0
	.byte 3,0,72,1,24,1,32,10,0,4,255,255,255,255,255,56,0,0,1,24,0,1,2,1,16,0,0,192,255,255,254,16
	.byte 0,0,23,128,128,72,128,140,208,0,0,13,12,208,0,0,13,8,0,3,0,72,1,24,1,32,10,0,4,255,255,255
	.byte 255,255,56,0,0,1,24,0,1,2,1,16,0,0,192,255,255,254,16,0,0,23,128,128,72,128,140,208,0,0,13,12
	.byte 208,0,0,13,8,0,3,0,72,1,24,1,32,10,0,4,255,255,255,255,255,56,0,0,1,24,0,1,2,1,16,0
	.byte 0,192,255,255,254,16,0,0,23,128,128,72,128,140,208,0,0,13,12,208,0,0,13,8,0,3,0,72,1,24,1,32
	.byte 10,0,5,255,255,255,255,255,56,0,0,1,24,0,1,2,7,28,0,1,3,1,16,0,0,192,255,255,247,16,0,0
	.byte 27,128,156,72,128,168,208,0,0,13,12,208,0,0,13,8,0,5,0,72,2,32,5,4,1,16,1,32,10,19,6,255
	.byte 255,255,255,255,52,0,0,1,24,0,1,2,1,16,0,1,3,1,24,1,1,4,5,24,0,0,192,255,255,248,16,0
	.byte 0,24,128,172,68,128,184,208,0,0,13,8,0,6,0,68,1,24,1,24,0,20,5,4,1,32,10,19,6,255,255,255
	.byte 255,255,52,0,0,1,24,0,1,2,1,16,0,1,3,1,24,1,1,4,5,24,0,0,192,255,255,248,16,0,0,24
	.byte 128,172,68,128,184,208,0,0,13,8,0,6,0,68,1,24,1,24,0,20,5,4,1,32,10,0,4,255,255,255,255,255
	.byte 56,0,0,1,24,0,1,2,1,16,0,0,192,255,255,254,16,0,0,23,128,128,72,128,140,208,0,0,13,12,208,0
	.byte 0,13,8,0,3,0,72,1,24,1,32,10,19,6,255,255,255,255,255,56,0,0,1,24,0,1,2,1,16,0,1,3
	.byte 2,32,1,1,4,5,28,0,0,192,255,255,247,16,0,0,29,128,188,72,128,200,208,0,0,13,12,208,0,0,13,8
	.byte 0,6,0,72,1,24,2,32,0,24,5,4,1,32,10,59,26,255,255,255,255,255,108,0,0,1,24,0,1,2,1,16
	.byte 0,1,3,2,32,1,1,4,5,28,0,1,5,6,52,0,1,6,1,20,1,1,7,5,36,1,2,8,11,5,28,0
	.byte 1,9,5,36,1,1,10,5,24,0,1,24,5,20,0,1,12,1,16,0,1,13,8,56,0,1,14,8,24,1,1,15
	.byte 5,28,1,1,16,1,24,0,1,17,1,20,1,1,18,6,52,1,1,19,12,52,1,1,20,5,36,1,1,21,6,44
	.byte 1,1,22,6,44,1,1,23,6,32,0,1,24,1,24,0,0,192,255,255,149,16,0,0,128,165,131,140,124,131,152,208
	.byte 0,0,13,28,208,0,0,13,24,6,5,208,0,0,13,8,4,0,70,0,124,1,24,2,32,0,24,5,4,0,16,0
	.byte 16,0,4,0,4,5,8,1,4,0,16,1,4,0,16,0,4,0,4,0,0,0,4,0,8,5,20,0,4,5,4,0
	.byte 16,5,20,0,20,5,4,0,16,5,4,1,16,8,56,0,16,8,8,0,20,0,8,5,20,1,4,0,16,1,4,0
	.byte 16,0,16,0,4,0,4,5,8,1,4,0,16,1,8,1,4,5,16,5,8,0,24,0,4,0,4,0,0,5,4,0
	.byte 16,1,8,5,20,0,24,0,4,0,4,0,0,5,4,1,8,0,20,0,4,0,4,0,0,6,4,1,16,1,40,10
	.byte 19,6,255,255,255,255,255,56,0,0,1,24,0,1,2,1,16,0,1,3,2,32,1,1,4,5,28,0,0,192,255,255
	.byte 247,16,0,0,29,128,188,72,128,200,208,0,0,13,12,208,0,0,13,8,0,6,0,72,1,24,2,32,0,24,5,4
	.byte 1,32,10,19,6,255,255,255,255,255,56,0,0,1,24,0,1,2,1,16,0,1,3,2,32,1,1,4,5,28,0,0
	.byte 192,255,255,247,16,0,0,29,128,188,72,128,200,208,0,0,13,12,208,0,0,13,8,0,6,0,72,1,24,2,32,0
	.byte 24,5,4,1,32,10,0,4,255,255,255,255,255,56,0,0,1,24,0,1,2,1,16,0,0,192,255,255,254,16,0,0
	.byte 23,128,128,72,128,140,208,0,0,13,12,208,0,0,13,8,0,3,0,72,1,24,1,32,10,0,4,255,255,255,255,255
	.byte 52,0,0,1,24,0,1,2,1,16,0,0,192,255,255,254,16,0,0,17,124,68,128,136,208,0,0,13,8,0,3,0
	.byte 68,1,24,1,32,10,0,5,255,255,255,255,255,56,0,0,1,24,0,1,2,7,28,0,1,3,1,16,0,0,192,255
	.byte 255,247,16,0,0,27,128,156,72,128,168,208,0,0,13,12,208,0,0,13,8,0,5,0,72,2,32,5,4,1,16,1
	.byte 32,10,84,9,255,255,255,255,255,64,0,0,1,24,0,1,2,7,36,0,1,3,2,24,0,1,4,13,52,1,1,5
	.byte 11,108,1,1,6,1,28,0,2,2,7,7,36,0,0,192,255,255,214,16,0,0,99,129,148,80,129,192,208,0,0,13
	.byte 8,6,5,4,0,42,0,80,0,24,1,4,5,4,1,4,0,24,1,4,1,4,0,16,1,4,0,4,0,4,5,4
	.byte 2,8,0,4,0,4,0,4,5,16,0,4,0,4,0,4,0,4,0,4,0,4,0,16,0,4,5,4,1,4,0,16
	.byte 0,4,0,4,0,4,0,4,0,8,5,24,1,4,0,20,1,4,1,4,0,4,5,4,1,32,10,84,9,255,255,255
	.byte 255,255,64,0,0,1,24,0,1,2,7,36,0,1,3,2,24,0,1,4,13,52,1,1,5,11,108,1,1,6,1,28
	.byte 0,2,2,7,7,36,0,0,192,255,255,214,16,0,0,99,129,148,80,129,192,208,0,0,13,8,6,5,4,0,42,0
	.byte 80,0,24,1,4,5,4,1,4,0,24,1,4,1,4,0,16,1,4,0,4,0,4,5,4,2,8,0,4,0,4,0
	.byte 4,5,16,0,4,0,4,0,4,0,4,0,4,0,4,0,16,0,4,5,4,1,4,0,16,0,4,0,4,0,4,0
	.byte 4,0,8,5,24,1,4,0,20,1,4,1,4,0,4,5,4,1,32,10,19,6,255,255,255,255,255,52,0,0,1,24
	.byte 0,1,2,1,16,0,1,3,1,24,1,1,4,5,24,0,0,192,255,255,248,16,0,0,24,128,172,68,128,184,208,0
	.byte 0,13,8,0,6,0,68,1,24,1,24,0,20,5,4,1,32,10,19,6,255,255,255,255,255,52,0,0,1,24,0,1
	.byte 2,1,16,0,1,3,1,24,1,1,4,5,24,0,0,192,255,255,248,16,0,0,24,128,172,68,128,184,208,0,0,13
	.byte 8,0,6,0,68,1,24,1,24,0,20,5,4,1,32,10,19,6,255,255,255,255,255,56,0,0,1,24,0,1,2,1
	.byte 16,0,1,3,2,32,1,1,4,5,28,0,0,192,255,255,247,16,0,0,29,128,188,72,128,200,208,0,0,13,12,208
	.byte 0,0,13,8,0,6,0,72,1,24,2,32,0,24,5,4,1,32,10,19,6,255,255,255,255,255,56,0,0,1,24,0
	.byte 1,2,1,16,0,1,3,2,32,1,1,4,5,28,0,0,192,255,255,247,16,0,0,29,128,188,72,128,200,208,0,0
	.byte 13,12,208,0,0,13,8,0,6,0,72,1,24,2,32,0,24,5,4,1,32,10,19,6,255,255,255,255,255,56,0,0
	.byte 1,24,0,1,2,1,16,0,1,3,2,32,1,1,4,5,28,0,0,192,255,255,247,16,0,0,29,128,188,72,128,200
	.byte 208,0,0,13,12,208,0,0,13,8,0,6,0,72,1,24,2,32,0,24,5,4,1,32,10,19,6,255,255,255,255,255
	.byte 56,0,0,1,24,0,1,2,1,16,0,1,3,2,32,1,1,4,5,28,0,0,192,255,255,247,16,0,0,29,128,188
	.byte 72,128,200,208,0,0,13,12,208,0,0,13,8,0,6,0,72,1,24,2,32,0,24,5,4,1,32,10,113,9,255,255
	.byte 255,255,255,56,0,0,1,24,0,1,2,1,16,0,1,3,2,24,1,1,4,5,36,0,2,5,7,11,32,0,1,6
	.byte 12,56,1,1,7,5,56,0,0,192,255,255,219,16,0,0,67,129,76,72,129,88,208,0,0,13,8,10,0,27,0,72
	.byte 1,24,0,16,1,4,1,4,0,16,0,4,0,4,0,4,5,8,0,16,1,4,5,4,0,4,5,4,0,16,1,4
	.byte 5,8,1,4,0,16,5,8,0,24,0,4,0,8,0,12,5,0,1,40,10,0,4,255,255,255,255,255,52,0,0,1
	.byte 24,0,1,2,1,16,0,0,192,255,255,254,16,0,0,17,124,68,128,136,208,0,0,13,8,0,3,0,68,1,24,1
	.byte 32,0,128,144,8,0,0,1,4,128,144,8,0,0,1,194,0,12,26,194,0,12,23,194,0,12,22,194,0,12,20,17
	.byte 128,162,195,0,1,234,24,0,0,4,195,0,1,243,194,0,12,23,195,0,1,234,194,0,12,20,195,0,1,230,195,0
	.byte 1,235,195,0,1,245,195,0,1,239,195,0,1,238,195,0,1,233,195,0,1,232,8,7,9,6,4,3,27,128,162,195
	.byte 0,1,234,28,0,0,4,195,0,1,243,194,0,12,23,195,0,1,234,194,0,12,20,195,0,1,230,195,0,1,235,195
	.byte 0,3,147,195,0,1,239,195,0,1,238,195,0,1,233,195,0,3,126,195,0,3,134,195,0,3,146,195,0,3,145,195
	.byte 0,3,144,195,0,3,143,195,0,3,142,195,0,3,141,11,17,16,15,14,12,195,0,3,131,195,0,3,130,195,0,3
	.byte 129,27,128,162,195,0,1,234,32,0,0,4,195,0,1,243,194,0,12,23,195,0,1,234,194,0,12,20,195,0,1,230
	.byte 195,0,1,235,195,0,3,147,195,0,1,239,195,0,1,238,195,0,1,233,195,0,3,126,195,0,3,134,195,0,3,146
	.byte 195,0,3,145,195,0,3,144,195,0,3,143,195,0,3,142,195,0,3,141,23,28,27,26,25,24,195,0,3,131,195,0
	.byte 3,130,195,0,3,129,98,111,101,104,109,0
.section __TEXT, __const
	.align 3
Lglobals_hash:

	.short 11, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0
.data
	.align 3
globals:
	.align 2
	.long Lglobals_hash

	.long 0,0
.section __DWARF, __debug_info,regular,debug
LTDIE_1:

	.byte 17
	.asciz "System_Object"

	.byte 8,7
	.asciz "System_Object"

LDIFF_SYM3=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM3
LTDIE_1_POINTER:

	.byte 13
LDIFF_SYM4=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM4
LTDIE_1_REFERENCE:

	.byte 14
LDIFF_SYM5=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM5
LTDIE_0:

	.byte 5
	.asciz "BeBabby_Application"

	.byte 8,16
LDIFF_SYM6=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM6
	.byte 2,35,0,0,7
	.asciz "BeBabby_Application"

LDIFF_SYM7=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM7
LTDIE_0_POINTER:

	.byte 13
LDIFF_SYM8=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM8
LTDIE_0_REFERENCE:

	.byte 14
LDIFF_SYM9=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM9
	.byte 2
	.asciz "BeBabby.Application:.ctor"
	.long _BeBabby_Application__ctor
	.long Lme_0

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM10=LTDIE_0_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM10
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM11=Lfde0_end - Lfde0_start
	.long LDIFF_SYM11
Lfde0_start:

	.long 0
	.align 2
	.long _BeBabby_Application__ctor

LDIFF_SYM12=Lme_0 - _BeBabby_Application__ctor
	.long LDIFF_SYM12
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde0_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.Application:Main"
	.long _BeBabby_Application_Main_string__
	.long Lme_1

	.byte 2,118,16,3
	.asciz "args"

LDIFF_SYM13=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM13
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM14=Lfde1_end - Lfde1_start
	.long LDIFF_SYM14
Lfde1_start:

	.long 0
	.align 2
	.long _BeBabby_Application_Main_string__

LDIFF_SYM15=Lme_1 - _BeBabby_Application_Main_string__
	.long LDIFF_SYM15
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde1_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_6:

	.byte 5
	.asciz "System_ValueType"

	.byte 8,16
LDIFF_SYM16=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM16
	.byte 2,35,0,0,7
	.asciz "System_ValueType"

LDIFF_SYM17=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM17
LTDIE_6_POINTER:

	.byte 13
LDIFF_SYM18=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM18
LTDIE_6_REFERENCE:

	.byte 14
LDIFF_SYM19=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM19
LTDIE_5:

	.byte 5
	.asciz "System_Boolean"

	.byte 9,16
LDIFF_SYM20=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM20
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM21=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM21
	.byte 2,35,8,0,7
	.asciz "System_Boolean"

LDIFF_SYM22=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM22
LTDIE_5_POINTER:

	.byte 13
LDIFF_SYM23=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM23
LTDIE_5_REFERENCE:

	.byte 14
LDIFF_SYM24=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM24
LTDIE_4:

	.byte 5
	.asciz "MonoTouch_Foundation_NSObject"

	.byte 20,16
LDIFF_SYM25=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM25
	.byte 2,35,0,6
	.asciz "handle"

LDIFF_SYM26=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM26
	.byte 2,35,8,6
	.asciz "super"

LDIFF_SYM27=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM27
	.byte 2,35,12,6
	.asciz "disposed"

LDIFF_SYM28=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM28
	.byte 2,35,16,6
	.asciz "IsDirectBinding"

LDIFF_SYM29=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM29
	.byte 2,35,17,0,7
	.asciz "MonoTouch_Foundation_NSObject"

LDIFF_SYM30=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM30
LTDIE_4_POINTER:

	.byte 13
LDIFF_SYM31=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM31
LTDIE_4_REFERENCE:

	.byte 14
LDIFF_SYM32=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM32
LTDIE_3:

	.byte 5
	.asciz "MonoTouch_UIKit_UIApplicationDelegate"

	.byte 20,16
LDIFF_SYM33=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM33
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIApplicationDelegate"

LDIFF_SYM34=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM34
LTDIE_3_POINTER:

	.byte 13
LDIFF_SYM35=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM35
LTDIE_3_REFERENCE:

	.byte 14
LDIFF_SYM36=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM36
LTDIE_9:

	.byte 5
	.asciz "MonoTouch_UIKit_UIResponder"

	.byte 20,16
LDIFF_SYM37=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM37
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIResponder"

LDIFF_SYM38=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM38
LTDIE_9_POINTER:

	.byte 13
LDIFF_SYM39=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM39
LTDIE_9_REFERENCE:

	.byte 14
LDIFF_SYM40=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM40
LTDIE_8:

	.byte 5
	.asciz "MonoTouch_UIKit_UIView"

	.byte 24,16
LDIFF_SYM41=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM41
	.byte 2,35,0,6
	.asciz "__mt_Subviews_var"

LDIFF_SYM42=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM42
	.byte 2,35,20,0,7
	.asciz "MonoTouch_UIKit_UIView"

LDIFF_SYM43=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM43
LTDIE_8_POINTER:

	.byte 13
LDIFF_SYM44=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM44
LTDIE_8_REFERENCE:

	.byte 14
LDIFF_SYM45=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM45
LTDIE_7:

	.byte 5
	.asciz "MonoTouch_UIKit_UIWindow"

	.byte 28,16
LDIFF_SYM46=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM46
	.byte 2,35,0,6
	.asciz "__mt_RootViewController_var"

LDIFF_SYM47=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM47
	.byte 2,35,24,0,7
	.asciz "MonoTouch_UIKit_UIWindow"

LDIFF_SYM48=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM48
LTDIE_7_POINTER:

	.byte 13
LDIFF_SYM49=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM49
LTDIE_7_REFERENCE:

	.byte 14
LDIFF_SYM50=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM50
LTDIE_2:

	.byte 5
	.asciz "BeBabby_AppDelegate"

	.byte 24,16
LDIFF_SYM51=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM51
	.byte 2,35,0,6
	.asciz "<Window>k__BackingField"

LDIFF_SYM52=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM52
	.byte 2,35,20,0,7
	.asciz "BeBabby_AppDelegate"

LDIFF_SYM53=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM53
LTDIE_2_POINTER:

	.byte 13
LDIFF_SYM54=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM54
LTDIE_2_REFERENCE:

	.byte 14
LDIFF_SYM55=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM55
	.byte 2
	.asciz "BeBabby.AppDelegate:get_Window"
	.long _BeBabby_AppDelegate_get_Window
	.long Lme_2

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM56=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM56
	.byte 2,125,8,11
	.asciz "V_0"

LDIFF_SYM57=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM57
	.byte 1,86,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM58=Lfde2_end - Lfde2_start
	.long LDIFF_SYM58
Lfde2_start:

	.long 0
	.align 2
	.long _BeBabby_AppDelegate_get_Window

LDIFF_SYM59=Lme_2 - _BeBabby_AppDelegate_get_Window
	.long LDIFF_SYM59
	.byte 12,13,0,72,14,8,135,2,68,14,16,134,4,136,3,142,1,68,14,32
	.align 2
Lfde2_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.AppDelegate:set_Window"
	.long _BeBabby_AppDelegate_set_Window_MonoTouch_UIKit_UIWindow
	.long Lme_3

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM60=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM60
	.byte 2,125,8,3
	.asciz "value"

LDIFF_SYM61=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM61
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM62=Lfde3_end - Lfde3_start
	.long LDIFF_SYM62
Lfde3_start:

	.long 0
	.align 2
	.long _BeBabby_AppDelegate_set_Window_MonoTouch_UIKit_UIWindow

LDIFF_SYM63=Lme_3 - _BeBabby_AppDelegate_set_Window_MonoTouch_UIKit_UIWindow
	.long LDIFF_SYM63
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde3_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.AppDelegate:.ctor"
	.long _BeBabby_AppDelegate__ctor
	.long Lme_4

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM64=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM64
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM65=Lfde4_end - Lfde4_start
	.long LDIFF_SYM65
Lfde4_start:

	.long 0
	.align 2
	.long _BeBabby_AppDelegate__ctor

LDIFF_SYM66=Lme_4 - _BeBabby_AppDelegate__ctor
	.long LDIFF_SYM66
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde4_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_10:

	.byte 5
	.asciz "MonoTouch_UIKit_UIApplication"

	.byte 20,16
LDIFF_SYM67=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM67
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIApplication"

LDIFF_SYM68=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM68
LTDIE_10_POINTER:

	.byte 13
LDIFF_SYM69=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM69
LTDIE_10_REFERENCE:

	.byte 14
LDIFF_SYM70=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM70
	.byte 2
	.asciz "BeBabby.AppDelegate:OnResignActivation"
	.long _BeBabby_AppDelegate_OnResignActivation_MonoTouch_UIKit_UIApplication
	.long Lme_5

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM71=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM71
	.byte 2,125,8,3
	.asciz "application"

LDIFF_SYM72=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM72
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM73=Lfde5_end - Lfde5_start
	.long LDIFF_SYM73
Lfde5_start:

	.long 0
	.align 2
	.long _BeBabby_AppDelegate_OnResignActivation_MonoTouch_UIKit_UIApplication

LDIFF_SYM74=Lme_5 - _BeBabby_AppDelegate_OnResignActivation_MonoTouch_UIKit_UIApplication
	.long LDIFF_SYM74
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde5_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.AppDelegate:DidEnterBackground"
	.long _BeBabby_AppDelegate_DidEnterBackground_MonoTouch_UIKit_UIApplication
	.long Lme_6

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM75=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM75
	.byte 2,125,8,3
	.asciz "application"

LDIFF_SYM76=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM76
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM77=Lfde6_end - Lfde6_start
	.long LDIFF_SYM77
Lfde6_start:

	.long 0
	.align 2
	.long _BeBabby_AppDelegate_DidEnterBackground_MonoTouch_UIKit_UIApplication

LDIFF_SYM78=Lme_6 - _BeBabby_AppDelegate_DidEnterBackground_MonoTouch_UIKit_UIApplication
	.long LDIFF_SYM78
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde6_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.AppDelegate:WillEnterForeground"
	.long _BeBabby_AppDelegate_WillEnterForeground_MonoTouch_UIKit_UIApplication
	.long Lme_7

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM79=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM79
	.byte 2,125,8,3
	.asciz "application"

LDIFF_SYM80=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM80
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM81=Lfde7_end - Lfde7_start
	.long LDIFF_SYM81
Lfde7_start:

	.long 0
	.align 2
	.long _BeBabby_AppDelegate_WillEnterForeground_MonoTouch_UIKit_UIApplication

LDIFF_SYM82=Lme_7 - _BeBabby_AppDelegate_WillEnterForeground_MonoTouch_UIKit_UIApplication
	.long LDIFF_SYM82
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde7_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.AppDelegate:WillTerminate"
	.long _BeBabby_AppDelegate_WillTerminate_MonoTouch_UIKit_UIApplication
	.long Lme_8

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM83=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM83
	.byte 2,125,8,3
	.asciz "application"

LDIFF_SYM84=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM84
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM85=Lfde8_end - Lfde8_start
	.long LDIFF_SYM85
Lfde8_start:

	.long 0
	.align 2
	.long _BeBabby_AppDelegate_WillTerminate_MonoTouch_UIKit_UIApplication

LDIFF_SYM86=Lme_8 - _BeBabby_AppDelegate_WillTerminate_MonoTouch_UIKit_UIApplication
	.long LDIFF_SYM86
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde8_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_12:

	.byte 5
	.asciz "MonoTouch_UIKit_UIViewController"

	.byte 28,16
LDIFF_SYM87=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM87
	.byte 2,35,0,6
	.asciz "__mt_View_var"

LDIFF_SYM88=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM88
	.byte 2,35,20,6
	.asciz "__mt_PresentedViewController_var"

LDIFF_SYM89=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM89
	.byte 2,35,24,0,7
	.asciz "MonoTouch_UIKit_UIViewController"

LDIFF_SYM90=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM90
LTDIE_12_POINTER:

	.byte 13
LDIFF_SYM91=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM91
LTDIE_12_REFERENCE:

	.byte 14
LDIFF_SYM92=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM92
LTDIE_11:

	.byte 5
	.asciz "BeBabby_MainViewController"

	.byte 28,16
LDIFF_SYM93=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM93
	.byte 2,35,0,0,7
	.asciz "BeBabby_MainViewController"

LDIFF_SYM94=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM94
LTDIE_11_POINTER:

	.byte 13
LDIFF_SYM95=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM95
LTDIE_11_REFERENCE:

	.byte 14
LDIFF_SYM96=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM96
	.byte 2
	.asciz "BeBabby.MainViewController:.ctor"
	.long _BeBabby_MainViewController__ctor_intptr
	.long Lme_9

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM97=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM97
	.byte 2,125,8,3
	.asciz "handle"

LDIFF_SYM98=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM98
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM99=Lfde9_end - Lfde9_start
	.long LDIFF_SYM99
Lfde9_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController__ctor_intptr

LDIFF_SYM100=Lme_9 - _BeBabby_MainViewController__ctor_intptr
	.long LDIFF_SYM100
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde9_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.MainViewController:DidReceiveMemoryWarning"
	.long _BeBabby_MainViewController_DidReceiveMemoryWarning
	.long Lme_a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM101=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM101
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM102=Lfde10_end - Lfde10_start
	.long LDIFF_SYM102
Lfde10_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_DidReceiveMemoryWarning

LDIFF_SYM103=Lme_a - _BeBabby_MainViewController_DidReceiveMemoryWarning
	.long LDIFF_SYM103
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde10_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.MainViewController:ViewDidLoad"
	.long _BeBabby_MainViewController_ViewDidLoad
	.long Lme_b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM104=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM104
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM105=Lfde11_end - Lfde11_start
	.long LDIFF_SYM105
Lfde11_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_ViewDidLoad

LDIFF_SYM106=Lme_b - _BeBabby_MainViewController_ViewDidLoad
	.long LDIFF_SYM106
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde11_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_14:

	.byte 5
	.asciz "MonoTouch_UIKit_UIControl"

	.byte 24,16
LDIFF_SYM107=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM107
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIControl"

LDIFF_SYM108=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM108
LTDIE_14_POINTER:

	.byte 13
LDIFF_SYM109=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM109
LTDIE_14_REFERENCE:

	.byte 14
LDIFF_SYM110=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM110
LTDIE_13:

	.byte 5
	.asciz "MonoTouch_UIKit_UIButton"

	.byte 24,16
LDIFF_SYM111=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM111
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIButton"

LDIFF_SYM112=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM112
LTDIE_13_POINTER:

	.byte 13
LDIFF_SYM113=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM113
LTDIE_13_REFERENCE:

	.byte 14
LDIFF_SYM114=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM114
	.byte 2
	.asciz "BeBabby.MainViewController:btnStartCamera"
	.long _BeBabby_MainViewController_btnStartCamera_MonoTouch_UIKit_UIButton
	.long Lme_c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM115=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM115
	.byte 2,125,8,3
	.asciz "sender"

LDIFF_SYM116=LTDIE_13_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM116
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM117=Lfde12_end - Lfde12_start
	.long LDIFF_SYM117
Lfde12_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_btnStartCamera_MonoTouch_UIKit_UIButton

LDIFF_SYM118=Lme_c - _BeBabby_MainViewController_btnStartCamera_MonoTouch_UIKit_UIButton
	.long LDIFF_SYM118
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde12_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.MainViewController:ViewWillAppear"
	.long _BeBabby_MainViewController_ViewWillAppear_bool
	.long Lme_d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM119=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM119
	.byte 2,125,8,3
	.asciz "animated"

LDIFF_SYM120=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM120
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM121=Lfde13_end - Lfde13_start
	.long LDIFF_SYM121
Lfde13_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_ViewWillAppear_bool

LDIFF_SYM122=Lme_d - _BeBabby_MainViewController_ViewWillAppear_bool
	.long LDIFF_SYM122
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde13_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_16:

	.byte 5
	.asciz "MonoTouch_UIKit_UIPopoverController"

	.byte 28,16
LDIFF_SYM123=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM123
	.byte 2,35,0,6
	.asciz "__mt_ContentViewController_var"

LDIFF_SYM124=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM124
	.byte 2,35,20,6
	.asciz "__mt_WeakDelegate_var"

LDIFF_SYM125=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM125
	.byte 2,35,24,0,7
	.asciz "MonoTouch_UIKit_UIPopoverController"

LDIFF_SYM126=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM126
LTDIE_16_POINTER:

	.byte 13
LDIFF_SYM127=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM127
LTDIE_16_REFERENCE:

	.byte 14
LDIFF_SYM128=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM128
LTDIE_18:

	.byte 5
	.asciz "MonoTouch_UIKit_UINavigationControllerDelegate"

	.byte 20,16
LDIFF_SYM129=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM129
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UINavigationControllerDelegate"

LDIFF_SYM130=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM130
LTDIE_18_POINTER:

	.byte 13
LDIFF_SYM131=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM131
LTDIE_18_REFERENCE:

	.byte 14
LDIFF_SYM132=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM132
LTDIE_17:

	.byte 5
	.asciz "MonoTouch_UIKit_UIImagePickerControllerDelegate"

	.byte 20,16
LDIFF_SYM133=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM133
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIImagePickerControllerDelegate"

LDIFF_SYM134=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM134
LTDIE_17_POINTER:

	.byte 13
LDIFF_SYM135=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM135
LTDIE_17_REFERENCE:

	.byte 14
LDIFF_SYM136=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM136
LTDIE_15:

	.byte 5
	.asciz "Xamarin_Media_MediaPicker"

	.byte 20,16
LDIFF_SYM137=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM137
	.byte 2,35,0,6
	.asciz "popover"

LDIFF_SYM138=LTDIE_16_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM138
	.byte 2,35,8,6
	.asciz "pickerDelegate"

LDIFF_SYM139=LTDIE_17_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM139
	.byte 2,35,12,6
	.asciz "<IsCameraAvailable>k__BackingField"

LDIFF_SYM140=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM140
	.byte 2,35,16,6
	.asciz "<PhotosSupported>k__BackingField"

LDIFF_SYM141=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM141
	.byte 2,35,17,6
	.asciz "<VideosSupported>k__BackingField"

LDIFF_SYM142=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM142
	.byte 2,35,18,0,7
	.asciz "Xamarin_Media_MediaPicker"

LDIFF_SYM143=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM143
LTDIE_15_POINTER:

	.byte 13
LDIFF_SYM144=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM144
LTDIE_15_REFERENCE:

	.byte 14
LDIFF_SYM145=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM145
LTDIE_20:

	.byte 5
	.asciz "Xamarin_Media_StoreMediaOptions"

	.byte 16,16
LDIFF_SYM146=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM146
	.byte 2,35,0,6
	.asciz "<Directory>k__BackingField"

LDIFF_SYM147=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM147
	.byte 2,35,8,6
	.asciz "<Name>k__BackingField"

LDIFF_SYM148=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM148
	.byte 2,35,12,0,7
	.asciz "Xamarin_Media_StoreMediaOptions"

LDIFF_SYM149=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM149
LTDIE_20_POINTER:

	.byte 13
LDIFF_SYM150=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM150
LTDIE_20_REFERENCE:

	.byte 14
LDIFF_SYM151=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM151
LTDIE_21:

	.byte 8
	.asciz "Xamarin_Media_CameraDevice"

	.byte 4
LDIFF_SYM152=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM152
	.byte 9
	.asciz "Rear"

	.byte 0,9
	.asciz "Front"

	.byte 1,0,7
	.asciz "Xamarin_Media_CameraDevice"

LDIFF_SYM153=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM153
LTDIE_21_POINTER:

	.byte 13
LDIFF_SYM154=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM154
LTDIE_21_REFERENCE:

	.byte 14
LDIFF_SYM155=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM155
LTDIE_19:

	.byte 5
	.asciz "Xamarin_Media_StoreCameraMediaOptions"

	.byte 20,16
LDIFF_SYM156=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM156
	.byte 2,35,0,6
	.asciz "<DefaultCamera>k__BackingField"

LDIFF_SYM157=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM157
	.byte 2,35,16,0,7
	.asciz "Xamarin_Media_StoreCameraMediaOptions"

LDIFF_SYM158=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM158
LTDIE_19_POINTER:

	.byte 13
LDIFF_SYM159=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM159
LTDIE_19_REFERENCE:

	.byte 14
LDIFF_SYM160=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM160
	.byte 2
	.asciz "BeBabby.MainViewController:ViewDidAppear"
	.long _BeBabby_MainViewController_ViewDidAppear_bool
	.long Lme_e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM161=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM161
	.byte 2,125,24,3
	.asciz "animated"

LDIFF_SYM162=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM162
	.byte 2,125,28,11
	.asciz "picker"

LDIFF_SYM163=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM163
	.byte 1,86,11
	.asciz "tempName"

LDIFF_SYM164=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM164
	.byte 1,85,11
	.asciz "V_2"

LDIFF_SYM165=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM165
	.byte 2,125,8,11
	.asciz "V_3"

LDIFF_SYM166=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM166
	.byte 1,84,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM167=Lfde14_end - Lfde14_start
	.long LDIFF_SYM167
Lfde14_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_ViewDidAppear_bool

LDIFF_SYM168=Lme_e - _BeBabby_MainViewController_ViewDidAppear_bool
	.long LDIFF_SYM168
	.byte 12,13,0,72,14,8,135,2,68,14,24,132,6,133,5,134,4,136,3,142,1,68,14,88
	.align 2
Lfde14_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.MainViewController:ViewWillDisappear"
	.long _BeBabby_MainViewController_ViewWillDisappear_bool
	.long Lme_f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM169=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM169
	.byte 2,125,8,3
	.asciz "animated"

LDIFF_SYM170=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM170
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM171=Lfde15_end - Lfde15_start
	.long LDIFF_SYM171
Lfde15_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_ViewWillDisappear_bool

LDIFF_SYM172=Lme_f - _BeBabby_MainViewController_ViewWillDisappear_bool
	.long LDIFF_SYM172
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde15_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.MainViewController:ViewDidDisappear"
	.long _BeBabby_MainViewController_ViewDidDisappear_bool
	.long Lme_10

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM173=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM173
	.byte 2,125,8,3
	.asciz "animated"

LDIFF_SYM174=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM174
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM175=Lfde16_end - Lfde16_start
	.long LDIFF_SYM175
Lfde16_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_ViewDidDisappear_bool

LDIFF_SYM176=Lme_10 - _BeBabby_MainViewController_ViewDidDisappear_bool
	.long LDIFF_SYM176
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde16_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.MainViewController:showInfo"
	.long _BeBabby_MainViewController_showInfo_MonoTouch_Foundation_NSObject
	.long Lme_11

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM177=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM177
	.byte 2,125,8,3
	.asciz "sender"

LDIFF_SYM178=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM178
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM179=Lfde17_end - Lfde17_start
	.long LDIFF_SYM179
Lfde17_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_showInfo_MonoTouch_Foundation_NSObject

LDIFF_SYM180=Lme_11 - _BeBabby_MainViewController_showInfo_MonoTouch_Foundation_NSObject
	.long LDIFF_SYM180
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde17_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.MainViewController:ReleaseDesignerOutlets"
	.long _BeBabby_MainViewController_ReleaseDesignerOutlets
	.long Lme_12

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM181=LTDIE_11_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM181
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM182=Lfde18_end - Lfde18_start
	.long LDIFF_SYM182
Lfde18_start:

	.long 0
	.align 2
	.long _BeBabby_MainViewController_ReleaseDesignerOutlets

LDIFF_SYM183=Lme_12 - _BeBabby_MainViewController_ReleaseDesignerOutlets
	.long LDIFF_SYM183
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde18_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_28:

	.byte 5
	.asciz "System_Reflection_MemberInfo"

	.byte 8,16
LDIFF_SYM184=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM184
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MemberInfo"

LDIFF_SYM185=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM185
LTDIE_28_POINTER:

	.byte 13
LDIFF_SYM186=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM186
LTDIE_28_REFERENCE:

	.byte 14
LDIFF_SYM187=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM187
LTDIE_27:

	.byte 5
	.asciz "System_Reflection_MethodBase"

	.byte 8,16
LDIFF_SYM188=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM188
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MethodBase"

LDIFF_SYM189=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM189
LTDIE_27_POINTER:

	.byte 13
LDIFF_SYM190=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM190
LTDIE_27_REFERENCE:

	.byte 14
LDIFF_SYM191=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM191
LTDIE_26:

	.byte 5
	.asciz "System_Reflection_MethodInfo"

	.byte 8,16
LDIFF_SYM192=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM192
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MethodInfo"

LDIFF_SYM193=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM193
LTDIE_26_POINTER:

	.byte 13
LDIFF_SYM194=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM194
LTDIE_26_REFERENCE:

	.byte 14
LDIFF_SYM195=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM195
LTDIE_30:

	.byte 5
	.asciz "System_Type"

	.byte 12,16
LDIFF_SYM196=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM196
	.byte 2,35,0,6
	.asciz "_impl"

LDIFF_SYM197=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM197
	.byte 2,35,8,0,7
	.asciz "System_Type"

LDIFF_SYM198=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM198
LTDIE_30_POINTER:

	.byte 13
LDIFF_SYM199=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM199
LTDIE_30_REFERENCE:

	.byte 14
LDIFF_SYM200=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM200
LTDIE_29:

	.byte 5
	.asciz "System_DelegateData"

	.byte 16,16
LDIFF_SYM201=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM201
	.byte 2,35,0,6
	.asciz "target_type"

LDIFF_SYM202=LTDIE_30_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM202
	.byte 2,35,8,6
	.asciz "method_name"

LDIFF_SYM203=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM203
	.byte 2,35,12,0,7
	.asciz "System_DelegateData"

LDIFF_SYM204=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM204
LTDIE_29_POINTER:

	.byte 13
LDIFF_SYM205=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM205
LTDIE_29_REFERENCE:

	.byte 14
LDIFF_SYM206=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM206
LTDIE_25:

	.byte 5
	.asciz "System_Delegate"

	.byte 44,16
LDIFF_SYM207=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM207
	.byte 2,35,0,6
	.asciz "method_ptr"

LDIFF_SYM208=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM208
	.byte 2,35,8,6
	.asciz "invoke_impl"

LDIFF_SYM209=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM209
	.byte 2,35,12,6
	.asciz "m_target"

LDIFF_SYM210=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM210
	.byte 2,35,16,6
	.asciz "method"

LDIFF_SYM211=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM211
	.byte 2,35,20,6
	.asciz "delegate_trampoline"

LDIFF_SYM212=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM212
	.byte 2,35,24,6
	.asciz "method_code"

LDIFF_SYM213=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM213
	.byte 2,35,28,6
	.asciz "method_info"

LDIFF_SYM214=LTDIE_26_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM214
	.byte 2,35,32,6
	.asciz "original_method_info"

LDIFF_SYM215=LTDIE_26_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM215
	.byte 2,35,36,6
	.asciz "data"

LDIFF_SYM216=LTDIE_29_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM216
	.byte 2,35,40,0,7
	.asciz "System_Delegate"

LDIFF_SYM217=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM217
LTDIE_25_POINTER:

	.byte 13
LDIFF_SYM218=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM218
LTDIE_25_REFERENCE:

	.byte 14
LDIFF_SYM219=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM219
LTDIE_24:

	.byte 5
	.asciz "System_MulticastDelegate"

	.byte 52,16
LDIFF_SYM220=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM220
	.byte 2,35,0,6
	.asciz "prev"

LDIFF_SYM221=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM221
	.byte 2,35,44,6
	.asciz "kpm_next"

LDIFF_SYM222=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM222
	.byte 2,35,48,0,7
	.asciz "System_MulticastDelegate"

LDIFF_SYM223=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM223
LTDIE_24_POINTER:

	.byte 13
LDIFF_SYM224=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM224
LTDIE_24_REFERENCE:

	.byte 14
LDIFF_SYM225=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM225
LTDIE_23:

	.byte 5
	.asciz "System_EventHandler"

	.byte 52,16
LDIFF_SYM226=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM226
	.byte 2,35,0,0,7
	.asciz "System_EventHandler"

LDIFF_SYM227=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM227
LTDIE_23_POINTER:

	.byte 13
LDIFF_SYM228=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM228
LTDIE_23_REFERENCE:

	.byte 14
LDIFF_SYM229=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM229
LTDIE_22:

	.byte 5
	.asciz "BeBabby_FlipsideViewController"

	.byte 32,16
LDIFF_SYM230=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM230
	.byte 2,35,0,6
	.asciz "Done"

LDIFF_SYM231=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM231
	.byte 2,35,28,0,7
	.asciz "BeBabby_FlipsideViewController"

LDIFF_SYM232=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM232
LTDIE_22_POINTER:

	.byte 13
LDIFF_SYM233=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM233
LTDIE_22_REFERENCE:

	.byte 14
LDIFF_SYM234=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM234
	.byte 2
	.asciz "BeBabby.FlipsideViewController:.ctor"
	.long _BeBabby_FlipsideViewController__ctor_intptr
	.long Lme_13

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM235=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM235
	.byte 2,125,8,3
	.asciz "handle"

LDIFF_SYM236=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM236
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM237=Lfde19_end - Lfde19_start
	.long LDIFF_SYM237
Lfde19_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController__ctor_intptr

LDIFF_SYM238=Lme_13 - _BeBabby_FlipsideViewController__ctor_intptr
	.long LDIFF_SYM238
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde19_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:add_Done"
	.long _BeBabby_FlipsideViewController_add_Done_System_EventHandler
	.long Lme_14

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM239=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM239
	.byte 1,86,3
	.asciz "value"

LDIFF_SYM240=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM240
	.byte 2,125,8,11
	.asciz "V_0"

LDIFF_SYM241=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM241
	.byte 1,85,11
	.asciz "V_1"

LDIFF_SYM242=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM242
	.byte 1,84,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM243=Lfde20_end - Lfde20_start
	.long LDIFF_SYM243
Lfde20_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_add_Done_System_EventHandler

LDIFF_SYM244=Lme_14 - _BeBabby_FlipsideViewController_add_Done_System_EventHandler
	.long LDIFF_SYM244
	.byte 12,13,0,72,14,8,135,2,68,14,32,132,8,133,7,134,6,136,5,138,4,139,3,142,1,68,14,56
	.align 2
Lfde20_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:remove_Done"
	.long _BeBabby_FlipsideViewController_remove_Done_System_EventHandler
	.long Lme_15

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM245=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM245
	.byte 1,86,3
	.asciz "value"

LDIFF_SYM246=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM246
	.byte 2,125,8,11
	.asciz "V_0"

LDIFF_SYM247=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM247
	.byte 1,85,11
	.asciz "V_1"

LDIFF_SYM248=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM248
	.byte 1,84,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM249=Lfde21_end - Lfde21_start
	.long LDIFF_SYM249
Lfde21_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_remove_Done_System_EventHandler

LDIFF_SYM250=Lme_15 - _BeBabby_FlipsideViewController_remove_Done_System_EventHandler
	.long LDIFF_SYM250
	.byte 12,13,0,72,14,8,135,2,68,14,32,132,8,133,7,134,6,136,5,138,4,139,3,142,1,68,14,56
	.align 2
Lfde21_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:DidReceiveMemoryWarning"
	.long _BeBabby_FlipsideViewController_DidReceiveMemoryWarning
	.long Lme_16

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM251=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM251
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM252=Lfde22_end - Lfde22_start
	.long LDIFF_SYM252
Lfde22_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_DidReceiveMemoryWarning

LDIFF_SYM253=Lme_16 - _BeBabby_FlipsideViewController_DidReceiveMemoryWarning
	.long LDIFF_SYM253
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde22_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:ViewDidLoad"
	.long _BeBabby_FlipsideViewController_ViewDidLoad
	.long Lme_17

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM254=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM254
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM255=Lfde23_end - Lfde23_start
	.long LDIFF_SYM255
Lfde23_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_ViewDidLoad

LDIFF_SYM256=Lme_17 - _BeBabby_FlipsideViewController_ViewDidLoad
	.long LDIFF_SYM256
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde23_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:ViewWillAppear"
	.long _BeBabby_FlipsideViewController_ViewWillAppear_bool
	.long Lme_18

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM257=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM257
	.byte 2,125,8,3
	.asciz "animated"

LDIFF_SYM258=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM258
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM259=Lfde24_end - Lfde24_start
	.long LDIFF_SYM259
Lfde24_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_ViewWillAppear_bool

LDIFF_SYM260=Lme_18 - _BeBabby_FlipsideViewController_ViewWillAppear_bool
	.long LDIFF_SYM260
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde24_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:ViewDidAppear"
	.long _BeBabby_FlipsideViewController_ViewDidAppear_bool
	.long Lme_19

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM261=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM261
	.byte 2,125,8,3
	.asciz "animated"

LDIFF_SYM262=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM262
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM263=Lfde25_end - Lfde25_start
	.long LDIFF_SYM263
Lfde25_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_ViewDidAppear_bool

LDIFF_SYM264=Lme_19 - _BeBabby_FlipsideViewController_ViewDidAppear_bool
	.long LDIFF_SYM264
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde25_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:ViewWillDisappear"
	.long _BeBabby_FlipsideViewController_ViewWillDisappear_bool
	.long Lme_1a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM265=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM265
	.byte 2,125,8,3
	.asciz "animated"

LDIFF_SYM266=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM266
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM267=Lfde26_end - Lfde26_start
	.long LDIFF_SYM267
Lfde26_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_ViewWillDisappear_bool

LDIFF_SYM268=Lme_1a - _BeBabby_FlipsideViewController_ViewWillDisappear_bool
	.long LDIFF_SYM268
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde26_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:ViewDidDisappear"
	.long _BeBabby_FlipsideViewController_ViewDidDisappear_bool
	.long Lme_1b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM269=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM269
	.byte 2,125,8,3
	.asciz "animated"

LDIFF_SYM270=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM270
	.byte 2,125,12,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM271=Lfde27_end - Lfde27_start
	.long LDIFF_SYM271
Lfde27_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_ViewDidDisappear_bool

LDIFF_SYM272=Lme_1b - _BeBabby_FlipsideViewController_ViewDidDisappear_bool
	.long LDIFF_SYM272
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,40
	.align 2
Lfde27_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_32:

	.byte 5
	.asciz "MonoTouch_UIKit_UIBarItem"

	.byte 20,16
LDIFF_SYM273=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM273
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIBarItem"

LDIFF_SYM274=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM274
LTDIE_32_POINTER:

	.byte 13
LDIFF_SYM275=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM275
LTDIE_32_REFERENCE:

	.byte 14
LDIFF_SYM276=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM276
LTDIE_31:

	.byte 5
	.asciz "MonoTouch_UIKit_UIBarButtonItem"

	.byte 20,16
LDIFF_SYM277=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM277
	.byte 2,35,0,0,7
	.asciz "MonoTouch_UIKit_UIBarButtonItem"

LDIFF_SYM278=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM278
LTDIE_31_POINTER:

	.byte 13
LDIFF_SYM279=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM279
LTDIE_31_REFERENCE:

	.byte 14
LDIFF_SYM280=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM280
	.byte 2
	.asciz "BeBabby.FlipsideViewController:done"
	.long _BeBabby_FlipsideViewController_done_MonoTouch_UIKit_UIBarButtonItem
	.long Lme_1c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM281=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM281
	.byte 1,90,3
	.asciz "sender"

LDIFF_SYM282=LTDIE_31_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM282
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM283=Lfde28_end - Lfde28_start
	.long LDIFF_SYM283
Lfde28_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_done_MonoTouch_UIKit_UIBarButtonItem

LDIFF_SYM284=Lme_1c - _BeBabby_FlipsideViewController_done_MonoTouch_UIKit_UIBarButtonItem
	.long LDIFF_SYM284
	.byte 12,13,0,72,14,8,135,2,68,14,16,136,4,138,3,142,1,68,14,48
	.align 2
Lfde28_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "BeBabby.FlipsideViewController:ReleaseDesignerOutlets"
	.long _BeBabby_FlipsideViewController_ReleaseDesignerOutlets
	.long Lme_1d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM285=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM285
	.byte 2,125,8,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM286=Lfde29_end - Lfde29_start
	.long LDIFF_SYM286
Lfde29_start:

	.long 0
	.align 2
	.long _BeBabby_FlipsideViewController_ReleaseDesignerOutlets

LDIFF_SYM287=Lme_1d - _BeBabby_FlipsideViewController_ReleaseDesignerOutlets
	.long LDIFF_SYM287
	.byte 12,13,0,72,14,8,135,2,68,14,12,136,3,142,1,68,14,32
	.align 2
Lfde29_end:

.section __DWARF, __debug_info,regular,debug

	.byte 0
Ldebug_info_end:
.section __DWARF, __debug_line,regular,debug
Ldebug_line_section_start:
Ldebug_line_start:

	.long Ldebug_line_end - . -4
	.short 2
	.long Ldebug_line_header_end - . -4
	.byte 1,1,251,14,13,0,1,1,1,1,0,0,0,1,0,0,1
.section __DWARF, __debug_line,regular,debug
	.asciz "/Users/giusepecasagrande/Dropbox/Lighthouse/BeeBaby/BeBabby/BeBabby"

	.byte 0
	.asciz "<unknown>"

	.byte 0,0,0
	.asciz "Main.cs"

	.byte 1,0,0
	.asciz "AppDelegate.cs"

	.byte 1,0,0
	.asciz "MainViewController.cs"

	.byte 1,0,0
	.asciz "MainViewController.designer.cs"

	.byte 1,0,0
	.asciz "FlipsideViewController.cs"

	.byte 1,0,0
	.asciz "FlipsideViewController.designer.cs"

	.byte 1,0,0,0
Ldebug_line_header_end:
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_Application_Main_string__

	.byte 3,12,4,2,1,3,12,2,196,0,1,8,119,3,1,2,208,0,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_AppDelegate_get_Window

	.byte 3,17,4,3,1,3,17,2,200,0,1,2,232,0,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_AppDelegate_set_Window_MonoTouch_UIKit_UIWindow

	.byte 3,18,4,3,1,3,18,2,200,0,1,2,208,0,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_AppDelegate_OnResignActivation_MonoTouch_UIKit_UIApplication

	.byte 3,23,4,3,1,3,23,2,200,0,1,8,117,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_AppDelegate_DidEnterBackground_MonoTouch_UIKit_UIApplication

	.byte 3,29,4,3,1,3,29,2,200,0,1,8,117,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_AppDelegate_WillEnterForeground_MonoTouch_UIKit_UIApplication

	.byte 3,33,4,3,1,3,33,2,200,0,1,8,117,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_AppDelegate_WillTerminate_MonoTouch_UIKit_UIApplication

	.byte 3,37,4,3,1,3,37,2,200,0,1,8,117,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController__ctor_intptr

	.byte 3,10,4,4,1,3,10,2,200,0,1,3,1,2,36,1,244,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_DidReceiveMemoryWarning

	.byte 3,16,4,4,1,3,16,2,196,0,1,8,118,3,3,2,48,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_ViewDidLoad

	.byte 3,26,4,4,1,3,26,2,196,0,1,8,117,3,2,2,48,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_btnStartCamera_MonoTouch_UIKit_UIButton

	.byte 3,32,4,4,1,3,32,2,200,0,1,8,118,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_ViewWillAppear_bool

	.byte 3,37,4,4,1,3,37,2,200,0,1,8,117,3,1,2,60,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_ViewDidAppear_bool

	.byte 3,42,4,4,1,3,42,2,252,0,1,8,117,3,1,2,60,1,3,2,2,52,1,3,1,2,212,0,1,3,1,2
	.byte 208,0,1,243,3,1,2,132,1,1,3,2,2,200,0,1,3,1,2,216,0,1,3,125,2,216,0,1,3,14,2,32
	.byte 1,244,2,52,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_ViewWillDisappear_bool

	.byte 3,197,0,4,4,1,3,197,0,2,200,0,1,8,117,3,1,2,60,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_ViewDidDisappear_bool

	.byte 3,202,0,4,4,1,3,202,0,2,200,0,1,8,117,3,1,2,60,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_showInfo_MonoTouch_Foundation_NSObject

	.byte 3,209,0,4,4,1,3,209,0,2,200,0,1,8,117,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_MainViewController_ReleaseDesignerOutlets

	.byte 3,21,4,5,1,3,21,2,196,0,1,8,117,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController__ctor_intptr

	.byte 3,9,4,6,1,3,9,2,200,0,1,3,1,2,36,1,243,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_DidReceiveMemoryWarning

	.byte 3,14,4,6,1,3,14,2,196,0,1,8,118,3,3,2,48,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_ViewDidLoad

	.byte 3,24,4,6,1,3,24,2,196,0,1,8,117,3,3,2,48,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_ViewWillAppear_bool

	.byte 3,31,4,6,1,3,31,2,200,0,1,8,117,3,1,2,60,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_ViewDidAppear_bool

	.byte 3,36,4,6,1,3,36,2,200,0,1,8,117,3,1,2,60,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_ViewWillDisappear_bool

	.byte 3,41,4,6,1,3,41,2,200,0,1,8,117,3,1,2,60,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_ViewDidDisappear_bool

	.byte 3,46,4,6,1,3,46,2,200,0,1,8,117,3,1,2,60,1,2,44,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_done_MonoTouch_UIKit_UIBarButtonItem

	.byte 3,53,4,6,1,3,53,2,200,0,1,8,117,3,2,2,60,1,8,229,3,1,2,232,0,1,2,52,1,0,1,1
.section __DWARF, __debug_line,regular,debug

	.byte 0,5,2
	.long _BeBabby_FlipsideViewController_ReleaseDesignerOutlets

	.byte 3,18,4,7,1,3,18,2,196,0,1,8,117,2,44,1,0,1,1,0,1,1
Ldebug_line_end:
.text
	.align 3
mem_end:
