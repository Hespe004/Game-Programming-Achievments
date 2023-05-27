using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool gameWon { get; private set; }
    public bool gameLost { get; private set; }
    public bool canMove { get; private set; }


    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWonUI;

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

        // Initialize game state variables
        gameWon = false;
        gameLost = false;
        canMove = true;
    }

    // Call this method when the player wins the game
    public void WinGame()
    {
        if (!gameLost) {
            canMove = false;
            gameWonUI.SetActive(true);
            gameWon = true;
            gameLost = false;
        }
    }

    // Call this method when the player loses the game
    public void LoseGame()
    {
        if (!gameWon) {
            canMove = false;
            gameWon = false;
            gameLost = true;
            gameOverUI.SetActive(true);
        }
    }
}
