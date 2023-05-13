using UnityEngine;

public enum ColorMode
{
    Default,
    Alternate
}

public class TileGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int numRows = 18;
    public int numColumns = 32;
    public ColorMode colorMode;

    private void Start()
    {
        // Calculate the total number of tiles
        int totalTiles = numRows * numColumns;

        // Get the size of the tile prefab
        Vector2 tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size;

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
                    tile.SetColor(tile.defaultColor);
                }
                else
                {
                    tile.SetColor(tile.alternateColor);
                }
            }
        }
    }
}
