using UnityEngine;
using TMPro;
using System.Collections;

public class NPCController : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] dialogueLines; // Array for multiple text lines

    [Header("UI References")]
    public GameObject bubbleCanvas;
    public TMP_Text dialogueText;  

    [Header("Timing")]
    public float timePerBubble = 5f; // Duration for each line

    private Coroutine dialogueRoutine;

    void Start()
    {
        if (bubbleCanvas != null)
        {
            bubbleCanvas.SetActive(false);
        }
    }

    
    
    public void Interact()
    {
            Debug.Log("Interacted with: " + gameObject.name); // Add this line!

        if (bubbleCanvas == null || dialogueText == null || dialogueLines == null || dialogueLines.Length == 0) return;
        
        if (dialogueRoutine != null)
        {
            StopCoroutine(dialogueRoutine);
        }
        dialogueRoutine = StartCoroutine(ShowDialogueSequence()); 
    }

    IEnumerator ShowDialogueSequence()
    {
        bubbleCanvas.SetActive(true);

        
        foreach (string line in dialogueLines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(timePerBubble);
        }

     
        bubbleCanvas.SetActive(false);
    }
}

