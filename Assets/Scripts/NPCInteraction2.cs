using UnityEngine;
using TMPro;

public class NPCInteraction2 : MonoBehaviour
{
    public GameObject interactText;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public GameObject okButton;
    public GameObject noThanksButton;
    public GameObject thanksButton;

    public PlayerController player;
    public NPCQuiz2 npcQuiz;

    private bool playerNearby;
    private bool isInteracting;

    public bool quizCompleted;

    void Update()
    {
        if (playerNearby && !isInteracting && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }
    }

    void OpenDialogue()
    {
        isInteracting = true;

        player.canMove = false;
        interactText.SetActive(false);
        dialoguePanel.SetActive(true);

        if (!quizCompleted)
        {
            dialogueText.text =
                "Welcome to USTHB University, Mario!\n\n" +
                "I prepared a QUIZ for you in Informatics.\n" +
                "Wanna take it?";

            okButton.SetActive(true);
            noThanksButton.SetActive(true);
            thanksButton.SetActive(false);
        }
        else
        {
            dialogueText.text =
                "Wow! You are really incredible.\n" +
                "You deserved that.";

            okButton.SetActive(false);
            noThanksButton.SetActive(false);
            thanksButton.SetActive(true);
        }
    }

    public void OnOK()
    {
        isInteracting = false;
        dialoguePanel.SetActive(false);
        npcQuiz.StartQuiz();
    }

    public void OnNoThanks()
    {
        CloseDialogue();
    }

    public void OnThanks()
    {
        CloseDialogue();
    }

    void CloseDialogue()
    {
        isInteracting = false;
        dialoguePanel.SetActive(false);
        player.canMove = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isInteracting)
            return;

        playerNearby = false;
        interactText.SetActive(false);
        dialoguePanel.SetActive(false);
        player.canMove = true;
    }
}
