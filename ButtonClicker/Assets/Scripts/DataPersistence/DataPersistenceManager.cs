using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] [SerializeField]
    private string fileName;

    [SerializeField] private bool useEncryption;
    public static DataPersistenceManager Instance { get; private set; }

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        var dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void Default()
    {
        _gameData = new GameData();
    }
    
    public void SaveGame()
    {
        foreach (var data in _dataPersistenceObjects)
        {
            data.SaveData(_gameData);
        }
        Debug.Log("Game Saved");
        
        _dataHandler.Save(_gameData);
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();
        
        if (_gameData == null)
        {
            Debug.Log("No Data Found. Setting Default Values.");
            Default();
        }

        foreach (IDataPersistence data in _dataPersistenceObjects)
        {
            data.LoadData(_gameData);
        }
        
        Debug.Log("Game Loaded");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
