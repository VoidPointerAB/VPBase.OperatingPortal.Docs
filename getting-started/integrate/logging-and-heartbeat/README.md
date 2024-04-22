# Integrate with the portal using logging and heartbeats
Below is information on how connecting an client application to submit log data and heart beats to the portal.

## Swagger and Code Gen

Check out the link below for the swagger definition, json-file.

[swagger.json](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/getting-started/integrate/swagger.json)

If you don't find any sample code or similar below, you can create your own rest-client stub using the code generator below: 

[Swagger Code Gen](https://swagger.io/tools/swagger-codegen/)

## Is your application written in .NET?
Then the job is very easy for you. 

### Example .NET Core Web Example with Api
You can check out our example below (the code exists in this git repo).

[Example .NET Core Web Example with Api](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/tree/master/example-code/client-and-api/OperatingPortal.NetCore.WebExampleApi)

This example is a .NET Core sample api app that logs itself using its own rest-api, using the SAME contract as Operations and api-methods.
The advantage is that you can test logging in and receiving the result in the same ex-app, and then test the entire flow without needing of two applications.
The operations and sample api app then uses a rest api to receive the logging and heartbeat.
The application now saves the result in a static list for the logging itself and the same for the heart beats. You can access these lists via the api.
You can also trigger the logging and heartbeat using the api if you want to do this instead of coding.

#### Log4Net - Log Appender

In the example and so we ourselves log in our platform, we use log4net as a log appender to send the data to operations. 
In .NET Core this is just an extension to the regular logging.

#### Nuget packages
The example uses two nuget packages that do most of the work.

- VPBase.ACC
- VPBase.Client

![Packages](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/getting-started/integrate/logging-and-heartbeat/nuget_packages.png)

**VPBase.ACC**

VPBase.ACC or the "ACC" is an assembly that is general to use and through all versions of our platforms in VPBase, between client and server, between our various modules and applications, 
and in this case operations that contain the contracts for logging and heartbeat etc.
This is to get clean C# classes that avoid dynamic objects + code for communication and lots of other things.

**VBase.Client**
VBase.Client is an assembly that contains the actual rest-appender in log4net etc.
The code for this can be found in the public git repo:

[VPBase.Client](https://github.com/VoidPointerAB/VPBase.Client)

Here you will find the code for the rest log appender itself, which is used to communicate with operations.

#### Initializing / Start-up

At start-up, you only need to inform the client (the rest appender for logging) as its settings and then add log4net as logging (see image below).
So it's extremely easy to get started.

![Apply Setting and Log4net](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/getting-started/integrate/logging-and-heartbeat/apply_settings_and_log4net.png)

Then also what settings the log4net appender has, this through log4net.config. 

![log4net.config](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/getting-started/integrate/logging-and-heartbeat/log4net_config.png)

But it should be able to be set programatically.
Here you can see the assembly path to the appender client itself.

## Logging

## Hearbeats
