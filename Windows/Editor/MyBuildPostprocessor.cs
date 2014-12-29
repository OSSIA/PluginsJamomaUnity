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
			string path_destination = Path.GetDirectoryName(pathToBuiltProject) + "/" + Path.GetFileNameWithoutExtension(pathToBuiltProject)+"_Data";

			string path = ""; 

			path = "Assets/Messages.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/Messages.txt", path_destination + "/Messages.txt");
			}

			path = "Assets/Returns.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/Returns.txt", path_destination + "/Returns.txt");
			}
		
			path = "Assets/Parameters.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/Parameters.txt", path_destination + "/Parameters.txt");
			}

			path = "Assets/JamomaConfiguration.txt"; 
			if (File.Exists(@path))
			{
				File.Copy("Assets/JamomaConfiguration.txt", path_destination + "/JamomaConfiguration.txt");
			}

			// Copy .score files
			string sourceDossier = @"Assets";
			string destinationDossier = @path_destination;

			string[] scoreFileList = Directory.GetFiles(sourceDossier, "*.score");

			foreach (string f in scoreFileList)
			{
				// Remove path from the file name
				string fName = f.Substring(sourceDossier.Length + 1);
				
				// Use the Path.Combine method to safely append the file name to the path
				// Will overwrite if the destination file already exists
				File.Copy(Path.Combine(sourceDossier, fName), Path.Combine(destinationDossier, fName), true);
			}

			// Copy librairy files
			string path_source_libraries = Path.GetDirectoryName (Application.dataPath);

			string path_destination_libraries = Path.GetDirectoryName (path_destination);

			// Copy .ttdll files
			string[] TTDLL_FileList = Directory.GetFiles(path_source_libraries, "*.ttdll");			

			foreach (string f in TTDLL_FileList)
			{
				// Remove path from the file name
				string fName = f.Substring(path_source_libraries.Length + 1);
				
				// Use the Path.Combine method to safely append the file name to the path
				// Will overwrite if the destination file already exists
				File.Copy(Path.Combine(path_source_libraries, fName), Path.Combine(path_destination_libraries, fName), true);
			}

			// Copy .dll files
			string[] DLL_FileList = Directory.GetFiles(path_source_libraries, "*.dll");			

			foreach (string f in DLL_FileList)
			{
				// Remove path from the file name
				string fName = f.Substring(path_source_libraries.Length + 1);
				
				// Use the Path.Combine method to safely append the file name to the path
				// Will overwrite if the destination file already exists
				File.Copy(Path.Combine(path_source_libraries, fName), Path.Combine(path_destination_libraries, fName), true);
			}

			Debug.Log("Copy successfully the data files.");
		}
		catch(Exception e)
		{
			Debug.Log("Exception: " + e.Message);
		}
	}
}