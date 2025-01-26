using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class PressSpace : MonoBehaviour
{

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadSceneAsync(3);
        }
    }
}
