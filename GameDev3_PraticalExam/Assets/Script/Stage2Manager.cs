
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Stage2Manager : MonoBehaviour
{
    public float timeRemaining = 30f;
    public int score = 0;
    public int targetScore = 10000;

    public static int finalScore = 0;

    public TMP_Text timerText;
    public TMP_Text scoreText;

    private bool gameEnded = false;

    public Animator playerAnimator;
    public float danceDuration = 4f;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (gameEnded)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateUI();

            if (score > targetScore)
                WinGame();
            else
                LoseGame();

            return;
        }

        UpdateUI();
    }

    public void AddTime(float amount)
    {
        if (gameEnded)
            return;

        timeRemaining += amount;
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (gameEnded)
            return;

        score += amount;
        UpdateUI();

        if (score >= targetScore)
            WinGame();
    }

    private void UpdateUI()
    {
        if (timerText != null)
            timerText.text =
                "Time: " + Mathf.CeilToInt(timeRemaining);

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private void SaveCurrentStage()
    {
        PlayerPrefs.SetString("RetryScene", "Stage2");
        PlayerPrefs.Save();
    }


    public void WinGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        finalScore = score;

        StartCoroutine(WinSequence());
    }

    private System.Collections.IEnumerator WinSequence()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Dance");
        }

        yield return new WaitForSeconds(danceDuration);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayWinSound();
        }

        SceneManager.LoadScene("Win");
    }


    public void LoseGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        finalScore = score;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayLoseSound();
        }

        SaveCurrentStage();

        SceneManager.LoadScene("Gameover");
    }
}
