using UnityEngine;
using UnityEngine.EventSystems;

public class OpenMenuOnClick : MonoBehaviour
{
    [SerializeField] private GameObject currentMenu;
    [SerializeField] private GameObject goToMenu;

    public void OpenMenu()
    {
        currentMenu.SetActive(false);
        goToMenu.SetActive(true);
    }
}
