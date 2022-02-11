using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 playerScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScaleReverse = new Vector3(1, 1, 1);
    public Transform cameraTransform;
    private bool checkCeiling;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //transform.position = transform.position + Camera.main.transform.forward * Time.deltaTime;

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        move = cameraTransform.TransformDirection(move);
        controller.Move(move * Time.deltaTime * playerSpeed);
    
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        checkCeiling = Physics.Raycast(this.transform.position, Vector3.up, 2.0f, -1);

        if (Input.GetButton("Fire3"))
        {
            gameObject.transform.localScale = playerScale;
            //gameObject.transform.Translate(0, -100, 0);
            controller.Move(new Vector3(0, -0.5f, 0));
            playerSpeed = 1.0f;
        }
        else if (!checkCeiling)
        {
            gameObject.transform.localScale = playerScaleReverse;
            playerSpeed = 2.0f;
        }
        else
        {
            gameObject.transform.localScale = playerScale;
            //gameObject.transform.Translate(0, -100, 0);
            controller.Move(new Vector3(0, -0.5f, 0));
            playerSpeed = 1.0f;
        }
        //if (Input.GetButtonUp("Fire3") && groundedPlayer)
        //{
        //    gameObject.transform.localScale = playerScaleReverse;
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
