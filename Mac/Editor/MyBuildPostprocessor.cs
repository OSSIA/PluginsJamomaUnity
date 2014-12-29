using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System;

public class MyBuildPostprocessor {
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
		try 
		{
			string path = ""; 

			path = "Assets/Messages.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/Messages.txt", pathToBuiltProject + "/Contents/Messages.txt");
			}

			path = "Assets/Returns.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/Returns.txt", pathToBuiltProject + "/Contents/Returns.txt");
			}

			path = "Assets/Parameters.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/Parameters.txt", pathToBuiltProject + "/Contents/Parameters.txt");
			}

			path = "Assets/JamomaConfiguration.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/JamomaConfiguration.txt", pathToBuiltProject + "/Contents/JamomaConfiguration.txt");
			}

			string sourceDossier = @"Assets";
			string destination = pathToBuiltProject + "/Contents";
			string destinationDossier = @destination;

			string[] scoreFileList = Directory.GetFiles(sourceDossier, "*.score");

			// Copy .score files
			foreach (string f in scoreFileList)
			{
				// Remove path from the file name
				string fName = f.Substring(sourceDossier.Length + 1);
				
				// Use the Path.Combine method to safely append the file name to the path
				// Will overwrite if the destination file already exists
				File.Copy(Path.Combine(sourceDossier, fName), Path.Combine(destinationDossier, fName), true);
			}

			Debug.Log("Copy successfully the data files.");
		}
		catch(Exception e)
		{
			Debug.Log("Exception: " + e.Message);
		}
	}
}