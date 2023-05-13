using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileGenerator tileGenerator;
    public Color highlightColor = Color.yellow;

    private Tile highlightedTile;

    private void Update()
    {
        HandleTileHover();
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

}
