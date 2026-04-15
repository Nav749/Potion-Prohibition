using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicMove : MonoBehaviour
{
    [SerializeField] List<Transform> cameraPositions;
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
        if (Input.GetMouseButtonDown(0) && index < cameraPositions.Count && cam.transform.position != cameraPositions[cameraPositions.Count - 1].position)
        {
            if (cam.transform.position != cameraPositions[index].position)
            {
                StopAllCoroutines();
                cam.transform.position = cameraPositions[index].position;
            }
            else
            {
                index++;
                StartCoroutine(CameraMove());
            }
        }

        if ((index > cameraPositions.Count - 1 || cam.transform.position == cameraPositions[cameraPositions.Count - 1].position))
        {
            StopAllCoroutines();
            cam.transform.position = cameraPositions[cameraPositions.Count - 1].position;
            Buttons.SetActive(true);
        }
    }

    IEnumerator CameraMove()
    {
        while (cam.transform.position != cameraPositions[index].position)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, cameraPositions[index].position, speed);
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
