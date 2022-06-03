using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUsername : MonoBehaviour
{
    public static string username;
    public static string username2;
    public Text showUsername;
    public Text showUsername2;
    public InputField getUsername;
    public InputField getUsername2;

    /// <summary>
    /// Sets the username to the value of the inputfield and displays the name
    /// </summary>
    /// <param name="player">1 for Player 1 and 2 for Player 2</param>
    public void SetAndDispalyUsername(int player)
    {
        if(player == 1)
        {
            username = getUsername.text;
            if(username != "")
            {
                showUsername.text = username;
                PlayerPrefs.SetString("Username", username);
            }
        } else
        {
            username2 = getUsername2.text;
            if(username2 != "")
            {
                showUsername2.text = username2;
                PlayerPrefs.SetString("Username2", username2);
            }
        }
    }
}
