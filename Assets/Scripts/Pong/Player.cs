using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rg;
    public bool left;
    public Ball ball;
    private bool startable = false;
    private bool pause = false;

    /// <summary>
    /// Hanlde the Input for Pausing the Game and 
    /// the Game selection as well as the Movemnt of 
    /// the lef tand right paddle
    /// </summary>
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !ball.paused) // Pause / Unpause game
        {
            if (pause)
            {
                Time.timeScale = 1;
                pause = false;
                ball.pause.text = "";
                ball.back.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pause = true;
                ball.pause.text = "Pause: Press F to restart!";
                ball.back.gameObject.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.F) && pause) // Unpause
        {
            Time.timeScale = 1;
            pause = false;
            ball.pause.text = "";
            ball.firstLaunch();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Slect first game mode
        {
            ball.winPoints = 5;
            startable = true;
            ball.instructions.text = "5 Points to Win! Press F to start!";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // slect second game mode
        {
            ball.winPoints = 10;
            startable = true;
            ball.instructions.text = "10 Points to Win! Press F to start!";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // select third game mode
        {
            ball.winPoints = 15;
            startable = true;
            ball.instructions.text = "15 Points to Win! Press F to start!";
        }
        if (startable) // Start game
        {
            if (Input.GetKeyDown(KeyCode.F) && ball.paused)
            {
                ball.firstLaunch();
            }
        }


        if (ball.win()) // Win game if true and make possible to start again
        {
            if (Input.GetKeyDown(KeyCode.F) && ball.paused)
            {
                ball.firstLaunch();
            }
        }


        //Movent for left / right paddle
        if (left)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rg.velocity = new Vector2(0f, speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rg.velocity = new Vector2(0f, -speed);
            }
            else
            {
                rg.velocity = new Vector2(0f, 0f);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rg.velocity = new Vector2(0f, speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rg.velocity = new Vector2(0f, -speed);
            }
            else
            {
                rg.velocity = new Vector2(0f, 0f);
            }
        }
    }
}