using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactText;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Buttons")]
    public GameObject okButton;
    public GameObject noThanksButton;
    public GameObject thanksButton;

    [Header("References")]
    public PlayerController player;
    public NPCQuiz npcQuiz;

    private bool playerNearby;
    private bool isInteracting;

    // 🔑 THIS IS THE KEY FLAG
    public bool quizCompleted = false;

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
            // BEFORE QUIZ
            dialogueText.text =
                "Welcome to USTHB University, Mario!\n\n" +
                "I prepared a QUIZ for you in Mathematics.\n" +
                "Wanna take it?";

            okButton.SetActive(true);
            noThanksButton.SetActive(true);
            thanksButton.SetActive(false);
        }
        else
        {
            // AFTER QUIZ
            dialogueText.text =
                "Wow! You are really incredible.\n" +
                "You deserved that.";

            okButton.SetActive(false);
            noThanksButton.SetActive(false);
            thanksButton.SetActive(true);
        }
    }

    // ---------------- BUTTONS ----------------

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

    // --------- TRIGGERS ----------

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
