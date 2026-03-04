using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Regular,
    Enemy1,
    Enemy2,
    Enemy3,
    Harvest1,
    Harvest2,
    Harvest3,
    Harvest4,
    Portal,
    Start
}

public class Cell : MonoBehaviour
{
    [HideInInspector] public int index;
    [HideInInspector] public int value;

    [HideInInspector] public bool north = false;
    [HideInInspector] public bool south = false;
    [HideInInspector] public bool east = false;
    [HideInInspector] public bool west = false;

    private GameObject room;

    public SpriteRenderer spriteRenderer;

    [Space]

    [Header("Walls To Spawn")]
    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;

    [HideInInspector] public RoomType roomType;

    [Space]

    [Header("Rooms To Spawn")]
    [Space]
    [Header("Regular Rooms")]
    public GameObject[] regularRooms;
    [Space]
    [Header("Enemy Rooms")]
    public GameObject[] newtRooms;
    public GameObject[] slimeRooms;
    public GameObject[] eyeRooms;
    [Space]
    [Header("Harvest Rooms")]
    public GameObject[] bonesRooms;
    public GameObject[] cabbageRooms;
    public GameObject[] oysterRooms;
    public GameObject[] mushroomRooms;
    [Space]
    [Header("Portal Rooms")]
    public GameObject[] portalRooms;
    [Space]
    [Header("Start Rooms")]
    public GameObject[] startRooms;

    [HideInInspector] public List<int> cellList = new List<int>();

    public void SetSpecialRoomSprite(Sprite icon)
    {
        spriteRenderer.sprite = icon;
    }

    public void SetRoomType(RoomType newRoomType)
    {
        roomType = newRoomType;
        switch (roomType)
        {
            case RoomType.Start:
                room = startRooms[Random.Range(0, startRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Enemy1:
                room = newtRooms[Random.Range(0, newtRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Enemy2:
                room = slimeRooms[Random.Range(0, slimeRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Enemy3:
                room = eyeRooms[Random.Range(0, eyeRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Harvest1:
                room = bonesRooms[Random.Range(0, bonesRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Harvest2:
                room = slimeRooms[Random.Range(0, bonesRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Harvest3:
                room = oysterRooms[Random.Range(0, bonesRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Portal:
                room = portalRooms[Random.Range(0, portalRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Regular:
                room = regularRooms[Random.Range(0, regularRooms.Length)];
                SpawnRoom(room);
                break;
        }

    }

    public void SetRoomType(RoomType newRoomType, int index)
    {
        roomType = newRoomType;
        switch (roomType)
        {
            case RoomType.Start:
                room = startRooms[Random.Range(0, startRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Enemy1:
                room = newtRooms[Random.Range(0, newtRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Enemy2:
                room = slimeRooms[Random.Range(0, slimeRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Enemy3:
                room = eyeRooms[Random.Range(0, eyeRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Harvest1:
                room = bonesRooms[Random.Range(0, bonesRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Harvest2:
                room = slimeRooms[Random.Range(0, bonesRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Harvest3:
                room = oysterRooms[Random.Range(0, bonesRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Portal:
                room = portalRooms[Random.Range(0, portalRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Regular:
                room = regularRooms[Random.Range(0, regularRooms.Length)];
                SpawnRoom(room, index);
                break;
        }

    }

    private void SpawnRoom(GameObject room)
    {
        GameObject Spawn = Instantiate(room, transform.position, Quaternion.identity);
        MapGenerator.instance.spawnedRooms.Add(Spawn);
        MapGenerator.instance.spawnedStates.Add(room.name);
    }

    private void SpawnRoom(GameObject room, int index)
    {
        GameObject Spawn = Instantiate(room, transform.position, Quaternion.identity);
        MapGenerator.instance.spawnedRooms.Insert(index, Spawn);
        MapGenerator.instance.spawnedStates.Insert(index, room.name);
    }

    public void Door()
    {
        if (north)
        {
            northDoor.SetActive(false);
        }
        else
        {
            northDoor.SetActive(true);
        }

        if (south)
        {
            southDoor.SetActive(false);
        }
        else
        {
            southDoor.SetActive(true);
        }

        if (east)
        {
            eastDoor.SetActive(false);
        }
        else
        {
            eastDoor.SetActive(true);
        }

        if (west)
        {
            westDoor.SetActive(false);
        }
        else
        {
            westDoor.SetActive(true);
        }
    }
}
