using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> numSprites;

    private SpriteRenderer sr;

    public int number;
    /// <summary>
    /// Gets the sprite render component
    /// </summary>
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// Draws the sprite of the current number value
    /// </summary>
    public void Draw()
    {
        sr.sprite = numSprites[number];
    }
}
