using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameoverUI : MonoBehaviour
{
    public TMP_Text finalScoreText;

    private void Start()
    {
        string retryScene =
            PlayerPrefs.GetString("RetryScene", "Stage1");

        int finalScore;

        if (retryScene == "Stage2")
        {
            finalScore = Stage2Manager.finalScore;
        }
        else
        {
            finalScore = Stage1Manager.finalScore;
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + finalScore;
        }
    }

    public void RestartStage()
    {
        string sceneName =
            PlayerPrefs.GetString("RetryScene", "Stage1");

        Stage1Manager.finalScore = 0;
        Stage2Manager.finalScore = 0;

        SceneManager.LoadScene(sceneName);
    }
}