using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private GameObject andreWin, azrilWin, draw;

    private void Start()
    {
        // Ambil data highscore dan pemenang dari PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        string winner = PlayerPrefs.GetString("Winner", "No Winner");

        // Cek siapa yang menang atau jika seri
        if (winner == "Draw")
        {
            // Tampilkan panel "Draw"
            draw.SetActive(true);
            HighScoreText.text = "Score: " + highScore;
        }
        else if (winner == "Player 1")
        {
            // Andre menang
            andreWin.SetActive(true);
            HighScoreText.text = "High Score: " + highScore;
        }
        else if (winner == "Player 2")
        {
            // Azril menang
            azrilWin.SetActive(true);
            HighScoreText.text = "High Score: " + highScore;
        }
        else
        {
            Debug.LogError("Winner data not found!");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
