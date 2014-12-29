using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MessageModifyWindow : EditorWindow 
{
	string messageName = "";
	string messageAddress = "";
	string description = "";
	
	string oldMessageName = "";
	string oldMessageAddress = "";
	
	string lineMessageModified = ""; 
	
	MessageModifyWindow()
	{
		// Read the "IndexMessageModified.txt" file
		string pathIndex = "Assets/IndexMessageModified.txt";		
		
		// Pass the file path and file name to the StreamReader constructor
		StreamReader srIndex = new StreamReader(pathIndex);
		
		int index = Convert.ToInt32(srIndex.ReadLine());
		
		// Close the "IndexMessageModified.txt" file
		srIndex.Close();
		
		// Delete the "IndexMessageModified.txt" file
		File.Delete(@pathIndex);
		
		// Read the "Messages.txt" file
		string pathMessage = "Assets/Messages.txt";		
		
		// Pass the file path and file name to the StreamReader constructor
		StreamReader srMessage = new StreamReader(pathMessage);
		
		int i = 0;

		while ((lineMessageModified = srMessage.ReadLine()) != null) 
		{
			if (index == i)
			{
				string[] words = Regex.Split(lineMessageModified, "::::");
				
				messageName = words[0];
				oldMessageName = messageName;
				
				messageAddress = words[1];
				oldMessageAddress = messageAddress;
				
				description = words[2];
				
				if (description.Equals("null"))
				{
					description = "";
				}		
				
				break;
			}
			
			i++;
		}
		
		// Close the "Messages.txt" file
		srMessage.Close();
	}
	
	void OnGUI()
	{
		messageName = EditorGUILayout.TextField ("Message Name (*)", messageName);
		messageAddress = EditorGUILayout.TextField ("Message Address (*)", messageAddress);
		description = EditorGUILayout.TextField ("Description", description);
		
		if(GUILayout.Button("Modify Message"))
		{
			if (messageName.Equals(""))
			{
				Debug.Log ("The name of the message is not valid");
			}
			else 
			{
				messageName = messageName.ToLower();
				
				if ((!(messageName.Equals(oldMessageName))) && CommonFunctions.IsNameDeclaredInTextFile(messageName))
				{
					Debug.Log ("The name of the message was declared, you have to choose another address");
				} 
				else if (messageAddress.Equals(""))
				{
					Debug.Log ("The address of the message is not valid");
				}
				else if ((!(messageAddress.Equals(oldMessageAddress))) && CommonFunctions.IsAddressDeclaredInTextFile(messageAddress))
				{
					Debug.Log ("The address of the message was declared, you have to choose another address");
				}
				else
				{
					try 
					{
						// The champs in the information of the message is split by "::::"
						string messageInformation = messageName + "::::" + messageAddress + "::::";
						
						if (description.Equals(""))
						{
							messageInformation += "null";
						}
						else
						{
							messageInformation += description;
						}
						
						string strMessage = File.ReadAllText("Assets/Messages.txt");
						strMessage = strMessage.Replace(lineMessageModified, messageInformation);
						File.WriteAllText("Assets/Messages.txt", strMessage);
						
						string pathMessageCode = "Assets/Scripts/Messages.cs";
						
						// Delete the old "Messages.cs" file
						File.Delete(@pathMessageCode);
						
						// Create a new "Messages.cs" file to write to
						using (StreamWriter sw = File.CreateText(@pathMessageCode)) 
						{
							sw.WriteLine("using System;");
							sw.WriteLine("using UnityEngine;\n");
							
							sw.WriteLine("public class Messages");
							sw.WriteLine("{");

							// Read the "Messages.txt" file to get the list of messages
							string pathMessage = "Assets/Messages.txt";
							
							// Pass the file path and file name to the StreamReader constructor
							StreamReader srMessage = new StreamReader(pathMessage);
							String lineMessage;
							
							while ((lineMessage = srMessage.ReadLine()) != null) 
							{
								string[] words = Regex.Split(lineMessage, "::::");
								
								string messageNameCode = words[0];
								
								string messageAddressCode = words[1];
								
								string str = "";

								str = "\tprivate static string " + messageNameCode + ";";
								sw.WriteLine(str);
								
								str = "\tpublic static string " + messageNameCode.ToUpper();
								sw.WriteLine(str);
								
								sw.WriteLine("\t{");
								sw.WriteLine("\t\tget");
								sw.WriteLine("\t\t{");
								
								str = "\t\t\treturn " + messageNameCode  + ";";
								sw.WriteLine(str);
								
								sw.WriteLine("\t\t}");
								sw.WriteLine("\t\tset");
								sw.WriteLine("\t\t{");
								
								str = "\t\t\t" + messageNameCode  + " = value;";
								sw.WriteLine(str);
								
								str = "\t\t\tPluginJamomaUnity.SetMessage (\"" + messageAddressCode + "\", " + messageNameCode + ");";
								sw.WriteLine(str);
								
								sw.WriteLine("\t\t}");
								sw.WriteLine("\t}\n");
							}
							
							sw.WriteLine("}");
							
							// Close the "Messages.cs" file
							sw.Close();
							
							// Close the "Messages.txt" file
							srMessage.Close();
						}
						
						Debug.Log ("Modify successfully the message at the " + messageAddress + " address");
						
						this.Close();
						EditorWindow.GetWindow(typeof(MessageListWindow));
					}
					catch(Exception e)
					{
						Debug.Log("Exception: " + e.Message);
					}
				}
			}
		}
		
		string help = "\n(*): REQUIRED INFORMATION.\n";
		
		EditorGUILayout.HelpBox(help, MessageType.Info);
	}
}