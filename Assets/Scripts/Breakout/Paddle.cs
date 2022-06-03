using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public float rightBorder;
    public float leftBorder;
    public float paddleSize;
    public GameManager gm;

    /// <summary>
    /// Update is called once per frame
    /// Handles diffrent states of the game
    /// </summary>
    void Update() 
    {
        if (gm.gameOver) // DO nothing if game is over
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");  // gets me "a" & "d" left right movement behavior ( number -1 for left, +1 for right)
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed); // Move paddle by physics in directio of horizontal to get fluid movement
       
        // Stop Paddle for going of screen
        if ( transform.position.x < leftBorder) 
        {
            transform.position = new Vector2(leftBorder, transform.position.y);
        }
        if (transform.position.x > rightBorder)
        {
            transform.position = new Vector2(rightBorder, transform.position.y);
        }
    }

    /// <summary>
    /// Checks for collision with Powerup
    /// </summary>
    /// <param name="other">Element that was collided</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Extralive powerup updates the Lives and destroys the PowerUp game object
        if (other.CompareTag("ExtraLife"))
        {
            gm.UpdateLives(1);
            Destroy(other.gameObject);
        }
    }
}
