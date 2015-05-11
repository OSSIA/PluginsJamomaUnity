using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class MessageListWindow : EditorWindow
{
	int index = 0;
	string[] messageList;
	string[] messageNameList;
	int messageNumber = 0;

	MessageListWindow()
	{
		try 
		{
			// Read the "Messages.txt" file
			string path = "Assets/Messages.txt";		
			
			//Pass the file path and file name to the StreamReader constructor
			StreamReader srMessage = new StreamReader(path);
			
			string lineMessage;
			
			while ((lineMessage = srMessage.ReadLine()) != null) 
			{
				messageNumber++;
			}
			
			// Close the "Messages.txt" file
			srMessage.Close();
			
			messageList = new string[messageNumber];
			messageNameList = new string[messageNumber];
			
			//Pass the file path and file name to the StreamReader constructor
			srMessage = new StreamReader(path);
			
			int i = 0;
			
			while ((lineMessage = srMessage.ReadLine()) != null) 
			{
				string[] words = Regex.Split(lineMessage, "::::");
				
				string messageName = words[0];
				
				messageNameList[i] = messageName;
				
				string messageAddress = words[1];
				
				messageList[i] = (i + 1) + ". Name: " + messageName + "; Address: " + messageAddress;
				
				string description = words[2];
				
				if (!(description.Equals("null")))
				{
					messageList[i] += "; Description: " + description;
				}
				
				i++;
			}
			
			// Close the "Messages.txt" file
			srMessage.Close();
		}
		catch(Exception e)
		{
			Debug.Log("Exception: " + e.Message);
		}
	}
	
	void OnGUI()
	{
		EditorGUILayout.LabelField(" ");
		EditorGUILayout.LabelField("LIST OF MESSAGES IN THE GAME");
		EditorGUILayout.LabelField(" ");
		
		for (int i = 0; i < messageNumber; i++)
		{
			EditorGUILayout.LabelField(messageList[i]);
			EditorGUILayout.LabelField(" ");
		}
		
		index = EditorGUILayout.Popup("Select a message in the list:", index, messageNameList);
		
		EditorGUILayout.LabelField(" ");
		
		if(GUILayout.Button("Modify the selected message"))
		{
			string pathIndex = "Assets/IndexMessageModified.txt";
			
			// Create a file "IndexMessageModified.txt" to write to
			using (StreamWriter sw = File.CreateText(@pathIndex)) 
			{
				sw.WriteLine(index);
				
				// Close the "IndexMessageModified.txt" file
				sw.Close();
			}	
			
			this.Close ();
			EditorWindow.GetWindow(typeof(MessageModifyWindow));
		}
		
		EditorGUILayout.LabelField(" ");
		
		if(GUILayout.Button("Delete the selected message"))
		{
			if (messageNumber == 1)
			{
				// Delete the "Messages.txt" file 
				string path = "Assets/Messages.txt";
				File.Delete(@path);
				
				// Delete the "Messages.cs" file
				path = "Assets/Scripts/Messages.cs";			
				File.Delete(@path);
				
				Debug.Log("You have deleted this message. There is no message in the game.");
				this.Close ();
			}
			else
			{
				var file = new List<string>(System.IO.File.ReadAllLines("Assets/Messages.txt"));
				file.RemoveAt(index);
				File.WriteAllLines("Assets/Messages.txt", file.ToArray());
				
				// Delete the old "Messages.cs" file and create a new "Messages.cs" file to write to
				string pathMessageCode = "Assets/Scripts/Messages.cs";
				File.Delete(@pathMessageCode);
				
				using (StreamWriter sw = File.CreateText(@pathMessageCode)) 
				{
					sw.WriteLine("using System;");
					sw.WriteLine("using UnityEngine;\n");
					
					sw.WriteLine("public class Messages");
					sw.WriteLine("{");
					
					// Read the "Messages.txt" file to get the list of returns
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
				
				this.Close ();
				EditorWindow.GetWindow(typeof(MessageListWindow));
			}		
		}
	}
}