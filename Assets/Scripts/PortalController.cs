using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class PortalController : MonoBehaviour
{
    public Transform destination;
    public int portalID; // Unique ID for each portal
    GameObject player;
    public static string portalSequence = ""; // Static string to track the sequence of portal entries
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.2f)
            {
                // Teleport player
                player.transform.position = destination.transform.position;

                // Add this portal's ID to the portalSequence string
                portalSequence += portalID.ToString();

                // Update the DialogueText (TextMeshPro) with the current portal sequence
                dialogueText.text = "Portal Sequence: " + portalSequence;

                // Optional: Print for debugging
                Debug.Log("Current portal sequence: " + portalSequence);
            }
        }
    }
}
