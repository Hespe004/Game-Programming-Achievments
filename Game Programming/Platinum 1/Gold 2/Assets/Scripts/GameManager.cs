using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> boxPrefabs;
    [SerializeField] private GameObject currentBox;
    [SerializeField] private bool isDroppingBox = false;
    
    public List<GameObject> allDroppedBoxes = new List<GameObject>();
    private GameObject respawn;
    private bool allowNewBox;

    // Start is called before the first frame update
    void Start()
    {
        respawn = GameObject.FindWithTag("Respawn");
        SpawnNewBox();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (allowNewBox && currentBox == null && !isDroppingBox)
        {
            SpawnNewBox();
            allowNewBox = false;
        }
    }

    void SpawnNewBox()
    {
        currentBox = Instantiate(boxPrefabs[Random.Range(0, boxPrefabs.Count)]);
        allDroppedBoxes.Add(currentBox);
        currentBox.transform.position = respawn.transform.position;
        isDroppingBox = true;
        currentBox.GetComponent<Rigidbody>().useGravity = false;

        Invoke("AllowDroppable", 0.2f);
    }

    private void AllowDroppable()
    {
        allowNewBox = true;
    }

    public void BoxLanded()
    {
        isDroppingBox = false;
        currentBox = null;
    }
}
