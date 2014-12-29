using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

public class Returns : Editor
{
	// Add menu item named "Create a new return" to the "Jamoma/Returns" menu
	[MenuItem("Jamoma/Returns/Create a new return")]
	public static void CreateReturn()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(ReturnCreateWindow));
	}

	// Add menu item named "Delete all returns" to the "Jamoma/Returns" menu
	[MenuItem("Jamoma/Returns/Delete all returns")]
	public static void DeleteAllReturns()
	{
		// Delete the "Returns.cs" file if exist
		string path = "Assets/Scripts/Returns.cs";
		if (File.Exists(@path)) 
		{
			File.Delete(@path);
		}

		// Delete the "Returns.txt" file if exist
		path = "Assets/Returns.txt";
		if (File.Exists(@path))
		{
			File.Delete(@path);
			
			Debug.Log ("Delete successfully the returns");
		}
		else
		{
			Debug.Log ("There is no return in the game");
		}
	}
	
	// Add menu item named "List of returns" to the "Jamoma/Returns" menu
	[MenuItem("Jamoma/Returns/List of returns")]
	public static void ListReturns()
	{
		string path = "Assets/Returns.txt";
		if (File.Exists(@path))
		{
			//Show existing window instance. If one doesn't exist, make one.
			EditorWindow.GetWindow(typeof(ReturnListWindow));
		}
		else
		{
			Debug.Log ("There is no return in the game");
		}
	}
}