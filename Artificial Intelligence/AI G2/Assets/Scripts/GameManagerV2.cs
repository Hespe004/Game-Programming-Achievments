using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManagerV2 : MonoBehaviour
{
    private GameObject[,] board;
    private float scale;
    private bool[][,] history;

    // Auto advance
    private bool isAutoAdvancing = false;
    private float autoAdvanceInterval = 1f;
    private float autoAdvanceTimer = 0f;

    // History dropdown
    [SerializeField]
    private TMP_Dropdown historyDropdown;
    private string[] histories;
    int currentHistoryIndex = 0;
    
    [Header("Tiles")]
    [SerializeField] private GameObject tile1;
    [SerializeField] private GameObject tile2;

    [Header("Gameboard")]
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Where to place tiles?")]
    [SerializeField] private Transform tilesParent;

    public static GameManagerV2 Instance { get; private set; }

    private void Awake()
    {
        // Singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        scale = 32f / (float)width;
        board = new GameObject[width, height];
        bool[,] historyFill = new bool[width, height];
        history = new bool[10][,];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                board[x,y] = Instantiate(((x + y) % 2) == 1? tile1 : tile2, new Vector3(x*scale, y*scale, 0f), Quaternion.identity, tilesParent);
                board[x, y].transform.localScale = new Vector3(scale, scale, 1);
            }
        }
    }

    void Update()
    {
        HandleAutoAdvance();
        UpdateDropdown();
    }

    private void UpdateDropdown()
    {
        historyDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < history.Length; i++)
        {
            string option = "Gen " + i;
            options.Add(option);
            
        }

        historyDropdown.AddOptions(options);
        historyDropdown.value = currentHistoryIndex;
        historyDropdown.RefreshShownValue();
    }

    public void GoToGen(int generationNumber)
    {
        // Lower history unitl generationNumber is reached
        for (int i = generationNumber; i > 0; i--)
        {
            history[i] = history[i - 1];
        }

        history[0] = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Set history
                history[0][x, y] = board[x, y].GetComponent<TileV2>().getAlive();
                // Clear board
                board[x, y].GetComponent<TileV2>().setAlive(false);
            }
        }
        
        currentHistoryIndex = generationNumber;
        historyDropdown.RefreshShownValue();
    }

    public void Next()
    {
        for (int i = 9; i > 0; i--)
        {
            history[i] = history[i - 1];
        }

        history[0] = new bool[width, height];
        //chuck latest board into the history and clear the current board
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                history[0][x, y] = board[x, y].GetComponent<TileV2>().getAlive();
                board[x, y].GetComponent<TileV2>().setAlive(false);
            }
        }

        //check every square
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //checking alive neighbours
                int livingNeighbours = 0;
                for (int a = x-1; a <= x+1; a++)
                {
                    for (int b = y-1; b <= y+1; b++)
                    {
                        if (a >= 0 && a < width && b >= 0 && b < height && history[0][a, b])
                        {
                            if (a != x || b != y)
                            {
                                livingNeighbours++;
                            }
                        }
                    }
                }

                //apply rules
                if (history[0][x,y])
                {
                    if (livingNeighbours == 2 || livingNeighbours == 3) 
                    {
                        board[x, y].GetComponent<TileV2>().setAlive(true);
                    } else
                    {
                        board[x, y].GetComponent<TileV2>().setAlive(false);
                    }
                } else
                {
                    if (livingNeighbours == 3)
                    {
                        board[x, y].GetComponent<TileV2>().setAlive(true);
                    }
                }

            }
        }
    }

    public void Previous()
    {
        if (history[0] != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    board[x, y].GetComponent<TileV2>().setAlive(history[0][x, y]);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                history[i] = history[i + 1];
            }
            history[9] = null;
        }
    }

    public void ClearBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                board[x, y].GetComponent<TileV2>().setAlive(false);
            }
        }
    }

    public void ClearHistory()
    {
        for (int i = 0; i < 10; i++)
        {
            history[i] = null;
        }
    }

    private void HandleAutoAdvance()
    {
        // if (Input.GetKeyDown(KeyCode.Space)) // Press spacebar to toggle auto-advancement
        // {
        //     isAutoAdvancing = !isAutoAdvancing;
        // }

        if (isAutoAdvancing)
        {
            autoAdvanceTimer += Time.deltaTime;
            if (autoAdvanceTimer >= autoAdvanceInterval)
            {
                autoAdvanceTimer = 0f;
                Next();
            }
        }
    }

    public void ToggleAutoAdvance() {
        isAutoAdvancing = !isAutoAdvancing;
    }
}
