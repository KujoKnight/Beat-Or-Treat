using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUIController : MonoBehaviour
{
    public static bool isWin;
    public GameObject loseScreen;
    public GameObject winScreen;
    public TextMeshProUGUI finalScoreLose;
    public TextMeshProUGUI finalScoreWin;

    private void Start()
    {
        GetComponent<AudioSource>().volume = GameManager.instance.audioLevel;
        finalScoreLose.text = GameManager.instance.finalScore.ToString();
        finalScoreWin.text = GameManager.instance.finalScore.ToString();
        if(isWin)
        {
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
        }
        else
        {
            loseScreen.SetActive(true);
            winScreen.SetActive(false);
        }
    }

    public void PlayGame()
    {
        GameManager.instance.timeRemaining = 601;
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
