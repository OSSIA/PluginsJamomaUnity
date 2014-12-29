#ifdef PLUGINJAMOMAUNITY_EXPORTS
#define PLUGINJAMOMAUNITY_API __declspec(dllexport) 
#else
#define PLUGINJAMOMAUNITY_API __declspec(dllimport) 
#endif

#include "TTModular.h"
#include "TTScore.h"
#include <vector>

extern "C"
{
	// Declare the application manager, the local application and the distant application
	TTObject mApplicationManager;   ///< The application manager of the Jamoma Modular framework
	TTObject mApplicationLocal;     ///< The local application is Unity, it is also "our application" in the following section
	TTObject mApplicationDistant;   ///< The distant application is i-score

	// Declare a Minuit protocol unit to use
	TTObject mProtocolMinuit;       ///< The Minuit protocol allows the communication between i-score and Unity

	// Declare publicly all datas of the local application to retreive them from the callback function
	int numberParameterData = 0;            ///< Number of parameters communicated between Jamoma and Unity
	std::vector<TTObject> mDataParameterVector;   ///< Vector of parameters which are relative to the state of the local application and which are communicated between Jamoma and Unity, the length of this vector is "numberParameterData"

	int numberReturnData = 0;            ///< Number of returns communicated between Jamoma and Unity
	std::vector<TTObject> mDataReturnVector;   ///< Vector of returns which are communicated between Jamoma and Unity (a return is a kind of notification sent by Unity to Jamoma), the length of this vector is "numberReturnData"

	int numberMessageData = 0;            ///< Number of messages communicated between i-score and Unity
	std::vector<TTObject> mDataMessageVector;   ///< Vector of messages which are communicated between Jamoma and Unity (a message is a kind of command to send to Jamoma from Unity), the length of this vector is "numberMessageData"

	// Declare publicly the scenario to retreive it from the callback function
	TTObject mScenario;             ///< Scenario to execute

	const char* eventName = "";     ///< Name of the current event during the scenario execution
	const char* eventStatus = "";   ///< Status of the current event during the scenario execution

	// Declare callbacks used to observe the scenario execution
	TTObject mEventStatusChangedCallback;  ///< Callbacks used to observe the scenario execution

	/** Callback function (used in C/C++) */
	PLUGINJAMOMAUNITY_API void DataReturnValueCallback();

	/** Callback function (used in C/C++) to get data's value back
	@param[in] baton #TTValue
	@param[out] value #TTValue
	void DataReturnValueCallback(const TTValue& baton, const TTValue& value);
	*/

	/** Callback function to get event's value back
	@param[in] baton #TTValue
	@param[out] value #TTValue */
	PLUGINJAMOMAUNITY_API void EventStatusChangedCallback(const TTValue& baton, const TTValue& value);

	/** Start i-score */
	PLUGINJAMOMAUNITY_API void StartIscorePlugin();

	/** Stop i-score */
	PLUGINJAMOMAUNITY_API void StopIscorePlugin();

	/** Get the name of the current event during the scenario execution
	@return Name of the current event during the scenario execution */
	PLUGINJAMOMAUNITY_API const char* GetEventNamePlugin();

	/** Get the status of the current event during the scenario execution
	@return Status of the current event during the scenario execution */
	PLUGINJAMOMAUNITY_API const char* GetEventStatusPlugin();

	/** Init the Jamoma Modular library
	@param[in] folderPath Folder path where all the dylibs are */
	PLUGINJAMOMAUNITY_API void InitModularLibraryPlugin(char folderPath[]);

	/** Create an application manager */
	PLUGINJAMOMAUNITY_API void CreateApplicationManagerPlugin();

	/** Create a local application, a distant application and register them to the application manager
	@param[in] localAppName Name of the local application
	@param[in] distantAppName Name of the distant application
	@return 0: Cannot create the local application
	@return 1: Cannot create the distant application
	@return 2: Create the local and distant applications and register them successfully */
	PLUGINJAMOMAUNITY_API int CreateAndRegisterApplicationsPlugin(char localAppName[], char distantAppName[]);

	/** Create a Minuit protocol unit
	@return 0: Cannot create the Minuit protocol unit
	@return 1: Create successfully the Minuit protocol unit */
	PLUGINJAMOMAUNITY_API int CreateMinuitProtocolUnitPlugin();

	/** Register the local and distant applications to the Minuit protocol
	@param[in] localAppName Name of the local application
	@param[in] distantAppName Name of the distant application
	@param[in] portLocal Port of the local application
	@param[in] ipLocal IP adress of the local application
	@param[in] portDistant Port of the distant application
	@param[in] ipDistant IP adress of the distant application */
	PLUGINJAMOMAUNITY_API void RegisterApplicationsToMinuitPlugin(char localAppName[], char distantAppName[], int portLocal, char ipLocal[], int portDistant, char ipDistant[]);

	/** Run the Minuit protocol */
	PLUGINJAMOMAUNITY_API void RunMinuitProtocolPlugin();

	/** Set the author's name of the local application
	@param[in] authorName The author's name */
	PLUGINJAMOMAUNITY_API void SetAuthorNamePlugin(char authorName[]);

	/** Get the author's name of the local application
	@return Author's name of the local application */
	PLUGINJAMOMAUNITY_API const char* GetAuthorNamePlugin();

	/** Set the version name of the local application
	@param[in] versionName The version name of the local appliation */
	PLUGINJAMOMAUNITY_API void SetVersionPlugin(char versionName[]);

	/** Get the version name of the local application
	@return Version name of the local application */
	PLUGINJAMOMAUNITY_API const char* GetVersionPlugin();

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
	PLUGINJAMOMAUNITY_API int CreateNumberParameterDataPlugin(char paramaterAddress[], char type[], float rangeBoundMin, float rangeBoundMax, char rangeClipmode[], char rampDrive[], char description[]);

	/** Create a parameter data (type "boolean") at an address (without the callback function in Unity)
	@param[in] paramaterAddress Address of this parameter registered in the local application
	@param[in] rangeClipmode "rangeClipmode" attribute of this parameter
	@param[in] rampDrive "rampDrive" attribute of this parameter
	@param[in] description "description" attribute of this parameter
	@return 0: Cannot create the parameter data at this address
	@return 1: Create successfully the parameter data at this address */
	PLUGINJAMOMAUNITY_API int CreateBooleanParameterDataPlugin(char paramaterAddress[], char rangeClipmode[], char rampDrive[], char description[]);

	/** Create a parameter data (type "string") at an address (without the callback function in Unity)
	@param[in] paramaterAddress Address of this parameter registered in the local application
	@param[in] rangeClipmode "rangeClipmode" attribute of this parameter
	@param[in] rampDrive "rampDrive" attribute of this parameter
	@param[in] description "description" attribute of this parameter
	@return 0: Cannot create the parameter data at this address
	@return 1: Create successfully the parameter data at this address */
	PLUGINJAMOMAUNITY_API int CreateStringParameterDataPlugin(char paramaterAddress[], char rangeClipmode[], char rampDrive[], char description[]);

	/** Create a return data at an address (without the callback function in Unity)
	@param[in] returnAddress Address of this return registered in the local application
	@param[in] type "type" attribute of this return (type is "decimal" or "integer" or "boolean")
	@param[in] description "description" attribute of this return
	@return 0: Cannot create the return data at this return
	@return 1: Create successfully the return data at this address */
	PLUGINJAMOMAUNITY_API int CreateReturnDataPlugin(char returnAddress[], char type[], char description[]);

	/** Create a message data at an address (without the callback function in Unity)
	@param[in] messageAddress Address of this message registered in the local application
	@param[in] description "description" attribute of this message
	@return 0: Cannot create the message data at this address
	@return 1: Create successfully the message data at this address */
	PLUGINJAMOMAUNITY_API int CreateMessageDataPlugin(char messageAddress[], char description[]);
		
	/** Init the Score library
	@param[in] folderPath Folder path where all the dylibs are */
	PLUGINJAMOMAUNITY_API void InitScoreLibraryPlugin(char folderPath[]);

	/** Run a scenario
	@param[in] pathName Name of the path leading to the scenario file
	@param[in] speed Execution speed of the scenario */
	PLUGINJAMOMAUNITY_API void RunScenarioPlugin(char pathName[], float speed);

	/** Stop the current scenario */
	PLUGINJAMOMAUNITY_API void StopScenarioPlugin();

	/** Set a new value for an attribute of a parameter (only applied to "type, rangeClipmode, rampDrive, description" attributes)
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@param[in] nameAttribute Name of this attribute
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetAttributeParameterPlugin(int indexParameter, char nameAttribute[], char newValue[]);

	/** Get the current value of an attribute of a parameter (only applied to "type, rangeClipmode, rampDrive, description" attributes)
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@param[in] nameAttribute Name of this attribute
	@return Current value of this attribute */
	PLUGINJAMOMAUNITY_API const char* GetAttributeParameterPlugin(int indexParameter, char nameAttribute[]);

	/** Set a new value for the "rangeBounds" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@param[in] rangeBoundMin Min bound of the "rangeBounds" attribute of the new value
	@param[in] rangeBoundMax Max bound of the "rangeBounds" attribute of the new value */
	PLUGINJAMOMAUNITY_API void SetRangeBoundsParameterPlugin(int indexParameter, float rangeBoundMin, float rangeBoundMax);

	/** Get the current value of the min bound of the "rangeBounds" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@return Current value of the min bound of the "rangeBounds" attribute of this parameter */
	PLUGINJAMOMAUNITY_API float GetMinRangeBoundParameterPlugin(int indexParameter);

	/** Get the current value of the max bound of the "rangeBounds" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@return Current value of the max bound of the "rangeBounds" attribute of this parameter */
	PLUGINJAMOMAUNITY_API float GetMaxRangeBoundParameterPlugin(int indexParameter);

	/** Set a new value (type "decimal") for the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetValueFloatParameterPlugin(int indexParameter, float newValue);

	/** Set a new value (type "integer") for the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetValueIntegerParameterPlugin(int indexParameter, int newValue);

	/** Set a new value (type "boolean") for the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetValueBooleanParameterPlugin(int indexParameter, bool newValue);

	/** Set a new value (type "string") for the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetValueStringParameterPlugin(int indexParameter, char newValue[]);

	/** Get the current value (type "decimal") of the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@return Current value of the "value" attribute of this parameter */
	PLUGINJAMOMAUNITY_API float GetValueFloatParameterPlugin(int indexParameter);

	/** Get the current value (type "integer") of the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@return Current value of the "value" attribute of this parameter */
	PLUGINJAMOMAUNITY_API int GetValueIntegerParameterPlugin(int indexParameter);

	/** Get the current value (type "boolean") of the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@return Current value of the "value" attribute of this parameter */
	PLUGINJAMOMAUNITY_API bool GetValueBooleanParameterPlugin(int indexParameter);

	/** Get the current value (type "string") of the "value" attribute of a parameter
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@return Current value of the "value" attribute of this parameter */
	PLUGINJAMOMAUNITY_API const char* GetValueStringParameterPlugin(int indexParameter);

	/** Set a new value for an attribute of a return (only applied to "type, description" attributes)
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@param[in] nameAttribute Name of this attribute
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetAttributeReturnPlugin(int indexReturn, char nameAttribute[], char newValue[]);

	/** Get the current value of an attribute of a return (only applied to "type, description" attributes)
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@param[in] nameAttribute Name of this attribute
	@return Current value of this attribute */
	PLUGINJAMOMAUNITY_API const char* GetAttributeReturnPlugin(int indexReturn, char nameAttribute[]);

	/** Set a new value (type "decimal") for the "value" attribute of a return
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetValueFloatReturnPlugin(int indexReturn, float newValue);

	/** Set a new value (type "integer") for the "value" attribute of a return
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetValueIntegerReturnPlugin(int indexReturn, int newValue);

	/** Set a new value (type "boolean") for the "value" attribute of a return
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetValueBooleanReturnPlugin(int indexReturn, bool newValue);

	/** Get the current value (type "decimal") of the "value" attribute of a return
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@return Current value of the "value" attribute of this return */
	PLUGINJAMOMAUNITY_API float GetValueFloatReturnPlugin(int indexReturn);

	/** Get the current value (type "integer") of the "value" attribute of a return
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@return Current value of the "value" attribute of this return */
	PLUGINJAMOMAUNITY_API int GetValueIntegerReturnPlugin(int indexReturn);

	/** Get the current value (type "boolean") of the "value" attribute of a return
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@return Current value of the "value" attribute of this return */
	PLUGINJAMOMAUNITY_API bool GetValueBooleanReturnPlugin(int indexReturn);

	/** Set a new value for the "description" attribute of a message
	@param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
	@param[in] newValue The new value set to this attribute */
	PLUGINJAMOMAUNITY_API void SetDescriptionMessagePlugin(int indexMessage, char newValue[]);

	/** Get the current value of the "description" attribute of a message
	@param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
	@return Current value of this attribute */
	PLUGINJAMOMAUNITY_API const char* GetDescriptionMessagePlugin(int indexMessage);

	/** Set a message
	@param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
	@param[in] message Content of the message */
	PLUGINJAMOMAUNITY_API void SetMessagePlugin(int indexMessage, char message[]);

	/** Save the namespace of the local applicatioin in a XML file
	@param[in] filePath Path leading to this file */
	PLUGINJAMOMAUNITY_API void SaveToXMLPlugin(char filePath[]);

	/** Unregister the parameter data at an address in the local application
	@param[in] paramaterAddress Address of this parameter in the local application
	@param[in] indexParameter Index of this parameter in the vector of parameters (indexParameter = 0, 1, 2,...)
	@return 0: Cannot unregister the parameter data at this address
	@return 1: Unregister successfully the parameter data at this address */
	PLUGINJAMOMAUNITY_API int UnregisterParameterDataPlugin(char paramaterAddress[], int indexParameter);

	/** Unregister the return data at an address in the local application
	@param[in] returnAddress Address of this return in the local application
	@param[in] indexReturn Index of this return in the vector of returns (indexReturn = 0, 1, 2,...)
	@return 0: Cannot unregister the return data at this address
	@return 1: Unregister successfully the return data at this address */
	PLUGINJAMOMAUNITY_API int UnregisterReturnDataPlugin(char returnAddress[], int indexReturn);

	/** Unregister the message data at an address in the local application
	@param[in] messageAddress Address of this message in the local application
	@param[in] indexMessage Index of this message in the vector of messages (indexMessage = 0, 1, 2,...)
	@return 0: Cannot unregister the message data at this address
	@return 1: Unregister successfully the message data at this address */
	PLUGINJAMOMAUNITY_API int UnregisterMessageDataPlugin(char messageAddress[], int indexMessage);

	/** Release the Minuit protocol */
	PLUGINJAMOMAUNITY_API void ReleaseMinuitProtocolPlugin();

	/** Release the local and distant applications
	@param[in] localAppName Name of the local application
	@param[in] distantAppName Name of the distant application */
	PLUGINJAMOMAUNITY_API void ReleaseApplicationsPlugin(char localAppName[], char distantAppName[]);
}