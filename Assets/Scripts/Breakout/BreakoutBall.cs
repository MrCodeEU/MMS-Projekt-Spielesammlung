using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutBall : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public bool inPlay;
    public Transform paddle;
    public Transform explosion;
    public GameManager gm;
    public Transform powerUp;
    new AudioSource audio;

    /// <summary>
    /// Start is called before the first frame update
    /// Gets required components
    /// </summary>
    void Start()
    {
        // get rigidBordy Element of ball and audio source
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update is called once per frame
    /// Handles diffrent states of the game
    /// </summary>
    void Update()
    {
        if (gm.gameOver) // if Game is Over don't do anything in update loop
        {
            return;
        }
        if (!inPlay) // if not playing 
        {
            // Set ball to be ontop of paddle
            transform.position = paddle.position + new Vector3(0, 0.5f, 0);
        }
        // if Spacebar (Jump) is pressed and game is not palying 
        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            // start playing
            inPlay = true; 
            Time.timeScale = 1;
            rb.AddForce(Vector2.up * speed * 20); // push ball upwards
        }
    }

    /// <summary>
    /// Handles collision with the Bottom Hit box
    /// </summary>
    /// <param name="other">Element that was collided</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //Collision with ELement Bottom
        if (other.CompareTag("Bottom"))
        {
            // Remove 1 live and stop playing
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }

    /// <summary>
    /// Handles collision with everything except Bottom hit box
    /// </summary>
    /// <param name="other">Element that was collided</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        // Collision with a Brick
        if (other.transform.CompareTag("Brick"))
        {
            Brick brick = other.gameObject.GetComponent<Brick>(); // get collided Brick
            if (brick.lives > 1)
            {
                // Call break Brick function if more than 1 live
                brick.BreakBrick();
            }
            else // Brick has now 1 live but was hit => break the brick
            {
                // Greate Powerup by random 
                int randomChance = Random.Range(1, 101);
                if (randomChance < 5)
                {
                    Instantiate(powerUp, other.transform.position, other.transform.rotation);
                }
                // Create an Exploisoneffect
                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 1.95f);

                //Update the Score and the number of bricks left on the board
                gm.UpdateScore(other.gameObject.GetComponent<Brick>().points);
                gm.UpdateNumberOfBricks();
                // Destroy the brick Object
                Destroy(other.gameObject);
            }
            audio.Play(); // Play breka sound effect
        }
        else // Collision with non brick element
        {
            paddle.GetComponent<AudioSource>().Play(); // Play sound effect for bounce
        }
    }
}
