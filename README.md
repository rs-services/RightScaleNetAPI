=====
RightScale .net API Wrapper
=====

This project is focused on getting a fully-fledged API wrapper for .net built for the RightScale API (v1.5).  The first phase of release will build out an almost verbatim solution using static methods on each object to represent the 'Resources' as defined in the API while the classes themselves are more akin to 'MediaTypes'.  The first release will comprise of 3 phases:

  * Phase 1 (v0.1.0.0) is to get the core logic developed, including a singleton instance of a class containing all of the HttpClient components to persist authentication information
    * Both oAuth and username/password/accountID authentication methods will be supported
	* A limited set of classes will be implemented--check the issue list for the latest progress and how the milestones are planned
	  * Core to this release are the Server, Instance, Deployment and supporting classes
  * Phase 2 (v0.2.0.0) will fold in more classes as is appropriate based on perceived frequency of use
    * ServerArray, MultiCloudImage and other supporting classes are planned for this milestone
	* We'll push out a v0.3.0.0 revision for demo purposes and this is simply a milestone that we're adding to get us from v0.2.0.0 to a more polished set of assets
  * Phase 3 (v0.4.0.0) will round out the rest of the object model 
  * Each phase/dev release will be pushed to nuget as a -pre package
  * Release v1.0.0.0 will be planned after v0.3.0.0 has been tested in the wild
  
As of now, there is only one set of external resources (also available on nuget) in that the Newtonsoft Json.net package is used for all Json deserializtion.  This might change at some point or another, but I'm really happy with it for now, so I'm happy to leave it in place.

-----
 App.config/web.config setup
-----

While the RightScale.netClient.Core.ApiClient.Instance object has authentication methods for both OAuth and standard auth, it will also pull in your configuration information via an app.config or web.onfig depending on your project type (even works in my unit test project!).  If you opt for using your OAuth refresh key (available via the RightScale dashboard), you'll simply need to add the following key into your appSettings collection:

    <add key="RightScaleAPIRefreshToken" value="ea7..................................e3e"/>

If you opt to use your username/password/accountID, you'll need to add 3 keys into your .config file:

	<add key="RightScaleAPIUserName" value="user@email.com"/>
	<add key="RightScaleAPIPassword" value="thisisyourpassword"/>
	<add key="RightScaleAPIAccountId" value="######"/>
 
-----
 Long-term goals
-----
 
 Long term there are a ton of cool ideas we've been kicking around, but we're happy to take some more input and add to it:
 
   * building out either strong [PowerShell examples and Cmdlets]() of how to consume this library or a PowerShell wrapper to make it more PS friendly
   * [hooking this into TFS]() (actually another pet project of ours) and using it to manage continuous deployment/automated deployment
   * writing [Windows Workflow Foundation processes]() with a custom set of [workflow activities]() around this object model for longer-running, automated deployments that are on the more complicated side of things
   * LINQ-ifying it so that it's fully LINQ compatible (though, with the Lists all over the place, it's pretty close).
   
 Let us know know we're doing, if you're interested in contributing or have a cool idea for us to throw on the roadmap!
 
 Thanks,
 
 Patrick and the RightScale Windows Professional Services Team