using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 20f;

    private float targetDistance;
    private float currentDistance;
    private float targetHeight;
    private float currentHeight;
    private List<GameObject> boxes = new List<GameObject>();
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        boxes = gameManager.allDroppedBoxes;
        targetDistance = transform.position.y;
        currentDistance = transform.position.y;
        targetHeight = 0f;
        currentHeight = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current height of the tower
        float towerHeight = 0f;
        foreach (GameObject box in boxes)
        {
            towerHeight += box.transform.localScale.y;
        }

        // Set the target height and distance of the camera
        targetHeight = towerHeight / boxes.ToArray().Length;
        targetDistance = Mathf.Clamp(targetHeight * zoomSpeed, minDistance, maxDistance);

        // Smoothly interpolate the current height and distance towards the target
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime);
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime);

        // Set the camera position and look at the center of the tower
        Vector3 cameraPosition = new Vector3(0f, currentHeight, -currentDistance);
        transform.position = cameraPosition;
        transform.LookAt(Vector3.zero);
    }
}
