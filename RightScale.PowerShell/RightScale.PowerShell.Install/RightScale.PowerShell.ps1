cd 'c:\program files (x86)\rightscale\powershell'; 
Import-Module .\RightScale.netClient.PowerShell.dll; 
Get-Content .\WelcomeMessage.txt; 
$VerbosePreference=\"Continue\"; 
cd 'C:\'