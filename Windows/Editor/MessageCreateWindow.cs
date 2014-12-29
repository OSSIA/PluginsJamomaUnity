using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MessageCreateWindow : EditorWindow 
{
	string messageName = "";
	string messageAddress = "";
	string description = "";
	
	void OnGUI()
	{
		messageName = EditorGUILayout.TextField ("Message Name (*)", messageName);
		messageAddress = EditorGUILayout.TextField ("Message Address (*)", messageAddress);
		description = EditorGUILayout.TextField ("Description", description);
		
		if(GUILayout.Button("Create Message"))
		{
			if (messageName.Equals(""))
			{
				Debug.Log ("The name of the message is not valid");
			}
			else 
			{
				messageName = messageName.ToLower();
				
				if (CommonFunctions.IsNameDeclaredInTextFile(messageName))
				{
					Debug.Log ("The name of the message was declared, you have to choose another name");
				}
				else if (messageAddress.Equals(""))
				{
					Debug.Log ("The address of the message is not valid");
				}
				else if (CommonFunctions.IsAddressDeclaredInTextFile(messageAddress))
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
						
						// Write to the "Messages.txt" file
						string pathMessage = "Assets/Messages.txt";
						if (!File.Exists(@pathMessage)) 
						{
							// Create the "Messages.txt" file to write to
							using (StreamWriter sw = File.CreateText(@pathMessage)) 
							{
								sw.WriteLine(messageInformation);
								
								// Close the "Messages.txt" file
								sw.Close();
							}	
						}
						else
						{
							using (StreamWriter sw = File.AppendText(@pathMessage)) 
							{
								sw.WriteLine(messageInformation);
								
								// Close the "Messages.txt" file
								sw.Close();
							}	
						}

						// Delete the "Messages.cs" file if exist
						string pathMessageCode = "Assets/Scripts/Messages.cs";
						
						if (File.Exists(@pathMessageCode)) 
						{
							File.Delete(@pathMessageCode);
						}

						// Create the "Messages.cs" file to write to
						using (StreamWriter sw = File.CreateText(@pathMessageCode)) 
						{
							sw.WriteLine("using System;");
							sw.WriteLine("using UnityEngine;\n");
							
							sw.WriteLine("public class Messages");
							sw.WriteLine("{");

							// Read the "Messages.txt" file to get the list of messages
							pathMessage = "Assets/Messages.txt";
							
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

						Debug.Log ("Create successfully the message at the " + messageAddress + " address");
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