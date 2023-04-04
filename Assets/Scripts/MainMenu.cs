using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void MenuChange(GameObject _menu)
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        _menu.SetActive(true);
    }

    public void SliderAdjust(TMP_Text _value)
    {
        _value.text = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value.ToString();
    }
}
