using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Color highlightColor = Color.yellow;

    private Tile highlightedTile;
    private bool isAutoAdvancing = false;
    private float autoAdvanceInterval = 1f;
    private float autoAdvanceTimer = 0f;
    [SerializeField] private GameObject gameboard;
    private List<Tile> originalTiles;

    private void Start() {
        originalTiles = new List<Tile>(gameboard.GetComponentsInChildren<Tile>());
    }

    private void Update()
    {
        HandleTileHover();
        HandleTileClick();
        HandleAutoAdvance();
    }

    private void HandleTileClick()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    tile.ToggleState();
                }
            }
        }
    }

    private void HandleTileHover()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                HighlightTile(tile);
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    private void ClearHighlight()
    {
        if (highlightedTile != null)
        {
            highlightedTile.ResetColor();
            highlightedTile = null;
        }
    }

    public void HighlightTile(Tile tile)
    {
        if (highlightedTile != null && highlightedTile != tile)
        {
            ClearHighlight();
        }

        highlightedTile = tile;
        highlightedTile.SetColor(highlightColor);
    }

    public void ToggleAutoAdvance() {
        isAutoAdvancing = !isAutoAdvancing;
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
                NextGeneration();
            }
        }
    }

    public void ToggleAllTilesToDead()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();

        Debug.LogWarning("Toggling all tiles to dead, amount of tiles: " + tiles.Length);

        foreach (Tile tile in tiles)
        {
            if (tile.isAlive) {
                tile.ToggleState();
            }
        }
    }


    public void NextGeneration()
    {
        Debug.Log("Going to new Generation");
        ApplyGameOfLifeRules();
    }

    private List<Tile> GetCopiedBoard() {
        //get all tiles
        originalTiles = new List<Tile>(gameboard.GetComponentsInChildren<Tile>());
        // GetTilesAmount(originalTiles, "current before running method");

        //Copy to new list
        List<Tile> newBoard = new List<Tile>();

        foreach (Tile originalTile in originalTiles)
        {
            Tile newTile = Instantiate(originalTile);
            newTile.transform.SetParent(gameboard.transform);
            newTile.transform.localPosition = originalTile.transform.localPosition;
            newBoard.Add(newTile);
        }

        // Remove old tiles from the scene
        foreach (Tile tile in originalTiles)
        {
            Destroy(tile.gameObject);
        }

        return newBoard;
    }

    private void ApplyGameOfLifeRules()
    {        GetTilesAmount(originalTiles, "before");

        List<Tile> newBoard = GetCopiedBoard();
        foreach (Tile tile in newBoard)
        {
            int aliveNeighbors = tile.GetAliveNeighborCount();

            // Cell is alive
            if (tile.isAlive)
            {
                if (aliveNeighbors < 2 || aliveNeighbors > 3)
                {
                    // Rule 1: Any live cell with fewer than 2 live neighbors dies
                    // Rule 3: Any live cell with more than 3 live neighbors dies
                    // Tile dies from under or overpopulation
                    tile.SetState(false);
                }
                else
                {
                    // Rule 2: Any live cell with two or three live neighbors lives.
                    // Tile survives
                    tile.SetState(true);
                }
            }
            // Cell is dead
            else
            {
                if (aliveNeighbors == 3)
                {
                    // Rule 4: Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                    tile.SetState(true);
                }
                else
                {
                    // Cell remains dead
                    tile.SetState(false);
                }
            }
        }

        GetTilesAmount(originalTiles, "after");

        // Apply the next state and update visuals for all tiles
        RedrawGrid(newBoard);
    }



    private void GetTilesAmount(List<Tile> board, string boardName) {
        int tilesAlive = 0;
        int tilesDead = 0;
        foreach (Tile tile in board)
        {
            if (tile.isAlive) {
                tilesAlive++;
            }
            else {
                tilesDead++;
            }
        }
        Debug.LogWarning("For the board: " + boardName);
        Debug.LogError("Tiles alive: " + tilesAlive);
        Debug.LogError("Tiles dead: " + tilesDead);
    }

    public void RedrawGrid(List<Tile> newBoard)
    {
        // GetTilesAmount(newBoard, "new");
        foreach (Tile tile in newBoard)
        {
            tile.SetColor(tile.isAlive ? tile.defaultColor : tile.alternateColor);
        }
    }




}