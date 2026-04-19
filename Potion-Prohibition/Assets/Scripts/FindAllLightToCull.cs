using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindAllLightToCull : MonoBehaviour
{
    private GameObject[] lightsToCull;
    public float distance = 1000f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("invoketime", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        

        if(lightsToCull != null)
        {
            for (int i = 0; i < lightsToCull.Length; i++)
            {
                if (Vector3.Distance(GameManager.Instance.PlayerGO.transform.position, lightsToCull[i].transform.position) > distance) lightsToCull[i].SetActive(false);
                else lightsToCull[i].SetActive(true);
            }
        }
    }

    private void invoketime()
    {
        lightsToCull = FindLights();
    }

    // Function to get a list og all gameobjects with a light
    private GameObject[] FindLights()
    {
        //Get all objects in scene
        GameObject[] objs = SceneManager.GetActiveScene().GetRootGameObjects();

        // initialize list of light gameObjects
        List<GameObject> lights = new List<GameObject>();

        //loop through the gameobjects to finds lights to add to the light list
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].layer == 3)
            {
                for(int j = 0; j < objs[i].transform.childCount; j++)
                {
                    if (objs[i].transform.GetChild(j).gameObject.layer == 13) lights.Add(objs[i].transform.GetChild(j).gameObject);
                }
            }
        }
        
        //return the list, return null if list is empty
        if (lights.Count == 0) return null;
        return lights.ToArray();
    }
}
