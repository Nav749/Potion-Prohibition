using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Regular,
    Enemy,
    Harvest,
    Portal,
    Start
}

public class Cell : MonoBehaviour
{
    public int index;
    public int value;

    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;

    public bool north = false;
    public bool south = false;
    public bool east = false;
    public bool west = false;

    private GameObject room;

    public SpriteRenderer spriteRenderer;

    public RoomType roomType;


    public GameObject[] regRooms;
    public GameObject[] eneRooms;
    public GameObject[] harRooms;
    public GameObject[] porRooms;
    public GameObject[] staRooms;

    public List<int> cellList = new List<int>();

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
                room = staRooms[Random.Range(0, staRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Enemy:
                room = eneRooms[Random.Range(0, eneRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Harvest:
                room = harRooms[Random.Range(0, harRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Portal:
                room = porRooms[Random.Range(0, porRooms.Length)];
                SpawnRoom(room);
                break;
            case RoomType.Regular:
                room = regRooms[Random.Range(0, regRooms.Length)];
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
                room = staRooms[Random.Range(0, staRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Enemy:
                room = eneRooms[Random.Range(0, eneRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Harvest:
                room = harRooms[Random.Range(0, harRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Portal:
                room = porRooms[Random.Range(0, porRooms.Length)];
                SpawnRoom(room, index);
                break;
            case RoomType.Regular:
                room = regRooms[Random.Range(0, regRooms.Length)];
                SpawnRoom(room, index);
                break;
        }

    }

    private void SpawnRoom(GameObject room)
    {
        GameObject Spawn = Instantiate(room, transform.position, Quaternion.identity);
        MapGenerator.instance.spawnedRooms.Add(Spawn);
    }

    private void SpawnRoom(GameObject room, int index)
    {
        GameObject Spawn = Instantiate(room, transform.position, Quaternion.identity);
        MapGenerator.instance.spawnedRooms.Insert(index, Spawn);
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
