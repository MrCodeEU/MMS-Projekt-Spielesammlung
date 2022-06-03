using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class Snake : MonoBehaviour
{
    private Vector2 _direction;
    // per default the snake is going to move to the right

    private List<Transform> _segments;
    public Transform segmentForSnake;
    public TextMeshProUGUI playerName;

    /// <summary>
    /// Initializes the snake and the username
    /// </summary>
    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        playerName.text = PlayerPrefs.GetString("Username");
        //Set TImescale to 0.5 to reduce speede an make playable
        Time.timeScale = 0.5f;
    }
    /// <summary>
    /// Handles the movement of the snake 
    /// depedning on the input of the user
    /// </summary>
    private void Update()
    {
        //assigning the direction based on the given Input
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _direction != Vector2.down)
        {
            _direction = Vector2.up;

        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _direction != Vector2.up)
        {
            _direction = Vector2.down;

        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _direction != Vector2.right)
        {
            _direction = Vector2.left;

        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
    }
    /// <summary>
    /// is running based on a fixed time-rate
    /// Moves the Elements of the snake
    /// </summary>
    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        //change the position (rounds the numbers, so that the snake always runs in the same lines)
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    /// <summary>
    /// Adds an Element to the snake
    /// </summary>
    private void Grow()
    //everytime this method gets called it puts another square to the snake
    {
        Transform segment = Instantiate(segmentForSnake);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    /// <summary>
    /// Restarts the game by reseting all Elements to there default values
    /// and saves the score to the leaderboard
    /// </summary>
    private void RestartTheGame()
    {
        //delete all segments
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        //destroying all references to the segments
        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
        _direction = Vector2.zero;

        int score = Score.scoreAmount;
        //Insert new Score if not 0
        if (score != 0)
        {
            int game = PlayerPrefs.GetInt("SelectedGame");
            SortedList<int, string> scores = new SortedList<int, string>(new DuplicateKeyComperator<int>());
            // Get previouse scores
            for (int i = 1; i <= 5; i++)
            {
                string entry = PlayerPrefs.GetString(game + i.ToString());
                if (entry == "")
                {
                    scores.Add(-i, "");
                }
                else
                {
                    scores[int.Parse(entry.Split(' ')[2])] = entry.Split(' ')[1];
                }
            }
            //Insert new Score
            scores[score] = PlayerPrefs.GetString("Username");
            // Write new Score in correct Order Limited to 5 Elements
            for (int i = 4; i >= 0; i--)
            {
                if (scores.ElementAt(i + 1).Key < 0) break;
                PlayerPrefs.SetString(game + (5 - i).ToString(), (5 - i).ToString() + ". " + scores.ElementAt(i + 1).Value + " " + scores.ElementAt(i + 1).Key);
            }
        }
        Score.scoreAmount = 0;
    }

    /// <summary>
    /// Handles the collision 
    /// </summary>
    /// <param name="other">Element that was collided with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //whenever it collides with an object, it checks, if it is the Food, if so it calls the Method grow, in order to get the snake bigger
        if (other.tag == "Food")
        {
            Grow();
            Score.scoreAmount += 1;

            //if it collides with an obstacle, it resets it to the beginning of the game   
        }
        else if (other.tag == "Obstacle")
        {
            RestartTheGame();
        }
        else if (other.tag == "Poisen")
        {
            RestartTheGame();
        }
        else if (other.tag == "Dopple")
        {
            Grow();
            Grow();
            Score.scoreAmount += 1;
            Score.scoreAmount += 1;
        }
    }

    /// <summary>
    /// Loads the Game Preview scene
    /// </summary>
    public void Back()
    {
        //Reset Timescale
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// This Comperator makes it possible to have suplicate Keys (same Scores) in a soreted Dictionary
    /// </summary>
    /// <typeparam name="TKey">Type of element to compare</typeparam>
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
