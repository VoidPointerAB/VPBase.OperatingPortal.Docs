# Integrate with VPBase Application Operations using Logging and Heartbeats
Below is information on how connecting an client application to submit log data and heart beats to the portal.

## Swagger and Code Gen

Check out the link below for the swagger definition, json-file.

[swagger.json](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/getting-started/integrate/swagger.json)

If you don't find any sample code or similar below, you can create your own rest-client stub using the code generator below: 

[Swagger Code Gen](https://swagger.io/tools/swagger-codegen/)

## Is your application written in .NET?
Then the job is very easy for you. 

### Full Example in .NET Core Web with Operations Api
You can check out our example below (the code exists in this git repo).

[Full Example .NET Core Web with Operations Api](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/tree/master/example-code/client-and-api/OperatingPortal.NetCore.WebExampleApi)

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

[VPBase.Client - Public Git Repo](https://github.com/VoidPointerAB/VPBase.Client)

Here you will find the code for the rest log appender itself, which is used to communicate with operations.

#### Initializing / Start-up

At start-up, you only need to inform the client (the rest appender for logging) as its settings and then add log4net as logging (see image below).
So it's extremely easy to get started.

![Apply Setting and Log4net](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/getting-started/integrate/logging-and-heartbeat/apply_settings_and_log4net.png)

[Program.cs in example](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/example-code/client-and-api/OperatingPortal.NetCore.WebExampleApi/Program.cs)

Then also what settings the log4net appender has, this through log4net.config. 

![log4net.config](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/getting-started/integrate/logging-and-heartbeat/log4net_config.png)

[log4net.config in example](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/blob/master/example-code/client-and-api/OperatingPortal.NetCore.WebExampleApi/log4net.config)

But it should be able to be set programatically.
Here you can see the assembly path to the appender client itself.

#### Easy Logging Example in .NET Core (use normal logging)

Just use normal logging in .NET Core

```c#
// Simple logging with INFO level
app.Logger.LogInformation("Example log information");

```
For more info, follow link below, Learn Microsoft.

[Logging in .NET Core and ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-8.0)

#### Easy Heartbeat Example in .NET Core

```c#
// Heartbeat example
HeartbeatClient.DefaultClient.Heartbeat("Startup Heartbeat Thread!");

```

#### Advanced Logging Example - Use scoped logging

Below is an example of scoped logging found in the example where you use full logging and can basically log anything.
To make it easier to use the same concepts in client and server, we have predefined certain properties so that these are the same between different applications.

Example is the UserId or the UserName.

The example below at the end of the scope, shows how to log custom properties with custom keys and also logging a fully serialized object in json string format.

```c#
var obj = new LogStartupExample() { Name = "Base Test Class Json Example" };
var authJsonConverter = new AuthContractJsonConverter();
var jsonData = authJsonConverter.SerializeObject(obj);

// Scope logging example
var dictionary = new Dictionary<string, object>()
{
    { LoggingKeyDefinitions.UserId, "baseUserId" },
    { LoggingKeyDefinitions.UserName, "baseUserName" },

    { LoggingKeyDefinitions.EntityId, "99999" },
    { LoggingKeyDefinitions.EntityType, (int)LoggingMessageEntityType.Customer },
    { LoggingKeyDefinitions.EntityTypeName, LoggingMessageEntityType.Customer.ToString() },
    { LoggingKeyDefinitions.EntityValueName, "Base Customer AB" },

    { LoggingKeyDefinitions.EntityId2, "88888" },
    { LoggingKeyDefinitions.EntityType2, (int)LoggingMessageEntityType.Vendor },
    { LoggingKeyDefinitions.EntityTypeName2, LoggingMessageEntityType.Vendor.ToString() },
    { LoggingKeyDefinitions.EntityValueName2, "Base Vendor AB" },

    { LoggingKeyDefinitions.AbsoluteUri, "https://voidpointer.se/admin" },
    { LoggingKeyDefinitions.RawUrl, "admin" },

    { LoggingKeyDefinitions.AdditionalInformation, "base additional info" },

    { "CustomStringKey", "baseValue" },
    { "CustomIntKey", 33 },
    { "CustomDateTimeKey", new DateTime(2023, 4, 28) },
    { "CustomBoolKey", true },
    { "CustomDecimalKey", 2.3m },
    { "CustomJsonKey", jsonData }
};

using (logger.BeginScope(dictionary))
{
    logger.LogInformation("Test logging from base server startup!");
}

```

## Logging Json Contract

Below is the actual contract for the logging object in json

```json
{
  "applicationName": "string",
  "additionalInformation": "string",
  "applicationId": "string",
  "tenantIds": [
    "string"
  ],
  "versionVPBase": 0,
  "applicationType": 0,
  "friendlyAppName": "string",
  "level": "string",
  "loggerName": "string",
  "message": "string",
  "exceptionString": "string",
  "exceptionSource": "string",
  "exceptionNumber": 0,
  "customerMessage": "string",
  "secretVPMonitorPassword": "string",
  "logMessageDate": "2024-04-22T12:07:09.547Z",
  "user": {
    "userId": "string",
    "userName": "string"
  },
  "entity": {
    "entityId": "string",
    "entityValueName": "string",
    "entityType": 0,
    "entityTypeName": "string"
  },
  "entity2": {
    "entityId": "string",
    "entityValueName": "string",
    "entityType": 0,
    "entityTypeName": "string"
  },
  "url": {
    "rawUrl": "string",
    "absoluteUri": "string"
  },
  "keyValues": [
    {
      "key": "string",
      "value": "string",
      "dataTypeFullName": "string"
    }
  ],
  "tenantThreadId": "string",
  "logMessageId": "string",
  "logMessageUtcDate": "2024-04-22T12:07:09.547Z"
}
```

Below you will find all the fields in the contact, specifying what type they are and whether it is mandatory or not, description, example and version

| Field                   | Type                | Mandatory     | Description               |  Example                                                           | Version |
| :---                    | :----:              | :----:        | :---                      |  :---                                                              | :----:  |
| applicationName         | string              | Yes           | Name of the Application   |  "Example.Development.Client"                                      | Base    |
| additionalInformation   | string              | No            | Extra Message / Data      |  "Extra log information"                                           | Base    |        
| applicationId           | string              | Yes           | Installation Id           |  "TEST_ExampleClient_Application_Development"                      | Base    |
| tenantIds               | list of strings     | Yes           | Organisation identifiers  |  "TEST_VPBase_Tenant_TestCompany"                                  | Base    |
| versionVPBase           | integer             | (Yes)         | Version of VPBase         |  0 = None, 3 = Version 3, 4 = Version 4, 5 = Version 5             | Base    |
| applicationType         | integer             | (Yes)         | Application Type          |  0 = Undefined, 1 = Web, 2 = Mobile, 3 = Printer, 4 = PalletScale  | Base    |
| friendlyAppName         | string              | Yes           | Friendly Name             |  "ExampleClient"                                                   | Base    |
| level                   | string              | Yes           | Log Level                 |  "DEBUG", "INFO", "WARN", "ERROR", "FATAL"                         | Base    |
| loggerName              | string              | Yes           | Logger Name               |  "Example.Logger"                                                  | Base    |
| message                 | string              | Yes           | Message                   |  "Example log information"                                         | Base    |

## Heartbeat Json Contract

Below is the actual contract for the heartbeat object in json

```json
{
  "applicationName": "string",
  "additionalInformation": "string",
  "applicationId": "string",
  "tenantIds": [ "string" ],
  "versionVPBase": 0,
  "applicationType": 0,
  "friendlyAppName": "string",
  "secretVPMonitorPassword": "string",
  "identifier": "string",
  "hearbeatMessageDate": "2024-04-22T12:09:00.329Z",
  "intervalInSeconds": 0,
  "hearbeatMessageUtcDate": "2024-04-22T12:09:00.329Z",
  "hearbeatId": "string"
}
```

Below you will find all the fields in the contact, specifying what type they are and whether it is mandatory or not, description, example and version

| Field                   | Type                | Mandatory     | Description               |  Example                                                           | Version |
| :---                    | :----:              | :----:        | :---                      |  :---                                                              | :----:  |
| applicationName         | string              | Yes           | Name of the Application   |  "Example.Development.Client"                                      | Base    |
| additionalInformation   | string              | Yes           | Message / Data            |  "Startup Heartbeat Thread!"                                       | Base    |        
| applicationId           | string              | Yes           | Installation Id           |  "TEST_ExampleClient_Application_Development"                      | Base    |
| tenantIds               | list of strings     | Yes           | Organisation identifiers  |  "TEST_VPBase_Tenant_TestCompany"                                  | Base    |
| versionVPBase           | integer             | (Yes)         | Version of VPBase         |  0 = None, 3 = Version 3, 4 = Version 4, 5 = Version 5             | Base    |
| applicationType         | integer             | (Yes)         | Application Type          |  0 = Undefined, 1 = Web, 2 = Mobile, 3 = Printer, 4 = PalletScale  | Base    |
| friendlyAppName         | string              | Yes           | Friendly Name             |  "ExampleClient"                                                   | Base    |
| secretVPMonitorPassword | string              | No            | Api Key                   |  "secretKey1234"                                                   | Base    |
| identifier              | string              | No            | Identifier                |  "CommandQueueTask"                                                | Extra   |
| hearbeatMessageDate     | datetime            | Yes           | Client Local Date Time    |  "2024-04-22T12:09:00.329Z"                                        | Base    |
| intervalInSeconds       | integer             | (Yes)         | Freq. of hearbeats in sec |  120, 0 = Not definied                                             | Extra   |
| hearbeatMessageUtcDate  | datetime            | Yes           | Client Utc Date Time      |  "2024-04-22T12:09:00.329Z"                                        | Base    |
| hearbeatId              | string              | By Server     | Created Server Id         |  "1024"                                                            | Base    |
