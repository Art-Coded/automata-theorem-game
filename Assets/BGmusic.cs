using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusic : MonoBehaviour
{
    public static BGmusic instance; // Singleton instance for global access

    private void Awake()
    {
        if (instance != null) // If an instance already exists, destroy this duplicate
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this; // Assign this instance
            DontDestroyOnLoad(gameObject); // Prevent destruction between scene loads

            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.loop = true; // Enable looping
                audio.Play();      // Start playing the audio
            }
            else
            {
                Debug.LogWarning("No AudioSource component found on BGmusic GameObject.");
            }
        }
    }
}
