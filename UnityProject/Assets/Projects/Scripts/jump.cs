using UnityEngine;

// Assurez-vous que le nom de la classe "jump" correspond au nom de votre fichier .cs
public class jump : MonoBehaviour
{
    // Références
    private Animator animator; 
    
    // Optionnel : nécessaire pour que le personnage bouge réellement
    public CharacterController controller; 
    public float moveSpeed = 5f; 

    // Hachage des paramètres pour l'Animator
    private int isWalkingHash; 
    private int jumpHash; 

    void Start()
    {
        // === MODIFICATION CLÉ POUR LA SOLUTION 1B ===
        // Cherche l'Animator sur cet objet OU sur n'importe quel objet enfant.
        animator = GetComponentInChildren<Animator>();
        
        controller = GetComponent<CharacterController>();
        
        // Initialisation des Hachages
        isWalkingHash = Animator.StringToHash("IsWalking"); 
        jumpHash = Animator.StringToHash("Jump");

        // Vérification pour débogage
        if (animator == null)
        {
            Debug.LogError("L'Animator est introuvable sur l'objet ou ses enfants. Vérifiez la hiérarchie.");
        }
    }

    void Update()
    {
        // Récupère l'entrée verticale (W = 1, S = -1, Rien = 0)
        float verticalInput = Input.GetAxis("Vertical"); 
        
        // La variable est VRAIE si l'utilisateur appuie sur W ou S
        bool isMoving = Mathf.Abs(verticalInput) > 0.1f; 

        // ===================================
        // 1. GESTION DE LA MARCHE (W et S)
        // ===================================
        
        if (animator != null)
        {
            // Dit à l'Animator de passer à Walking (True) ou à Idle (False)
            animator.SetBool(isWalkingHash, isMoving);
        }
        
        // OPTIONNEL : Mouvement physique
        if (controller != null)
        {
            // Déplace le personnage vers l'avant/arrière
            Vector3 moveDirection = transform.forward * verticalInput;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // ===================================
        // 2. GESTION DU SAUT (Barre d'espace)
        // ===================================

        if (Input.GetButtonDown("Jump") && animator != null) 
        {
            animator.SetTrigger(jumpHash); 
        }
    }
}