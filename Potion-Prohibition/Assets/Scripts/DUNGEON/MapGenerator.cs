using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.EditorTools;
using UnityEngine;
using static UnityEditor.Progress;

public class MapGenerator : MonoBehaviour
{
    private int[] floorPlan;

    public int[] getFloorPlan => floorPlan;

    public int num;
    private int numOfEndRooms = 2;

    private int floorPlanCount;
    private int minRooms;
    private int maxRooms;
    private List<int> endRooms;
    private List<int> Rooms;
    private List<int> nonEndRooms;

    private int enemy1RoomIndex;
    private int enemy2RoomIndex;
    private int enemy3RoomIndex;
    private int harvest1RoomIndex;
    private int harvest2RoomIndex;
    private int portalRoomIndex;

    public Cell cellPrefab;
    private float cellSize;
    private Queue<int> cellQueue;
    private List<Cell> spawnedCells;

    public List<GameObject> spawnedRooms;

    public List<Cell> getSpawnedCells => spawnedCells;

    [Header("Sprite References")]
    [SerializeField] private Sprite enemy;
    [SerializeField] private Sprite harvest;
    [SerializeField] private Sprite start;
    [SerializeField] private Sprite portal;

    public static MapGenerator instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        minRooms = 14;
        maxRooms = 30;
        cellSize = 2f;
        spawnedCells = new();
        spawnedRooms = new();

        SetupDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetupDungeon();
        }
    }

    void SetupDungeon()
    {
        for (int i = 0; i < spawnedCells.Count; i++)
        {
            Destroy(spawnedCells[i].gameObject);
        }

        spawnedCells.Clear();

        for (int i = 0; i < spawnedRooms.Count; i++)
        {
            Destroy(spawnedRooms[i].gameObject);
        }

        spawnedRooms.Clear();

        floorPlan = new int[num * num];
        floorPlanCount = default;
        cellQueue = new Queue<int>();
        endRooms = new List<int>();
        Rooms = new List<int>();

        VisitCell(67);

        GenerateDungeon();
    }
    void GenerateDungeon()
    {
        while (cellQueue.Count > 0)
        {
            int index = cellQueue.Dequeue();
            int x = index % num;

            bool created = false;

            if (x > 1) created |= VisitCell(index - 1);
            if (x < num - 1) created |= VisitCell(index + 1);
            if (index > num * 2) created |= VisitCell(index - num);
            if (index < num * 7) created |= VisitCell(index + num);

            if (created == false)
                endRooms.Add(index);
        }

        if (floorPlanCount < minRooms)
        {
            SetupDungeon();
            return;
        }

        SetupSpecialRooms();
    }

    void SetupSpecialRooms()
    {

        Rooms.RemoveAt(0);

        IEnumerable<int> difference = Rooms.Except(endRooms);
        nonEndRooms = difference.ToList();

        enemy1RoomIndex = RandomRoom();
        enemy2RoomIndex = RandomRoom();
        enemy3RoomIndex = RandomRoom();
        portalRoomIndex = PortalRoom();
        harvest1RoomIndex = RandomEndRoom();
        harvest2RoomIndex = RandomEndRoom();

        if (harvest1RoomIndex == -1 || harvest2RoomIndex == -1 || enemy1RoomIndex == -1 || enemy2RoomIndex == -1 || enemy3RoomIndex == -1 || portalRoomIndex == -1) 
        {
            SetupDungeon();
            return;
        }

        UpdateSpecialRoomVisuals();
    }

    void UpdateSpecialRoomVisuals()
    {
        foreach (var cell in spawnedCells)
        {
            if (cell.index == harvest1RoomIndex || cell.index == harvest2RoomIndex)
            {
                cell.SetSpecialRoomSprite(harvest);
                int index = spawnedCells.IndexOf(cell);
                Destroy(spawnedRooms[index].gameObject);
                cell.SetRoomType(RoomType.Harvest);
            }
            if (cell.index == enemy1RoomIndex || cell.index == enemy2RoomIndex || cell.index == enemy3RoomIndex)
            {
                cell.SetSpecialRoomSprite(enemy);
                int index = spawnedCells.IndexOf(cell);
                Destroy(spawnedRooms[index].gameObject);
                cell.SetRoomType(RoomType.Enemy);
            }
            if (cell.index == 67)
            {
                cell.SetSpecialRoomSprite(start);
                int index = spawnedCells.IndexOf(cell);
                Destroy(spawnedRooms[index].gameObject);
                cell.SetRoomType(RoomType.Start);
            }
            if (cell.index == portalRoomIndex)
            {
                cell.SetSpecialRoomSprite(portal);
                int index = spawnedCells.IndexOf(cell);
                Destroy(spawnedRooms[index].gameObject);
                cell.SetRoomType(RoomType.Portal);
            }

            CheckNeighbour(cell);
            cell.door();
        }
    }

    int RandomEndRoom()
    {
        if (endRooms.Count == 0) return -1;

        int randomRoom = Random.Range(0, endRooms.Count);
        int index = endRooms[randomRoom];

        endRooms.RemoveAt(randomRoom);

        return index;
    }

    int RandomRoom()
    {
        if (nonEndRooms.Count == 0) return -1;

        int randomRoom = Random.Range(0, nonEndRooms.Count);
        int index = nonEndRooms[randomRoom];

        nonEndRooms.RemoveAt(randomRoom);

        return index;
    }

    int PortalRoom()
    {
        if (nonEndRooms.Count == 0) return -1;

        int randomRoom = Random.Range(0, nonEndRooms.Count);
        int index = nonEndRooms[randomRoom];

        if (index == 37 || index == 51 || index == 52 || index == 53 || index == 65 || index == 66 || index == 68 || index == 69 || index == 81 || index == 82 || index == 83 || index == 97) return -1;

        nonEndRooms.RemoveAt(randomRoom);


        return index;
    }


    private int GetNeighbourCount(int index)
    {
        return floorPlan[index - num] + floorPlan[index - 1] + floorPlan[index + 1] + floorPlan[index + num];
    }

    private bool VisitCell(int index)
    {
        if (floorPlan[index] != 0 || GetNeighbourCount(index) > 1 || floorPlanCount > maxRooms || Random.value < 0.5f)
            return false;
        cellQueue.Enqueue(index);
        floorPlan[index] = 1;
        floorPlanCount++;

        SpawnRoom(index);

        Rooms.Add(index);

        return true;
    }

    private void SpawnRoom(int index)
    {
        int x = index % num;
        int y = index / num;
        Vector3 position = new Vector3(x * cellSize,0, -y * cellSize);

        Cell newCell = Instantiate(cellPrefab, position, Quaternion.identity);
        newCell.value = 1;
        newCell.index = index;
        newCell.SetRoomType(RoomType.Regular);
        newCell.cellList.Add(index);
        spawnedCells.Add(newCell);
    }

    private void CheckNeighbour(Cell cell)
    {
        if (floorPlan[cell.index - num] == 1) cell.north = true;
        if (floorPlan[cell.index + num] == 1) cell.south = true;
        if (floorPlan[cell.index - 1] == 1) cell.east = true;
        if (floorPlan[cell.index + 1] == 1) cell.west = true;
    }

}
