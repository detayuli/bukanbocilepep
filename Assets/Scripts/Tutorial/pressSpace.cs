using UnityEngine;
using UnityEngine.SceneManagement;

public class PressSpace : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Detect space key press
        {
            SceneManager.LoadSceneAsync(3);
        }
    }
}
