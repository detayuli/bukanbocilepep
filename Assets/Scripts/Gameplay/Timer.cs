using TMPro;
using UnityEngine;
using UnityEngine.UI; // Jika ingin menampilkan timer pada UI

public class CountdownTimer : MonoBehaviour
{
    public float timerInSeconds = 180f; // Waktu dalam detik (3 menit = 180 detik)
    public TextMeshProUGUI timerText; // Referensi ke UI Text untuk menampilkan timer (opsional)

    private void Update()
    {
        // Kurangi waktu berdasarkan waktu yang berlalu
        if (timerInSeconds > 0)
        {
            timerInSeconds -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timerInSeconds = 0; // Pastikan waktu tidak negatif
            TimerFinished();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timerInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timerInSeconds % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void TimerFinished()
    {
        Debug.Log("Timer selesai!");
        // Tambahkan logika jika timer selesai, misalnya memicu event tertentu.
    }
}
