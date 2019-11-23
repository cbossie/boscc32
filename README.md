# boscc32
Boston Code Camp 32 Materials

#Thank you for attending my session at Boston Code Camp 32!

This repository contains the companion code for the presentation contained in the following powerpoint file:

https://github.com/cbossie/boscc32/blob/master/Boscc32%20-%20Serverless%20DotNetCore.pptx

# The 5 steps required to update your API:

1) Create serverless.template
- See Attached


2) Add Nuget Packages:
Amazon.Lambda.AspNetCoreServer
Amazon.Lambda.Core
Amazon.Lambda.RuntimeSupport

3) Create LambdaEntryPoint
-- See attached

4) Program.cs

-- In Main()
```csharp
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")))
{
	CreateHostBuilder(args).Build().Run();
}
else
{
	var lambdaEntry = new LambdaEntryPoint();
	var functionHandler = (Func<APIGatewayProxyRequest, ILambdaContext, Task<APIGatewayProxyResponse>>)(lambdaEntry.FunctionHandlerAsync);
	using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper(functionHandler, new JsonSerializer()))
	using (var bootstrap = new LambdaBootstrap(handlerWrapper))
	{
		bootstrap.RunAsync().Wait();
	}
}
```

5) Install AWS lambda tools
dotnet tool install -g Amazon.Lambda.Tools

5a) Create aws-lambda-tools-defaults.json
Important things to note:
-profile

```json
{
  "Information": [
    "This file provides default values for the deployment wizard inside Visual Studio and the AWS Lambda commands added to the .NET Core CLI.",
    "To learn more about the Lambda commands with the .NET Core CLI execute the following command at the command line in the project root directory.",
    "dotnet lambda help",
    "All the command line options for the Lambda command can be specified in this file."
  ],
  "profile": "default",
  "region": "us-east-1",
  "configuration": "Release",
  "framework": "netcoreapp3.0",
  "s3-bucket": "cbtest-dotnet-serverless-deploy",  
  "s3-prefix": "DotNetServerless-CodeCamp/",
  "template": "serverless.template",
  "template-parameters": "",
  "msbuild-parameters": "--self-contained true /p:AssemblyName=bootstrap",
  "stack-name": "DotNetServerless-CodeCamp"
} 
```

6) Optionally, if we do not indicate the assembly name as "bootstrap", then we can include the following, in a shell script named "bootstrap"

```bash
!/bin/sh
# This is the script that the Lambda host calls to start the custom runtime.

/var/task/DotNetServerless
```
