using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIHandler : MonoBehaviour
{
    public List<GameObject> panels;
    public List<GameObject> selectObjects;
    public Slider volumeSlider;
    public TMP_Dropdown dropdown;

    private void Start()
    {
        SwapPanels(0);
        volumeSlider.value = GameManager.instance.audioLevel;
        GetComponent<AudioSource>().volume = GameManager.instance.audioLevel;
        if(Input.GetJoystickNames().Length != 0)
        {
            EventSystem.current.SetSelectedGameObject(selectObjects[0]);
        }
    }

    public void SwapPanels(int index)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == index)
            {
                panels[i].SetActive(true);
                if (Input.GetJoystickNames().Length != 0)
                {
                    EventSystem.current.SetSelectedGameObject(selectObjects[i]);
                }
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    public void SetGameVolume()
    {
        GameManager.instance.audioLevel = volumeSlider.value;
        GetComponent<AudioSource>().volume = GameManager.instance.audioLevel;
    }

    public void SetQualityLevel()
    {
        switch (dropdown.value)
        {
            case 0:
                QualitySettings.SetQualityLevel(2);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                QualitySettings.SetQualityLevel(0);
                break;
        }
    }

    public void PlayGame()
    {
        GameManager.instance.timeRemaining = 601;
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
