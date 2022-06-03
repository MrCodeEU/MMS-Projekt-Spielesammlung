using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameSettings : MonoBehaviour
{
    public Text gameName;
    public Text playerName1;
    public Text playerName2;
    public InputField player1;
    public InputField player2;
    public Button setPlayer1;
    public Button setPlayer2;
    public Dropdown difficultyMinesweeper;
    public CanvasGroup player2Group;
    /// <summary>
    /// Start is called before the first frame update
    /// Hanldes the Layout of the Scene depending on the slected game
    /// </summary>
    void Start()
    {
        int game = PlayerPrefs.GetInt("SelectedGame");
        difficultyMinesweeper.gameObject.SetActive(false);
        // Swith handles diffrent games
        switch (game)
        {
            case 6:
                singlePlayer("Tetris - Coming Soon");
                break;
            case 4:
                singlePlayer("Breakout");
                break;
            case 3:
                singlePlayer("Snake");
                break;
            case 1:
                singlePlayer("Minesweeper");
                difficultyMinesweeper.gameObject.SetActive(true);
                difficultyMinesweeper.value = PlayerPrefs.GetInt("Difficulty");
                break;
            case 2:
                multiPlayer("Pong");
                break;
            case 5:
                multiPlayer("Tic Tac Toe - Coming Soon");
                break;
        }
        //Set default names if no name is Present
        if(PlayerPrefs.GetString("Username") == "")
        {
            PlayerPrefs.SetString("Username", "Player1");
        }
        if (PlayerPrefs.GetString("Username2") == "")
        {
            PlayerPrefs.SetString("Username2", "Player2");
        }
    }

    /// <summary>
    /// Hides the Player 2 Name selection
    /// </summary>
    /// <param name="game">Name of the game to display</param>
    void singlePlayer(string game)
    {
        gameName.text = game;
        playerName1.text = PlayerPrefs.GetString("Username");
        player2Group.interactable = false;
        player2Group.alpha = 0;
    }

    /// <summary>
    /// Shows the Player 2 Name selection
    /// </summary>
    /// <param name="game">Name of the game</param>
    void multiPlayer(string game)
    {
        gameName.text = game;
        playerName1.text = PlayerPrefs.GetString("Username");
        playerName2.text = PlayerPrefs.GetString("Username2");
        player2Group.interactable = true;
        player2Group.alpha = 1;
    }

    /// <summary>
    /// Loads the Game selection scene
    /// </summary>
    public void gameSelection()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Loads the Game scene depending on the selected game
    /// THe difficulty is always set but only used in Minesweeper
    /// </summary>
    public void StartGame()
    {
        PlayerPrefs.SetInt("Difficulty", difficultyMinesweeper.value);
        SceneManager.LoadScene(PlayerPrefs.GetInt("SelectedGame")+2);
    }

    /// <summary>
    /// Exit the Application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
