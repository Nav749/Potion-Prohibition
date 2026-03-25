using System.Collections.Generic;
using UnityEngine;

public class SpawnProps : MonoBehaviour
{

    [SerializeField] List<GameObject> spawnableProps;

    void Start()
    {
        int randint = Random.Range(0, spawnableProps.Count);
        Instantiate(spawnableProps[randint], this.transform);
    }

}
