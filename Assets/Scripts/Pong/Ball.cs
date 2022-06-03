using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rg;
    public float speed;
    private int lPunkte, rPunkte;
    public TextMeshProUGUI textR, textL, textWin, instructions, name1, name2, pause;
    public Button back;
    public int winPoints;
    private float[] randomValues = new float[] { -0.6f, -0.5f, -0.4f, 0.4f, 0.5f, 0.6f };
    public bool paused = true;
    private string playerName1;
    private string playerName2;
    /// <summary>
    /// Displays the usernames and prints the instruction text
    /// </summary>
    public void Start()
    {
        textWin.text = "";
        playerName1 = PlayerPrefs.GetString("Username");
        playerName2 = PlayerPrefs.GetString("Username2");
        name1.text = playerName1;
        name2.text = playerName2;
        instructions.text = playerName1 + " uses W and S, " + playerName2 + " uses Arrows to navigate! \n  Please enter a number [1,2,3] to decide the score to win: \n 1 5 Points\n 2 10 Points\n 3 15 Points\n Then press F to Start the Game!";
    }
    /// <summary>
    /// Initialises the Text Elements 
    /// </summary>
    public void firstLaunch()
    {
        lPunkte = 0;
        rPunkte = 0;
        textR.text = rPunkte.ToString();
        textL.text = lPunkte.ToString();
        Launch();
    }
    /// <summary>
    /// Sets the game state to playing (not paused) and starts the movement of he ball
    /// </summary>
    void Launch()
    {
        back.gameObject.SetActive(false);
        paused = false;
        textWin.text = "";
        transform.position = Vector2.zero;
        int randomX = Random.Range(0, 5);
        int randomY = Random.Range(0, 5);
        //Random.Range(-0.6f, 0.6f);
        rg.velocity = new Vector2(randomValues[randomX] * speed, randomValues[randomY] * speed);
    }

    /// <summary>
    /// Handles the collision with the left and the right wall
    /// and the updating of the score plus reseting the ball
    /// </summary>
    /// <param name="collision">The element that was collided</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        instructions.text = "";

        if (collision.gameObject.tag == "Links")
        {
            if (lPunkte != winPoints)
            {
                rPunkte++;
                textR.text = rPunkte.ToString();
                Launch();
            }


        }
        if (collision.gameObject.tag == "Rechts")
        {
            if (rPunkte != winPoints)
            {
                lPunkte++;
                textL.text = lPunkte.ToString();
                Launch();
            }

        }
    }
    /// <summary>
    /// Handles the printing of the win message depending on
    /// who got the required amount of points to win
    /// </summary>
    /// <returns>true if wither player reched the required amount of points</returns>
    public bool win()
    {
        if (rPunkte == winPoints)
        {
            textWin.text = "Congratulations, " + playerName2 + "!, Press F to play again!";
            transform.position = Vector2.zero;
            rg.velocity = new Vector2(0, 0);
            back.gameObject.SetActive(true);
            AddToLastGame();
            paused = true;
            return true;
        }

        if (lPunkte == winPoints)
        {
            textWin.text = "Congratulations, " + playerName1 + "!, Press F to play again!";
            transform.position = Vector2.zero;
            rg.velocity = new Vector2(0, 0);
            back.gameObject.SetActive(true);
            AddToLastGame();
            paused = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Save the last 5 games to display on the Leaderboard 
    /// </summary>
    private void AddToLastGame()
    {
        if (!paused)
        {
            // Add current Game to Leaderboard/Last Games Board
            int game = PlayerPrefs.GetInt("SelectedGame");
            string[] games = new string[5];
            // Get previouse games
            for (int i = 1; i < 5; i++)
            {
                string entry = PlayerPrefs.GetString(game + i.ToString());
                if (entry == "")
                {
                    games[i] = "";
                }
                else
                {
                    games[i] = entry.Substring(3);
                }
            }
            //Insert current Game as first Element
            games[0] = playerName1 + " " + lPunkte + " : " + rPunkte + " " + playerName2;
            //Write Games in new Order (current one at first then the last ones in Order)
            for (int i = 0; i < 5; i++)
            {
                if (games[i] == "") break;
                PlayerPrefs.SetString(game + (i + 1).ToString(), (i + 1).ToString() + ". " + games[i]);
            }
        }
    }
    /// <summary>
    /// Loads the Game Preview scene
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(2);
    }
}
