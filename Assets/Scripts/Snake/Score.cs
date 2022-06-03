using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static int scoreAmount;
    private TextMeshProUGUI scoreText;

    /// <summary>
    /// Start is called before the first frame update
    /// Sets the score to 0
    /// </summary>
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreAmount = 0;
    }

    /// <summary>
    /// Update is called once per frame
    /// Prints the current score amount
    /// </summary>
    void Update()
    {
        scoreText.text = "Score: " + scoreAmount;
    }
}
