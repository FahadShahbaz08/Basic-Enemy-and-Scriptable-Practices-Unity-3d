using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GamePlayManager;

public class Movements : MonoBehaviour
{
    [Header("Refernces")]

    [SerializeField] GameObject hitIndicator;

    private float moveSpeed = 10f;
    private float mouseSensitivity = 500f; 

    private float rotationX = 0f;

    public static event ScoreUpdate OnTargetHit;

    public int scoreValue = 0;

    GameObject hittingGameobject;
    void Update()
    {
        HandleMovement();
        HandleMouseRotation();

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            ShowIndicator(hit.point);
            if (hit.transform.gameObject.tag == "Target")
            {
                hittingGameobject = hit.transform.gameObject;
            }
            else 
            {
                hittingGameobject = hit.transform.gameObject;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (hittingGameobject != null)
                {
                    if (hittingGameobject.tag == "Target")
                    {

                        Target target = hittingGameobject.GetComponent<Target>();
                        TargetData targetData = target.targetData;

                        targetData.targetHealth -= 10;
                        scoreValue += 10;
                        Hit();
                        if (targetData.targetHealth <= 0)
                        {
                            hittingGameobject.SetActive(false);
                            print("damage to "+ hittingGameobject.name);
                            scoreValue += 10;
                            Hit();
                        }
                    }
                }
            }
        }
    }

    public float jumpForce = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    void HandleMovement()
    {
        
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical"); 

        // Movement in local space (relative to player orientation)
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    // Handle mouse rotation for looking around
    void HandleMouseRotation()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update vertical rotation (up/down movement)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Clamp to prevent flipping over

        // Rotate the player around the Y-axis (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically (pitch)
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }


    void ShowIndicator(Vector3 hitPosition)
    {
        Vector3 screePosition = Camera.main.WorldToScreenPoint(hitPosition);

        hitIndicator.transform.position = screePosition;

    }

    void Hit()
    {
        print("hitting");
        OnTargetHit?.Invoke(scoreValue);
 
    }
}
