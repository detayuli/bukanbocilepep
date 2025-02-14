using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject playButton, creditsButton, title, creditPanel1, creditPanel2;
    private int creditStage = 0; // 0 = Menu, 1 = Credit Panel 1, 2 = Credit Panel 2

    void Update()
    {
        if (Input.GetButtonUp("Submit"))
        {
            if (creditStage == 1) TogglePanels(creditPanel1, creditPanel2, 2); // Panel 1 → Panel 2
            else if (creditStage == 2) TogglePanels(creditPanel2, null, 0);   // Panel 2 → Menu
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void CreditPanel()
    {
        TogglePanels(null, creditPanel1, 1); // Menu → Panel 1
    }

    private void TogglePanels(GameObject from, GameObject to, int newStage)
    {
        if (from) from.SetActive(false);
        if (to) to.SetActive(true);

        bool isMainMenu = newStage == 0;
        playButton.SetActive(isMainMenu);
        creditsButton.SetActive(isMainMenu);
        title.SetActive(isMainMenu);

        creditStage = newStage;
    }
}
