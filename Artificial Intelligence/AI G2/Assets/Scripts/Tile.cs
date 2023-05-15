using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color defaultColor;
    public Color alternateColor;
    public bool isAlive = false;

    private new Renderer renderer;
    private Color previousColor;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        if(renderer.material.color==defaultColor) {
            previousColor = renderer.material.color;
        }        
        else if(renderer.material.color==alternateColor) {
            previousColor = renderer.material.color;
        }
        renderer.material.color = color;
    }

    public void ResetColor() {
        renderer.material.color = previousColor;
    }

    public void ToggleState()
    {
        isAlive = !isAlive;
        SetColor(isAlive ? defaultColor : alternateColor);

        // Notify the GameManager about the toggle
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.HighlightTile(this);
        }
    }

    public void SetState(bool SetAlive) {
        isAlive = SetAlive;
        SetColor(isAlive ? defaultColor : alternateColor);

        // Notify the GameManager about the toggle
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.HighlightTile(this);
        }
    }

    public Tile[] GetNeighbors()
    {
        Tile[] neighbors = new Tile[8];

        // Get the position of this tile in the grid
        int x = (int)transform.localPosition.x;
        int y = (int)transform.localPosition.y;

        // Top-Left
        neighbors[0] = TileGenerator.Instance.GetTileAtPosition(x - 1, y + 1);
        // Top
        neighbors[1] = TileGenerator.Instance.GetTileAtPosition(x, y + 1);
        // Top-Right
        neighbors[2] = TileGenerator.Instance.GetTileAtPosition(x + 1, y + 1);
        // Left
        neighbors[3] = TileGenerator.Instance.GetTileAtPosition(x - 1, y);
        // Right
        neighbors[4] = TileGenerator.Instance.GetTileAtPosition(x + 1, y);
        // Bottom-Left
        neighbors[5] = TileGenerator.Instance.GetTileAtPosition(x - 1, y - 1);
        // Bottom
        neighbors[6] = TileGenerator.Instance.GetTileAtPosition(x, y - 1);
        // Bottom-Right
        neighbors[7] = TileGenerator.Instance.GetTileAtPosition(x + 1, y - 1);

        return neighbors;
    }

    public int GetAliveNeighborCount()
    {
        int count = 0;

        Tile[] neighbors = GetNeighbors();

        foreach (Tile neighbor in neighbors)
        {
            if (neighbor != null && neighbor.isAlive)
            {
                count++;
            }
        }

        return count;
    }

}
