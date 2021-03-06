using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] [SerializeField]
    private string fileName;

    [SerializeField] private bool useEncryption;

    [Header("Auto Saving")] [SerializeField]
    private float _autoSaveTimeInSeconds = 30f;
    
    public static DataPersistenceManager Instance { get; private set; }

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    private Coroutine _autoSaveCoroutine;
    
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
            FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
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

        foreach (var data in _dataPersistenceObjects)
        {
            data.LoadData(_gameData);
        }
        
        Debug.Log("Game Loaded");
        
        if (_autoSaveCoroutine != null)
        {
            StopCoroutine(_autoSaveCoroutine);
        }

        _autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(_autoSaveTimeInSeconds);
            SaveGame();
            Debug.Log("Auto Saved");
        }
    }
}
