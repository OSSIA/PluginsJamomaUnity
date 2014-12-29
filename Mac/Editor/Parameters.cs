using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

public class Parameters : Editor
{
	// Add menu item named "Create a new parameter" to the "Jamoma/Parameters" menu
	[MenuItem("Jamoma/Parameters/Create a new parameter")]
	public static void CreateParameter()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(ParameterCreateWindow));
	}

	// Add menu item named "Delete all parameters" to the "Jamoma/Parameters" menu
	[MenuItem("Jamoma/Parameters/Delete all parameters")]
	public static void DeleteAllParameters()
	{
		// Delete the "Parameters.cs" file if exist
		string path = "Assets/Scripts/Parameters.cs";
		if (File.Exists(@path)) 
		{
			File.Delete(@path);
		}

		// Delete the "Parameters.txt" file if exist
		path = "Assets/Parameters.txt";
		if (File.Exists(@path))
		{
			File.Delete(@path);
			
			Debug.Log ("Delete successfully the parameters");
		}
		else
		{
			Debug.Log ("There is no parameters in the game");
		}
	}

	// Add menu item named "List of parameters" to the "Jamoma/Parameters" menu
	[MenuItem("Jamoma/Parameters/List of parameters")]
	public static void ListParameters()
	{
		string path = "Assets/Parameters.txt";
		if (File.Exists(@path))
		{
			//Show existing window instance. If one doesn't exist, make one.
			EditorWindow.GetWindow(typeof(ParameterListWindow));
		}
		else
		{
			Debug.Log ("There is no parameter in the game");
		}
	}
}