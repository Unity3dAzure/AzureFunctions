# Azure Functions for Unity
For Unity developers looking to use Azure Functions in their Unity game / app.

## External dependencies
**First download the required dependencies and extract the contents into your Unity project "Assets" folder.**
* [RESTClient](https://github.com/Unity3dAzure/RESTClient)
* [AppServices](https://github.com/Unity3dAzure/AppServices)

## How to setup Azure Functions with a new Unity project
1. [Download AppServices](https://github.com/Unity3dAzure/AppServices/archive/master.zip) and [REST Client](https://github.com/Unity3dAzure/RESTClient/archive/master.zip) for Unity.
	* Copy 'AppServices' and 'RESTClient' into project `Assets` folder.
2. Create an [Azure Function App](https://portal.azure.com)
	* Create an HTTP Trigger function.

## Azure Function demos for Unity
Try the [Azure Function Demo](https://github.com/Unity3dAzure/AzureFunctionsDemo) project for Unity 2017 on Mac / Windows. (The demo project has got everything already bundled in and does not require any additional assets to work. Just hook it up with your [Azure Function App](https://portal.azure.com) account and run it right inside the Unity Editor.)

## Minimum Requirements
Requires Unity v5.3 or greater as [UnityWebRequest](https://docs.unity3d.com/Manual/UnityWebRequest.html) and [JsonUtility](https://docs.unity3d.com/ScriptReference/JsonUtility.html) features are used. Unity will be extending platform support for UnityWebRequest so keep Unity up to date if you need to support these additional platforms.

## Supported platforms
Intended to work on all the platforms [UnityWebRequest](https://docs.unity3d.com/Manual/UnityWebRequest.html) supports including:
* Unity Editor (Mac/PC) and Standalone players
* iOS
* Android
* Windows

## Beta version
This is a work in progress so stuff may change frequently.

Questions or tweet [@deadlyfingers](https://twitter.com/deadlyfingers)
