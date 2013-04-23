
function WriteConsHeader
{
  param($msg)
  
  	$retMsg = "*  " + $msg + ($spc * ($colwidth - $msg.length)) + "*"
	
	return $retMsg

}

Function Set-RSConsole 
{ 


 $Host.UI.RawUI.WindowTitle = "RIGHTSCALE - " +  $Host.UI.RawUI.WindowTitle

 $host.ui.RawUI.ForegroundColor = "White" 
 $host.ui.RawUI.BackgroundColor = "DarkBlue" 
 $host.PrivateData.ErrorBackgroundColor = "DarkBlue" 
 $Host.PrivateData.WarningBackgroundColor = "DarkBlue" 
 $Host.PrivateData.VerboseBackgroundColor = "DarkBlue" 
 $host.PrivateData.ErrorForegroundColor = "red" 
 $host.PrivateData.WarningForegroundColor = "DarkGreen" 
 $host.PrivateData.VerboseForegroundColor = "Yellow" 
 
 $bufferSize = new-object System.Management.Automation.Host.Size 300,500

 $host.UI.RawUI.BufferSize = $bufferSize

 $maxWS = $host.UI.RawUI.Get_MaxWindowSize()
 $ws = $host.ui.RawUI.WindowSize

 If($maxws.width -ge 85){$ws.width = 85}else{$ws.width = $maxws.width}
 if($maxws.height -ge 42){$ws.height = 42}else{$ws.height = $maxws.height}

}

$rsPowershellPath = (gp HKCU:Software\RightScale\Powershell).Path

$progDir = "${Env:ProgramFiles(x86)}" 
$rsDir = $progDir + "\" + $rsPowershellPath

Set-Location $rsDir

$rsPSDLL = "RightScale.netClient.Powershell.dll"

#RS console settings
set-RSConsole

# Load RS DLL
Write-Host "Loading RightScale.Powershell Cmdlets - $rsDLL"

try
{
  Import-Module .\$rsPSDLL -ErrorAction SilentlyContinue
  
  if(!$?)
  {
    Write-Host "Could not load RightScale.Powershell DLL - $rsPSDLL" -ForegroundColor red
	Write-Host $error[0]
	
	exit
  }
}
catch
{
  Write-Host "Could not load RightScale.Powershell DLL - $rsDLLPath" -ForegroundColor red
  Write-Host "Verify path file exists - $rsPSDLL" -ForegroundColor Red
  Write-Host $_ -ForegroundColor Red
  exit
}


$version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($rsPSDLL).FileVersion

cls

$colwidth = 80
$spc = " "
	
Write-Host ("*" * ($colwidth + 4))
WriteConsHeader "RightScale PowerShell - RightScale.Powershell Version $version"
WriteConsHeader ""
WriteConsHeader "To get started with RightScale Powershell Commands`:"
WriteConsHeader "Connect to RightScale using new-RSSession"
WriteConsHeader ""
WriteConsHeader "To list available commands use`:"
writeConsHeader "get-command -Module RightScale.netClient.Powershell"
Write-Host ("*" * ($colwidth + 4))
Write-Host ""
Write-Host ""