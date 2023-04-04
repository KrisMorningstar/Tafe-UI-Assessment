using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public void MenuChange(GameObject _menu)
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        _menu.SetActive(true);
    }
}
