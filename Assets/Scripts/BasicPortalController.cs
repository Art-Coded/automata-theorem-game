using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPortalController : MonoBehaviour
{
    public Transform destination; // Set the destination portal
    GameObject player;

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
                player.transform.position = destination.position;

                // Optional: Print for debugging
                Debug.Log("Player teleported to: " + destination.position);
            }
        }
    }
}
