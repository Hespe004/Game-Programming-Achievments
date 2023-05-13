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
        SetColor(isAlive ? defaultColor : Color.black);

        // Notify the GameManager about the toggle
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.HighlightTile(this);
        }
    }
}
