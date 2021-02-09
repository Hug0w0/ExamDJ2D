using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;
    SpriteRenderer sp;
    AudioSource[] audioSource;

    public int score = 0;
    public int lives = 2;
    public float bonusScore = 600.0f;
    public float timer = 60.0f;
    public bool timeFlows = true;

    float horizontal, vertical;
    bool onGround;
    float jumpForce = 200.0f;
    bool verticalMovementOn = false;

    Vector2 jump = new Vector2(0.0f, 2.0f);

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Gets the Rigidbody2D component from the inspector
        anim = GetComponent<Animator>(); // Gets the Animator component from the inspector
        sp = GetComponent<SpriteRenderer>(); // Gets the SpriteRenderer component from the inspector
        audioSource = GetComponents<AudioSource>(); // Gets all the AudioSource components from the inspector and arranges them in an array

        InvokeRepeating("RemoveBonus", 1.0f, 1.0f); // Repeatedly invokes a method by name, in a certain interval, a certain number of times
    }

    // Update is called once per frame
    void Update()
    {
        if(timeFlows == true) // Checks if the timeFlows bool is true
        {
            timer-=Time.deltaTime; // Removes time from the timer
        }

        horizontal = Input.GetAxis("Horizontal"); // Receives horizontal input from the default keys
        vertical = Input.GetAxis("Vertical"); // Receives the vertical input from the default keys

        if (horizontal > 0 && sp.flipX == false) // Checks if there is positive horizontal input and the sprite is not flipped on the x-axis
        {
            anim.SetBool("Idle", false); // Sets the Idle parameter of the Animator as false
            anim.SetBool("Walking", true); // Sets the Walking parameter of the Animator as true
        }
        else if (horizontal > 0 && sp.flipX == true) // Checks if there is positive horizontal input and the sprite is flipped on the x-axis
        {
            sp.flipX = false; // Flips the sprite to the original position
            anim.SetBool("Idle", false); // Sets the Idle parameter of the Animator as false
            anim.SetBool("Walking", true); // Sets the Walking parameter of the Animator as true
        }
        else if (horizontal < 0 && sp.flipX == false) // Checks if there is negative horizontal input and the sprite is not flipped on the x-axis
        {
            sp.flipX = true; // Flips the sprite on the x-axis
            anim.SetBool("Idle", false); // Sets the Idle parameter of the Animator as false
            anim.SetBool("Walking", true); // Sets the Walking parameter of the Animator as true
        }
        else if (horizontal < 0 && sp.flipX == true) // Checks if there is negative horizontal input and the sprite is flipped on the x-axis
        {
            anim.SetBool("Idle", false); // Sets the Idle parameter of the Animator as false
            anim.SetBool("Walking", true); // Sets the Walking parameter of the Animator as true
        }
        else if (vertical != 0) // Checks if there is vertical input
        {
            anim.SetBool("Idle", false); // Sets the Idle parameter of the Animator as false
            anim.SetBool("Walking", true); // Sets the Walking parameter of the Animator as true
        }
        else // If none of the other conditions are true, executes this one
        {
            anim.SetBool("Idle", true); // Sets the Idle parameter of the Animator as true
            anim.SetBool("Walking", false); // Sets the Walking parameter of the Animator as false
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround) // Checks if the spacebar key is pressed and the onGround bool is true
        {
            rb2d.AddForce(jump * jumpForce, ForceMode2D.Impulse); // Adds a force to the rigidbody to make it go up
            onGround = false; // Sets the onGround bool as false
            audioSource[0].Play(); // Plays the audio from the first AudioSource component
        }
        
        if(horizontal != 0 && !audioSource[1].isPlaying) // Checks if there is horizontal input and the audio from the second AudioSource isn't playing
        {
            audioSource[1].Play(); // Plays the audio from the second AudioSource
        }
        else if (vertical != 0 && verticalMovementOn && !audioSource[1].isPlaying) // Checks if there is vertical input, the verticalMovementOn bool is true and the audio from the second AudioSource isn't playing
        {
            audioSource[1].Play(); // Plays the audio from the second AudioSource
        }
        else if(horizontal == 0 && vertical == 0) // Checks if there is no vertical or horizontal input
        {
            audioSource[1].Stop(); // Stops playing the audio from the second AudioSource
        }
    }

    // OnTriggerEnter2D is called on a 2D project once a collider interacts with a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Map") // Checks if the tag on the other collider's gameObject is Map
        {
            verticalMovementOn = true; // Sets the verticalMovementOn bool as true
        }
    }

    // OnTriggerExit2D is called on a 2D project once a collider leaves a trigger collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Map") // Checks if the tag on the other collider's gameObject is Map
        {
            verticalMovementOn = false; // Sets the verticalMovementOn bool as false
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb2d.position; //pega na posição do rigidbody 2D
        position.x = position.x + 5f * horizontal * Time.deltaTime; // Adds the speed multiplied by the direction and time to the current horizontal position
        if (verticalMovementOn == true) // Checks if the verticalMovementOn bool is true
        {
            position.y = position.y + 5f * vertical * Time.deltaTime; // Adds the speed multiplied by the direction and time to the current vertical position
        }
        rb2d.MovePosition(position); //atualiza a posição

        if (position.y < -5) // Checks if the current vertical position in under -5
        {
            lives -= 1; // Removes 1 from lives
            Respawn(); // Invokes the Respawn method
        }
    }

    // OnCollisionEnter2D is called on a 2D project once a collision is detected between this gameObject's collider and another one
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy") // Checks if the tag on the other collider is Enemy
        {
            audioSource[2].Play(); // Plays the audio from the third AudioSource
            lives -= 1; // Removes 1 from lives
            Respawn(); // Invokes the Respawn method
        }
    }

    // OnCollisionStay2D is called on a 2D project once a collision is detected & held between this gameObject's collider and another one
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Map") // Checks if the tag on the other collider is Map
        {
            onGround = true; // Sets the onGround bool as true
        }
    }

    // OnCollisionExit2D is called on a 2D project once the collision between this gameObject's collider and another one is stopped
    private void OnCollisionExit2D()
    {
        onGround = false; // Sets the onGround bool as false
    }

    // This method is used to send the player to his starting position
    private void Respawn()
    {
        Vector2 respawnPos = rb2d.position;
        respawnPos.x = 6.9f;
        respawnPos.y = -3.5f;
        rb2d.MovePosition(respawnPos); // Moves the Rigidbody2D using the position vector
    }

    // This method is used to remove points from the bonus, and is called repeatedly above
    private void RemoveBonus()
    {
        if(timeFlows) // Checks if the timeFlows bool is true
        {
            bonusScore -= 10; // Removes 10 from the bonusScore
        }
    }
}
