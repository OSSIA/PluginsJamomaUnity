using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ReturnCreateWindow : EditorWindow
{
	string returnName = "";
	string returnAddress = "";
	ReturnType returnType = ReturnType.DECIMAL;
	string description = "";

	void OnGUI()
	{
		returnName = EditorGUILayout.TextField ("Return Name (*)", returnName);
		returnAddress = EditorGUILayout.TextField ("Return Address (*)", returnAddress);
		returnType = (ReturnType)EditorGUILayout.EnumPopup("Return Type (*)", returnType);
		description = EditorGUILayout.TextField ("Description", description);
		
		if(GUILayout.Button("Create Return"))
		{
			if (returnName.Equals(""))
			{
				Debug.Log ("The name of the return is not valid");
			}
			else 
			{
				returnName = returnName.ToLower();

				if (CommonFunctions.IsNameDeclaredInTextFile(returnName))
				{
					Debug.Log ("The name of the return was declared, you have to choose another name");
				}
				else if (returnAddress.Equals(""))
				{
					Debug.Log ("The address of the return is not valid");
				}
				else if (CommonFunctions.IsAddressDeclaredInTextFile(returnAddress))
				{
					Debug.Log ("The address of the return was declared, you have to choose another address");
				}
				else
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

					try 
					{
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
							
						// Write to the "Returns.txt" file
						string pathReturn = "Assets/Returns.txt";
						if (!File.Exists(@pathReturn)) 
						{
							// Create the "Returns.txt" file to write to
							using (StreamWriter sw = File.CreateText(@pathReturn)) 
							{
								sw.WriteLine(returnInformation);

								// Close the "Returns.txt" file
								sw.Close();
							}	
						}
						else
						{
							using (StreamWriter sw = File.AppendText(@pathReturn)) 
							{
								sw.WriteLine(returnInformation);

								// Close the "Returns.txt" file
								sw.Close();
							}	
						}

						// Delete the "Returns.cs" file if exist
						string pathReturnCode = "Assets/Scripts/Returns.cs";

						if (File.Exists(@pathReturnCode)) 
						{
							File.Delete(@pathReturnCode);
						}

						// Create the "Returns.cs" file to write to
						using (StreamWriter sw = File.CreateText(@pathReturnCode)) 
						{
							sw.WriteLine("using System;");
							sw.WriteLine("using UnityEngine;\n");
							
							sw.WriteLine("public class Returns");
							sw.WriteLine("{");

							// Read the "Returns.txt" file to get the list of returns
							pathReturn = "Assets/Returns.txt";
								
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
						
						Debug.Log ("Create successfully the return at the " + returnAddress + " address");
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