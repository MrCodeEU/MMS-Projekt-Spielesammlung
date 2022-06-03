using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSelect : MonoBehaviour
{
    /// <summary>
    /// Call happens one on start of scene and
    /// sets the Usernames to last saved username or default
    /// </summary>
    void Start()
    {
        //Get usernames of both Players
        string p1 = PlayerPrefs.GetString("Username");
        string p2 = PlayerPrefs.GetString("Username2");
        // if usernames not set => default username Player1 and Player2
        if(p1 == "") PlayerPrefs.SetString("Username", "Player 1");
        if (p2 == "") PlayerPrefs.SetString("Username2", "Player 2");

    }

    /// <summary>
    /// Loads the Settings scene
    /// </summary>
    public void GoToSettings()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Loads the game settings scene of the selected game by id
    /// </summary>
    /// <param name="id">Id of the selected game</param>
    public void StartGame(int id)
    {
        PlayerPrefs.SetInt("SelectedGame", id);
        SceneManager.LoadScene(2);
    }
    /// <summary>
    /// Exits the Application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
