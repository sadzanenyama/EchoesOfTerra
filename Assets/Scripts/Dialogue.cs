using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public LevelDialogueSO levelDialogue;

    public GameObject sideMessage;
    public Button continueButton;
    public float timeBtwCharacters = 0.05f;
    public AudioClip typingSound;
    private AudioSource audioSource;

    public TextMeshProUGUI nameDisplay;
    public Image icon;
    public TextMeshProUGUI mainTextDisplay;
    public TextMeshProUGUI sideTextDisplay;

    public int currentMessageIndex; 
    public int currentDialogueIndex;

    public static Dialogue instance;


    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        continueButton.onClick.AddListener(ShowNextMessage);
        currentMessageIndex = -1;
        currentDialogueIndex = 0;
    }

    // Typewriter effect for Unity Text
    public IEnumerator TypeText(TextMeshProUGUI uiText, string textToType, float timeBtwCharacters = 0.05f)
    {
        uiText.maxVisibleCharacters = 0;
        uiText.text = textToType;

        foreach (char letter in textToType.ToCharArray())
        {
            uiText.maxVisibleCharacters += 1;
            if (typingSound != null)
                audioSource.PlayOneShot(typingSound, 0.1f);
            yield return new WaitForSecondsRealtime(timeBtwCharacters);
        }
    }

    public void ShowNextMessage() {
        // Move to the next sentence
        currentDialogueIndex++;
        StopAllCoroutines();

        // If there are more sentences, display the next one
        if (currentDialogueIndex < levelDialogue.messages[currentMessageIndex].dialogue.Length)
        {
            StartCoroutine(TypeText(mainTextDisplay, levelDialogue.messages[currentMessageIndex].dialogue[currentDialogueIndex]));
        }
        else
        {
            // All sentences have been shown, handle what happens next (e.g., start a new scene, etc.)
            Debug.Log("All messages displayed");
            gameObject.SetActive(false);
            PauseManager.Resume();
            WaveSpawner.instance.WaveStart();
        }
    }
 
    public void DisplayMainMessage()
    {
        gameObject.SetActive(true);
        currentMessageIndex++;
        currentDialogueIndex = 0;
        PauseManager.Pause(false);
        StopAllCoroutines();  // Stop any previous coroutine to avoid overlapping
        StartCoroutine(TypeText(mainTextDisplay, levelDialogue.messages[currentMessageIndex].dialogue[0]));
    }


/*    public void UpdateInGameMessage(string newText)
    {
        StopAllCoroutines();  // Stop any previous coroutine to avoid overlapping
        StartCoroutine(TypeText(sideTextDisplay.GetComponent<Text>(), newText));
    }*/
}