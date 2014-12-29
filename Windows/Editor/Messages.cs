using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

public class Messages : Editor 
{
	// Add menu item named "Create a new message" to the "Jamoma/Messages" menu
	[MenuItem("Jamoma/Messages/Create a new message")]
	public static void CreateMessage()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(MessageCreateWindow));
	}

	// Add menu item named "Delete all messages" to the "Jamoma/Messages" menu
	[MenuItem("Jamoma/Messages/Delete all messages")]
	public static void DeleteAllMessages()
	{
		// Delete the "Messages.cs" file if exist
		string path = "Assets/Scripts/Messages.cs";
		if (File.Exists(@path)) 
		{
			File.Delete(@path);
		}
		
		// Delete the "Messages.txt" file if exist
		path = "Assets/Messages.txt";
		if (File.Exists(@path))
		{
			File.Delete(@path);
			
			Debug.Log ("Delete successfully the messages");
		}
		else
		{
			Debug.Log ("There is no message in the game");
		}
	}

	// Add menu item named "List of messages" to the "Jamoma/Messages" menu
	[MenuItem("Jamoma/Messages/List of messages")]
	public static void ListMessages()
	{
		string path = "Assets/Messages.txt";
		if (File.Exists(@path))
		{
			//Show existing window instance. If one doesn't exist, make one.
			EditorWindow.GetWindow(typeof(MessageListWindow));
		}
		else
		{
			Debug.Log ("There is no message in the game");
		}
	}
}