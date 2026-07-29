using UnityEngine;
using TMPro; // Required for TextMeshPro text
using System.Collections;

public class NPCController : MonoBehaviour
{
    [TextArea]
    public string dialogue = "Hello!";

    [Header("UI References")]
    public GameObject bubbleCanvas;  // The speech bubble object
    public TMP_Text dialogueText;    // The text component inside the bubble

    private Coroutine hideRoutine;

    void Start()
    {
        // Make sure the bubble is hidden when the game starts
        if (bubbleCanvas != null)
        {
            bubbleCanvas.SetActive(false);
        }
    }

    public void Interact()
    {
        if (bubbleCanvas == null || dialogueText == null) return;

        // Set the text and reveal the bubble
        dialogueText.text = dialogue;
        bubbleCanvas.SetActive(true);

        // Reset the timer if you press Z multiple times
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
        }
        hideRoutine = StartCoroutine(HideBubbleAfterDelay(3f)); // Hides after 3 seconds
    }

    IEnumerator HideBubbleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        bubbleCanvas.SetActive(false);
    }
}

