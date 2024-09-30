using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject mainMessage;
    public GameObject sideMessage;
    public Button continueButton;
    public float typeDelay = 0.1f;  // Delay between each letter

    private string[] lvlOnePreMessage;
    public string[] messages;
    private int currentMessageIndex; //tracks which sentence is currently displayed
    private void Start()
    {
        continueButton.onClick.AddListener(ShowNextMessage);
        lvlOnePreMessage = new string[] {
        "I hope you enjoyed your space leave, but I have to say I’m happy you’re back.", 
        "We’ve been getting reports of attacks on this mining colony—probably pirates.",
        "Just keep an eye out for any suspicious ships."
        };
        messages = lvlOnePreMessage;
        currentMessageIndex = 0;

        UpdateLevelMessage(messages[currentMessageIndex]);
    }

    // Typewriter effect for Unity Text
    public IEnumerator TypeText(Text uiText, string fullText)
    {
        uiText.text = "";  // Clear the text initially

        foreach (char letter in fullText.ToCharArray())
        {
            uiText.text += letter;  // Add one letter at a time
            yield return new WaitForSeconds(typeDelay);  // Wait before the next letter
        }
    }

    public void ShowNextMessage() {
        // Move to the next sentence
        currentMessageIndex++;

        // If there are more sentences, display the next one
        if (currentMessageIndex < messages.Length)
        {
            StartCoroutine(TypeText(mainMessage.GetComponent<Text>(), messages[currentMessageIndex]));
        }
        else
        {
            // All sentences have been shown, handle what happens next (e.g., start a new scene, etc.)
            Debug.Log("All messages displayed");
        }
    }
 
    public void UpdateLevelMessage(string newText)
    {
        StopAllCoroutines();  // Stop any previous coroutine to avoid overlapping
        StartCoroutine(TypeText(mainMessage.GetComponent<Text>(), newText));
    }


    public void UpdateInGameMessage(string newText)
    {
        StopAllCoroutines();  // Stop any previous coroutine to avoid overlapping
        StartCoroutine(TypeText(sideMessage.GetComponent<Text>(), newText));
    }
}

/*
 * Pre Level:
 * "I hope you enjoyed your space leave, but I have to say I’m happy you’re back. 
 * We’ve been getting reports of attacks on this mining colony—probably pirates. 
 * Just keep an eye out for any suspicious ships."
 * 
 * "
 * 
 * 
 * 
 */