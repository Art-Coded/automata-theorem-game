using System.Collections;
using UnityEngine;
using TMPro;

public class SecondNPC : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component
    public string[] dialogue; // Array for holding dialogue lines
    private int index = 0; // Index to track current dialogue line

    public float wordSpeed; // Speed of typing effect
    public bool playerIsClose; // To check if player is near the NPC

    void Start()
    {
        dialogueText.text = ""; // Clear text at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true); // Show dialogue panel
                StartCoroutine(Typing()); // Start typing the dialogue
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine(); // Show next line of dialogue
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText(); // Close dialogue panel
        }
    }

    public void RemoveText()
    {
        dialogueText.text = ""; // Clear text
        index = 0; // Reset index
        dialoguePanel.SetActive(false); // Hide dialogue panel
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter; // Display letters one by one
            yield return new WaitForSeconds(wordSpeed); // Wait for specified speed
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++; // Move to the next dialogue line
            dialogueText.text = ""; // Clear text for new line
            StartCoroutine(Typing()); // Start typing the next line
        }
        else
        {
            RemoveText(); // Close dialogue if no more lines
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true; // Set playerIsClose to true when player enters trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false; // Set playerIsClose to false when player exits trigger
            RemoveText(); // Close dialogue when player leaves
        }
    }
}
