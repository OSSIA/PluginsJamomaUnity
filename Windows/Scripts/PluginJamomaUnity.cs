using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class PluginJamomaUnity : MonoBehaviour {
	
	/** Callback function which allows informing the mocification of value of a data */
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void ValueCallback();


	/* ================================================================================================================================================

	DECLARE THE FUNCTIONS FROM THE C NATIVE PLUGIN, MUST NOT USE DIRECTLY THESES FUNCTIONS

	================================================================================================================================================ */


	/** Start i-score */
	[DllImport ("PluginJamomaUnity")]
	public static extern void StartIscorePlugin();
	
	/** Stop i-score */
	[DllImport ("PluginJamomaUnity")]
	public static extern void StopIscorePlugin();
	
	/** Get the name of the current event during the scenario execution
     @return Name of the current event during the scenario execution */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetEventNamePlugin();
	
	/** Get the status of the current event during the scenario execution
     @return Status of the current event during the scenario execution */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetEventStatusPlugin();
	
	/** Init the Jamoma Modular library
     @param[in] folderPath Folder path where all the dylibs are */
	[DllImport ("PluginJamomaUnity")]
	public static extern void InitModularLibraryPlugin(string folderPath);
	
	/** Create an application manager */
	[DllImport ("PluginJamomaUnity")]
	public static extern void CreateApplicationManagerPlugin();
	
	/** Create a local application, a distant application and register them to the application manager
     @param[in] localAppName Name of the local application
     @param[in] distantAppName Name of the distant application
     @return 0: Cannot create the local application
     @return 1: Cannot create the distant application
     @return 2: Create the local and distant applications and register them successfully */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateAndRegisterApplicationsPlugin(string localAppName, string distantAppName);
	
	/** Create a Minuit protocol unit
     @return 0: Cannot create the Minuit protocol unit
     @return 1: Create successfully the Minuit protocol unit */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateMinuitProtocolUnitPlugin(); 
	
	/** Register the local and distant applications to the Minuit protocol
	 @param[in] localAppName Name of the local application
	 @param[in] distantAppName Name of the distant application
	 @param[in] portLocal Port of the local application
	 @param[in] ipLocal IP adress of the local application
	 @param[in] portDistant Port of the distant application
	 @param[in] ipDistant IP adress of the distant application */
	[DllImport ("PluginJamomaUnity")]
	public static extern void RegisterApplicationsToMinuitPlugin(string localAppName, string distantAppName, int portLocal, string ipLocal, int portDistant, string ipDistant);
	
	/** Run the Minuit protocol */
	[DllImport ("PluginJamomaUnity")]
	public static extern void RunMinuitProtocolPlugin();
	
	/** Set the author's name of the local application
     @param[in] authorName The author's name */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetAuthorNamePlugin(string authorName);
	
	/** Get the author's name of the local application
     @return Author's name of the local application */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetAuthorNamePlugin();
	
	/** Set the version name of the local application
     @param[in] versionName The version name of the local appliation */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetVersionPlugin(string versionName);
	
	/** Get the version name of the local application
     @return Version name of the local application */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetVersionPlugin();
	
	/** Create a parameter data (type "integer" or "decimal") at an address (without the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] type "type" attribute of this parameter
     @param[in] rangeBoundMin Min bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeBoundMax Max bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateNumberParameterDataPlugin(string paramaterAddress, string type, float rangeBoundMin, float rangeBoundMax, string rangeClipmode, string rampDrive, string description);
	
	/** Create a parameter data (type "integer" or "decimal") at an address (with the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] type "type" attribute of this parameter
     @param[in] rangeBoundMin Min bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeBoundMax Max bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @param[in] callbackPointer The poiter to the callback function corresponding to this parameter
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateNumberParameterDataPluginWithCallbackUnity(string paramaterAddress, string type, float rangeBoundMin, float rangeBoundMax, string rangeClipmode, string rampDrive, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer);
	
	/** Create a parameter data (type "boolean") at an address (without the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateBooleanParameterDataPlugin(string paramaterAddress, string rangeClipmode, string rampDrive, string description);
	
	/** Create a parameter data (type "boolean") at an address (with the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @param[in] callbackPointer The poiter to the callback function corresponding to this parameter
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateBooleanParameterDataPluginWithCallbackUnity(string paramaterAddress, string rangeClipmode, string rampDrive, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer);
	
	/** Create a parameter data (type "string") at an address (without the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateStringParameterDataPlugin(string paramaterAddress, string rangeClipmode, string rampDrive, string description);
	
	/** Create a parameter data (type "string") at an address (with the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @param[in] callbackPointer The poiter to the callback function corresponding to this parameter
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateStringParameterDataPluginWithCallbackUnity(string paramaterAddress, string rangeClipmode, string rampDrive, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer);
	
	/** Create a return data at an address (without the callback function in Unity)
     @param[in] returnAddress Address of this return registered in the local application
     @param[in] type "type" attribute of this return (type is "decimal" or "integer" or "boolean")
     @param[in] description "description" attribute of this return
     @return 0: Cannot create the return data at this address
     @return 1: Create successfully the return data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateReturnDataPlugin(string returnAddress, string type, string description);
	
	/** Create a return data at an address (with the callback function in Unity)
     @param[in] returnAddress Address of this return registered in the local application
     @param[in] type "type" attribute of this return (type is "decimal" or "integer" or "boolean")
     @param[in] description "description" attribute of this return
     @param[in] callbackPointer The poiter to the callback function corresponding to this return
     @return 0: Cannot create the return data at this address
     @return 1: Create successfully the return data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateReturnDataPluginWithCallbackUnity(string returnAddress, string type, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer);
	
	/** Create a message data at an address (without the callback function in Unity)
     @param[in] messageAddress Address of this message registered in the local application
     @param[in] description "description" attribute of this message
     @return 0: Cannot create the message data at this address
     @return 1: Create successfully the message data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateMessageDataPlugin(string messageAddress, string description);
	
	/** Create a message data at an address (with the callback function in Unity)
     @param[in] messageAddress Address of this message registered in the local application
     @param[in] description "description" attribute of this message
     @param[in] callbackPointer The poiter to the callback function corresponding to this message
     @return 0: Cannot create the message data at this address
     @return 1: Create successfully the message data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int CreateMessageDataPluginWithCallbackUnity(string messageAddress, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer);
	
	/** Setup the control of i-score from an external Tick message */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetupMessageTickPlugin();
	
	/** Send a Tick message to i-score to control it */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SendMessageTickPlugin();
	
	/** Init the Score library
     @param[in] folderPath Folder path where all the dylibs are */
	[DllImport ("PluginJamomaUnity")]
	public static extern void InitScoreLibraryPlugin(string folderPath);
	
	/** Run a scenario
     @param[in] pathName Name of the path leading to the scenario file
     @param[in] speed Execution speed of the scenario */
	[DllImport ("PluginJamomaUnity")]
	public static extern void RunScenarioPlugin(string pathName, float speed);
	
	/** Stop the current scenario */
	[DllImport ("PluginJamomaUnity")]
	public static extern void StopScenarioPlugin();
	
	/** Set a new value for an attribute of a parameter (only applied to "type, rangeClipmode, rampDrive, description" attributes)
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @param[in] nameAttribute Name of this attribute
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetAttributeParameterPlugin(int indexParameter, string nameAttribute, string newValue);
	
	/** Get the current value of an attribute of a parameter (only applied to "type, rangeClipmode, rampDrive, description" attributes)
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @param[in] nameAttribute Name of this attribute
     @return Current value of this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetAttributeParameterPlugin(int indexParameter, string nameAttribute);
	
	/** Set a new value for the "rangeBounds" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @param[in] rangeBoundMin Min bound of the "rangeBounds" attribute of the new value
     @param[in] rangeBoundMax Max bound of the "rangeBounds" attribute of the new value */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetRangeBoundsParameterPlugin(int indexParameter, float rangeBoundMin, float rangeBoundMax);
	
	/** Get the current value of the min bound of the "rangeBounds" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @return Current value of the min bound of the "rangeBounds" attribute of this parameter */
	[DllImport ("PluginJamomaUnity")]
	public static extern float GetMinRangeBoundParameterPlugin(int indexParameter);
	
	/** Get the current value of the max bound of the "rangeBounds" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @return Current value of the max bound of the "rangeBounds" attribute of this parameter */
	[DllImport ("PluginJamomaUnity")]
	public static extern float GetMaxRangeBoundParameterPlugin(int indexParameter);
	
	/** Set a new value (type "decimal") for the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetValueFloatParameterPlugin(int indexParameter, float newValue);
	
	/** Set a new value (type "integer") for the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetValueIntegerParameterPlugin(int indexParameter, int newValue);
	
	/** Set a new value (type "boolean") for the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetValueBooleanParameterPlugin(int indexParameter, bool newValue);
	
	/** Set a new value (type "string") for the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetValueStringParameterPlugin(int indexParameter, string newValue);
	
	/** Get the current value (type "decimal") of the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @return Current value of the "value" attribute of this parameter */
	[DllImport ("PluginJamomaUnity")]
	public static extern float GetValueFloatParameterPlugin(int indexParameter);
	
	/** Get the current value (type "integer") of the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @return Current value of the "value" attribute of this parameter */
	[DllImport ("PluginJamomaUnity")]
	public static extern int GetValueIntegerParameterPlugin(int indexParameter);
	
	/** Get the current value (type "boolean") of the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @return Current value of the "value" attribute of this parameter */
	[DllImport ("PluginJamomaUnity")]
	public static extern bool GetValueBooleanParameterPlugin(int indexParameter);
	
	/** Get the current value (type "string") of the "value" attribute of a parameter
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @return Current value of the "value" attribute of this parameter */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetValueStringParameterPlugin(int indexParameter);
	
	/** Set a new value for an attribute of a return (only applied to "type, description" attributes)
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @param[in] nameAttribute Name of this attribute
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetAttributeReturnPlugin(int indexReturn, string nameAttribute, string newValue);
	
	/** Get the current value of an attribute of a return (only applied to "type, description" attributes)
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @param[in] nameAttribute Name of this attribute
     @return Current value of this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetAttributeReturnPlugin(int indexReturn, string nameAttribute);
	
	/** Set a new value (type "decimal") for the "value" attribute of a return
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetValueFloatReturnPlugin(int indexReturn, float newValue);
	
	/** Set a new value (type "integer") for the "value" attribute of a return
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetValueIntegerReturnPlugin(int indexReturn, int newValue);
	
	/** Set a new value (type "boolean") for the "value" attribute of a return
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetValueBooleanReturnPlugin(int indexReturn, bool newValue);
	
	/** Get the current value (type "decimal") of the "value" attribute of a return
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @return Current value of the "value" attribute of this return */
	[DllImport ("PluginJamomaUnity")]
	public static extern float GetValueFloatReturnPlugin(int indexReturn);
	
	/** Get the current value (type "integer") of the "value" attribute of a return
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @return Current value of the "value" attribute of this return */
	[DllImport ("PluginJamomaUnity")]
	public static extern int GetValueIntegerReturnPlugin(int indexReturn);
	
	/** Get the current value (type "boolean") of the "value" attribute of a return
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @return Current value of the "value" attribute of this return */
	[DllImport ("PluginJamomaUnity")]
	public static extern bool GetValueBooleanReturnPlugin(int indexReturn);
	
	/** Set a new value for the "description" attribute of a message
     @param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
     @param[in] newValue The new value set to this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetDescriptionMessagePlugin(int indexMessage, string newValue);
	
	/** Get the current value of the "description" attribute of a message
     @param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
     @return Current value of this attribute */
	[DllImport ("PluginJamomaUnity")]
	public static extern IntPtr GetDescriptionMessagePlugin(int indexMessage);
	
	/** Set a message
     @param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
     @param[in] message Content of the message */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SetMessagePlugin(int indexMessage, string message);
	
	/** Save the namespace of the local applicatioin in a XML file
     @param[in] filePath Path leading to this file */
	[DllImport ("PluginJamomaUnity")]
	public static extern void SaveToXMLPlugin(string filePath);
	
	/** Unregister the parameter data at an address in the local application
     @param[in] paramaterAddress Address of this parameter in the local application
     @param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
     @return 0: Cannot unregister the parameter data at this address
     @return 1: Unregister successfully the parameter data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int UnregisterParameterDataPlugin(string paramaterAddress, int indexParameter);
	
	/** Unregister the return data at an address in the local application
     @param[in] returnAddress Address of this return in the local application
     @param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
     @return 0: Cannot unregister the return data at this address
     @return 1: Unregister successfully the return data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int UnregisterReturnDataPlugin(string returnAddress, int indexReturn);
	
	/** Unregister the message data at an address in the local application
     @param[in] messageAddress Address of this message in the local application
     @param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
     @return 0: Cannot unregister the message data at this address
     @return 1: Unregister successfully the message data at this address */
	[DllImport ("PluginJamomaUnity")]
	public static extern int UnregisterMessageDataPlugin(string messageAddress, int indexMessage);
	
	/** Release the Minuit protocol */
	[DllImport ("PluginJamomaUnity")]
	public static extern void ReleaseMinuitProtocolPlugin();
	
	/** Release the local and distant applications
     @param[in] localAppName Name of the local application
     @param[in] distantAppName Name of the distant application */
	[DllImport ("PluginJamomaUnity")]
	public static extern void ReleaseApplicationsPlugin(string localAppName, string distantAppName);
	
	
	
	/* ================================================================================================================================================

	USE THE FOLLOWING FUNCTIONS TO BUILD VIDEO GAMES

	================================================================================================================================================ */
	
	
	/** List of addresses which allows managing the addresses of the parameters declared in Unity */
	public static List<string> addressParameterList = new List<string>();
	
	/** List of addresses which allows managing the addresses of the returns declared in Unity */
	public static List<string> addressReturnList = new List<string> ();
	
	/** List of addresses which allows managing the addresses of the messages declared in Unity */
	public static List<string> addressMessageList = new List<string> ();
	
	/** Name of the local application (unity) */
	public static string localApplicationName = "";
	
	/** Name of the distant application (i-score) */
	public static string distantApplicationName = "";
	
	/** If the game video runs a scenario file, runScenario = true; if not, runScenario = false */
	public static bool runScenario = false;
	
	/** This function checks if an address has been declared in Unity (either in the parameter address list or in the return address list or in the message address list)
     @param[in] address Address to check
     @return FALSE: This address is not declared yet in Unity
     @return TRUE: This address has been declared in Unity */
	public static bool IsAddressDeclared (string address)
	{
		for (int i = 0; i < addressParameterList.Count; i++)
		{
			if (addressParameterList[i].Equals(address))
			{
				return true;
			}
		}
		
		for (int i = 0; i < addressReturnList.Count; i++)
		{
			if (addressReturnList[i].Equals(address))
			{
				return true;
			}
		}
		
		for (int i = 0; i < addressMessageList.Count; i++)
		{
			if (addressMessageList[i].Equals(address))
			{
				return true;
			}
		}
		
		return false;
	}
	
	/** This function checks if an address is in the list of addresses of the parameters in Unity
     @param[in] paramaterAddress Address of the parameter to check
     @return FALSE: This address is not in the list of addresses of the parameters in Unity
     @return TRUE: This address is in the list of addresses of the parameters in Unity */
	public static bool HaveParameterAddress (string paramaterAddress)
	{
		for (int i = 0; i < addressParameterList.Count; i++)
		{
			if (addressParameterList[i].Equals(paramaterAddress))
			{
				return true;
			}
		}
		
		return false;
	}
	
	/** This function checks if an address is in the list of addresses of the returns in Unity
     @param[in] returnAddress Address of the return to check
     @return FALSE: This address is not in the list of addresses of the returns in Unity
     @return TRUE: This address is in the list of addresses of the returns in Unity */
	public static bool HaveReturnAddress (string returnAddress)
	{
		for (int i = 0; i < addressReturnList.Count; i++)
		{
			if (addressReturnList[i].Equals(returnAddress))
			{
				return true;
			}
		}
		
		return false;
	}
	
	/** This function checks if an address is in the list of addresses of the messages in Unity
     @param[in] messageAddress Address of the message to check
     @return FALSE: This address is not in the list of addresses of the messages in Unity
     @return TRUE: This address is in the list of addresses of the messages in Unity */
	public static bool HaveMessageAddress (string messageAddress)
	{
		for (int i = 0; i < addressMessageList.Count; i++)
		{
			if (addressMessageList[i].Equals(messageAddress))
			{
				return true;
			}
		}
		
		return false;
	}
	
	/** This function returns the index of an address of a parameter in the list of addresses in Unity (index = 0, 1, 2,...)
     @param[in] paramaterAddress Address of the parameter whose index returned
     @return Index corresponding to this address in the list of addresses in Unity (if "paramaterAddress" is not in the list of addresses in Unity then this function returns -1) */
	public static int ParameterAddressIndexInList (string paramaterAddress)
	{
		for (int i = 0; i < addressParameterList.Count; i++)
		{
			if (addressParameterList[i].Equals(paramaterAddress))
			{
				return i;
			}
		}
		
		return -1;
	}
	
	/** This function returns the index of an address of a return in the list of addresses in Unity (index = 0, 1, 2,...)
     @param[in] returnAddress Address of the return whose index returned
     @return Index corresponding to this address in the list of addresses in Unity (if "returnAddress" is not in the list of addresses in Unity then this function returns -1) */
	public static int ReturnAddressIndexInList (string returnAddress)
	{
		for (int i = 0; i < addressReturnList.Count; i++)
		{
			if (addressReturnList[i].Equals(returnAddress))
			{
				return i;
			}
		}
		
		return -1;
	}
	
	/** This function returns the index of an address of a message in the list of addresses in Unity (index = 0, 1, 2,...)
     @param[in] messageAddress Address of the message whose index returned
     @return Index corresponding to this address in the list of addresses in Unity (if "messageAddress" is not in the list of addresses in Unity then this function returns -1) */
	public static int MessageAddressIndexInList (string messageAddress)
	{
		for (int i = 0; i < addressMessageList.Count; i++)
		{
			if (addressMessageList[i].Equals(messageAddress))
			{
				return i;
			}
		}
		
		return -1;
	}
	
	/** This function checks if the execution of the current scenario file finished
     @return FALSE: The execution of the current scenario file does not finish yet
     @return TRUE: The execution of the current scenario file finished */
	public static bool ScenarioFileExecutionFinished ()
	{
		string eventName = Marshal.PtrToStringAnsi (GetEventName ());
		string eventStatus = Marshal.PtrToStringAnsi (GetEventStatus ());
		
		if (eventName.Equals("end") && eventStatus.Equals("eventHappened"))
		{
			return true;
		}
		
		return false;
	}
	
	/** Start i-score */
	public static void StartIscore()
	{
		StartIscorePlugin();
	}
	
	/** Stop i-score */
	public static void StopIscore()
	{
		StopIscorePlugin();
	}
	
	/** Get the name of the current event during the scenario execution
     @return Name of the current event during the scenario execution */
	public static IntPtr GetEventName()
	{
		return GetEventNamePlugin();
	}
	
	/** Get the status of the current event during the scenario execution
     @return Status of the current event during the scenario execution */
	public static IntPtr GetEventStatus()
	{
		return GetEventStatusPlugin();
	}
	
	/** Init the Jamoma Modular library
     @param[in] folderPath Folder path where all the dylibs are */
	public static void InitModularLibrary(string folderPath)
	{
		InitModularLibraryPlugin(folderPath);
	}
	
	/** Create an application manager */
	public static void CreateApplicationManager()
	{
		CreateApplicationManagerPlugin();
	}
	
	/** Create a local application (Unity), a distant application (i-score) and register them to the application manager
     @param[in] localAppName Name of the local application (Unity)
     @param[in] distantAppName Name of the distant application (i-score)
     @return 0: Cannot create the local application
     @return 1: Cannot create the distant application
     @return 2: Create the local and distant applications and register them successfully */
	public static int CreateAndRegisterApplications(string localAppName, string distantAppName)
	{
		return CreateAndRegisterApplicationsPlugin(localAppName, distantAppName);
	}
	
	/** Create a Minuit protocol unit
     @return 0: Cannot create the Minuit protocol unit
     @return 1: Create successfully the Minuit protocol unit */
	public static int CreateMinuitProtocolUnit()
	{
		return CreateMinuitProtocolUnitPlugin();
	}
	
	/** Register the local and distant applications to the Minuit protocol
	 @param[in] localAppName Name of the local application
	 @param[in] distantAppName Name of the distant application
	 @param[in] portLocal Port of the local application
	 @param[in] ipLocal IP adress of the local application
	 @param[in] portDistant Port of the distant application
	 @param[in] ipDistant IP adress of the distant application */
	public static void RegisterApplicationsToMinuit(string localAppName, string distantAppName, int portLocal, string ipLocal, int portDistant, string ipDistant)
	{
		RegisterApplicationsToMinuitPlugin(localAppName, distantAppName, portLocal, ipLocal, portDistant, ipDistant);
	}
	
	/** Run the Minuit protocol */
	public static void RunMinuitProtocol()
	{
		RunMinuitProtocolPlugin();
	}
	
	/** Set the author's name of the local application
     @param[in] authorName The author's name */
	public static void SetAuthorName(string authorName)
	{
		SetAuthorNamePlugin(authorName);
	}
	
	/** Get the author's name of the local application
     @return Author's name of the local application */
	public static IntPtr GetAuthorName()
	{
		return GetAuthorNamePlugin();
	}
	
	/** Set the version name of the local application
     @param[in] versionName The version name of the local appliation */
	public static void SetVersion(string versionName)
	{
		SetVersionPlugin(versionName);
	}
	
	/** Get the version name of the local application
     @return Version name of the local application */
	public static IntPtr GetVersion()
	{
		return GetVersionPlugin();
	}
	
	/** Create a parameter data (type "integer" or "decimal") at an address (without the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] type "type" attribute of this parameter
     @param[in] rangeBoundMin Min bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeBoundMax Max bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @return -1: Cannot create the parameter data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	public static int CreateParameterData(string paramaterAddress, string type, float rangeBoundMin, float rangeBoundMax, string rangeClipmode, string rampDrive, string description)
	{
		if (IsAddressDeclared(paramaterAddress))
		{
			return -1;
		}
		else
		{
			if (CreateNumberParameterDataPlugin(paramaterAddress, type, rangeBoundMin, rangeBoundMax, rangeClipmode, rampDrive, description) == 0)
			{
				return 0;
			}
			else
			{
				// Add the address of this parameter in the list of addresses
				addressParameterList.Add (paramaterAddress);
				
				return 1;
			}
		}
	}
	
	/** Create a parameter data (type "integer" or "decimal") at an address (with the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] type "type" attribute of this parameter
     @param[in] rangeBoundMin Min bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeBoundMax Max bound of the "rangeBounds" attribute of this parameter
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @param[in] callbackPointer The poiter to the callback function corresponding to this parameter
     @return -1: Cannot create the parameter data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	public static int CreateParameterData(string paramaterAddress, string type, float rangeBoundMin, float rangeBoundMax, string rangeClipmode, string rampDrive, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer)
	{
		if (IsAddressDeclared(paramaterAddress))
		{
			return -1;
		}
		else
		{
			if (CreateNumberParameterDataPluginWithCallbackUnity(paramaterAddress, type, rangeBoundMin, rangeBoundMax, rangeClipmode, rampDrive, description, callbackPointer) == 0)
			{
				return 0;
			}
			else
			{
				// Add the address of this parameter in the list of addresses
				addressParameterList.Add (paramaterAddress);
				
				return 1;
			}
		}
	}
	
	/** Create a parameter data (type "boolean" or "string") at an address (without the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] type "type" attribute of this parameter
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @return -1: Cannot create the parameter data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	public static int CreateParameterData(string paramaterAddress, string type, string rangeClipmode, string rampDrive, string description)
	{
		if (IsAddressDeclared(paramaterAddress))
		{
			return -1;
		}
		else
		{
			if (type.Equals("boolean"))
			{
				if (CreateBooleanParameterDataPlugin(paramaterAddress, rangeClipmode, rampDrive, description) == 0)
				{
					return 0;
				}
				else
				{
					// Add the address of this parameter in the list of addresses
					addressParameterList.Add (paramaterAddress);
					
					return 1;
				}
			}
			else // type is "string"
			{
				if (CreateStringParameterDataPlugin(paramaterAddress, rangeClipmode, rampDrive, description) == 0)
				{
					return 0;
				}
				else
				{
					// Add the address of this parameter in the list of addresses
					addressParameterList.Add (paramaterAddress);
					
					return 1;
				}
			}
		}
	}
	
	/** Create a parameter data (type "boolean" or "string") at an address (with the callback function in Unity)
     @param[in] paramaterAddress Address of this parameter registered in the local application
     @param[in] type "type" attribute of this parameter
     @param[in] rangeClipmode "rangeClipmode" attribute of this parameter
     @param[in] rampDrive "rampDrive" attribute of this parameter
     @param[in] description "description" attribute of this parameter
     @param[in] callbackPointer The poiter to the callback function corresponding to this parameter
     @return -1: Cannot create the parameter data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the parameter data at this address
     @return 1: Create successfully the parameter data at this address */
	public static int CreateParameterData(string paramaterAddress, string type, string rangeClipmode, string rampDrive, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer)
	{
		if (IsAddressDeclared(paramaterAddress))
		{
			return -1;
		}
		else
		{
			if (type.Equals("boolean"))
			{
				if (CreateBooleanParameterDataPluginWithCallbackUnity(paramaterAddress, rangeClipmode, rampDrive, description, callbackPointer) == 0)
				{
					return 0;
				}
				else
				{
					// Add the address of this parameter in the list of addresses
					addressParameterList.Add (paramaterAddress);
					
					return 1;
				}
			}
			else // type is "string"
			{
				if (CreateStringParameterDataPluginWithCallbackUnity(paramaterAddress, rangeClipmode, rampDrive, description, callbackPointer) == 0)
				{
					return 0;
				}
				else
				{
					// Add the address of this parameter in the list of addresses
					addressParameterList.Add (paramaterAddress);
					
					return 1;
				}
			}
		}
	}
	
	/** Create a return data at an address (without the callback function in Unity)
     @param[in] returnAddress Address of this return registered in the local application
     @param[in] type "type" attribute of this return (type is "decimal" or "integer" or "boolean")
     @param[in] description "description" attribute of this return
     @return -1: Cannot create the return data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the return data at this address
     @return 1: Create successfully the return data at this address */
	public static int CreateReturnData(string returnAddress, string type, string description)
	{	
		if (IsAddressDeclared(returnAddress))
		{
			return -1;
		}
		else
		{
			if (CreateReturnDataPlugin(returnAddress, type, description) == 0)
			{
				return 0;
			}
			else
			{
				// Add the address of this return in the list of addresses
				addressReturnList.Add (returnAddress);
				
				return 1;
			}
		}
	}
	
	/** Create a return data at an address (with the callback function in Unity)
     @param[in] returnAddress Address of this return registered in the local application
     @param[in] type "type" attribute of this return (type is "decimal" or "integer" or "boolean")
     @param[in] description "description" attribute of this return
     @param[in] callbackPointer The poiter to the callback function corresponding to this return
     @return -1: Cannot create the return data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the return data at this address
     @return 1: Create successfully the return data at this address */
	public static int CreateReturnData(string returnAddress, string type, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer)
	{	
		if (IsAddressDeclared(returnAddress))
		{
			return -1;
		}
		else
		{
			if (CreateReturnDataPluginWithCallbackUnity(returnAddress, type, description, callbackPointer) == 0)
			{
				return 0;
			}
			else
			{
				// Add the address of this return in the list of addresses
				addressReturnList.Add (returnAddress);
				
				return 1;
			}
		}
	}
	
	/** Create a message data at an address (without the callback function in Unity)
     @param[in] messageAddress Address of this message registered in the local application
     @param[in] description "description" attribute of this message
     @return -1: Cannot create the message data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the message data at this address
     @return 1: Create successfully the message data at this address */
	public static int CreateMessageData(string messageAddress, string description)
	{		
		if (IsAddressDeclared(messageAddress))
		{
			return -1;
		}
		else
		{
			if (CreateMessageDataPlugin(messageAddress, description) == 0)
			{
				return 0;
			}
			else
			{
				// Add the address of this message in the list of addresses
				addressMessageList.Add (messageAddress);
				
				return 1;
			}
		}
	}
	
	/** Create a message data at an address (with the callback function in Unity)
     @param[in] messageAddress Address of this message registered in the local application
     @param[in] description "description" attribute of this message
     @param[in] callbackPointer The poiter to the callback function corresponding to this message
     @return -1: Cannot create the message data because this address has been declared in the list of addresses in Unity
     @return 0: Cannot create the message data at this address
     @return 1: Create successfully the message data at this address */
	public static int CreateMessageData(string messageAddress, string description, [MarshalAs(UnmanagedType.FunctionPtr)] ValueCallback callbackPointer)
	{	
		if (IsAddressDeclared(messageAddress))
		{
			return -1;
		}
		else
		{
			if (CreateMessageDataPluginWithCallbackUnity(messageAddress, description, callbackPointer) == 0)
			{
				return 0;
			}
			else
			{
				// Add the address of this message in the list of addresses
				addressMessageList.Add (messageAddress);
				
				return 1;
			}
		}
	}
	
	/** Setup the control of i-score from an external Tick message */
	public static void SetupMessageTick()
	{
		SetupMessageTickPlugin ();
	}
	
	/** Send a Tick message to i-score to control it */
	public static void SendMessageTick()
	{
		SendMessageTickPlugin ();
	}
	
	/** Init the Score library
     @param[in] folderPath Folder path where all the dylibs are */
	public static void InitScoreLibrary(string folderPath)
	{
		InitScoreLibraryPlugin(folderPath);
	}
	
	/** Run a scenario
     @param[in] pathName Name of the path leading to the scenario file
     @param[in] speed Execution speed of the scenario */
	public static void RunScenario(string pathName, float speed)
	{
		runScenario = true;
		
		RunScenarioPlugin(pathName, speed);
	}
	
	/** Stop the current scenario */
	public static void StopScenario()
	{
		StopScenarioPlugin();
	}
	
	/** Set a new value for an attribute of a parameter (only applied to "type, rangeClipmode, rampDrive, description" attributes)
     @param[in] paramaterAddress Address of this parameter in Unity
     @param[in] nameAttribute Name of this attribute
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "paramaterAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetParameter(string paramaterAddress, string nameAttribute, string newValue)
	{
		int index = ParameterAddressIndexInList (paramaterAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetAttributeParameterPlugin(index, nameAttribute, newValue);
			
			return true;
		}
	}
	
	/** Get the current value of an attribute of a parameter (only applied to "type, rangeClipmode, rampDrive, description" attributes)
     @param[in] paramaterAddress Address of this parameter in Unity
     @param[in] nameAttribute Name of this attribute
     @return Current value of this attribute (before using this function, you should check if "paramaterAddress" is in the list of addresses in Unity by using the "HaveParameterAddress (paramaterAddress)" function) */
	public static IntPtr GetParameter(string paramaterAddress, string nameAttribute)
	{
		return GetAttributeParameterPlugin(ParameterAddressIndexInList (paramaterAddress), nameAttribute);
	}
	
	/** Set a new value for the "rangeBounds" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @param[in] rangeBoundMin Min bound of the "rangeBounds" attribute of the new value
     @param[in] rangeBoundMax Max bound of the "rangeBounds" attribute of the new value 
	 @return FALSE: Cannot set the new value to this attribute because "paramaterAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetRangeBoundsParameter(string paramaterAddress, float rangeBoundMin, float rangeBoundMax)
	{
		int index = ParameterAddressIndexInList (paramaterAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetRangeBoundsParameterPlugin(index, rangeBoundMin, rangeBoundMax);
			
			return true;
		}
	}
	
	/** Get the current value of the min bound of the "rangeBounds" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @return Current value of the min bound of the "rangeBounds" attribute of this parameter (before using this function, you should check if "paramaterAddress" is in the list of addresses in Unity by using the "HaveParameterAddress (paramaterAddress)" function) */
	public static float GetMinRangeBoundParameter(string paramaterAddress)
	{
		return GetMinRangeBoundParameterPlugin(ParameterAddressIndexInList (paramaterAddress));
	}
	
	/** Get the current value of the max bound of the "rangeBounds" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @return Current value of the max bound of the "rangeBounds" attribute of this parameter (before using this function, you should check if "paramaterAddress" is in the list of addresses in Unity by using the "HaveParameterAddress (paramaterAddress)" function) */
	public static float GetMaxRangeBoundParameter(string paramaterAddress)
	{
		return GetMaxRangeBoundParameterPlugin(ParameterAddressIndexInList (paramaterAddress));
	}
	
	/** Set a new value (type "decimal") for the "value" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "paramaterAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetParameter(string paramaterAddress, float newValue)
	{
		int index = ParameterAddressIndexInList (paramaterAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetValueFloatParameterPlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Set a new value (type "integer") for the "value" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "paramaterAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetParameter(string paramaterAddress, int newValue)
	{
		int index = ParameterAddressIndexInList (paramaterAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetValueIntegerParameterPlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Set a new value (type "boolean") for the "value" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "paramaterAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetParameter(string paramaterAddress, bool newValue)
	{
		int index = ParameterAddressIndexInList (paramaterAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetValueBooleanParameterPlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Set a new value (type "string") for the "value" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "paramaterAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetParameter(string paramaterAddress, string newValue)
	{
		int index = ParameterAddressIndexInList (paramaterAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetValueStringParameterPlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Get the current value (type "decimal") of the "value" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @return Current value of the "value" attribute of this parameter (before using this function, you should check if "paramaterAddress" is in the list of addresses in Unity by using the "HaveParameterAddress (paramaterAddress)" function) */
	public static float GetFloatParameter(string paramaterAddress)
	{
		return GetValueFloatParameterPlugin(ParameterAddressIndexInList (paramaterAddress));
	}
	
	/** Get the current value (type "integer") of the "value" attribute of a parameter
	 @param[in] paramaterAddress Address of this parameter in Unity
     @return Current value of the "value" attribute of this parameter (before using this function, you should check if "paramaterAddress" is in the list of addresses in Unity by using the "HaveParameterAddress (paramaterAddress)" function) */
	public static int GetIntegerParameter(string paramaterAddress)
	{
		return GetValueIntegerParameterPlugin(ParameterAddressIndexInList (paramaterAddress));
	}
	
	/** Get the current value (type "boolean") of the "value" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @return Current value of the "value" attribute of this parameter (before using this function, you should check if "paramaterAddress" is in the list of addresses in Unity by using the "HaveParameterAddress (paramaterAddress)" function) */
	public static bool GetBooleanParameter(string paramaterAddress)
	{
		return GetValueBooleanParameterPlugin(ParameterAddressIndexInList (paramaterAddress));
	}
	
	/** Get the current value (type "string") of the "value" attribute of a parameter
     @param[in] paramaterAddress Address of this parameter in Unity
     @return Current value of the "value" attribute of this parameter (before using this function, you should check if "paramaterAddress" is in the list of addresses in Unity by using the "HaveParameterAddress (paramaterAddress)" function) */
	public static IntPtr GetStringParameter(string paramaterAddress)
	{
		return GetValueStringParameterPlugin(ParameterAddressIndexInList (paramaterAddress));
	}
	
	/** Set a new value for an attribute of a return (only applied to "type, description" attributes)
     @param[in] returnAddress Address of this return in Unity
     @param[in] nameAttribute Name of this attribute
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "returnAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetReturn(string returnAddress, string nameAttribute, string newValue)
	{
		int index = ReturnAddressIndexInList (returnAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetAttributeReturnPlugin(index, nameAttribute, newValue);
			
			return true;
		}
	}
	
	/** Get the current value of an attribute of a return (only applied to "type, description" attributes)
     @param[in] returnAddress Address of this return in Unity
     @param[in] nameAttribute Name of this attribute
     @return Current value of this attribute (before using this function, you should check if "returnAddress" is in the list of addresses in Unity by using the "HaveReturnAddress (returnAddress)" function) */
	public static IntPtr GetReturn(string returnAddress, string nameAttribute)
	{
		return GetAttributeReturnPlugin(ReturnAddressIndexInList (returnAddress), nameAttribute);
	}
	
	/** Set a new value (type "decimal") for the "value" attribute of a return
     @param[in] returnAddress Address of this return in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "returnAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetReturn(string returnAddress, float newValue)
	{
		int index = ReturnAddressIndexInList (returnAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetValueFloatReturnPlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Set a new value (type "integer") for the "value" attribute of a return
     @param[in] returnAddress Address of this return in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "returnAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetReturn(string returnAddress, int newValue)
	{
		int index = ReturnAddressIndexInList (returnAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetValueIntegerReturnPlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Set a new value (type "boolean") for the "value" attribute of a return
     @param[in] returnAddress Address of this return in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "returnAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetReturn(string returnAddress, bool newValue)
	{
		int index = ReturnAddressIndexInList (returnAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetValueBooleanReturnPlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Get the current value (type "decimal") of the "value" attribute of a return
     @param[in] returnAddress Address of this return in Unity
     @param[in] nameAttribute Name of this attribute
     @return Current value of this attribute (before using this function, you should check if "returnAddress" is in the list of addresses in Unity by using the "HaveReturnAddress (returnAddress)" function) */
	public static float GetFloatReturn(string returnAddress)
	{
		return GetValueFloatReturnPlugin(ReturnAddressIndexInList (returnAddress));
	}
	
	/** Get the current value (type "integer") of the "value" attribute of a return
     @param[in] returnAddress Address of this return in Unity
     @param[in] nameAttribute Name of this attribute
     @return Current value of this attribute (before using this function, you should check if "returnAddress" is in the list of addresses in Unity by using the "HaveReturnAddress (returnAddress)" function) */
	public static int GetIntegerReturn(string returnAddress)
	{
		return GetValueIntegerReturnPlugin(ReturnAddressIndexInList (returnAddress));
	}
	
	/** Get the current value (type "boolean") of the "value" attribute of a return
     @param[in] returnAddress Address of this return in Unity
     @param[in] nameAttribute Name of this attribute
     @return Current value of this attribute (before using this function, you should check if "returnAddress" is in the list of addresses in Unity by using the "HaveReturnAddress (returnAddress)" function) */
	public static bool GetBooleanReturn(string returnAddress)
	{
		return GetValueBooleanReturnPlugin(ReturnAddressIndexInList (returnAddress));
	}
	
	/** Set a new value for the "description" attribute of a message
     @param[in] messageAddress Address of this message in Unity
     @param[in] newValue The new value set to this attribute 
	 @return FALSE: Cannot set the new value to this attribute because "messageAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetDescriptionMessage(string messageAddress, string newValue)
	{
		int index = MessageAddressIndexInList (messageAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetDescriptionMessagePlugin(index, newValue);
			
			return true;
		}
	}
	
	/** Get the current value of the "description" attribute of a message
     @param[in] messageAddress Address of this message in Unity
     @return Current value of this attribute (before using this function, you should check if "messageAddress" is in the list of addresses in Unity by using the "HaveMessageAddress (messageAddress)" function) */
	public static IntPtr GetDescriptionMessage(string messageAddress)
	{
		return GetDescriptionMessagePlugin(MessageAddressIndexInList (messageAddress));
	}
	
	/** Set a message
     @param[in] messageAddress Address of this message in Unity
     @param[in] message Content of the message 
	 @return FALSE: Cannot set the content of the message because "messageAddress" is not in the list of addresses in Unity
     @return TRUE: Set successfully the new value to this attribute */
	public static bool SetMessage(string messageAddress, string message)
	{
		int index = MessageAddressIndexInList (messageAddress);
		
		if (index == -1)
		{ 
			return false;
		}
		else
		{
			SetMessagePlugin(index, message);
			
			return true;
		}
	}
	
	/** Save the namespace of the local applicatioin in a XML file
     @param[in] filePath Path leading to this file */
	public static void SaveToXML(string filePath)
	{		
		SaveToXMLPlugin(filePath);
	}
	
	/** Unregister the parameter data at an address in the local application
     @param[in] paramaterAddress Address of this parameter in the local application
     @return -1: Cannot unregister the parameter data because this address is not in the list of addresses in Unity
     @return 0: Cannot unregister the parameter data at this address
     @return 1: Unregister successfully the parameter data at this address */
	public static int UnregisterParameterData(string paramaterAddress)
	{
		int index = ParameterAddressIndexInList (paramaterAddress);
		
		if (index == -1)
		{ 
			return -1;
		}
		else
		{
			if (UnregisterParameterDataPlugin(paramaterAddress, index) == 0)
			{
				return 0;
			}
			else
			{
				// Remove the address of this parameter in the list of addresses in Unity
				addressParameterList.RemoveAt(index);
				
				return 1;
			}
		}
	}
	
	/** Unregister the return data at an address in the local application
 	 @param[in] returnAddress Address of this return in the local application 
 	 @return -1: Cannot unregister the return data because this address is not in the list of addresses in Unity
     @return 0: Cannot unregister the return data at this address
 	 @return 1: Unregister successfully the return data at this address */
	public static int UnregisterReturnData(string returnAddress)
	{	
		int index = ReturnAddressIndexInList (returnAddress);
		
		if (index == -1)
		{ 
			return -1;
		}
		else
		{
			if (UnregisterReturnDataPlugin(returnAddress, index) == 0)
			{
				return 0;
			}
			else
			{
				// Remove the address of this return in the list of addresses in Unity
				addressReturnList.RemoveAt(index);
				
				return 1;
			}
		}
	}
	
	/** Unregister the message data in the local application
 	 @param[in] messageAddress Address of this message in the local application 
 	 @return -1: Cannot unregister the message data because this address is not in the list of addresses in Unity
     @return 0: Cannot unregister the message data at this address
 	 @return 1: Unregister successfully the message data at this address */
	public static int UnregisterMessageData(string messageAddress)
	{	
		int index = MessageAddressIndexInList (messageAddress);
		
		if (index == -1)
		{ 
			return -1;
		}
		else
		{
			if (UnregisterMessageDataPlugin(messageAddress, index) == 0)
			{
				return 0;
			}
			else
			{
				// Remove the address of this message in the list of addresses in Unity
				addressMessageList.RemoveAt(index);
				
				return 1;
			}
		}
	}
	
	/** Release the Minuit protocol */
	public static void ReleaseMinuitProtocol()
	{		
		ReleaseMinuitProtocolPlugin();
	}
	
	/** Release the local and distant applications
	 @param[in] localAppName Name of the local application
 	 @param[in] distantAppName Name of the distant application */
	public static void ReleaseApplications(string localAppName, string distantAppName)
	{		
		ReleaseApplicationsPlugin(localAppName, distantAppName);
	}
	
	/** This function sets up the game according to the data in the "JamomaConfiguration.txt", "Parameters.txt", "Returns.txt" and "Messages.txt" files */
	public static void SetupDeclaredData ()
	{
		string jamomaModularFolderPath = Path.GetDirectoryName (Application.dataPath);
		
		// Read the "JamomaConfiguration.txt" file
		string pathJamomaConfiguration = Application.dataPath + "/JamomaConfiguration.txt";
		
		if (File.Exists(@pathJamomaConfiguration))
		{
			try 
			{
				// Pass the file path and file name to the StreamReader constructor
				StreamReader srJamomaConfiguration = new StreamReader(pathJamomaConfiguration);
				
				localApplicationName = srJamomaConfiguration.ReadLine();
				
				distantApplicationName = srJamomaConfiguration.ReadLine();
				
				int localApplicationPort = Convert.ToInt32(srJamomaConfiguration.ReadLine());
				
				string localApplicationIpAddress = srJamomaConfiguration.ReadLine();
				
				int distantApplicationPort = Convert.ToInt32(srJamomaConfiguration.ReadLine());
				
				string distantApplicationIpAddress = srJamomaConfiguration.ReadLine();
				
				string authorName = srJamomaConfiguration.ReadLine();
				
				string versionName = srJamomaConfiguration.ReadLine();
				
				InitModularLibrary (jamomaModularFolderPath);
				CreateApplicationManager ();
				CreateAndRegisterApplications (localApplicationName, distantApplicationName);
				CreateMinuitProtocolUnit ();
				RegisterApplicationsToMinuit (localApplicationName, distantApplicationName, localApplicationPort, localApplicationIpAddress, distantApplicationPort, distantApplicationIpAddress);
				RunMinuitProtocol ();
				
				if (!authorName.Equals("null"))
				{
					SetAuthorName (authorName);
				}
				
				if (!versionName.Equals("null"))
				{
					SetVersion (versionName);
				}
				
				// Close the "JamomaConfiguration.txt" file
				srJamomaConfiguration.Close();
			}
			catch(Exception e)
			{
				Debug.Log("Exception: " + e.Message);
			}
			
			// Read the "Parameters.txt" file
			string pathParameter = Application.dataPath + "/Parameters.txt";
			
			if (File.Exists(@pathParameter))
			{
				string lineParameter;
				
				try 
				{
					// Pass the file path and file name to the StreamReader constructor
					StreamReader srParameter = new StreamReader(pathParameter);
					
					while ((lineParameter = srParameter.ReadLine()) != null) 
					{
						string[] words = Regex.Split(lineParameter, "::::");
						
						string paramaterAddress = words[1];
						
						string type = words[2];
						
						string rangeClipmode = words[3];
						
						string rampDrive = words[4];
						
						float rangeBoundMin = 0F;
						float rangeBoundMax = 0F;
						string description = "";
						
						if ((type.Equals("decimal")) || (type.Equals("integer")))
						{
							rangeBoundMin = float.Parse(words[5]);
							
							rangeBoundMax = float.Parse(words[6]);	
							
							description = words[7];
							if (description.Equals("null"))
							{
								description = "";
							}
						} 
						else
						{
							description = words[5];
							if (description.Equals("null"))
							{
								description = "";
							}
						} 
						
						int code;
						//		ValueCallback callBack = new ValueCallback(Parameters.Callback);
						
						if ((type.Equals("decimal")) || (type.Equals("integer")))
						{
							//			code = CreateParameterData (paramaterAddress, type, rangeBoundMin, rangeBoundMax, rangeClipmode, rampDrive, description, callBack);
							
							code = CreateParameterData (paramaterAddress, type, rangeBoundMin, rangeBoundMax, rangeClipmode, rampDrive, description);														
						} 
						else // type is "boolean" or "string"
						{
							code = CreateParameterData(paramaterAddress, type, rangeClipmode, rampDrive, description);
							
							//  code = CreateParameterData(paramaterAddress, type, rangeClipmode, rampDrive, description, callBack);
						}
						
						if (code == -1)
						{
							Debug.Log ("There has been " + paramaterAddress + " address in the list of addresses in Unity, you have to choose another address");
						}
						else if (code == 0)
						{
							Debug.Log ("Cannot create the parameter at the " + paramaterAddress + " address");
						} 
						//			else if (code == 1)
						//			{	
						//				Debug.Log ("Create successfully the parameter at the " + paramaterAddress + " address");
						//			} 
					}
					
					// Close the "Parameters.txt" file
					srParameter.Close();
				}
				catch(Exception e)
				{
					Debug.Log("Exception: " + e.Message);
				}
			}
			else
			{
				Debug.Log("There is no parameter in the game");
			}
			
			// Read the "Returns.txt" file
			string pathReturn = Application.dataPath + "/Returns.txt";
			
			if (File.Exists(@pathReturn))
			{
				string lineReturn;
				
				try 
				{
					// Pass the file path and file name to the StreamReader constructor
					StreamReader srReturn = new StreamReader(pathReturn);
					
					while ((lineReturn = srReturn.ReadLine()) != null) 
					{
						string[] words = Regex.Split(lineReturn, "::::");
						
						string returnAddress = words[1];
						
						string type = words[2];
						
						string description = words[3];
						if (description.Equals("null"))
						{
							description = "";
						}
						
						//		ValueCallback callBack = new ValueCallback(Returns.Callback);
						
						//		int code = CreateReturnDataWithCallbackUnity(returnAddress, type, description, callBack);
						
						int code = CreateReturnData(returnAddress, type, description);
						
						if (code == -1) 
						{
							Debug.Log ("There has been " + returnAddress + " address in the list of addresses in Unity, you have to choose another address");
						}
						else if (code == 0)
						{
							Debug.Log ("Cannot create the return at the " + returnAddress + " address");
						} 
						//				else if (code == 1)
						//				{	
						//					Debug.Log ("Create successfully the return at the " + returnAddress + " address");
						//				} 
					}
					
					// Close the "Returns.txt" file
					srReturn.Close();
				}
				catch(Exception e)
				{
					Debug.Log("Exception: " + e.Message);
				}
			}
			else
			{
				Debug.Log("There is no return in the game");
			}
			
			// Read the "Messages.txt" file
			string pathMessage = Application.dataPath + "/Messages.txt";
			
			if (File.Exists(@pathMessage))
			{
				string lineMessage;
				
				try 
				{
					// Pass the file path and file name to the StreamReader constructor
					StreamReader srMessage = new StreamReader(pathMessage);
					
					while ((lineMessage = srMessage.ReadLine()) != null) 
					{
						string[] words = Regex.Split(lineMessage, "::::");
						
						string messageAddress = words[1];
						
						string description = words[2];
						if (description.Equals("null"))
						{
							description = "";
						}
						
						int code = CreateMessageData(messageAddress, description);
						
						if (code == -1) 
						{
							Debug.Log ("There has been " + messageAddress + " address in the list of addresses in Unity, you have to choose another address");
						}
						else if (code == 0)
						{
							Debug.Log ("Cannot create the message at the " + messageAddress + " address");
						}
					}
					
					// Close the "Messages.txt" file
					srMessage.Close();
				}
				catch(Exception e)
				{
					Debug.Log("Exception: " + e.Message);
				}
			}
			else
			{
				Debug.Log("There is no message in the game");
			}
			
			InitScoreLibrary (jamomaModularFolderPath);
			
			// Setup i-score to be controlled from an external Tick message
			//			SetupMessageTick();
		}
		else
		{
			Debug.Log("There is no configuration of the Jamoma library");
		}
	}
	
	/** Release the Jamoma Modular library
     @return FALSE: Release unsuccessfully the Jamoma Modular library
     @return TRUE: Release successfully the Jamoma Modular library */
	public static bool ReleaseJamoma()
	{
		if (runScenario) // Unity uses directly the Score library to unfold the game
		{
			// Stop the current scenario if necessaire
			StopScenario ();
		}
		else   // Unity uses i-score to unfold the game
		{
			// Stop i-score if necessaire
			StopIscore ();
		}
		
		// Unregister the parameters
		while (addressParameterList.Count > 0)
		{
			int index = addressParameterList.Count - 1;
			string paramaterAddress = addressParameterList[index];
			
			if (UnregisterParameterDataPlugin(paramaterAddress, index) == 0)
			{
				return false;
			}
			else
			{
				// Remove the address of this parameter in the list of addresses in Unity
				addressParameterList.RemoveAt(index);
			}
			
			//		Debug.Log("Unregister successfully the parameter at the " + paramaterAddress + " address");
		}
		
		// Unregister the returns
		while (addressReturnList.Count > 0)
		{
			int index = addressReturnList.Count - 1;
			string returnAddress = addressReturnList[index];
			
			if (UnregisterReturnDataPlugin(returnAddress, index) == 0)
			{
				return false;
			}
			else
			{
				// Remove the address of this return in the list of addresses in Unity
				addressReturnList.RemoveAt(index);
			}
			
			//			Debug.Log("Unregister successfully the return at the " + returnAddress + " address");
		}
		
		// Unregister the messages
		while (addressMessageList.Count > 0)
		{
			int index = addressMessageList.Count - 1;
			string messageAddress = addressMessageList[index];
			
			if (UnregisterMessageDataPlugin(messageAddress, index) == 0)
			{
				return false;
			}
			else
			{
				// Remove the address of this message in the list of addresses in Unity
				addressMessageList.RemoveAt(index);
			}
		}
		
		// Release the Minuit protocol
		ReleaseMinuitProtocol();
		//		Debug.Log("Release successfully the Minuit protocol");
		
		// Release the local and distant applications	 
		ReleaseApplications (localApplicationName, distantApplicationName);
		//		Debug.Log("Release successfully the local and distant applications");
		
		return true;
	}
}
