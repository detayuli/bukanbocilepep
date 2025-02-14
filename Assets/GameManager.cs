using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int Player1Score = 0, Player2Score = 0;
    [SerializeField] private TextMeshProUGUI AndreScoreText, AzrilScoreText, TimerText;
    [SerializeField] private float timer = 60f;
    [SerializeField] private GameObject kota, maduraMart, gunungButton, starKota, starMaduraMart, stargunungButton; // Backgrounds
    [SerializeField] private GameObject pauseUI, environment, character, bakso;
    private bool isPaused = false;
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

    private void Start()
    {
        RandomizeBackground(); // Call the randomizer at the start of the game
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

        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseUI.SetActive(isPaused);
        environment.SetActive(!isPaused);
        character.SetActive(!isPaused);
        bakso.SetActive(!isPaused);
    }

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
        return playerNumber == 1 ? Player1Score : (playerNumber == 2 ? Player2Score : 0);
    }

    public void SetTimer(float time)
    {
        timer = time;
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

        // Save the environment used for the GameOverMenu
        PlayerPrefs.Save();

        // Pindah ke scene Game Over
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(5);
    }

    private void RandomizeBackground()
    {
        // Hide all backgrounds first
        kota.SetActive(false);
        maduraMart.SetActive(false);
        gunungButton.SetActive(false);
        starKota.SetActive(false);
        starMaduraMart.SetActive(false);
        stargunungButton.SetActive(false);

        // Ambil background sebelumnya dari PlayerPrefs
        int lastBackground = PlayerPrefs.GetInt("LastBackground", -1);
        int randomBackground;

        do
        {
            randomBackground = UnityEngine.Random.Range(0, 3); // 0, 1, atau 2
        }
        while (randomBackground == lastBackground); // Hindari memilih yang sama dua kali berturut-turut

        string selectedEnvironment = "";

        switch (randomBackground)
        {
            case 0:
                kota.SetActive(true);
                starKota.SetActive(true);
                selectedEnvironment = "kota";
                break;
            case 1:
                maduraMart.SetActive(true);
                starMaduraMart.SetActive(true);
                selectedEnvironment = "maduraMart";
                break;
            case 2:
                gunungButton.SetActive(true);
                stargunungButton.SetActive(true);
                selectedEnvironment = "gunungButton";
                break;
        }

        // Simpan background yang baru dipilih ke PlayerPrefs
        PlayerPrefs.SetInt("LastBackground", randomBackground);
        PlayerPrefs.SetString("SelectedEnvironment", selectedEnvironment);
        PlayerPrefs.Save();

        Debug.Log("Environment Selected: " + selectedEnvironment);
    }





}
