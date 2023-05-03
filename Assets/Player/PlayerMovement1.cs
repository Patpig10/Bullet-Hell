using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement1 : MonoBehaviour
{
    public float speed = 5f; // movement speed
    public float jumpForce = 7f; // force of the jump
    public float sensitivity = 2f; // mouse sensitivity
    private Rigidbody rb;
    private bool isGrounded = true; // check if the player is on the ground
    private float mouseX, mouseY; // mouse position
    private Vector3 currentVelocity = Vector3.zero; // current velocity for smoothing
    private DashCounter dashCounter;
    private int maxDashCount = 3;
    private int currentDashCount = 0;
    private float dashRechargeTime = 1f;
    private float timeSinceLastDash = 0f;
   public Animator mELEE;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // lock cursor in center of screen
        dashCounter = GetComponent<DashCounter>();
    }

    void Update()
    {
        // get input from keyboard and mouse
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f); // limit camera rotation

        // rotate camera based on mouse movement
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        // calculate movement direction
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        movement.y = 0f; // prevent player from moving up/down
        movement.Normalize(); // prevent diagonal movement from being faster

        // prevent player from moving during dash and if dash counter is zero
       /* if (!dashCounter.CanDash() || dashCounter.IsDashing() || currentDashCount == 0)
        {
            movement = Vector3.zero;
        }
       */
        // allow movement after dashing
        if (!dashCounter.IsDashing())
        {
            Vector3 targetPosition = transform.position + movement * speed * Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, dashCounter.IsDashing() ? 0.01f : 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.R))
            {
            SceneManager.LoadScene(0);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            mELEE.SetTrigger("Attack");
        }

        // jump if the player is on the ground and space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // dash if left shift key is pressed and dash counter is not zero
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCounter.CanDash() && currentDashCount > 0)
        {
            dashCounter.StartDash();
            currentDashCount--;
        }

        // recharge dash counters after a set time
        if (currentDashCount < maxDashCount)
        {
            timeSinceLastDash += Time.deltaTime;
            if (timeSinceLastDash >= dashRechargeTime)
            {
                currentDashCount++;
                timeSinceLastDash = 0f;
            }
        }
        else if (currentDashCount == maxDashCount)
        {
            dashCounter.ResetDashes(); // reset dashes to maximum value
        }

        

    }

    void OnCollisionEnter(Collision other)
    {
        // check if the player is touching the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
