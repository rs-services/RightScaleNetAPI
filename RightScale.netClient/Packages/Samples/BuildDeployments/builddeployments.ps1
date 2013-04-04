
#-------------------------------------------------------------
#FUNCTONS
#-------------------------------------------------------------
function ConvertFrom-SecureToPlain {
    
    param( [Parameter(Mandatory=$true)][System.Security.SecureString] $SecurePassword)
    
    # Create a "password pointer"
    $PasswordPointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($SecurePassword)
    
    # Get the plain text version of the password
    $PlainTextPassword = [Runtime.InteropServices.Marshal]::PtrToStringAuto($PasswordPointer)
    
    # Free the pointer
    [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($PasswordPointer)
    
    # Return the plain text password
    $PlainTextPassword
    
}

#-------------------------------------------------------------------


cls
cd
Write-Host "Loading RightScale cmdlets"

$rsPoshDllPath = 'C:\Users\michael\Documents\GitHub\RightScaleNetAPI\RightScale.netClient\RSPosh\bin\Debug\RSPosh.dll'
[Reflection.Assembly]::LoadFrom($rsPoshDllPath)

$rsModelFile = "deploymentModels.xml"


[xml]$mdlBuild  = gc .\$rsModelFile
if(!$mdlBuild){Write-Host "Error loading model file - $rsModelFIle";exit 1}

#get rs creds
$rsAccountID = $mdlBuild.RSMODEL.RIGHTSCALE.Account
$rsAccountUserName = $mdlBuild.RSMODEL.RIGHTSCALE.Username
$rsAccountPwd = $mdlBuild.RSMODEL.RIGHTSCALE.password

#get default inputs
$rsDefInputs = $mdlBuild.RSMODEL.DEPLOYMENTS.DEFAULTS.INPUTS.INPUT

#get credentials if not in xml file
if([String]::IsNullOrEmpty($rsAccountID)){$rsAccountID = Read-Host "RightScale Account"}
if([String]::IsNullOrEmpty($rsAccountUserName)){$rsAccountUserName = Read-Host "RightScale Username"}
if([String]::IsNullOrEmpty($rsAccountPwd)){$rsAccountPwd = Read-Host "RightScale Password" -AsSecureString}


Write-Host "Logging in to RightScale account - $rsAccountID"
$session = New-RSSession -username $rsAccountUserName -password (ConvertFrom-SecureToPlain $rsAccountPwd) -accountid $rsAccountID

Write-Host $session

if($session -match "Connected")
{

	foreach($deployment in $mdlBuild.RSMODEL.DEPLOYMENTS.deployment)
	{
  		$dplyName		= $deployment.NAME
  		$dplyDesc 	= $deployment.DESCRIPTION
	  	$dplyCloudID	= $deployment.CLOUDID
   
  		#create deployment
  		Write-Host "Create Deployment`:  $dplyName"
  
	  	try
  	  	{
    		$dplyID = new-rsdeployment -name $dplyName
  	
  		}
  		catch
  		{
    		Write-Host "Error creating deployment"
			Write-Host "$($_)"
			Write-Host "$($_.Exception.APIHref)"
	
		    exit 1
  		}

  #add dply creds
  
  
  #add  dply inputs
  $serverList = $deployment.SERVERS.SERVER
  
  
  #new servers
  $dplyServers = $deployment.SERVERS.SERVER
 
  $newServerIDs = @()
  
  foreach($server in $dplyServers)
  {    
    $newServerName = $server.NAME
	$serverTemplate = $server.servertemplate
	
	Write-Host "Creating Server - $newServerName" 
	Write-Host "ServerTemplate - $serverTemplate"
	Write-Host "Cloud ID - $dplyCloudID"
	
	try
	{
		
      	$newServersObj = new-rsserver -servername $newServerName -deploymentid $dplyID.DeploymentID -servertemplate $serverTemplate -cloudid  $dplyCloudID
	    
		$newServerID = $newServersObj.ServerID		
		Write-Host "New Server ID - $newServerID"
		
	    #add obj to use later
		$newServerIDs += $newServerObj
		
		#build string array of input values
		$arrInputs = @()
		foreach($input in $rsDefInputs)
		{
		  $val = $input.name + ":" + $input.value		  
		  $arrInputs += $val		  
		}
		
		#set inputs
		Write-Host "Setting server input values"
		$resSetInputs = Set-RSServerInputs -serverid $newServerID -inputs $arrInputs
		
		$shouldLaunch = $server.launch
		
		if($shouldLaunch -eq $true)
		{
		  Write-Host "Launching server"
		  $resLaunch = launch-RSServer -serverID $newServerID
		  
		  Write-Host $resLaunch.Message
		}
	
	}
	catch
	{
	  Write-Host "$_"
	  Write-Host $_.errordata
	  Write-Host $_.Exception.InnerException.Message

	}
	
	
  }
  
  	#launch / delete ?
  	foreach($obj in $newServersObj)
  	{
    	Write-Host "Getting server  - $($obj.ServerID)"
		get-rsserver -serverid $obj.ServerID
  
 
  	}
  

  }
}
else
{
  Write-Host "Error connecting to RightScale"
  exit 1
}

