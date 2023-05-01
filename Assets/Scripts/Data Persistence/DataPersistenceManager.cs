using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataPersistenceManager : MonoBehaviour
{
    // USES "dataPath" INSTEAD OF "persistentDataPath" TO ALLOW FOR DEFAULT SAVES TO SHOWCASE

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public UnityEngine.UI.Button continueButton;

    private string recentFile;
    private string savePattern = "*.save";

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        // Check for multiple Data Persistence Managers
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SavesCheck();
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        instance = this;
    } 

    public void RecentFile()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/Saves");
        recentFile = (from f in dirInfo.GetFiles(savePattern) orderby f.LastWriteTime descending select f).First().ToString();
    }

    public void ContinueGame()
    {
        RecentFile();
        LoadGame(recentFile);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame(string _fileName)
    {
        this.dataHandler = new FileDataHandler(Application.dataPath + "/Saves", _fileName);
        this.gameData = dataHandler.Load();

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame(string _fileName)
    {
        this.dataHandler = new FileDataHandler(Application.dataPath + "/Saves", _fileName);
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void SavesCheck()
    {
        if (Directory.EnumerateFiles(Application.dataPath + "/Saves", savePattern).Any())
        {
            continueButton.interactable = true;
        }
        else { continueButton.interactable = false; }
    }
}
