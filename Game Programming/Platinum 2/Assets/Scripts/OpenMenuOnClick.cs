using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuOnClick : MonoBehaviour
{
    [SerializeField] private GameObject currentMenu;
    [SerializeField] private GameObject goToMenu;
    public void OpenMenu() {
        currentMenu.SetActive(false);
        goToMenu.SetActive(true);
        SetCurrentMenu();
    }

    public void CloseSideMenu() {
        currentMenu.SetActive(false);
    }

    public void OpenSideMenu() {
        goToMenu.SetActive(true);
        SetCurrentMenu();
    }

    public void SetCurrentMenu() {
        UIManager.Instance.currentMenu = goToMenu;
    }
}
