using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler instance;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public List<Image> livesImages;
    public List<Sprite> livesSprites;
    public List<GameObject> panels;
    public List<GameObject> selectObjects;
    private PlayerController player;
    public Slider volumeSlider;
    public TMP_Dropdown dropdown;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = PlayerController.instance;
        SwapPanels(0);
        volumeSlider.value = GameManager.instance.audioLevel;
        GetComponent<AudioSource>().volume = GameManager.instance.audioLevel;
        setLivesSprites();
        GameManager.instance.StartTime();
    }

    private void FixedUpdate()
    {
        scoreText.text = player.score.ToString();
        timerText.text = GameManager.instance.timeText;
    }

    public void setLivesSprites()
    {
        switch(player.livesCount)
        {
            case 0:
                livesImages[0].sprite = livesSprites[1];
                livesImages[1].sprite = livesSprites[1];
                livesImages[2].sprite = livesSprites[1];
                break;
            case 1:
                livesImages[0].sprite = livesSprites[0];
                livesImages[1].sprite = livesSprites[1];
                livesImages[2].sprite = livesSprites[1];
                break;
            case 2:
                livesImages[0].sprite = livesSprites[0];
                livesImages[1].sprite = livesSprites[0];
                livesImages[2].sprite = livesSprites[1];
                break;
            case 3:
                livesImages[0].sprite = livesSprites[0];
                livesImages[1].sprite = livesSprites[0];
                livesImages[2].sprite = livesSprites[0];
                break;
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

    public void SwapPanels(int index)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if(i == index)
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
    public void Unpause() => PlayerController.instance.Pause();

    public void QuitToTitle()
    {
        GameManager.instance.timeRemaining = 601;
        SceneManager.LoadSceneAsync(0);
    }
}
