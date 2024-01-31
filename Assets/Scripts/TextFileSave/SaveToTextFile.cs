using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class SaveToTextFile : MonoBehaviour
{
    // Input fields for folder name, file name, and file content
    [Header("Folders")]
    public TMP_InputField folderNameInputField;
    public TMP_Text folderNameInputFeedback;
    public string customFolderName;

    [Header("Files")]
    public TMP_InputField fileNameInputField;
    public TMP_Text fileNameInputFeedback;
    public string customFileName;

    [Header("File Content")]
    public TMP_InputField fileContentInputField;
    public TMP_Text fileContentInputFeedback;
    public string contentForTextFile;

    // Directory path to store files
    public string directoryName;

    // Prints the device persistent path to console
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
    }

    // Checks if a folder with the given name exists and provides feedback
    public void CheckForDirectory()
    {
        // Check for empty folder name
        if (folderNameInputField.text == "")
        {
            folderNameInputFeedback.text = "Enter a folder name";
        }
        else
        {
            customFolderName = folderNameInputField.text;
            directoryName = Application.persistentDataPath + "/" + customFolderName + "/";

            // Check if the folder exists
            if (Directory.Exists(directoryName))
            {
                folderNameInputFeedback.text = "There is a folder by that name in the directory";
            }
            else
            {
                folderNameInputFeedback.text = "Folder by that name does not exist";
            }
        }
    }

    // Creates a folder at the specified location if it does not already exist
    public void CreateDirectory()
    {
        // Check for empty folder name
        if (folderNameInputField.text == "")
        {
            folderNameInputFeedback.text = "Enter a folder name";
        }
        else
        {
            // Check if the directory already exists
            if (Directory.Exists(directoryName))
            {
                folderNameInputFeedback.text = "A directory of that name already exists";
            }
            else
            {
                Directory.CreateDirectory(directoryName);
                folderNameInputFeedback.text = "Your directory has now been created @ " + directoryName;
                Debug.Log("Your directory has now been created @ " + directoryName);
            }
        }
    }

    // Deletes a folder at the specified location if it exists
    public void DeleteDirectory()
    {
        // Check for empty folder name
        if (folderNameInputField.text == "")
        {
            folderNameInputFeedback.text = "Enter a folder name";
        }
        else
        {
            customFolderName = folderNameInputField.text;
            directoryName = Application.persistentDataPath + "/" + customFolderName + "/";

            // Check if the folder exists and delete it
            if (Directory.Exists(directoryName))
            {
                Directory.Delete(directoryName);
                folderNameInputFeedback.text = "The folder has been found and deleted";
            }
            else
            {
                folderNameInputFeedback.text = "Folder by that name does not exist";
            }
        }
    }

    // Checks if a file with the given name exists at the desired location
    public void CheckForTextFile()
    {
        customFileName = fileNameInputField.text;

        // Set default directory to persistent data path if not specified
        if (directoryName == null || directoryName == "")
        {
            directoryName = Application.persistentDataPath + "/";
        }
        else
        {
            directoryName = Application.persistentDataPath + "/" + customFolderName + "/";
        }

        // Check for empty file name
        if (customFileName == null || customFileName == "")
        {
            fileNameInputFeedback.text = "Enter a file name";
        }
        else
        {
            string textDocumentName = directoryName + customFileName + ".txt";

            // Check if the file exists
            if (File.Exists(textDocumentName))
            {
                fileNameInputFeedback.text = "A file exists at " + textDocumentName;
            }
            else
            {
                // Provide feedback for non-existent file
                if (directoryName == null || directoryName == "")
                {
                    fileNameInputFeedback.text = "There is no file found called " + customFileName + " @ " + Application.persistentDataPath;
                }
                else
                {
                    fileNameInputFeedback.text = "There is no file found called " + customFileName + " @ " + directoryName;
                }
            }
        }
    }

    // Creates a text file at the specified location if it does not already exist
    public void CreateTextFile()
    {
        customFileName = fileNameInputField.text;
        string textDocumentName = directoryName + customFileName + ".txt";

        // Check for empty file name
        if (customFileName == null || customFileName == "")
        {
            fileNameInputFeedback.text = "Enter a file name";
        }
        else
        {
            // Check if the file already exists
            if (directoryName == null || directoryName == "")
            {
                if (File.Exists(textDocumentName))
                {
                    fileNameInputFeedback.text = "There is already a file named " + customFileName + " @ " + Application.persistentDataPath + " and a new one has NOT been created";
                    Debug.Log("There is already a file named " + customFileName + " @ " + Application.persistentDataPath + " and a new one has NOT been created");
                }
                else
                {
                    // Create the file and provide feedback
                    File.Create(textDocumentName).Dispose();
                    fileNameInputFeedback.text = "This file " + customFileName + " has been saved to the persistent data path @ " + Application.persistentDataPath;
                    Debug.Log("This file " + customFileName + " has been saved to the persistent data path @ " + Application.persistentDataPath);
                }
            }
            else
            {
                if (File.Exists(textDocumentName))
                {
                    fileNameInputFeedback.text = "There is already a file named " + customFileName + " @ " + directoryName + " and a new one has NOT been created";
                    Debug.Log("There is already a file named " + customFileName + " @ " + directoryName + " and a new one has NOT been created");
                }
                else
                {
                    // Create the file in the specified folder and provide feedback
                    File.Create(textDocumentName).Dispose();
                    fileNameInputFeedback.text = "Your file " + customFileName + " has been saved to your custom folder @ " + textDocumentName;
                    Debug.Log("Your file " + customFileName + " has been saved to your custom folder @ " + textDocumentName);
                }
            }
        }
    }

    // Deletes a text file at the specified location if it exists
    public void DeleteTextFile()
    {
        customFileName = fileNameInputField.text;
        string textDocumentName = directoryName + customFileName + ".txt";

        // Check for empty file name
        if (customFileName == null || customFileName == "")
        {
            fileNameInputFeedback.text = "Enter a file name";
        }
        else
        {
            // Check if the file exists and delete it
            if (File.Exists(textDocumentName))
            {
                File.Delete(textDocumentName);
                fileNameInputFeedback.text = "The file " + customFileName + " has been found and deleted";
            }
            else
            {
                // Provide feedback for non-existent file
                if (directoryName == null || directoryName == "")
                {
                    fileNameInputFeedback.text = "There is no file found called " + customFileName + " @ " + Application.persistentDataPath;
                }
                else
                {
                    fileNameInputFeedback.text = "There is no file found called " + customFileName + " @ " + directoryName;
                }
            }
        }
    }

    // Checks if file content has been added, checks for folder name applied, checks for file name.
    // Will only return an error for no file name or content, else will create or add to the file at location (persistent path if nothing applied)
    public void CreateOrAddToTextFile()
    {
        contentForTextFile = fileContentInputField.text;

        // Check for empty content
        if (contentForTextFile == null || contentForTextFile == "")
        {
            fileContentInputFeedback.text = "Enter some content to be saved";
        }
        else
        {
            // Check for default directory (persistent data path) if none specified
            if (directoryName == null || directoryName == "")
            {
                // Check for empty file name
                if (customFileName == null || customFileName == "")
                {
                    fileContentInputFeedback.text = "Enter a file name to be able to save this content";
                }
                else
                {
                    // Append content to the file and provide feedback
                    string textDocumentName = directoryName + customFileName + ".txt";
                    File.AppendAllText(textDocumentName, contentForTextFile + "\n \n");
                    fileContentInputFeedback.text = "Your content has been added to the file " + customFileName + " saved at the persistent data path @ " + Application.persistentDataPath;
                    Debug.Log("Your content has been added to the file " + customFileName + " saved at the persistent data path @ " + Application.persistentDataPath);
                }
            }
            else
            {
                // Check for empty file name
                if (customFileName == null || customFileName == "")
                {
                    fileContentInputFeedback.text = "Enter a file name to be able to save this content";
                }
                else
                {
                    // Append content to the file in the specified folder and provide feedback
                    string textDocumentName = directoryName + customFileName + ".txt";
                    File.AppendAllText(textDocumentName, contentForTextFile + "\n \n");
                    fileContentInputFeedback.text = "Your content has been added to the file " + customFileName + " @ " + directoryName;
                    Debug.Log("Your content has been added to the file " + customFileName + " @ " + directoryName);
                }
            }
        }
    }

    // Same as above, but will overwrite contents of an existing file
    public void OverwriteTextFile()
    {
        string textDocumentName = directoryName + customFileName + ".txt";

        contentForTextFile = fileContentInputField.text;

        // Check for empty content
        if (contentForTextFile == null || contentForTextFile == "")
        {
            fileContentInputFeedback.text = "Enter some content to be saved";
        }
        else
        {
            // Check for default directory (persistent data path) if none specified
            if (directoryName == null || directoryName == "")
            {
                // Check for empty file name
                if (customFileName == null || customFileName == "")
                {
                    fileContentInputFeedback.text = "Enter a file name to be able to save this content";
                }
                else
                {
                    // Overwrite existing file with new content and provide feedback
                    File.WriteAllText(textDocumentName, "");
                    File.AppendAllText(textDocumentName, contentForTextFile + "\n \n");
                    fileContentInputFeedback.text = "Your content has been added to the file " + customFileName + " saved at the persistent data path @ " + Application.persistentDataPath;
                    Debug.Log("Your content has been added to the file " + customFileName + " saved at the persistent data path @ " + Application.persistentDataPath);
                }
            }
            else
            {
                // Check for empty file name
                if (customFileName == null || customFileName == "")
                {
                    fileContentInputFeedback.text = "Enter a file name to be able to save this content";
                }
                else
                {
                    // Overwrite existing file in the specified folder with new content and provide feedback
                    File.WriteAllText(textDocumentName, "");
                    File.AppendAllText(textDocumentName, contentForTextFile + "\n \n");
                    fileContentInputFeedback.text = "Your content has been added to the file @ " + textDocumentName;
                    Debug.Log("Your content has been added to the file @ " + textDocumentName);
                }
            }
        }
    }

    public void ReadTextFile()
    {
        customFileName = fileNameInputField.text;

        // Set default directory to persistent data path if not specified
        if (directoryName == null || directoryName == "")
        {
            directoryName = Application.persistentDataPath + "/";
        }
        else
        {
            directoryName = Application.persistentDataPath + "/" + customFolderName + "/";
        }

        // Check for empty file name
        if (customFileName == null || customFileName == "")
        {
            fileNameInputFeedback.text = "Enter a file name";
        }
        else
        {
            string textDocumentName = directoryName + customFileName + ".txt";

            // Check if the directory exists
            if (!Directory.Exists(directoryName))
            {
                fileNameInputFeedback.text = "Directory does not exist";
            }
            // Check if the file exists
            else if (!File.Exists(textDocumentName))
            {
                fileNameInputFeedback.text = "File does not exist";
            }
            else
            {
                try
                {
                    // Read the content from the file
                    string fileContent = File.ReadAllText(textDocumentName);

                    // Check if the content is not empty
                    if (!string.IsNullOrEmpty(fileContent))
                    {
                        // Display the content
                        fileContentInputField.text = fileContent;
                        Debug.Log("File content:\n" + fileContent);
                    }
                    else
                    {
                        // Provide feedback for empty file
                        fileContentInputFeedback.text = "The file is empty";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, if any
                    fileContentInputFeedback.text = "Error reading the file: " + ex.Message;
                    Debug.LogError("Error reading the file: " + ex.Message);
                }
            }
        }
    }
}
