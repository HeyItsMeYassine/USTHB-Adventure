using UnityEngine;

public class DialogueButtons : MonoBehaviour
{
    public GameObject dialoguePanel;

    public void OnOK()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Start Quiz!");
        // Load quiz scene or open quiz UI here
    }

    public void OnNoThanks()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Quiz declined");
    }
}
