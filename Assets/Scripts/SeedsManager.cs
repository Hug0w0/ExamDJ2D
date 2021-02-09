using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsManager : MonoBehaviour
{
    Player player;
    Enemy enemy;
    AudioSource pickupAudio;

    private void Start()
    {
        pickupAudio = GetComponent<AudioSource>(); // Gets the AudioSource component from the inspector
    }

    // OnTriggerEnter2D is called on a 2D project once a collider interacts with a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<Player>(); // Gets the Player component from the gameObject associated with the other collider
        enemy = other.GetComponent<Enemy>(); // Gets the Enemy component from the gameObject associated with the other collider

        if (other.tag == "Player") // Checks if the tag on the other collider's gameObject is Player
        {
            pickupAudio.Play(); // Plays the audio from the AudioSource
            player.score += 50; // Adds 50 to the player's score
            player.bonusScore += 50; // Adds 50 to the player's bonus score
            player.timer += 5.0f; // Adds 5 to the player's timer
            Destroy(gameObject); // Destroys the gameObject that this script is associated to
        }
        else if (other.tag == "Enemy") // If the condition above is false, this checks if the tag on the other collider's gameObject is Enemy
        {
            enemy.speed = enemy.speed/2; // Halves the enemy's speed
            Destroy(gameObject); // Destroys the gameObject that this script is associated to
        }
    }

    /*IEnumerator PlayerTimePause() // This is a coroutine that was meant to be used if the tag was player on the trigger method above, but didn't work correctly
    {
        player.score += 50;
        player.timeFlows = false;
        yield return new WaitForSecondsRealtime(3.0f);
        player.timeFlows = true;
    }*/

    /*IEnumerator EnemyPause() // This is a coroutine that was meant to be used if the tag was enemy on the trigger method above, but didn't work correctly
    {
        enemy.direction = 0;
        yield return new WaitForSecondsRealtime(3.0f);
        enemy.direction = 1;
    }*/
}
