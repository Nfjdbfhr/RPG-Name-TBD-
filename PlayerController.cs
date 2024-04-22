using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 1f;

    public bool isOnGround;
    public bool isRunning = true;

    public KeyCode forward = KeyCode.W;
    public KeyCode left = KeyCode.A;
    public KeyCode back = KeyCode.S;
    public KeyCode right = KeyCode.D;
    public KeyCode sprint = KeyCode.LeftControl;
    public KeyCode jump = KeyCode.Space;
    public KeyCode settingsKey = KeyCode.E;

    public GameObject settingsCanvas;

    public InventoryManager inven;
    
    public Animator playerAnim;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();
        settingsCanvas.SetActive(false);
        isOnGround = false;
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            checkForMovement();
            if (Input.GetKeyDown(settingsKey))
            {
                isRunning = false;
                settingsCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerAnim.SetBool("walking", false);
                inven.swapTab(-1);
            }
        }
        else
        {
            if (Input.GetKeyDown(settingsKey))
            {
                isRunning = true;
                settingsCanvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void checkForMovement()
{
    float horizontalInput = 0f;
    float verticalInput = 0f;

    // Get the camera's forward direction without considering its y-component
    Vector3 cameraForward = Camera.main.transform.forward;
    cameraForward.y = 0f;
    cameraForward.Normalize();

    if (Input.GetKey(sprint))
    {
        moveSpeed = 5f;
        playerAnim.SetBool("running", true);
    }
    else
    {
        moveSpeed = 3f;
        playerAnim.SetBool("running", false);
    }

    // Calculate the movement direction based on camera's forward and right vectors
    Vector3 moveDirection = Vector3.zero;
    if (Input.GetKey(forward))
    {
        moveDirection += cameraForward;
    }
    if (Input.GetKey(back))
    {
        moveDirection -= cameraForward;
    }
    if (Input.GetKey(right))
    {
        moveDirection += Camera.main.transform.right;
    }
    if (Input.GetKey(left))
    {
        moveDirection -= Camera.main.transform.right;
    }

    // Normalize the movement direction to ensure consistent speed
    if (moveDirection.magnitude > 1f)
    {
        moveDirection.Normalize();
    }

    // Apply movement only if there's input
    if (moveDirection != Vector3.zero)
    {
        // Set player's rotation to face the movement direction
        Quaternion newRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);

        // Translate the player in the calculated movement direction
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        
        // Set walking animation
        playerAnim.SetBool("walking", true);
    }
    else
    {
        // Set idle animation
        playerAnim.SetBool("walking", false);
    }

    // Check for jump input
    if (Input.GetKeyDown(jump) && isOnGround)
    {
        isOnGround = false;
        playerAnim.SetTrigger("jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}


    public IEnumerator land()
    {
        playerAnim.SetTrigger("land");

        yield return new WaitForSeconds(0.34f);

        isOnGround = true;
        playerAnim.ResetTrigger("land");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            StartCoroutine(land());
        }
    }
}
