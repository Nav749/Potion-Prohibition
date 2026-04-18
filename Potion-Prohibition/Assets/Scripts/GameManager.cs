using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

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

        restart = playerStats.GetBool();
        if (!restart)
        {
            PlayerGO.GetComponent<playerHealth>().maxHealth = playerStats.GetHealth();
            PlayerGO.GetComponent<playerHealth>().currentHealth = PlayerGO.GetComponent<playerHealth>().maxHealth;
            PlayerGO.GetComponent<playerSpellShoot>().spellBulletDamage = playerStats.GetDamage();
        }
        else
        {
            playerStats.SetHealth(5);
            playerStats.SetDamage(1);
            PlayerGO.GetComponent<playerHealth>().maxHealth = playerStats.GetHealth();
            PlayerGO.GetComponent<playerHealth>().currentHealth = PlayerGO.GetComponent<playerHealth>().maxHealth;
            PlayerGO.GetComponent<playerSpellShoot>().spellBulletDamage = playerStats.GetDamage();
            playerStats.SetBool(true);
        }
    }

    #endregion

    [SerializeField] GameObject playerGO;
    public GameObject filter;
    [SerializeField] int increment;
    public bool speakable = true;
    public GameObject PlayerGO => playerGO;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject nextDayScreen;
    public GameObject alwayson;
    public int orderQuota;
    public int currentOrderQuota;
    [SerializeField] Timesmet[] stats;
    private bool restart;
    [SerializeField] private Color color1;
    [SerializeField] private float fogDensity;
    [SerializeField] private GameObject aListen;
    [SerializeField] public int healthups;
    [SerializeField] public int damageups;

    public int slimeScale;
    public int newtScale;
    public int eyeScale;

    public bool inMenu = false;
    private bool checkerbool;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            ReadJSON();
        }
        else
        {
            playerGO.gameObject.GetComponent<playerHealth>().maxHealth = 5;
            playerGO.gameObject.GetComponent<playerSpellShoot>().spellBulletDamage = 1;
            levelsPassed = 1;
            coins = 0;
        }

        if (File.Exists(Application.persistentDataPath + "/MouseData.json"))
        {
            MouseReadJson();
        }
        else
        {
            playerGO.transform.GetChild(0).gameObject.GetComponent<playerLook>().mouseSensitivity = 4.5f;
        }

            ResetMonies();
        orderQuota = (int)(3 * Mathf.Sqrt(levelsPassed));
        currentOrderQuota = 0;
        nextDayScreen.SetActive(false);
        StartCoroutine(LoadLevel(levelNames[0]));
        savedRooms = new();
        savedRoomPositions = new();
        savedDoors = new();
        if (levelsPassed == 1)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].SetInt(0);
            }
        }
        WriteJSON();
        WriteMouseJson();
    }

    private void Update()
    {
        if (currentLevelName == "Kitchen") UI.SetActive(false);
        else UI.SetActive(true);

        if (currentOrder == null)
        {
            PickRandomPotion();
        }

        if (orderQuota == currentOrderQuota)
        {
            if (checkerbool)
            {
                checkerbool = false;

                for (int i = 0; i < 8; i++)
                    nextDayScreen.transform.GetChild(i + 2).gameObject.SetActive(false);
                switch (orderQuota)
                {
                    case 3:
                        {
                            nextDayScreen.transform.GetChild(2).gameObject.SetActive(true);
                            break;
                        }
                    case 4:
                        {
                            nextDayScreen.transform.GetChild(3).gameObject.SetActive(true);
                            break;
                        }
                    case 5:
                        {
                            nextDayScreen.transform.GetChild(4).gameObject.SetActive(true);
                            break;
                        }
                    case 6:
                        {
                            nextDayScreen.transform.GetChild(5).gameObject.SetActive(true);
                            break;
                        }
                    case 7:
                        {
                            nextDayScreen.transform.GetChild(6).gameObject.SetActive(true);
                            break;
                        }
                    case 8:
                        {
                            nextDayScreen.transform.GetChild(7).gameObject.SetActive(true);
                            break;
                        }
                    case 9:
                        {
                            nextDayScreen.transform.GetChild(8).gameObject.SetActive(true);
                            break;
                        }
                    default:
                        {
                            nextDayScreen.transform.GetChild(9).gameObject.SetActive(true);
                            break;
                        }
                }
            }
            nextDayScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        text.text = coins.ToString();
        UpdateDevView();

        if (!isLoading) aListen.SetActive(false);
        else aListen.SetActive(true);
    }

    #region SceneManagment

    [SerializeField] private string[] levelNames;
    public bool isLoading = false;
    [HideInInspector] public string currentLevelName;
    int currentLevelIndex = 0;
    bool hasGenerated = false;
    public bool HasGenerated => hasGenerated;

    public void LoadLevels()
    {
        currentLevelIndex = (currentLevelIndex + 1) % levelNames.Length;
        StartCoroutine(LoadLevel(levelNames[currentLevelIndex]));
    }

    IEnumerator LoadLevel(string levelName)
    {
        RenderSettings.fog = false;
        isLoading = true;
        playerGO.SetActive(false);
        PlayerGO.GetComponent<playerMovement>().setMoveLock(true);
        LoadingScreen.SetActive(true);

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
            RenderSettings.fog = false;
            playerGO.transform.position = new Vector3(-26.27864f, -8.846062f, 11.87466f);
            playerGO.GetComponent<playerSpellShoot>().tavernNeutral = true;
            playerGO.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        if (currentLevelName == "Dungeon")
        {
            playerGO.GetComponent<playerHealth>().initializeHealthBar();
            RenderSettings.fog = true;
            RenderSettings.fogColor = color1;
            RenderSettings.fogDensity = fogDensity;
            playerGO.transform.position = new Vector3(446.5671f, -10f, -257.3981f);
            playerGO.GetComponent<playerSpellShoot>().tavernNeutral = false;
            playerGO.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }


        Invoke("TurnOffLoadingScreen", 2.5f);
    }

    void TurnOffLoadingScreen()
    {
        LoadingScreen.SetActive(false);
        PlayerGO.GetComponent<playerMovement>().setMoveLock(false);
        playerGO.SetActive(true);
        isLoading = false;
    }

    public void NextDay()
    {
        isLoading = true;
        PlayerGO.GetComponent<playerMovement>().setMoveLock(true);
        LoadingScreen.SetActive(true);

        levelsPassed++;
        playerGO.transform.position = new Vector3(-26.27864f, -8.846062f, 11.87466f);
        PlayerGO.GetComponent<playerHealth>().currentHealth = PlayerGO.GetComponent<playerHealth>().maxHealth;
        PlayerGO.GetComponent<playerHealth>().initializeHealthBar();
        clearPotionInventory();
        clearInventory();
        increment = (int)(increment * 1.25f);
        orderQuota = (int)(3 * Mathf.Sqrt(levelsPassed));
        currentOrderQuota = 0;
        Cursor.lockState = CursorLockMode.Locked;
        nextDayScreen.SetActive(false);
        hasGenerated = false;
        playerGO.SetActive(false);
        speakable = false;
        WriteJSON();
        checkerbool = true;
        Invoke("TurnOffLoadingScreen", 2f);
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
            var room = Resources.Load("Models/FINAL/FinalRooms/" + savedRooms[i]);
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
    [SerializeField] TextMeshProUGUI levelPassedValue;

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
        int rPrice = OrdertoCheck.isOnTheRocks() ? (int)(increment * 0.1) : 0;
        int sPrice = OrdertoCheck.isSpiced() ? (int)(increment * 0.1) : 0;
        if (correctOrder)
        {
            coins += increment + rPrice + sPrice;
        }
        checkDone = true;
        RemovePotion(OrdertoCheck);
    }

    public void UpdateQuota()
    {
        if (correctOrder && checkDone)
        {
            currentOrderQuota++;
        }
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

    public void RemovePotion(Potion potion)
    {
        potions.Remove(potion);
    }

    #endregion

    #region Customers

    public int customerNum;
    public bool customerGenerated = false;
    public int currentlines = 0;

    #endregion

    #region Nuke

    public void DeleteGameManager()
    {
        if (instance != null)
        {
            Destroy(instance);
            instance = null;
        }
    }

    #endregion

    #region StatUpdate

    public void UpdateHealth()
    {
        playerStats.IncrementHealth();
        PlayerGO.GetComponent<playerHealth>().maxHealth = playerStats.GetHealth();
        PlayerGO.GetComponent<playerHealth>().currentHealth += 1;
        PlayerGO.GetComponent<playerHealth>().initializeHealthBar();
        healthups++;
    }

    public void UpdateDamage()
    {
        playerStats.IncrementDamage();
        PlayerGO.GetComponent<playerSpellShoot>().spellBulletDamage = playerStats.GetDamage();
        damageups++;
    }

    #endregion

    #region Monies

    public int coins = 0;
    public int healthPrice = 20;
    public int damagePrice = 20;
    [SerializeField] TextMeshProUGUI text;

    private void ResetMonies()
    {
        healthPrice = 20;
        damagePrice = 20;
    }

    #endregion

    #region camLock

    public void lockCamara(bool input)
    {
        playerGO.GetComponentInChildren<playerLook>().locked = input;

    }

    #endregion

    #region SaveData

    [System.Serializable]
    public class PlayerData
    {
        public int health = 5;
        public float damage = 1;
        public int levels = 1;
        public int money = 0;
    }

    public PlayerData playerData = new PlayerData();

    [System.Serializable]
    public class MouseData
    {
        public float sens = 4.50f;
    }

    public MouseData playerData2 = new MouseData();

    public void WriteJSON()
    {
        playerData.health = playerGO.gameObject.GetComponent<playerHealth>().maxHealth;
        playerData.damage = playerGO.gameObject.GetComponent<playerSpellShoot>().spellBulletDamage;
        playerData.levels = levelsPassed;
        playerData.money = coins;

        string stringOutput = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", stringOutput);
    }

    public void ReadJSON()
    {
        string filepath = Application.persistentDataPath + "/PlayerData.json";
        string playerDataRead = System.IO.File.ReadAllText(filepath);

        playerData = JsonUtility.FromJson<PlayerData>(playerDataRead);

        playerGO.gameObject.GetComponent<playerHealth>().maxHealth = playerData.health;
        playerGO.gameObject.GetComponent<playerSpellShoot>().spellBulletDamage = playerData.damage;
        levelsPassed = playerData.levels;
        coins = playerData.money;
        healthups = playerData.health - 5;
        damageups = (int)(playerData.damage - 1);
    }

    public void WriteMouseJson()
    {
        playerData2.sens = playerGO.transform.GetChild(0).gameObject.GetComponent<playerLook>().mouseSensitivity;

        string stringOutput = JsonUtility.ToJson(playerData2);
        File.WriteAllText(Application.persistentDataPath + "/MouseData.json", stringOutput);
    }

    public void MouseReadJson()
    {
        string filepath = Application.persistentDataPath + "/MouseData.json";
        string playerDataRead = System.IO.File.ReadAllText(filepath);

        playerData2 = JsonUtility.FromJson<MouseData>(playerDataRead);

        playerGO.transform.GetChild(0).gameObject.GetComponent<playerLook>().mouseSensitivity = playerData2.sens;
    }

    #endregion
}
