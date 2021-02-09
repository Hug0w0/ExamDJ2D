using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer sp;
    Animator anim;
    AudioSource audioSource;

    public int direction = 1;
    public float speed = 2.0f;

    float timer = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Gets the Rigidbody2D component from the inspector
        sp = GetComponent<SpriteRenderer>(); // Gets the SpriteRenderer component from the inspector
        anim = GetComponent<Animator>(); // Gets the Animator component from the inspector
        audioSource = GetComponent<AudioSource>(); // Gets the AudioSource component from the inspector
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; // Removes time from the variable
        if (timer < 0) // Checks if the timer is under 0
        {
            direction = -direction; // Changes the direction to its opposite
            timer = 3.0f; // Sets the timer as 3
            sp.flipX = !sp.flipX; // Flips the sprite on the X-axis
        }
        
        if (speed != 0 || direction != 0) // Checks if either the speed or the direction are not 0
        {
            if (!audioSource.isPlaying) // Checks if the audio is already playing
            {
                audioSource.Play(); // Plays the audio from the AudioSource
            }
        }
        else if(speed == 0 || direction == 0) // Checks if either the speed or the direction are 0
        {
            audioSource.Stop(); // Stops the audio from the AudioSource
        }
    }

    // OnCollisionStay2D is called on a 2D project once a collision is detected & held between this gameObject's collider and another one
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Map") // Checks if the tag on the other collider is Map
        {
            Vector2 position = rb2d.position;
            if(position.x < 10) // Checks if the position on the x-axis is under 10
            {
                position.x = position.x + speed * direction * Time.deltaTime; // Adds the speed multiplied by the direction and time to the current horizontal position
                anim.SetBool("Idle", false); // Defines the Idle parameter of the Animator as false
                anim.SetBool("Walking", true); // Defines the Walking parameter of the Animator as true
            }
            else if(position.x > 10) // Checks if the position on the x-axis is above 10
            {
                position.x = position.x + speed * -direction * Time.deltaTime; // Adds the speed multiplied by the inverse direction and time to the current horizontal position
                anim.SetBool("Idle", false); // Defines the Idle parameter of the Animator as false
                anim.SetBool("Walking", true); // Defines the Walking parameter of the Animator as true
            }
            rb2d.MovePosition(position); // Moves the Rigidbody2D using the position vector
        }
    }
}
