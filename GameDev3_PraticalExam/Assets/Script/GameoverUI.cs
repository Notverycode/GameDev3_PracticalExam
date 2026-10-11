
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameoverUI : MonoBehaviour
{
    public TMP_Text finalScoreText;

    private void Start()
    {
        finalScoreText.text =
            "Score: " + Stage1Manager.finalScore;
    }

    public void RestartStage()
    {
        string sceneName =
            PlayerPrefs.GetString("RetryScene", "Stage1");

        Stage1Manager.finalScore = 0;

        SceneManager.LoadScene(sceneName);
    }
}



