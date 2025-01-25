using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int Player1Score = 0, Player2Score = 0;
    [SerializeField] private TextMeshProUGUI AndreScoreText, AzrilScoreText, TimerText;
    [SerializeField] private float timer = 60f; 

    public void AddScore(int playerNumber, int score)
    {
        if (playerNumber == 1)
        {
            Player1Score += score;
            AndreScoreText.text = Player1Score.ToString();
        }
        else if (playerNumber == 2)
        {
            Player2Score += score;
            AzrilScoreText.text = Player2Score.ToString();
        }
    }

    public int GetScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return Player1Score;
        }
        else if (playerNumber == 2)
        {
            return Player2Score;
        }
        else
        {
            return 0;
        }
    }

    public void SetTimer(float time)
    {
        timer = time;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timer = 0;
            Time.timeScale = 0;
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        if (Player1Score == Player2Score)
        {
            PlayerPrefs.SetString("Winner", "Draw");
            PlayerPrefs.SetInt("HighScore", Player1Score);
        }
        else if (Player1Score > Player2Score)
        {
            PlayerPrefs.SetString("Winner", "Player 1");
            PlayerPrefs.SetInt("HighScore", Player1Score);
        }
        else
        {
            PlayerPrefs.SetString("Winner", "Player 2");
            PlayerPrefs.SetInt("HighScore", Player2Score);
        }

        // Pindah ke scene Game Over
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(4);
    }

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
