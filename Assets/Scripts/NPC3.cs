using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC3 : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel UI
    public TextMeshProUGUI dialogueText; // Reference to the dialogue text UI
    public string[] dialogue; // Array for the dialogue lines
    private int index = 0; // Current dialogue index

    public float wordSpeed; // Speed at which words are typed out
    public bool playerIsClose; // Check if player is nearby

    void Start()
    {
        dialogueText.text = ""; // Initialize dialogue text to empty
    }

    void Update()
    {
        // Check for player interaction with E key
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true); // Show dialogue panel
                StartCoroutine(Typing()); // Start typing the first line
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine(); // Show the next line if current line is complete
            }
        }

        // Check for quitting dialogue with Q key
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText(); // Hide dialogue panel and reset
        }
    }

    public void RemoveText()
    {
        dialogueText.text = ""; // Clear the text
        index = 0; // Reset index to start
        dialoguePanel.SetActive(false); // Hide dialogue panel
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter; // Add each letter to the dialogue text
            yield return new WaitForSeconds(wordSpeed); // Wait before adding the next letter
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1) // Check if there are more lines
        {
            index++; // Move to the next line
            dialogueText.text = ""; // Clear text before showing the next line
            StartCoroutine(Typing()); // Start typing the next line
        }
        else
        {
            RemoveText(); // No more lines, remove text
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true; // Player is close
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false; // Player left the area
            RemoveText(); // Remove text if the player exits
        }
    }
}
