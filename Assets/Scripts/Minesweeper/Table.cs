using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

/// <summary>
/// Enum for possible difficulty values
/// </summary>
public enum Difficulty
{
    Hard = 4,
    Normal = 8,
    Easy = 16
}

public class Table : MonoBehaviour
{
    [SerializeField]
    private const int SIZE = 16;
    [SerializeField]
    private Difficulty difficulty;
    [SerializeField]
    public Board board;
    [SerializeField]
    private Background background;
    [SerializeField]
    private bool hintAtStart;
    [SerializeField]
    public Counter counter;

    public static bool gameOver;

    private DateTime startTime;

    /// <summary>
    /// Start is called before the first frame update
    /// Initialises all Elements and sets the Difficulty
    /// </summary>
    void Start()
    {
        gameOver = false;
        int dif = PlayerPrefs.GetInt("Difficulty");
        switch (dif)
        {
            case 0:
                difficulty = Difficulty.Easy;
                break;
            case 2:
                difficulty = Difficulty.Hard;
                break;
            default:
                difficulty = Difficulty.Normal;
                break;
        }
        background = Instantiate(background, new Vector2(8, 8.5f), Quaternion.identity);
        board.Init(SIZE, difficulty);
        startTime = DateTime.Now;
        Counter.numFlags = board.GetNumMines();
        counter.Init();
        board.HintAtStart();
    }

    /// <summary>
    /// Handles all game states and time based elements
    /// </summary>
    void Update()
    {
        if (!gameOver)
        {
            if (board.GetNumMines() == board.GetBlockedFields())
            {
                int time = counter.GetTime();
                gameOver = true;
                FindObjectOfType<AudioManager>().Play("Winning");
                //Add to Leaderboard if time is low enought
                int game = PlayerPrefs.GetInt("SelectedGame");
                int dif = PlayerPrefs.GetInt("Difficulty");
                SortedDictionary<int, string> score = new SortedDictionary<int, string>(new DuplicateKeyComperator<int>());
                // Get previouse scores
                for (int i = 1; i <= 5; i++)
                {
                    string entry = PlayerPrefs.GetString(game + i.ToString() + dif);
                    if(entry == "")
                    {
                        score.Add(5000+i, "");
                    } else
                    {
                        score[int.Parse(entry.Split(' ')[2])] = entry.Split(' ')[1];
                    }
                }
                //Insert new Score
                score[time] = PlayerPrefs.GetString("Username");
                //Write Scores Limited to 5 Elements
                for (int i = 0; i < 5; i++)
                {
                    if (score.ElementAt(i).Key >= 5000) break;
                    PlayerPrefs.SetString(game + (i+1).ToString() + dif, (i + 1).ToString() + ". " + score.ElementAt(i).Value + " " + score.ElementAt(i).Key);
                }
            }
            counter.SetTime((int)DateTime.Now.Subtract(startTime).TotalSeconds);
            counter.Draw();
        }
    }
    /// <summary>
    /// This Comperator makes it possible to have suplicate Keys (same Scores) in a soreted Dictionary
    /// </summary>
    /// <typeparam name="TKey">Type of the key</typeparam>
    private class DuplicateKeyComperator<TKey> : IComparer<TKey> where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            int result = x.CompareTo(y);

            if (result == 0)
                return 1; // Handle equality as being greater. Note: this will break Remove(key) or
            else          // IndexOfKey(key) since the comparer never returns 0 to signal key equality
                return result;
        }
    }
}
