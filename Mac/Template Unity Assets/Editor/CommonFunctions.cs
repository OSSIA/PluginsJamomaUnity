using System;
using System.IO;
using System.Text.RegularExpressions;

public enum ParameterType { DECIMAL, INTEGER, BOOLEAN, STRING }

public enum ReturnType { DECIMAL, INTEGER, BOOLEAN }

public class CommonFunctions
	{
		/** This function checks if a name has been declared in the "Parameters.txt" and "Returns.txt" and "Messages.txt" files
	     @param[in] name Name to check
	     @return FALSE: This name is not declared yet
	     @return TRUE: This name has been declared */
		public static bool IsNameDeclaredInTextFile (string name)
		{
			string pathParameter = "Assets/Parameters.txt";
			if (File.Exists(@pathParameter))
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader srParameter = new StreamReader(pathParameter);
				
				string lineParameter;
				
				while ((lineParameter = srParameter.ReadLine()) != null) 
				{
					string[] words = Regex.Split(lineParameter, "::::");
					
					if (name.Equals(words[0]))
					{
						return true;
					}
				}
				
				//close the file
				srParameter.Close();
			}
			
			string pathReturn = "Assets/Returns.txt";
			if (File.Exists(@pathReturn))
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader srReturn = new StreamReader(pathReturn);
				
				string lineReturn;
				
				while ((lineReturn = srReturn.ReadLine()) != null) 
				{
					string[] words = Regex.Split(lineReturn, "::::");
					
					if (name.Equals(words[0]))
					{
						return true;
					}
				}
				
				//close the file
				srReturn.Close();
			}

			string pathMessage = "Assets/Messages.txt";
			if (File.Exists(@pathMessage))
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader srMessage = new StreamReader(pathMessage);

				string lineMessage;
				
				while ((lineMessage = srMessage.ReadLine()) != null) 
				{
					string[] words = Regex.Split(lineMessage, "::::");
					
					if (name.Equals(words[0]))
					{
						return true;
					}
				}
				
				//close the file
				srMessage.Close();
			}
			
			return false;
		}

		/** This function checks if an address has been declared in the "Parameters.txt" and "Returns.txt" and "Messages.txt" files
     	@param[in] address Address to check
     	@return FALSE: This address is not declared yet
     	@return TRUE: This address has been declared */
		public static bool IsAddressDeclaredInTextFile (string address)
		{
			string pathParameter = "Assets/Parameters.txt";
			if (File.Exists(@pathParameter))
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader srParameter = new StreamReader(pathParameter);
				
				string lineParameter;

				while ((lineParameter = srParameter.ReadLine()) != null) 
				{
					string[] words = Regex.Split(lineParameter, "::::");
					
					if (address.Equals(words[1]))
					{
						return true;
					}
				}
				
				//close the file
				srParameter.Close();
			}

			string pathReturn = "Assets/Returns.txt";
			if (File.Exists(@pathReturn))
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader srReturn = new StreamReader(pathReturn);
				
				string lineReturn;
				
				while ((lineReturn = srReturn.ReadLine()) != null) 
				{
					string[] words = Regex.Split(lineReturn, "::::");
					
					if (address.Equals(words[1]))
					{
						return true;
					}
				}
				
				//close the file
				srReturn.Close();
			}

			string pathMessage = "Assets/Messages.txt";
			if (File.Exists(@pathMessage))
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader srMessage = new StreamReader(pathMessage);

				string lineMessage;
				
				while ((lineMessage = srMessage.ReadLine()) != null) 
				{
					string[] words = Regex.Split(lineMessage, "::::");
					
					if (address.Equals(words[1]))
					{
						return true;
					}
				}
				
				//close the file
				srMessage.Close();
			}
			
			return false;
		}
	}