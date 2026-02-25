using System.Collections;
using System.Collections.Generic;
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

        clearInventory();
    }

    #endregion

    [SerializeField] GameObject playerGO;
    public GameObject PlayerGO => playerGO;
    [SerializeField] GameObject UI;



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
        if (currentLevelName == "Kitchen") UI.SetActive(false);
        else UI.SetActive(true);

        if (currentOrder == null)
        {
            PickRandomPotion();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PickRandomPotion();
        }

        //UpdateDevView();

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

    public void LoadLevels()
    {
        currentLevelIndex++;
        StartCoroutine(LoadLevel(levelNames[currentLevelIndex % levelNames.Length]));
    }

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

        if (currentLevelName == "Kitchen")
        {
            playerGO.transform.position = Vector3.zero;
            playerGO.GetComponent<playerSpellShoot>().tavernNeutral = true;
            playerGO.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        if (currentLevelName == "Dungeon")
        {
            playerGO.transform.position = new Vector3(480, 5, -235);
            playerGO.GetComponent<playerSpellShoot>().tavernNeutral = false;
            playerGO.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

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
            var room = Resources.Load("Models/FINAL/FinalRooms" + savedRooms[i]);
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

    #region Orders

    [SerializeField] Potion[] Orders;
    public Potion currentOrder;
    public Potion OrdertoCheck;
    public bool correctOrder;
    public bool checkDone = false;
    public bool orderTime = false;

    public void PickRandomPotion()
    {
        currentOrder = Orders[Random.Range(0, Orders.Length)];
        checkDone = false;
    }

    public void TimeToCheckOrder()
    {
        correctOrder = currentOrder == OrdertoCheck;
        checkDone = true;
    }

    #endregion

    #region Inventory

    public Item[] inventory;
    public void clearInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].resetAmount();
        }
    }

    #endregion

    #region PotionInventory

    public List<Potion> potions;

    public void clearPotionInventory()
    {
        potions.Clear();
    }

    #endregion

    #region Customers

    public int customerNum;
    public bool customerGenerated = false;
    public int currentlines = 0;

    #endregion

}
