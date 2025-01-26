using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject playButton, creditsButton, title, creditPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(creditPanel)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                playButton.SetActive(true);
                creditsButton.SetActive(true);
                title.SetActive(true);
                creditPanel.SetActive(false);
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void CreditPanel()
    {
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        title.SetActive(false);
        creditPanel.SetActive(true);
    }
}
