using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Minesweeper : MonoBehaviour
{
    public Text username;
    public Button pause;
    bool isPaused;
    /// <summary>
    /// Start is called before the first frame update
    /// Sets the name of the player
    /// </summary>
    void Start()
    {
        username.text = PlayerPrefs.GetString("Username");
    }

    /// <summary>
    /// Loads the Game preview scene
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(2);
    }
    /// <summary>
    /// Restarts the gam eby reloading the scene
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(3);
    }
}
