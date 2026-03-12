
using System.IO;
using Data;
using Newtonsoft.Json;
using UnityEngine;

public class StorageHandler
{
    private static StorageHandler _instance = null;
    private string _fileName = "GameData.json";
       
    private StorageHandler()
    {
    }
    
    public static StorageHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StorageHandler();
            }
            return _instance;
        }
    }
    
    public GameData GetGameData()
    {
        string json = ReadFromFile();
        return JsonConvert.DeserializeObject<GameData>(json);
    }
    
    public void WriteToFile(string gameData)
    {
        try
        {
            string filePath = Application.persistentDataPath + "/" + _fileName;
            StreamWriter writer = new StreamWriter(filePath, false);
            writer.WriteLine(gameData);
            writer.Close();
        }catch (IOException e)
        {
            Debug.LogError("IOException : " + e.StackTrace);
        }
    }
    
    public string ReadFromFile()
    {
        string filePath = Application.persistentDataPath + "/" + _fileName;
        string fileContent = "";
        if (File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath);
            fileContent = reader.ReadToEnd();
            reader.Close();
        }
        return fileContent;
    }
}
