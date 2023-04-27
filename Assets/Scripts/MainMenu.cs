using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private int toggleStatus = 0;

    public void MenuChange(GameObject _menu)
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        _menu.SetActive(true);
    }

    public void SliderAdjust(TMP_Text _value)
    {
        _value.text = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value.ToString();
    }

    public void ToggleSwitch(GameObject _light)
    {
        Color _off = new Color(0.2f, 0.07f, 0.07f, 1);
        Color _on = new Color(0.8f, 0.07f, 0.07f, 1);
        Color _half = new Color(0.4f, 0.07f, 0.07f, 1);
        switch (toggleStatus)
        {
            case 0:
                _light.GetComponent<Image>().color = _on;
                toggleStatus = 1;
                break;
            case 1:
                _light.GetComponent<Image>().color = _half;
                toggleStatus = 2;
                break;
            case 2:
                _light.GetComponent<Image>().color = _off;
                toggleStatus = 0;
                break;
        }
    }
}
