using UnityEngine;

public enum ColorMode
{
    Default,
    Alternate
}

public class TileGenerator : MonoBehaviour
{
    public static TileGenerator Instance { get; private set; }

    [Header("Tiles")]
    public GameObject tilePrefab;

    [Header("Gameboard")]
    public int numRows = 18;
    public int numColumns = 32;
    public ColorMode colorMode;

    private Tile[,] tiles;

    private void Awake()
    {
        // Singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GenerateTiles();
    }

    private void GenerateTiles()
    {
        // Calculate the total number of tiles
        int totalTiles = numRows * numColumns;

        // Get the size of the tile prefab
        Vector2 tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size;

        // Initialize the tiles array
        tiles = new Tile[numColumns, numRows];

        // Instantiate and position the tiles
        for (int row = 0; row < numRows; row++)
        {
            for (int column = 0; column < numColumns; column++)
            {
                // Instantiate a new tile prefab
                GameObject tileGO = Instantiate(tilePrefab, transform);

                // Set the tile's position
                float xPos = column * tileSize.x + tileSize.x / 2f;
                float yPos = -row * tileSize.y - tileSize.y / 2f;
                tileGO.transform.localPosition = new Vector3(xPos, yPos, 0f);

                // Set the tile's color based on the checkerboard pattern
                Tile tile = tileGO.GetComponent<Tile>();
                if (colorMode == ColorMode.Default || (row + column) % 2 == 0)
                {
                    tile.isAlive = true;
                    tile.SetColor(tile.defaultColor);
                }
                else
                {
                    tile.SetColor(tile.alternateColor);
                }

                // Add the tile to the tiles array
                tiles[column, row] = tile;
            }
        }
    }

    public Tile GetTileAtPosition(int x, int y)
    {
        // Check if the provided position is within the grid bounds
        if (x >= 0 && x < numColumns && y >= 0 && y < numRows)
        {
            // Get the tile from the tiles array using the provided coordinates
            return tiles[x, y];
        }

        // If the provided position is outside the grid bounds, return null
        return null;
    }
}
