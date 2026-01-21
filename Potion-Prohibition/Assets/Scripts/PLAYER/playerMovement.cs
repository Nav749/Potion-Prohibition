using UnityEngine;

public class playerMovement : MonoBehaviour {

    public CharacterController playerController;

    public float playerSpeed = 12f;

    public float playerGravRate = -9.8f;

    public float playerJumpHeight = 3f;

    Vector3 playerVelocity;

    public Transform playerGroundCheck;

    public float playerGroundDistance = 0.4f;

    public LayerMask playerGroundMask;

    bool playerIsGrounded;
 
    void Update()
    {
        playerIsGrounded = Physics.CheckSphere(playerGroundCheck.position, playerGroundDistance, playerGroundMask);

        if(playerIsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f;
        }

        float playerX = Input.GetAxis("Horizontal");
        float playerZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * playerX + transform.forward * playerZ;

        playerController.Move(move * playerSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && playerIsGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(playerJumpHeight * -2f * playerGravRate);
        }

        if(playerVelocity.y > -150)
        {
            playerVelocity.y += playerGravRate * Time.deltaTime;
            playerController.Move(playerVelocity * Time.deltaTime);
        }
        
    }

    
}
