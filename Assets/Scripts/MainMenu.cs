using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private int toggleStatus = 0;

    [SerializeField] private GameObject subBar;

    public void MenuChange(GameObject _menu)
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        _menu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SliderAdjust(TMP_Text _value)
    {
        _value.text = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value.ToString();
    }
    
    public void MusicSlider(AudioSource _audioSource)
    {
        _audioSource.volume = (EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value)/200;
    }
    public void MasterSlider(AudioSource _audioSource)
    {
        // proof of concept, will be blended in full version
        _audioSource.volume = (EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value)/200;
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
                subBar.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                StartCoroutine("Subtitles");
                break;
            case 1:
                // cancel coroutine
                subBar.SetActive(false);
                _light.GetComponent<Image>().color = _half;
                toggleStatus = 2;
                subBar.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                StartCoroutine("Subtitles");
                break;
            case 2:
                // cancel coroutine
                subBar.SetActive(false);
                _light.GetComponent<Image>().color = _off;
                toggleStatus = 0;
                
                break;
        }
    }

    public void ScreenModeDropdown(int _id)
    {
        TMP_Dropdown _resolution = GameObject.Find("ResolutionDropdown").GetComponent<TMP_Dropdown>();
        switch (_id)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    public void ResolutionDropdown()
    {
        string _name = EventSystem.current.currentSelectedGameObject.name;
        string[] _names = _name.Split(' ');
        int _length;
        int _height;
        int.TryParse(_names[2], out _length);
        int.TryParse(_names[4], out _height);

        Screen.SetResolution(_length, _height, Screen.fullScreenMode);
    }

    public IEnumerator Subtitles()
    {
        // make cancellable in polished version
        subBar.SetActive(true);
        yield return new WaitForSeconds(1);
        subBar.SetActive(false);
        yield return null;
    }

}
