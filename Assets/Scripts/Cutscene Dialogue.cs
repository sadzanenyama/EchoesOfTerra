using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class CutsceneDialogue : MonoBehaviour
{
    public GameObject mainMessage;
    public float typeDelay = 0.05f;  // Delay between each letter
    public float waitTimeBetweenMessages = 0.5f;  // Delay between each message

   [TextArea(3,5)]
    public string[] SceneOne;

    private int currentMessageIndex; // Tracks which sentence is currently displayed

    private void Start()
    {
        currentMessageIndex = 0;
        //Thread.Sleep(300);
        StartCoroutine(ShowNextMessage());
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

    public IEnumerator ShowNextMessage()
    {
        if (currentMessageIndex < SceneOne.Length)
        {
            // Display the current message
            yield return StartCoroutine(TypeText(mainMessage.GetComponent<Text>(), SceneOne[currentMessageIndex]));

            // Wait for 5 seconds before displaying the next message
            yield return new WaitForSeconds(waitTimeBetweenMessages);

            // Move to the next sentence
            currentMessageIndex++;

            // Automatically call the next message
            StartCoroutine(ShowNextMessage());
        }
        else
        {
            // All sentences have been shown, handle what happens next (e.g., start a new scene, etc.)
          //  Debug.Log("All messages displayed, Start Game?");
            // ingameUI.SetActive(true);
            // DialogueUI.SetActive(false);
        }
    }

    public void UpdateMessage(string newText)
    {
        StopAllCoroutines();  // Stop any previous coroutine to avoid overlapping
        StartCoroutine(TypeText(mainMessage.GetComponent<Text>(), newText));
    }
}
