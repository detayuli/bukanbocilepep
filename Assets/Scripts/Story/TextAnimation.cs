using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Text display
    public float typingSpeed; // Typing speed
    [TextArea] public string[] dialogues; // Array of dialogues
    private int currentDialogueIndex = 0; // Track current dialogue
    private Coroutine typingCoroutine; // For handling typing effect
    private bool isTyping = false; // Flag to check typing state
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject dialogue1, dialogue2;

    void Start()
    {
        StartTyping();
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (isTyping) SkipTyping(); // Skip if still typing
            else
            NextDialogue(); // Go to next dialogue
        }
    }

    void StartTyping()
    {
        if (currentDialogueIndex >= dialogues.Length) return; // Exit if all dialogues are done
        textComponent.text = ""; // Reset text
        typingCoroutine = StartCoroutine(TypeText(dialogues[currentDialogueIndex]));
    }

    IEnumerator TypeText(string dialogue)
    {
        isTyping = true;
        foreach (char letter in dialogue)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        nextButton.SetActive(true);
    }

    void SkipTyping()
    {
        nextButton.SetActive(true); 
        if (typingCoroutine != null) StopCoroutine(typingCoroutine); // Stop typing
        textComponent.text = dialogues[currentDialogueIndex]; // Show full dialogue
        isTyping = false;
    }

    void NextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length - 1) // If more dialogues exist
        {
            dialogue2.SetActive(true);
            dialogue1.SetActive(false); 
            currentDialogueIndex++;
            StartTyping();
        }
        else
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
