using UnityEngine;
using TMPro;

public class NPCQuiz : MonoBehaviour
{
    public PlayerController player;
    public NPCInteraction npcInteraction;

    public GameObject quizPanel;
    public TextMeshProUGUI questionText;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI finalButtonText;

    public GameObject badgeUI;

    private bool answeredCorrectly;

    void Start()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        badgeUI.SetActive(false);
    }

    public void StartQuiz()
    {
        player.canMove = false;
        quizPanel.SetActive(true);

        questionText.text =
            "In Algebra, what is the name of the matrix denoted I " +
            "with 1s only on the main diagonal?";
    }

    public void AnswerA() => SubmitAnswer(false);
    public void AnswerB() => SubmitAnswer(true);
    public void AnswerC() => SubmitAnswer(false);
    public void AnswerD() => SubmitAnswer(false);

    void SubmitAnswer(bool isCorrect)
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);

        answeredCorrectly = isCorrect;

        if (isCorrect)
        {
            npcInteraction.quizCompleted = true;

            resultText.text =
                "Correct! Please take this gift as a congratulation.";
            finalButtonText.text = "Thanks";
        }
        else
        {
            resultText.text =
                "Sorry, but you are wrong! Good luck next time.";
            finalButtonText.text = "Alright";
        }
    }

    public void FinishQuiz()
    {
        resultPanel.SetActive(false);

        if (answeredCorrectly)
        {
            badgeUI.SetActive(true);
            GameProgress.Instance.GiveMathBadge();
        }

        player.canMove = true;
    }
}
