using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;
    public int lives;
    public Sprite hitSprite;

    /// <summary>
    /// Reduce lives of brick by one and give it the cracked sprite
    /// </summary>
    public void BreakBrick()
    {
        lives--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }
}
