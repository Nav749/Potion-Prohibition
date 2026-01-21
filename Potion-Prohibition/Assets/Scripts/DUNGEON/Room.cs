using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;

    public void changeDoors(bool north, bool south, bool west, bool east)
    {
        northDoor.SetActive(north); southDoor.SetActive(south); eastDoor.SetActive(east);  westDoor.SetActive(west);
    }
}
