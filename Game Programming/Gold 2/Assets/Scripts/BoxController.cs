using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float fallingSpeed = 1f;
    public float moveSpeed = 1f;

    private bool isLanded = false;
    private Rigidbody rb;
    private GameManager gameManager;
    private bool dropKeyPressed = false;
    private GameObject respawn;

    void Start()
    {
        respawn = GameObject.FindWithTag("Respawn");
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            dropKeyPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (!isLanded)
        {
            // Move the box left and right
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

            // Move the box down
            if (dropKeyPressed) {
                rb.GetComponent<Rigidbody>().useGravity = true;
                rb.MovePosition(transform.position - Vector3.up * fallingSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Table")
        {
            isLanded = true;
            if (respawn.transform.position.y - rb.transform.position.y <= 4) {
                respawn.transform.Translate(0, 1f, 0);
            }
            else if (respawn.transform.position.y - rb.transform.position.y >= 6) {
                respawn.transform.Translate(0, 0.2f, 0);
            }

            // Set the box's position to be aligned with the stack
            float yPos = Mathf.Round(transform.position.y * 2) / 2f;
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

            // Let the GameManager know the box has landed
            rb.GetComponent<Rigidbody>().useGravity = true;
            gameManager.BoxLanded();

            // Destroy the script so the box can no longer be moved
            Destroy(this);
        }
    }
}
