using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ParameterCreateWindow : EditorWindow
{
	string parameterName = "";
	string parameterAddress = "";
	ParameterType parameterType = ParameterType.DECIMAL;
	string rangeClipmode = "both";
	string rampDrive = "System";
	string rangeBoundMin = "";
	string rangeBoundMax = "";
	string description = "";

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
		
		if(GUILayout.Button("Create Parameter"))
		{
			if (parameterName.Equals(""))
			{
				Debug.Log ("The name of the parameter is not valid");
			}
			else 
			{
				parameterName = parameterName.ToLower();
				
				if (CommonFunctions.IsNameDeclaredInTextFile(parameterName))
				{
					Debug.Log ("The name of the parameter was declared, you have to choose another name");
				}
				else if (parameterAddress.Equals(""))
				{
					Debug.Log ("The address of the parameter is not valid");
				}
				else if (CommonFunctions.IsAddressDeclaredInTextFile(parameterAddress))
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
								// Write to the "Parameters.txt" file
								string pathParameter = "Assets/Parameters.txt";
								if (!File.Exists(@pathParameter)) 
								{
									// Create the "Parameters.txt" file to write to 
									using (StreamWriter sw = File.CreateText(@pathParameter)) 
									{
										sw.WriteLine(parameterInformation);
										
										// Close the "Parameters.txt" file
										sw.Close();
									}	
								}
								else
								{
									using (StreamWriter sw = File.AppendText(@pathParameter)) 
									{
										sw.WriteLine(parameterInformation);
										
										// Close the "Parameters.txt" file
										sw.Close();
									}	
								}

								// Delete the "Parameters.cs" file if exist
								string pathParameterCode = "Assets/Scripts/Parameters.cs";
								
								if (File.Exists(@pathParameterCode)) 
								{
									File.Delete(@pathParameterCode);
								}
								
								// Create the "Parameters.cs" file to write to
								using (StreamWriter sw = File.CreateText(@pathParameterCode)) 
								{
									sw.WriteLine("using System;");
									sw.WriteLine("using UnityEngine;");
									sw.WriteLine("using System.Runtime.InteropServices;\n");
									
									sw.WriteLine("public class Parameters");
									sw.WriteLine("{");
									
									// Read the "Parameters.txt" file to get the list of parameters
									pathParameter = "Assets/Parameters.txt";
									
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

								Debug.Log ("Create successfully the parameter at the " + parameterAddress + " address");
							}
							catch(Exception e)
							{
								Debug.Log("Exception: " + e.Message);
							}
						}
					}
					else  	// type is "boolean" or "string"
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
							// Write to the "Parameters.txt" file
							string pathParameter = "Assets/Parameters.txt";
							if (!File.Exists(@pathParameter)) 
							{
								// Create the "Parameters.txt" file to write to 
								using (StreamWriter sw = File.CreateText(@pathParameter)) 
								{
									sw.WriteLine(parameterInformation);
									
									// Close the "Parameters.txt" file
									sw.Close();
								}	
							}
							else
							{
								using (StreamWriter sw = File.AppendText(@pathParameter)) 
								{
									sw.WriteLine(parameterInformation);
									
									// Close the "Parameters.txt" file
									sw.Close();
								}	
							}
							
							// Delete the "Parameters.cs" file if exist
							string pathParameterCode = "Assets/Scripts/Parameters.cs";
							
							if (File.Exists(@pathParameterCode)) 
							{
								File.Delete(@pathParameterCode);
							}
							
							// Create the "Parameters.cs" file to write to
							using (StreamWriter sw = File.CreateText(@pathParameterCode)) 
							{
								sw.WriteLine("using System;");
								sw.WriteLine("using UnityEngine;");
								sw.WriteLine("using System.Runtime.InteropServices;\n");
								
								sw.WriteLine("public class Parameters");
								sw.WriteLine("{");
								
								// Read the "Parameters.txt" file to get the list of parameters
								pathParameter = "Assets/Parameters.txt";
								
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
							
							Debug.Log ("Create successfully the parameter at the " + parameterAddress + " address");
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