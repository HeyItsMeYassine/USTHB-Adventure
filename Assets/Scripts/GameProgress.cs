using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public bool mathBadge;
    public bool infoBadge;

    public GameObject startPanel;
    public GameObject winPanel;

    public PlayerController player;

    public GameObject mathResultPanel;
    public GameObject infoResultPanel;

    void Awake()
    {
        Instance = this;

        if (startPanel != null)
            startPanel.SetActive(true);

        if (winPanel != null)
            winPanel.SetActive(false);

        player.canMove = false;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        player.canMove = true;
    }

    public void GiveMathBadge()
    {
        mathBadge = true;
        CheckWinCondition();
    }

    public void GiveInfoBadge()
    {
        infoBadge = true;
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (mathBadge && infoBadge)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        if (mathResultPanel != null)
            mathResultPanel.SetActive(false);

        if (infoResultPanel != null)
            infoResultPanel.SetActive(false);

        player.canMove = false;
        winPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        winPanel.SetActive(false);
        player.canMove = true;
    }
}
