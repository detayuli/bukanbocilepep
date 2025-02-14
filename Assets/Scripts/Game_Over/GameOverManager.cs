using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private GameObject andreWin, azrilWin, draw;
    [SerializeField] private GameObject kota, maduraMart, gunungButton; // Backgrounds

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

        ShowSelectedEnvironment();
    }

    private void ShowSelectedEnvironment()
    {
        string environment = PlayerPrefs.GetString("SelectedEnvironment", "kota");
        Debug.Log("Selected Environment: " + environment);  // Debug to check the retrieved value

        // Set the background based on environment selection
        if (environment == "kota")
        {
            kota.SetActive(true);
        }
        if (environment == "maduraMart")
        {
            maduraMart.SetActive(true);
        }
        if (environment == "gunungButton")
        {
            gunungButton.SetActive(true);
        }
        else
        {
            Debug.LogError("Environment not found!");
        }
    }




    public void RestartGame()
    {
        // Restart the game (load the main game scene)
        SceneManager.LoadSceneAsync(2); // Assuming the main game is scene 1
    }

    public void BacktoMainMenu()
    {
        // Load the main menu scene (assuming main menu is scene 0)
        SceneManager.LoadSceneAsync(0);
    }

    // Call this function from the game scene when the game ends
    public static void SetGameOverData(int highScore, string winner, string environment)
    {
        // Save game over data in PlayerPrefs
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetString("Winner", winner);
        PlayerPrefs.SetString("SelectedEnvironment", environment);

        // Load the game over scene (assuming it's scene 2)
        SceneManager.LoadSceneAsync(2); 
    }
}
