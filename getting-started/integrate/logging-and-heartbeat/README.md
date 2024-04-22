# Integrate with the portal using logging and heartbeats
Below is information on how connecting an client application to submit log data and heart beats to the portal.

## Is your application written in .NET?
Then the job is very easy for you. 

### Example .NET Core Web Example with Api
You can check out our example below (the code exists in this git repo).
[Example .NET Core Web Example with Api](https://github.com/VoidPointerAB/VPBase.OperatingPortal.Docs/tree/master/example-code/client-and-api/OperatingPortal.NetCore.WebExampleApi)

This example is a .NET Core sample api app that logs itself using its own rest-api, using the SAME contract as Operations and api-methods.
The advantage is that you can test logging in and receiving the result in the same ex-app, and then test the entire flow without needing of two applications.
The operations and sample api app then uses a rest api to receive the logging and heartbeat, and which is also documented via Swagger.
The application now saves the result in a static list for the logging itself and the same for the heart beats. You can access these lists via the api.
You can also trigger the logging and heartbeat using the api if you want to do this instead of coding.

### Nuget packages
The example uses two nuggets that do most of the work.

- VPBase.ACC
- VPBase.Client


where the contract between server and client is already defined.



## Logging

## Hearbeats
