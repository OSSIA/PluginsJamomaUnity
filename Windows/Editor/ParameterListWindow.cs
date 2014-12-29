using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;

class ParameterListWindow : EditorWindow
{
	int index = 0;
	string[] parameterList;
	string[] parameterNameList;
	int parameterNumber = 0;

	ParameterListWindow()
	{
		try 
		{
			// Read the "Parameters.txt" file
			string path = "Assets/Parameters.txt";		
		
			// Pass the file path and file name to the StreamReader constructor
			StreamReader srParameter = new StreamReader(path);
		
			string lineParameter;
		
			while ((lineParameter = srParameter.ReadLine()) != null) 
			{
				parameterNumber++;
			}
		
			// Close the "Parameters.txt" file
			srParameter.Close();

			parameterList = new string[parameterNumber];
			parameterNameList = new string[parameterNumber];
		
			// Pass the file path and file name to the StreamReader constructor
			srParameter = new StreamReader(path);

			int i = 0;
		
			while ((lineParameter = srParameter.ReadLine()) != null) 
			{
				string[] words = Regex.Split(lineParameter, "::::");
			
				string parameterName = words[0];
				parameterNameList[i] = parameterName;

				string parameterAddress = words[1];
			
				string type = words[2];

				string rangeClipmode = words[3];

				string rampDrive = words[4];

				parameterList[i] = (i + 1) + ". " + parameterName + " (Type: " + type + "; Address: " + parameterAddress + "; Range Clipmode: " + rangeClipmode + "; Ramp Drive: " + rampDrive;

				if ((type.Equals("decimal")) || (type.Equals("integer")))
				{
					string rangeBoundMin = words[5];
					string rangeBoundMax = words[6];

					parameterList[i] += "; Range Bound: " + rangeBoundMin + " - " + rangeBoundMax + ")";
				}
				else
				{
					parameterList[i] += ")";
				}

				i++;
			}
		
			// Close the "Parameters.txt" file
			srParameter.Close();
		}
		catch(Exception e)
		{
			Debug.Log("Exception: " + e.Message);
		}
	}

	void OnGUI()
	{
		EditorGUILayout.LabelField(" ");
		EditorGUILayout.LabelField("LIST OF PARAMETERS IN THE GAME");
		EditorGUILayout.LabelField(" ");

		for (int i = 0; i < parameterNumber; i++)
		{
			EditorGUILayout.LabelField(parameterList[i]);
			EditorGUILayout.LabelField(" ");
		}

		index = EditorGUILayout.Popup("Select a parameter:", index, parameterNameList);

		EditorGUILayout.LabelField(" ");

		if(GUILayout.Button("Modify the selected parameter"))
		{
			string pathIndex = "Assets/IndexParameterModified.txt";

			// Create a file "IndexParameterModified.txt" to write to 
			using (StreamWriter sw = File.CreateText(@pathIndex)) 
			{
				sw.WriteLine(index);
					
				// Close the "IndexParameterModified.txt" file
				sw.Close();
			}	

			this.Close ();
			EditorWindow.GetWindow(typeof(ParameterModifyWindow));
		}
		
		EditorGUILayout.LabelField(" ");
		
		if(GUILayout.Button("Delete the selected parameter"))
		{
			if (parameterNumber == 1)
			{
				// Delete the "Parameters.txt" file 
				string path = "Assets/Parameters.txt";
				File.Delete(@path);

				// Delete the "Parameters.cs" file
				path = "Assets/Scripts/Parameters.cs";			
				File.Delete(@path);
				
				Debug.Log("You have deleted this parameter. There is no parameter in the game.");
				this.Close ();
			}
			else
			{
				var file = new List<string>(System.IO.File.ReadAllLines("Assets/Parameters.txt"));
				file.RemoveAt(index);
				File.WriteAllLines("Assets/Parameters.txt", file.ToArray());

				// Delete the old "Parameters.cs" file and create a new "Returns.cs" file to write to
				string pathParameterCode = "Assets/Scripts/Parameters.cs";				
				File.Delete(@pathParameterCode);
				
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
				
				this.Close ();
				EditorWindow.GetWindow(typeof(ParameterListWindow));
			}		
		}
	}
}