@ECHO OFF
taskkill /im BatteryChangeCharger.exe
md C:\BatteryCharger
md C:\BatteryCharger\application
del C:\BatteryCharger\application /s /f /q
xcopy *.* C:\BatteryCharger\application\ /e /h /k /y
exit