using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject rewardItem; // Drag l'objet collectible correspondant (désactivé au start)
    public bool playerInRange = false; // Détecte si Player est près
    public string dialogueMessage = "Salut Mario ! Passe mon quiz pour obtenir l'objet !"; // Message initial
    public string challengeQuestion = "Quelle est la capitale de l'Algérie ?"; // Question quiz
    public string[] challengeOptions = {"Alger", "Oran", "Constantine"}; // Options
    public int correctAnswerIndex = 0; // Index bonne réponse (0 = Alger)

    private bool challengeActive = false; // Challenge en cours ?
    private bool challengeSuccess = false; // Succès ?

    void Update()
    {
        // Interaction quand Player est près et appuie E
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!challengeActive)
            {
                StartChallenge(); // Lance dialogue + challenge
            }
            else if (challengeActive && !challengeSuccess)
            {
                Debug.Log("Réponds au quiz ! Options : " + string.Join(", ", challengeOptions));
                // Ici, tu peux ajouter UI pour vraie réponse (voir note)
            }
        }
    }

    void StartChallenge()
    {
        challengeActive = true;
        Debug.Log(dialogueMessage); // "Parle" à Mario
        Debug.Log(challengeQuestion); // Pose la question
    }

    public void ValidateAnswer(int answerIndex) // Vérifie réponse (via UI ou test)
    {
        if (answerIndex == correctAnswerIndex)
        {
            challengeSuccess = true;
            Debug.Log("Bravo Mario ! Voici ton objet : " + rewardItem.name);
            rewardItem.SetActive(true); // Débloque l'objet
        }
        else
        {
            Debug.Log("Mauvaise réponse. Réessaye !");
        }
        challengeActive = false;
    }

    // Détection entrée/sortie Player (Tâche 1 : Interaction)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Proche de " + gameObject.name + "! Appuie E pour interagir.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            challengeActive = false;
        }
    }
}