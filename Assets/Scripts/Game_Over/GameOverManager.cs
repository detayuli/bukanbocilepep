using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private GameObject andreWin, azrilWin, draw;
    [SerializeField] private GameObject kota, maduraMart, gunungButton; // Backgrounds
    [SerializeField] private GameObject starkota, starmaduraMart, stargunungButton; // Backgrounds


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

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void ShowSelectedEnvironment()
{
    string environment = PlayerPrefs.GetString("SelectedEnvironment", "kota");
    Debug.Log("Selected Environment: " + environment);  // Debug to check the retrieved value

    // Set the background based on environment selection
    if (environment == "kota")
    {
        kota.SetActive(true);
        starkota.SetActive(true);
    }
    else if (environment == "maduraMart")
    {
        maduraMart.SetActive(true);
        starmaduraMart.SetActive(true);
    }
    else if (environment == "gunungButton")
    {
        gunungButton.SetActive(true);
        stargunungButton.SetActive(true);
    }
    else
    {
        Debug.LogError("Environment not found!");
    }
}

}
