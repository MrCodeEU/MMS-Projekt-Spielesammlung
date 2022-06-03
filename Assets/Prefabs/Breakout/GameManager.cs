using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    //public float paddleSize;
    //public float ballSize;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerName;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadNextLevelPanel;
    public GameObject pausePanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    public BreakoutBall ball;


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "LIVES: " + CreatePreZeroString(lives, 3) + lives;
        scoreText.text = "SCORE: " + CreatePreZeroString(score, 7) + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        playerName.text = PlayerPrefs.GetString("Username");
        //Load first level
        ball.inPlay = false;
        ball.rb.velocity = Vector2.zero;
        int rand = UnityEngine.Random.Range(0, levels.Length);
        Instantiate(levels[0], Vector2.zero, Quaternion.identity);
        currentLevelIndex++;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        loadNextLevelPanel.SetActive(false);
        Time.timeScale = 1;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
        }
    }

    public void UpdateLives(int livesAdded)
    {
        lives += livesAdded;
        // Check for no lives left and end of game trigger!
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        if (livesAdded > 0)
        {
            UpdateScore(50);
        }
        livesText.text = "LIVES: " + CreatePreZeroString(lives, 3) + lives;
    }

    public void UpdateScore(int scoreAdded)
    {
        score += scoreAdded;
        scoreText.text = "SCORE: " + CreatePreZeroString(score, 6) + score;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            Time.timeScale = 0;
            if (currentLevelIndex >= levels.Length - 1)
            {
                gameOverPanel.GetComponentInChildren<TextMeshProUGUI>().text = "All Levels Completed";
                gameOverPanel.GetComponentInChildren<TextMeshProUGUI>().fontSize = 80;
                GameOver();
            }
            else
            {
                loadNextLevelPanel.SetActive(true);
                loadNextLevelPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Loading Level " + (currentLevelIndex + 1);
                gameOver = true;  // freezes Paddle and Ball
            }
        }
    }

    public void LoadNewLevel()
    {
        ball.inPlay = false;
        ball.rb.velocity = Vector2.zero;
        int rand = UnityEngine.Random.Range(1, levels.Length);
        Instantiate(levels[rand], Vector2.zero, Quaternion.identity);
        currentLevelIndex++;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        loadNextLevelPanel.SetActive(false);
        Time.timeScale = 1;
    }

    string CreatePreZeroString(int num, int displayLength)
    {
        string result = "";
        for (int i = 0; i < displayLength; i++)
        {
            if (num == 0)
            {
                result += "0";
            }
            else
            {
                num = num / 10;
            }
        }
        return result;
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SaveScore();
        SceneManager.LoadScene("BreakOut1");
    }

    public void Quit()
    {
        SaveScore();
        Application.Quit();
    }
    public void Back()
    {
        SaveScore();
        SceneManager.LoadScene(2);
    }

    public void SaveScore()
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

    // This Comperator makes it possible to have suplicate Keys (same Scores) in a soreted Dictionary
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
