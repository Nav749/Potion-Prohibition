using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController playerController;

    public float playerSpeed = 12f;

    public float playerGravRate = -9.8f;

    public float playerJumpHeight = 3f;

    Vector3 playerVelocity;

    public Transform playerGroundCheck;

    public float playerGroundDistance = 0.4f;

    public LayerMask playerGroundMask;

    bool playerIsGrounded;

    private bool JumpLock = true;

    private bool moveLock = false;

    void Update()
    {
        playerIsGrounded = Physics.CheckSphere(playerGroundCheck.position, playerGroundDistance, playerGroundMask);

        if (playerIsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f;
        }


        float playerX = Input.GetAxis("Horizontal");
        float playerZ = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * playerX + transform.forward * playerZ);

        if (move.sqrMagnitude > 1f) move.Normalize();

        if (!moveLock)
        {
            if (playerX != 0 || playerZ != 0)
            {
                playerController.Move(move * playerSpeed * Time.deltaTime);
            }
        }


        if (Input.GetButtonDown("Jump") && playerIsGrounded && JumpLock == false)
        {
            playerVelocity.y = Mathf.Sqrt(playerJumpHeight * -2f * playerGravRate);
        }

        if (playerVelocity.y < 150)
        {
            playerVelocity.y += playerGravRate * Time.deltaTime;
        }


        playerController.Move(playerVelocity * Time.deltaTime);

    }

    public void setMoveLock(bool moveLock)
    {
        this.moveLock = moveLock;
    }

    public void setJumpLock()
    {
        if (JumpLock == false)
        {
            JumpLock = true;
        }
        else
        {
            JumpLock = false;
        }
    }
}
