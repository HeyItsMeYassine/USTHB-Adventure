using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string nomObjet = "Objet Informatique"; // Nom de l'objet (modifiable dans Inspector)
    public AudioClip collectSound; // Son de collecte (drag sfx_collect_item_01)
    public GameObject collectEffect; // Effet visuel (drag Particle System)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Détecte si Player touche
        {
            Collect();
        }
    }

    void Collect()
    {
        Debug.Log("Ramassé : " + nomObjet); // Log pour debug
        if (collectSound != null) AudioSource.PlayClipAtPoint(collectSound, transform.position); // Joue son
        if (collectEffect != null) Instantiate(collectEffect, transform.position, Quaternion.identity); // Effet
        gameObject.SetActive(false); // Désactive l'objet (destruction alternative : Destroy(gameObject))
    }
}