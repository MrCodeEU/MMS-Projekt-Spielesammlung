using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public Text entry1;
    public Text entry2;
    public Text entry3;
    public Text entry4;
    public Text entry5;
    public Dropdown difficultyMinesweeper;
    /// <summary>
    /// Start is called before the first frame update
    /// Prints th Leaderboard
    /// </summary>
    void Start()
    {
        PrintLeaderboard();
    }
    /// <summary>
    /// Prints the Leaderboard of the selected game
    /// and with the selected difficulty
    /// </summary>
    public void PrintLeaderboard()
    {
        int game = PlayerPrefs.GetInt("SelectedGame");
        if (game == 1) // Game is Minesweeper => dependent on the difficulty
        {
            entry1.text = PlayerPrefs.GetString(game + "1" + difficultyMinesweeper.value);
            entry2.text = PlayerPrefs.GetString(game + "2" + difficultyMinesweeper.value);
            entry3.text = PlayerPrefs.GetString(game + "3" + difficultyMinesweeper.value);
            entry4.text = PlayerPrefs.GetString(game + "4" + difficultyMinesweeper.value);
            entry5.text = PlayerPrefs.GetString(game + "5" + difficultyMinesweeper.value);
        }
        else
        {
            entry1.text = PlayerPrefs.GetString(game + "1");
            entry2.text = PlayerPrefs.GetString(game + "2");
            entry3.text = PlayerPrefs.GetString(game + "3");
            entry4.text = PlayerPrefs.GetString(game + "4");
            entry5.text = PlayerPrefs.GetString(game + "5");
        }
    }
}
