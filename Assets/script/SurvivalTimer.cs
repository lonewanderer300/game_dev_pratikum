using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI deathMessageText;
    public GameObject restartButton; // << ADD THIS

    private float timeSurvived = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timeSurvived += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeSurvived / 60f);
        int seconds = Mathf.FloorToInt(timeSurvived % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;

        int minutes = Mathf.FloorToInt(timeSurvived / 60f);
        int seconds = Mathf.FloorToInt(timeSurvived % 60f);
        string finalTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (deathMessageText != null)
        {
            deathMessageText.gameObject.SetActive(true);
            deathMessageText.text = "You survived for " + finalTime;
        }

        if (restartButton != null)
        {
            restartButton.SetActive(true); // Show the button
        }
    }

    // This function is called by the button OnClick()
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
