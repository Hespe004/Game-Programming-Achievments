using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float playerSpeed = 40f;
    [SerializeField] private float turnSpeed = 5f;
    private float floorWidth;
    private float floorLength;

    private Animator animator;
    [SerializeField] private bool thirdPerson = false;
    public static PlayerMovement Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // Get the size of the "floor" plane
        MeshRenderer floorRenderer = GameObject.Find("Floor").GetComponent<MeshRenderer>();
        floorWidth = floorRenderer.bounds.size.x;
        floorLength = floorRenderer.bounds.size.z;
    }

    private void Update()
    {
        if (GameManager.Instance.canMove) {
            if (thirdPerson) {
                MoveThirdPerson();
            }
            else {
                MoveTopDown();
            }
        }
    }

    private void MoveThirdPerson()
    {
        Camera cam = Camera.main;
        float horizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        Vector3 movement = cam.transform.right * horizontal + cam.transform.forward * vertical;
        movement.y = 0f;

        controller.Move(movement);

        if (movement.magnitude != 0f)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(-movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveTopDown() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical);
        Vector3 moveSpeedUp = move * Time.deltaTime * playerSpeed;

        // Set Y to 0
        moveSpeedUp.y = 0f;

        // Move player
        controller.Move(moveSpeedUp);

        // Limit the player's position within the boundaries
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -floorWidth / 2f, floorWidth / 2f);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -floorLength / 2f, floorLength / 2f);
        transform.position = currentPosition;
    
        // Rotation
        if (move != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(-move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }


}
