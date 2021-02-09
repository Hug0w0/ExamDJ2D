using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Checks if the spacebar key is pressed
        {
            SceneManager.LoadScene("MainMenu"); // Loads a different scene through name if the condition above is true
        }
    }
}
