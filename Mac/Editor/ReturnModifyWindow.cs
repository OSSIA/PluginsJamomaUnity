using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ReturnModifyWindow : EditorWindow
{
	string returnName = "";
	string returnAddress = "";
	ReturnType returnType;
	string description = "";

	string oldReturnName = "";
	string oldReturnAddress = "";

	string lineReturnModified = ""; 

	ReturnModifyWindow()
	{
		// Read the "IndexReturnModified.txt" file
		string pathIndex = "Assets/IndexReturnModified.txt";		
		
		// Pass the file path and file name to the StreamReader constructor
		StreamReader srIndex = new StreamReader(pathIndex);

		int index = Convert.ToInt32(srIndex.ReadLine());
		
		// Close the "IndexReturnModified.txt" file
		srIndex.Close();

		// Delete the "IndexReturnModified.txt" file
		File.Delete(@pathIndex);

		// Read the "Returns.txt" file
		string pathReturn = "Assets/Returns.txt";		
		
		// Pass the file path and file name to the StreamReader constructor
		StreamReader srReturn = new StreamReader(pathReturn);

		int i = 0;

		while ((lineReturnModified = srReturn.ReadLine()) != null) 
		{
			if (index == i)
			{
				string[] words = Regex.Split(lineReturnModified, "::::");

				returnName = words[0];
				oldReturnName = returnName;
				
				returnAddress = words[1];
				oldReturnAddress = returnAddress;
				
				string type = words[2];

				if (type.Equals("decimal"))
				{
					returnType = ReturnType.DECIMAL;
				}
				else if (type.Equals("integer"))
				{
					returnType = ReturnType.INTEGER;
				}
				else
				{
					returnType = ReturnType.BOOLEAN;
				}

				description = words[3];
				
				if (description.Equals("null"))
				{
					description = "";
				}		

				break;
			}

			i++;
		}

		// Close the "Returns.txt" file
		srReturn.Close();
	}
	
	void OnGUI()
	{
		returnName = EditorGUILayout.TextField ("Return Name (*)", returnName);
		returnAddress = EditorGUILayout.TextField ("Return Address (*)", returnAddress);
		returnType = (ReturnType)EditorGUILayout.EnumPopup("Return Type (*)", returnType);
		description = EditorGUILayout.TextField ("Description", description);
		
		if(GUILayout.Button("Modify Return"))
		{
			if (returnName.Equals(""))
			{
				Debug.Log ("The name of the return is not valid");
			}
			else 
			{
				returnName = returnName.ToLower();

				if ((!(returnName.Equals(oldReturnName))) && CommonFunctions.IsNameDeclaredInTextFile(returnName))
				{
					Debug.Log ("The name of the return was declared, you have to choose another address");
				} 
				else if (returnAddress.Equals(""))
				{
					Debug.Log ("The address of the return is not valid");
				}
				else if ((!(returnAddress.Equals(oldReturnAddress))) && CommonFunctions.IsAddressDeclaredInTextFile(returnAddress))
				{
					Debug.Log ("The address of the return was declared, you have to choose another address");
				}
				else
				{
					try 
					{
						string type = "";
						if (returnType.ToString().Equals("DECIMAL"))
						{
							type = "decimal";
						}
						else if (returnType.ToString().Equals("INTEGER"))
						{
							type = "integer";
						}
						else
						{
							type = "boolean";
						}

						// The champs in the information of the return is split by "::::"
						string returnInformation = returnName + "::::" + returnAddress + "::::" + type + "::::";

						if (description.Equals(""))
						{
							returnInformation += "null";
						}
						else
						{
							returnInformation += description;
						}
						
						string strReturn = File.ReadAllText("Assets/Returns.txt");
						strReturn = strReturn.Replace(lineReturnModified,returnInformation);
						File.WriteAllText("Assets/Returns.txt", strReturn);

						string pathReturnCode = "Assets/Scripts/Returns.cs";
						
						// Delete the old "Returns.cs" file
						File.Delete(@pathReturnCode);

						// Create a new "Returns.cs" file to write to
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
						
						Debug.Log ("Modify successfully the return at the " + returnAddress + " address");
						
						this.Close();
						EditorWindow.GetWindow(typeof(ReturnListWindow));
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