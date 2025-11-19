using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Score das Frutas")]
    public int totalScore;
    public Text scoreText;

    [Header("Vidas do Player")]
    public int lives = 3;
    public Text lifeText;

    [Header("Game Over")]
    public GameObject gameOver;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip gameOverClip;

    public static GameController instance;

    void Start()
    {
        instance = this;

        // Atualiza HUD inicial
        UpdateScoreText();
        UpdateLifeText();
    }

    // ---------------- SCORE ----------------
    public void UpdateScoreText()
    {
        if(scoreText != null)
            scoreText.text = totalScore.ToString();
    }

    // ---------------- VIDAS ----------------
    public void UpdateLifeText()
    {
        if(lifeText != null)
            lifeText.text = "x " + lives;
    }

    public void LoseLife()
    {
        lives--;
        UpdateLifeText();

        if (lives <= 0)
        {
            ShowGameOver();
        }
    }

    // ---------------- GAME OVER ----------------
    public void ShowGameOver()
    {
        if(gameOver != null)
            gameOver.SetActive(true);

        // Toca som do Game Over
        if(audioSource != null && gameOverClip != null)
            audioSource.PlayOneShot(gameOverClip);
    }

    // ---------------- REINICIAR FASE ----------------
    public void RestartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // ---------------- ADICIONAR PONTOS ----------------
    public void AddScore(int amount)
    {
        totalScore += amount;
        UpdateScoreText();
    }
}
