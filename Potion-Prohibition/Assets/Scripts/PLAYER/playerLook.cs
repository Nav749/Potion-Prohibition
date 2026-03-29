using UnityEngine;

public class playerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform playerBody;
    public playerHealth referenceForHealth;

    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

       
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
       
        if(referenceForHealth.currentHealth <= 0)
        {
            mouseSensitivity = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }



}
