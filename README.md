# Xamarin-Microsoft-Logging-Sample

Sample project demonstrating Logging in Xamarin using the modern `Microsoft.Extensions.Logging` library

This project currently only contains a sample for native mac (Xamarin.Mac) since that was the platform I required logging on and didn't fancy maintaining my own framework.

This sample allows for full configuration from `Microsoft.Extensions.Logging` using `appsettings.json`.

## Required Packages

The following packages will need to be installed to your project in order to get file logging working. For the iOS & Android apps you can get away with only referencing them in the shared project but the mac project must reference them.

- `Microsoft.Extensions.Logging`
- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.Logging.Console`
- `Karambolo.Extensions.Logging.File`

## Native Setup

 - Step 1.. Todo
 - Step 2.. Todo

