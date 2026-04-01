using System.IO;
using UnityEngine;

public class ResetJson : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public int health = 5;
        public float damage = 1;
        public int levels = 1;
        public int money = 0;
    }

    public PlayerData playerData = new PlayerData();

    public void WriteJSON()
    {
        playerData.health = 5;
        playerData.damage = 1;
        playerData.levels = 1;
        playerData.money = 0;

        string stringOutput = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", stringOutput);
    }

    private void Start()
    {
        WriteJSON();
    }
}
