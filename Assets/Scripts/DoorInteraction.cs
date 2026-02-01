using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Transform destination;
    public GameObject loadingPanel;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip doorSound;
    public AudioClip loadingSound;

    private bool playerInside;
    private bool isTeleporting;
    private bool inputLocked;
    private Collider doorCollider;

    void Awake()
    {
        doorCollider = GetComponent<Collider>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!playerInside || isTeleporting || inputLocked)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            inputLocked = true;

            if (doorSound != null)
                audioSource.PlayOneShot(doorSound);

            StartCoroutine(TeleportRoutine());
        }
    }

    IEnumerator TeleportRoutine()
    {
        isTeleporting = true;
        playerInside = false;

        doorCollider.enabled = false;

        if (loadingSound != null)
            audioSource.PlayOneShot(loadingSound);

        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = true;

        player.transform.position =
            destination.position + Vector3.up * 1.5f + destination.forward * 1.2f;

        player.transform.rotation = destination.rotation;

        yield return null;

        if (rb != null)
            rb.isKinematic = false;

        loadingPanel.SetActive(false);

        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
        yield return new WaitForSeconds(0.3f);

        doorCollider.enabled = true;
        isTeleporting = false;
        inputLocked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}
