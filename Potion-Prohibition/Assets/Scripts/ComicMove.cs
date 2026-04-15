using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ComicMove : MonoBehaviour
{
    [SerializeField] List<Vector3> cameraPositions;
    [SerializeField] Camera cam;
    private int index;
    public float wait;
    public float speed;
    public GameObject Buttons;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(CameraMove());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && index < cameraPositions.Count && cam.transform.position != cameraPositions[cameraPositions.Count - 1])
        {
            if(cam.transform.position != cameraPositions[index])
            {
                StopAllCoroutines();
                cam.transform.position = cameraPositions[index];
            }
            else
            {
                index++;
                StartCoroutine(CameraMove());
            }
        }

        if((index > cameraPositions.Count-1 || cam.transform.position == cameraPositions[cameraPositions.Count - 1]))
        {
            StopAllCoroutines();
            cam.transform.position = cameraPositions[cameraPositions.Count - 1];
            Buttons.SetActive(true);
        }
    }

    IEnumerator CameraMove()
    {
        while(cam.transform.position != cameraPositions[index])
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, cameraPositions[index], speed);
            yield return new WaitForSeconds(wait);
        }
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Persistent");
    }
}
