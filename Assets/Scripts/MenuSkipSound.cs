using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSkipSound : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Gets the AudioSource component from the inspector
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Checks if the spacebar key is pressed
        {
            audioSource.Play(); // Plays the audio from the AudioSource
        }
    }
}
