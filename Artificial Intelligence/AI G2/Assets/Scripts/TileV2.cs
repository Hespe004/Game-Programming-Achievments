using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileV2 : MonoBehaviour
{
    private bool alive;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color hoverColor;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = baseColor;
    }

    public void setAlive(bool alivent) 
    {
        alive = alivent;
        if (alive)
        {
            _renderer.color = Color.black;
        } else
        {
            _renderer.color = baseColor;
        }
    }

    public void toggleAlive()
    {
        setAlive(!alive);
    }

    public bool getAlive() { return alive; }

    void OnMouseOver()
    {
        if (!alive){ _renderer.color = Color.Lerp(baseColor, hoverColor, .7f); } else { _renderer.color = Color.Lerp(Color.black, hoverColor, .7f); }
        if ( Input.GetKeyDown(KeyCode.Mouse0) )
        {
            toggleAlive();
        }
    }

    void OnMouseExit()
    {
        if (alive) { _renderer.color = Color.black; } else { _renderer.color = baseColor; }
    }

}
