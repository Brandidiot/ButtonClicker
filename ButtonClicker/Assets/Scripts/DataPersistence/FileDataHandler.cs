using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string _path = "";
    private string _fileName = "";
    private bool _useEncryption = false;
    private readonly string _encryptionCodeWord = "admin";

    public FileDataHandler(string path, string fileName, bool useEncryption)
    {
        _path = path;
        _fileName = fileName;
        _useEncryption = useEncryption;
    }

    public GameData Load()
    {
        var fullPath = Path.Combine(_path, _fileName);
        GameData loadedData = null;

        if (!File.Exists(fullPath)) return loadedData;
        
        try
        {
            //Read Serialized Data
            var dataToLoad = "";
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }
                
            if (_useEncryption)
            {
                dataToLoad = EncryptDecrypt(dataToLoad);
            }
            
            //Deserialize
            loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
        }
        catch (Exception e)
        {
            Debug.LogError("Error When Trying To Load From: " + fullPath + "\n" + e);
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        var fullPath = Path.Combine(_path, _fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            var dataToStore = JsonUtility.ToJson(data, true);
            
            //Encrypting?
            if (_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error When Trying To Save To: " + fullPath + "\n" + e);
            throw;
        }
    }

    //XOR Encryption
    private string EncryptDecrypt(string data)
    {
        var modifiedData = "";

        for (var i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);
        }

        return modifiedData;
    }
}
