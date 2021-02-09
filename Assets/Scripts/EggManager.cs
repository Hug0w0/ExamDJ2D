using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EggManager : MonoBehaviour
{
    Player player;
    AudioSource pickupAudio;

    public int pickedEggs;

    int totalEggs;

    // Start is called before the first frame update
    void Start()
    {
        pickupAudio = GetComponent<AudioSource>(); // Gets the AudioSource component from the inspector
        
        totalEggs = 12; // Sets the number of total eggs as 12
        pickedEggs = 0; // Sets the number of picked eggs as 0
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedEggs == totalEggs) // Checks if the number of picked eggs is equal to the number of total eggs
        {
            SceneManager.LoadScene("Victory"); // Loads a different scene through name
        }
    }

    // OnTriggerEnter2D is called on a 2D project once a collider interacts with a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<Player>(); // Gets the Player component from the gameObject associated with the other collider

        if (other.tag == "Player") // Checks if the tag on the other collider's gameObject is Player
        {
            pickupAudio.Play(); // Plays the audio from the AudioSource
            player.score += 100; // Adds 100 to the player's score
            pickedEggs += 1; // Adds 1 to the number of picked eggs
            Destroy(gameObject); // Destroys the gameObject that this script is associated to
        }
    }
}
