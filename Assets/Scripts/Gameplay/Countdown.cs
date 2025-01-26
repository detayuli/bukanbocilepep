using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText; // Reference to the TextMeshPro UI
    [SerializeField] private string[] countdownMessages = { "VS", "3", "2", "1" }; // The countdown messages
    [SerializeField] private float delayBetweenSteps = 1f; // Delay between each step

    private void Start()
    {
        StartCoroutine(PlayCountdown());
    }

    private IEnumerator PlayCountdown()
    {
        // Loop through each message in the countdownMessages array
        foreach (string message in countdownMessages)
        {
            countdownText.text = message; // Display the current message
            yield return new WaitForSeconds(delayBetweenSteps); // Wait before moving to the next step
        }

        // Clear the countdown and start the game
        countdownText.text = "GO"; // Clear the countdown text
        StartGame(); // Call the function to start the game
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync(4);
    }
}
