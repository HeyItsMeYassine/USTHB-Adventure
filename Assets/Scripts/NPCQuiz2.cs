using UnityEngine;
using TMPro;

public class NPCQuiz2 : MonoBehaviour
{
    public PlayerController player;
    public NPCInteraction2 npcInteraction;

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
        quizPanel.transform.SetAsLastSibling();

        questionText.text =
            "Which of these components is the physical brain of the computer?";
    }

    public void AnswerA() => SubmitAnswer(false);
    public void AnswerB() => SubmitAnswer(false);
    public void AnswerC() => SubmitAnswer(false);
    public void AnswerD() => SubmitAnswer(true);

    void SubmitAnswer(bool isCorrect)
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);
        resultPanel.transform.SetAsLastSibling();

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
            GameProgress.Instance.GiveInfoBadge();
        }

        player.canMove = true;
    }
}
