using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ParameterModifyWindow : EditorWindow
{
	string parameterName = "";
	string parameterAddress = "";
	ParameterType parameterType;
	string rangeClipmode = "";
	string rampDrive = "";
	string rangeBoundMin = "";
	string rangeBoundMax = "";
	string description = "";

	string oldParameterName = "";
	string oldParameterAddress = "";

	string lineParameterModified = ""; 

	ParameterModifyWindow()
	{
		// Read the "IndexParameterModified.txt" file
		string pathIndex = "Assets/IndexParameterModified.txt";		
		
		// Pass the file path and file name to the StreamReader constructor
		StreamReader srIndex = new StreamReader(pathIndex);

		int index = Convert.ToInt32(srIndex.ReadLine());
		
		// Close the "IndexParameterModified.txt" file
		srIndex.Close();

		// Delete the "IndexParameterModified.txt" file
		File.Delete(@pathIndex);

		// Read the "Parameters.txt" file
		string pathParameter = "Assets/Parameters.txt";		
		
		// Pass the file path and file name to the StreamReader constructor
		StreamReader srParameter = new StreamReader(pathParameter);

		int i = 0;

		while ((lineParameterModified = srParameter.ReadLine()) != null) 
		{
			if (index == i)
			{
				string[] words = Regex.Split(lineParameterModified, "::::");

				parameterName = words[0];
				oldParameterName = parameterName;

				parameterAddress = words[1];
				oldParameterAddress = parameterAddress;
				
				string type = words[2];

				if (type.Equals("decimal"))
				{
					parameterType = ParameterType.DECIMAL;
				}
				else if (type.Equals("integer"))
				{
					parameterType = ParameterType.INTEGER;
				}
				else if (type.Equals("string"))
				{
					parameterType = ParameterType.STRING;
				}
				else
				{
					parameterType = ParameterType.BOOLEAN;
				}

				rangeClipmode = words[3];

				rampDrive = words[4];

				if ((type.Equals("decimal")) || (type.Equals("integer")))
				{
					rangeBoundMin = words[5];
					rangeBoundMax = words[6];
					description = words[7];
				}
				else
				{
					description = words[5];
				}

				if (description.Equals("null"))
				{
					description = "";
				}

				break;
			}

			i++;
		}

		// Close the "Parameters.txt" file
		srParameter.Close();
	}
	
	void OnGUI()
	{
		parameterName = EditorGUILayout.TextField ("Parameter Name (*)", parameterName);
		parameterAddress = EditorGUILayout.TextField ("Parameter Address (*)", parameterAddress);
		parameterType = (ParameterType)EditorGUILayout.EnumPopup("Parameter Type (*)", parameterType);
		rangeClipmode = EditorGUILayout.TextField ("Range Clipmode (*)", rangeClipmode);
		rampDrive = EditorGUILayout.TextField ("Ramp Drive (*)", rampDrive);
		rangeBoundMin = EditorGUILayout.TextField ("Range Bound Min", rangeBoundMin);
		rangeBoundMax = EditorGUILayout.TextField ("Range Bound Max", rangeBoundMax);
		description = EditorGUILayout.TextField ("Description", description);
		
		if(GUILayout.Button("Modify Parameter"))
		{
			if (parameterName.Equals(""))
			{
				Debug.Log ("The name of the parameter is not valid");
			}
			else 
			{
				parameterName = parameterName.ToLower();

				if ((!(parameterName.Equals(oldParameterName))) && CommonFunctions.IsNameDeclaredInTextFile(parameterName))
				{
					Debug.Log ("The name of the parameter was declared, you have to choose another address");
				} 
				else if (parameterAddress.Equals(""))
				{
					Debug.Log ("The address of the parameter is not valid");
				}
				else if ((!(parameterAddress.Equals(oldParameterAddress))) && CommonFunctions.IsAddressDeclaredInTextFile(parameterAddress))
				{
					Debug.Log ("The address of the parameter was declared, you have to choose another address");
				}
				else if (rangeClipmode.Equals(""))
				{
					Debug.Log ("Range Clipmode is not valid");
				}
				else if (rampDrive.Equals(""))
				{
					Debug.Log ("Ramp Drive is not valid");
				}
				else
				{
					string type = "";
					if (parameterType.ToString().Equals("DECIMAL"))
					{
						type = "decimal";
					}
					else if (parameterType.ToString().Equals("INTEGER"))
					{
						type = "integer";
					}
					else if (parameterType.ToString().Equals("BOOLEAN"))
					{
						type = "boolean";
					}
					else
					{
						type = "string";
					}

					// The champs in the information of the parameter is split by "::::"
					string parameterInformation = parameterName + "::::" + parameterAddress + "::::" + type + "::::" + rangeClipmode + "::::" + rampDrive + "::::";
					
					if ((type.Equals("decimal")) || (type.Equals("integer")))
					{
						float rangeBoundMinFloat = 0F;
						bool rangeBoundMinValid = true;
						
						try 
						{
							rangeBoundMinFloat = float.Parse(rangeBoundMin);
						}
						catch (Exception e)
						{
							rangeBoundMinValid = false;
							Debug.Log("Exception: " + e.Message);
						}
						
						float rangeBoundMaxFloat = 0F;
						bool rangeBoundMaxValid = true;
						
						try 
						{
							rangeBoundMaxFloat = float.Parse(rangeBoundMax);
						}
						catch (Exception e)
						{
							rangeBoundMaxValid = false;
							Debug.Log("Exception: " + e.Message);
						}
						
						if (!rangeBoundMinValid)
						{
							Debug.Log ("Range Bound Min is not valid");
						}
						else if (!rangeBoundMaxValid)
						{
							Debug.Log ("Range Bound Max is not valid");
						}
						else if (rangeBoundMaxFloat < rangeBoundMinFloat)
						{
							Debug.Log ("Range Bound Max is smaller than Range Bound Min: NOT VALID");
						}
						else
						{
							parameterInformation += rangeBoundMin + "::::" + rangeBoundMax + "::::";
							
							if (description.Equals(""))
							{
								parameterInformation += "null";
							}
							else
							{
								parameterInformation += description;
							}

							try 
							{
								string strParameter = File.ReadAllText("Assets/Parameters.txt");
								strParameter = strParameter.Replace(lineParameterModified,parameterInformation);
								File.WriteAllText("Assets/Parameters.txt", strParameter);

								// Delete the old "Parameters.cs" file
								string pathParameterCode = "Assets/Scripts/Parameters.cs";
								
								File.Delete(@pathParameterCode);
								
								// Create a new "Parameters.cs" file to write to
								using (StreamWriter sw = File.CreateText(@pathParameterCode)) 
								{
									sw.WriteLine("using System;");
									sw.WriteLine("using UnityEngine;");
									sw.WriteLine("using System.Runtime.InteropServices;\n");
									
									sw.WriteLine("public class Parameters");
									sw.WriteLine("{");
									
									// Read the "Parameters.txt" file to get the list of parameters
									string pathParameter = "Assets/Parameters.txt";
									
									// Pass the file path and file name to the StreamReader constructor
									StreamReader srParameter = new StreamReader(pathParameter);
									String lineParameter;
									
									while ((lineParameter = srParameter.ReadLine()) != null) 
									{
										string[] words = Regex.Split(lineParameter, "::::");
										
										string parameterNameCode = words[0];
										
										string parameterAddressCode = words[1];
										
										string typeCode = words[2];
										
										string str = "";
										
										if (typeCode.Equals("integer"))
										{
											str = "\tprivate static int " + parameterNameCode + ";";
											sw.WriteLine(str);
											
											str = "\tpublic static int " + parameterNameCode.ToUpper();
											sw.WriteLine(str);
										}
										else if (typeCode.Equals("decimal"))
										{
											str = "\tprivate static float " + parameterNameCode + ";";
											sw.WriteLine(str);
											
											str = "\tpublic static float " + parameterNameCode.ToUpper();
											sw.WriteLine(str);
										}
										else if (typeCode.Equals("boolean"))
										{
											str = "\tprivate static bool " + parameterNameCode + ";";
											sw.WriteLine(str);
											
											str = "\tpublic static bool " + parameterNameCode.ToUpper();
											sw.WriteLine(str);
										}
										else
										{
											str = "\tprivate static string " + parameterNameCode + ";";
											sw.WriteLine(str);
											
											str = "\tpublic static string " + parameterNameCode.ToUpper();
											sw.WriteLine(str);
										}
										
										sw.WriteLine("\t{");
										sw.WriteLine("\t\tget");
										sw.WriteLine("\t\t{");
										
										str = "\t\t\tif (PluginJamomaUnity.HaveParameterAddress(\"" + parameterAddressCode + "\"))";
										sw.WriteLine(str);
										
										sw.WriteLine("\t\t\t{");
										
										if (typeCode.Equals("integer"))
										{
											str = "\t\t\t\t" + parameterNameCode + " = PluginJamomaUnity.GetIntegerParameter (\"" + parameterAddressCode + "\");"; 
										}
										else if (typeCode.Equals("decimal"))
										{
											str = "\t\t\t\t" + parameterNameCode + " = PluginJamomaUnity.GetFloatParameter (\"" + parameterAddressCode + "\");"; 
										}
										else if (typeCode.Equals("boolean"))
										{
											str = "\t\t\t\t" + parameterNameCode + " = PluginJamomaUnity.GetBooleanParameter (\"" + parameterAddressCode + "\");"; 
										}
										else
										{
											str = "\t\t\t\t" + parameterNameCode + " = Marshal.PtrToStringAnsi(PluginJamomaUnity.GetStringParameter (\"" + parameterAddressCode + "\"));"; 
										}
										
										sw.WriteLine(str);
										
										sw.WriteLine("\t\t\t}");
										sw.WriteLine("\t\t\telse");
										sw.WriteLine("\t\t\t{");
										
										str = "\t\t\t\tDebug.Log(\"There is not " + parameterAddressCode + " in the list of addresses in Unity\");";
										sw.WriteLine(str);
										
										sw.WriteLine("\t\t\t}\n");
										
										str = "\t\t\treturn " + parameterNameCode  + ";";
										sw.WriteLine(str);
										
										sw.WriteLine("\t\t}");
										sw.WriteLine("\t\tset");
										sw.WriteLine("\t\t{");
										
										str = "\t\t\t" + parameterNameCode  + " = value;";
										sw.WriteLine(str);
										
										str = "\t\t\tPluginJamomaUnity.SetParameter (\"" + parameterAddressCode + "\", " + parameterNameCode + ");";
										sw.WriteLine(str);
										
										sw.WriteLine("\t\t}");
										sw.WriteLine("\t}\n");
									}
									
									sw.WriteLine("}");
									
									// Close the "Parameters.cs" file
									sw.Close();
									
									// Close the "Parameters.txt" file
									srParameter.Close();
								}
								
								Debug.Log ("Modify successfully the parameter at the " + parameterAddress + " address");
								
								this.Close();
								EditorWindow.GetWindow(typeof(ParameterListWindow));
							}
							catch(Exception e)
							{
								Debug.Log("Exception: " + e.Message);
							}
						}
					}
					else
					{
						if (description.Equals(""))
						{
							parameterInformation += "null";
						}
						else
						{
							parameterInformation += description;
						}
						
						try 
						{
							string strParameter = File.ReadAllText("Assets/Parameters.txt");
							strParameter = strParameter.Replace(lineParameterModified,parameterInformation);
							File.WriteAllText("Assets/Parameters.txt", strParameter);
							
							// Delete the old "Parameters.cs" file
							string pathParameterCode = "Assets/Scripts/Parameters.cs";
							
							File.Delete(@pathParameterCode);
							
							// Create a new "Parameters.cs" file to write to
							using (StreamWriter sw = File.CreateText(@pathParameterCode)) 
							{
								sw.WriteLine("using System;");
								sw.WriteLine("using UnityEngine;");
								sw.WriteLine("using System.Runtime.InteropServices;\n");
								
								sw.WriteLine("public class Parameters");
								sw.WriteLine("{");
								
								// Read the "Parameters.txt" file to get the list of parameters
								string pathParameter = "Assets/Parameters.txt";
								
								// Pass the file path and file name to the StreamReader constructor
								StreamReader srParameter = new StreamReader(pathParameter);
								String lineParameter;
								
								while ((lineParameter = srParameter.ReadLine()) != null) 
								{
									string[] words = Regex.Split(lineParameter, "::::");
									
									string parameterNameCode = words[0];
									
									string parameterAddressCode = words[1];
									
									string typeCode = words[2];
									
									string str = "";
									
									if (typeCode.Equals("integer"))
									{
										str = "\tprivate static int " + parameterNameCode + ";";
										sw.WriteLine(str);
										
										str = "\tpublic static int " + parameterNameCode.ToUpper();
										sw.WriteLine(str);
									}
									else if (typeCode.Equals("decimal"))
									{
										str = "\tprivate static float " + parameterNameCode + ";";
										sw.WriteLine(str);
										
										str = "\tpublic static float " + parameterNameCode.ToUpper();
										sw.WriteLine(str);
									}
									else if (typeCode.Equals("boolean"))
									{
										str = "\tprivate static bool " + parameterNameCode + ";";
										sw.WriteLine(str);
										
										str = "\tpublic static bool " + parameterNameCode.ToUpper();
										sw.WriteLine(str);
									}
									else
									{
										str = "\tprivate static string " + parameterNameCode + ";";
										sw.WriteLine(str);
										
										str = "\tpublic static string " + parameterNameCode.ToUpper();
										sw.WriteLine(str);
									}
									
									sw.WriteLine("\t{");
									sw.WriteLine("\t\tget");
									sw.WriteLine("\t\t{");
									
									str = "\t\t\tif (PluginJamomaUnity.HaveParameterAddress(\"" + parameterAddressCode + "\"))";
									sw.WriteLine(str);
									
									sw.WriteLine("\t\t\t{");
									
									if (typeCode.Equals("integer"))
									{
										str = "\t\t\t\t" + parameterNameCode + " = PluginJamomaUnity.GetIntegerParameter (\"" + parameterAddressCode + "\");"; 
									}
									else if (typeCode.Equals("decimal"))
									{
										str = "\t\t\t\t" + parameterNameCode + " = PluginJamomaUnity.GetFloatParameter (\"" + parameterAddressCode + "\");"; 
									}
									else if (typeCode.Equals("boolean"))
									{
										str = "\t\t\t\t" + parameterNameCode + " = PluginJamomaUnity.GetBooleanParameter (\"" + parameterAddressCode + "\");"; 
									}
									else
									{
										str = "\t\t\t\t" + parameterNameCode + " = Marshal.PtrToStringAnsi(PluginJamomaUnity.GetStringParameter (\"" + parameterAddressCode + "\"));"; 
									}
									
									sw.WriteLine(str);
									
									sw.WriteLine("\t\t\t}");
									sw.WriteLine("\t\t\telse");
									sw.WriteLine("\t\t\t{");
									
									str = "\t\t\t\tDebug.Log(\"There is not " + parameterAddressCode + " in the list of addresses in Unity\");";
									sw.WriteLine(str);
									
									sw.WriteLine("\t\t\t}\n");
									
									str = "\t\t\treturn " + parameterNameCode  + ";";
									sw.WriteLine(str);
									
									sw.WriteLine("\t\t}");
									sw.WriteLine("\t\tset");
									sw.WriteLine("\t\t{");
									
									str = "\t\t\t" + parameterNameCode  + " = value;";
									sw.WriteLine(str);
									
									str = "\t\t\tPluginJamomaUnity.SetParameter (\"" + parameterAddressCode + "\", " + parameterNameCode + ");";
									sw.WriteLine(str);
									
									sw.WriteLine("\t\t}");
									sw.WriteLine("\t}\n");
								}
								
								sw.WriteLine("}");
								
								// Close the "Parameters.cs" file
								sw.Close();
								
								// Close the "Parameters.txt" file
								srParameter.Close();
							}
							
							Debug.Log ("Modify successfully the parameter at the " + parameterAddress + " address");
							
							this.Close();
							EditorWindow.GetWindow(typeof(ParameterListWindow));
						}
						catch(Exception e)
						{
							Debug.Log("Exception: " + e.Message);
						}
					}
				}
			}
		}
		
		string help = "(*): REQUIRED INFORMATION.\n\n";
		help += "'Range Bound Min' and 'Range Bound Max' are REQUIRED if 'Parameter Type' is 'INTEGER' or 'DECIMAL'.";
		
		EditorGUILayout.HelpBox(help, MessageType.Info);
	}
}