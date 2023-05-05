using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    [SerializeField] private List<GameObject> boxPrefabs;
    [SerializeField] private GameObject currentBox;
    [SerializeField] private bool isDroppingBox = false;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    public List<GameObject> allDroppedBoxes = new List<GameObject>();
    private GameObject respawn;
    private bool allowNewBox;
    private float currentScore = 0;

    [SerializeField] private float timeScale = 1f;

    public void SetTimeScale(float speed) {
        // Changing time scale
        timeScale = speed;
        Time.timeScale = timeScale;
    }

    public float GetTimeScale() {
        return timeScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ResetHighScore();
        respawn = GameObject.FindWithTag("Respawn");
        SpawnNewBox();
        UpdateHighScoretext();
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
        currentScore++;
        CheckHighScore();
        isDroppingBox = false;
        currentBox = null;
    }

    public void CheckHighScore() {
        if (currentScore > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);
            if (highScoreText!=null) {
                UpdateHighScoretext();
            }
        }
    }

    public void UpdateHighScoretext() {
        highScoreText.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore", 0)}";
    }

    public void ResetHighScore() {
        PlayerPrefs.SetFloat("HighScore", 0);
    }
}
