using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class ReturnListWindow : EditorWindow
{
	int index = 0;
	string[] returnList;
	string[] returnNameList;
	int returnNumber = 0;

	ReturnListWindow()
	{
		try 
		{
			// Read the "Returns.txt" file
			string path = "Assets/Returns.txt";		
		
			//Pass the file path and file name to the StreamReader constructor
			StreamReader srReturn = new StreamReader(path);
		
			string lineReturn;
		
			while ((lineReturn = srReturn.ReadLine()) != null) 
			{
				returnNumber++;
			}
		
			// Close the "Returns.txt" file
			srReturn.Close();

			returnList = new string[returnNumber];
			returnNameList = new string[returnNumber];
		
			//Pass the file path and file name to the StreamReader constructor
			srReturn = new StreamReader(path);

			int i = 0;
		
			while ((lineReturn = srReturn.ReadLine()) != null) 
			{
				string[] words = Regex.Split(lineReturn, "::::");
				
				string returnName = words[0];

				returnNameList[i] = returnName;
				
				string returnAddress = words[1];
				
				string type = words[2];

				returnList[i] = (i + 1) + ". " + returnName + " (type: " + type + "; address: " + returnAddress + ")";
			
				string description = words[3];

				if (!(description.Equals("null")))
				{
					returnList[i] += ". Description: " + description;
				}

				i++;
			}
		
			// Close the "Returns.txt" file
			srReturn.Close();
		}
		catch(Exception e)
		{
			Debug.Log("Exception: " + e.Message);
		}
	}

	void OnGUI()
	{
		EditorGUILayout.LabelField(" ");
		EditorGUILayout.LabelField("LIST OF RETURNS IN THE GAME");
		EditorGUILayout.LabelField(" ");

		for (int i = 0; i < returnNumber; i++)
		{
			EditorGUILayout.LabelField(returnList[i]);
			EditorGUILayout.LabelField(" ");
		}

		index = EditorGUILayout.Popup("Select a return in the list:", index, returnNameList);

		EditorGUILayout.LabelField(" ");

		if(GUILayout.Button("Modify the selected return"))
		{
			string pathIndex = "Assets/IndexReturnModified.txt";

			// Create a file "IndexReturnModified.txt" to write to
			using (StreamWriter sw = File.CreateText(@pathIndex)) 
			{
				sw.WriteLine(index);
					
				// Close the "IndexReturnModified.txt" file
				sw.Close();
			}	

			this.Close ();
			EditorWindow.GetWindow(typeof(ReturnModifyWindow));
		}
		
		EditorGUILayout.LabelField(" ");
		
		if(GUILayout.Button("Delete the selected return"))
		{
			if (returnNumber == 1)
			{
				// Delete the "Returns.txt" file 
				string path = "Assets/Returns.txt";
				File.Delete(@path);

				// Delete the "Returns.cs" file
				path = "Assets/Scripts/Returns.cs";			
				File.Delete(@path);
								
				Debug.Log("You have deleted this return. There is no return in the game.");
				this.Close ();
			}
			else
			{
				var file = new List<string>(System.IO.File.ReadAllLines("Assets/Returns.txt"));
				file.RemoveAt(index);
				File.WriteAllLines("Assets/Returns.txt", file.ToArray());

				// Delete the old "Returns.cs" file and create a new "Returns.cs" file to write to
				string pathReturnCode = "Assets/Scripts/Returns.cs";
				File.Delete(@pathReturnCode);

				using (StreamWriter sw = File.CreateText(@pathReturnCode)) 
				{
					sw.WriteLine("using System;");
					sw.WriteLine("using UnityEngine;\n");
							
					sw.WriteLine("public class Returns");
					sw.WriteLine("{");

					// Read the "Returns.txt" file to get the list of returns
					string pathReturn = "Assets/Returns.txt";
								
					// Pass the file path and file name to the StreamReader constructor
					StreamReader srReturn = new StreamReader(pathReturn);
					String lineReturn;
									
					while ((lineReturn = srReturn.ReadLine()) != null) 
					{
						string[] words = Regex.Split(lineReturn, "::::");

						string returnNameCode = words[0];

						string returnAddressCode = words[1];
										
						string typeCode = words[2];

						string str = "";

						if (typeCode.Equals("integer"))
						{
							str = "\tprivate static int " + returnNameCode + ";";
							sw.WriteLine(str);

							str = "\tpublic static int " + returnNameCode.ToUpper();
							sw.WriteLine(str);
						}
						else if (typeCode.Equals("decimal"))
						{
							str = "\tprivate static float " + returnNameCode + ";";
							sw.WriteLine(str);
									
							str = "\tpublic static float " + returnNameCode.ToUpper();
							sw.WriteLine(str);
						}
						else
						{
							str = "\tprivate static bool " + returnNameCode + ";";
							sw.WriteLine(str);
								
							str = "\tpublic static bool " + returnNameCode.ToUpper();
							sw.WriteLine(str);
						}
								
						sw.WriteLine("\t{");
						sw.WriteLine("\t\tget");
						sw.WriteLine("\t\t{");

						str = "\t\t\treturn " + returnNameCode  + ";";
						sw.WriteLine(str);

						sw.WriteLine("\t\t}");
						sw.WriteLine("\t\tset");
						sw.WriteLine("\t\t{");

						str = "\t\t\t" + returnNameCode  + " = value;";
						sw.WriteLine(str);

						str = "\t\t\tPluginJamomaUnity.SetReturn (\"" + returnAddressCode + "\", " + returnNameCode + ");";
						sw.WriteLine(str);
							
						sw.WriteLine("\t\t}");
						sw.WriteLine("\t}\n");
					}
							
					sw.WriteLine("}");
							
					// Close the "Returns.cs" file
					sw.Close();
							
					// Close the "Returns.txt" file
					srReturn.Close();
				}

				this.Close ();
				EditorWindow.GetWindow(typeof(ReturnListWindow));
			}		
		}
	}
}