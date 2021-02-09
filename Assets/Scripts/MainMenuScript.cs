using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // Checks if the spacebar is pressed
        {
            SceneManager.LoadScene("ControlsScreen"); // Loads a different scene through name if the condition above is true
        }
    }
}
