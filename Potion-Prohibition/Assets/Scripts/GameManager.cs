using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
                instance = FindAnyObjectByType<GameManager>();

            if (!instance)
                throw new System.Exception("Suspicious Lack of Game Manager...");

            return instance;
        }
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] GameObject playerGO;
    public GameObject PlayerGO => playerGO;

    private void Start()
    {
        levelsPassed = 0;
        StartCoroutine(LoadLevel(levelNames[0]));
        savedRooms = new();
        savedRoomPositions = new();
        savedDoors = new();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentLevelIndex++;
            StartCoroutine(LoadLevel(levelNames[currentLevelIndex % levelNames.Length]));
        }

        UpdateDevView();

        if (Input.GetKeyDown(KeyCode.P) && currentLevelName == "Kitchen")
        {
            levelsPassed++;
            hasGenerated = false;
        }
    }

    #region SceneManagment

    [SerializeField] private string[] levelNames;

    bool isLoading = false;
    string currentLevelName;
    int currentLevelIndex = 0;
    bool hasGenerated = false;
    public bool HasGenerated => hasGenerated;

    IEnumerator LoadLevel(string levelName)
    {
        isLoading = true;
        playerGO.SetActive(false);

        if (!string.IsNullOrEmpty(currentLevelName))
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentLevelName);
            while (!asyncUnload.isDone)
                yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
        currentLevelName = levelName;

        if (currentLevelName == "Dungeon" && !hasGenerated)
            hasGenerated = true;

        playerGO.SetActive(true);
        isLoading = false;
    }

    #endregion

    #region DungeonInfo
    [SerializeField] GameObject cellPrefab;

    [SerializeField] List<string> savedRooms;
    [SerializeField] List<Vector3> savedRoomPositions;
    [SerializeField] List<bool[]> savedDoors;
    public List<string> SavedRooms => savedRooms;
    public List<Vector3> SavedRoomPositions => savedRoomPositions;
    public List<bool[]> SavedDoors => savedDoors;

    public void SaveRooms(List<string> roomsToSave, List<Vector3> positionsToSave, List<bool[]> doorsToSave)
    {
        savedRooms = roomsToSave;
        savedRoomPositions = positionsToSave;
        savedDoors = doorsToSave;
        hasGenerated = true;
    }

    public void SpawnRooms()
    {
        for (int i = 0; i < savedRooms.Count; i++)
        {
            var room = Resources.Load("Models/TEST/TEST ROOMS/" + savedRooms[i]);
            Instantiate(room, savedRoomPositions[i], Quaternion.identity);
            GameObject cell = Instantiate(cellPrefab, savedRoomPositions[i], Quaternion.identity);
            bool[] doorsToSpawn = savedDoors[i];
            for (int j = 0; j < doorsToSpawn.Length; j++)
            {
                GameObject door = cell.transform.GetChild(j).gameObject;
                door.SetActive(!doorsToSpawn[j]);
            }
        }
    }

    #endregion

    #region DevViewport

    private int levelsPassed;
    public int LevelsPassed => levelsPassed;
    [SerializeField] Text levelPassedValue;

    void UpdateDevView()
    {
        levelPassedValue.text = levelsPassed.ToString();
    }

    #endregion

}
