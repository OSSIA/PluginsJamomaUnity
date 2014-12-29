using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;


public class JamomaConfigureWindow : EditorWindow
{
	string localApplicationName = "";
	string distantApplicationName = "";
	string localApplicationPort = "";
	string localApplicationIpAddress = "";
	string distantApplicationPort = "";
	string distantApplicationIpAddress = "";
	string authorName = "";
	string versionName = "";

	JamomaConfigureWindow()
	{
		string path = "Assets/JamomaConfiguration.txt";
		if (File.Exists(@path))
		{
			try 
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader srJamomaConfiguration = new StreamReader(path);
				
				localApplicationName = srJamomaConfiguration.ReadLine();
				
				distantApplicationName = srJamomaConfiguration.ReadLine();
				
				localApplicationPort = srJamomaConfiguration.ReadLine();
				
				localApplicationIpAddress = srJamomaConfiguration.ReadLine();
				
				distantApplicationPort = srJamomaConfiguration.ReadLine();
				
				distantApplicationIpAddress = srJamomaConfiguration.ReadLine();
				
				authorName = srJamomaConfiguration.ReadLine();
				if (authorName.Equals("null"))
				{
					authorName = "";
				}
				
				versionName = srJamomaConfiguration.ReadLine();
				if (versionName.Equals("null"))
				{
					versionName = "";
				}
				
				//close the file
				srJamomaConfiguration.Close();
			}
			catch(Exception e)
			{
				Debug.Log("Exception: " + e.Message);
				
				localApplicationName = "unity";
				distantApplicationName = "i-score";
				localApplicationPort = "9998";
				localApplicationIpAddress = "127.0.0.1";
				distantApplicationPort = "13579";
				distantApplicationIpAddress = "127.0.0.1";
				authorName = "";
				versionName = "";
			}
		}
		else
		{
			localApplicationName = "unity";
			distantApplicationName = "i-score";
			localApplicationPort = "9998";
			localApplicationIpAddress = "127.0.0.1";
			distantApplicationPort = "13579";
			distantApplicationIpAddress = "127.0.0.1";
			authorName = "";
			versionName = "";		
		}
	}
	
	// Add menu item named "Configure Jamoma" to the "Jamoma" menu
	[MenuItem("Jamoma/Configure Jamoma")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(JamomaConfigureWindow));
	}
	
	void OnGUI()
	{
		localApplicationName = EditorGUILayout.TextField ("Local Application Name", localApplicationName);
		distantApplicationName = EditorGUILayout.TextField ("Distant Application Name", distantApplicationName);
		localApplicationPort = EditorGUILayout.TextField ("Local Application Port", localApplicationPort);
		localApplicationIpAddress = EditorGUILayout.TextField ("Local Application Ip", localApplicationIpAddress);
		distantApplicationPort = EditorGUILayout.TextField ("Distant Application Port", distantApplicationPort);
		distantApplicationIpAddress = EditorGUILayout.TextField ("Distant Application Ip", distantApplicationIpAddress);
		authorName = EditorGUILayout.TextField ("Author Name", authorName);
		versionName = EditorGUILayout.TextField ("Version Name", versionName);		
		
		if(GUILayout.Button("Setup Jamoma Configuration"))
		{
			int localApplicationPortInt;
			bool localPortValid = true;
			
			try 
			{
				localApplicationPortInt = Convert.ToInt32(localApplicationPort);
			}
			catch (Exception e)
			{
				localPortValid = false;
				Debug.Log("Exception: " + e.Message);
			}
			
			int distantApplicationPortInt;
			bool distantPortValid = true;
			
			try 
			{
				distantApplicationPortInt = Convert.ToInt32(distantApplicationPort);
			}
			catch (Exception e)
			{
				distantPortValid = false;
				Debug.Log("Exception: " + e.Message);
			}
			
			if (localApplicationName.Equals(""))
			{
				Debug.Log ("Local Application Name is not valid");
			}
			else if (distantApplicationName.Equals(""))
			{
				Debug.Log ("Distant Application Name is not valid");
			}
			else if (!localPortValid)
			{
				Debug.Log ("Local Application Port is not valid");
			}
			else if (localApplicationIpAddress.Equals(""))
			{
				Debug.Log ("Local Application Ip Address is not valid");
			}
			else if (!distantPortValid)
			{
				Debug.Log ("Distant Application Port is not valid");
			}
			else if (distantApplicationIpAddress.Equals(""))
			{
				Debug.Log ("Distant Application Ip Address is not valid");
			}
			else
			{
				// Record the configuration of the Jamoma library into the "JamomaConfiguration.txt" file
				try 
				{	
					// Pass the filepath and filename to the StreamWriter Constructor
					StreamWriter sw = new StreamWriter("Assets/JamomaConfiguration.txt");
					
					sw.WriteLine(localApplicationName);
					sw.WriteLine(distantApplicationName);
					sw.WriteLine(localApplicationPort);
					sw.WriteLine(localApplicationIpAddress);
					sw.WriteLine(distantApplicationPort);
					sw.WriteLine(distantApplicationIpAddress);
					
					if (authorName.Equals(""))
					{
						sw.WriteLine("null");
					}
					else
					{
						sw.WriteLine(authorName);
					}
					
					if (versionName.Equals(""))
					{
						sw.WriteLine("null");
					}
					else
					{
						sw.WriteLine(versionName);
					}
					
					//Close the file
					sw.Close();
					
					Debug.Log ("Setup successfully the configuration of the Jamoma library");
				}
				catch(Exception e)
				{
					Debug.Log("Exception: " + e.Message);
				}
			}
		}
		
		string help = "\nAll information is REQUIRED (EXCEPT 'Author Name' and 'Version Name').\n";
		
		EditorGUILayout.HelpBox(help, MessageType.Info);
	}
}