using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [HideInInspector]
    public static int numFlags;
    [SerializeField]
    private Number number;
    [SerializeField]
    private int time;

    private const int SIZE = 3;
    private Number[] numbers;
    private Number[] timeNums;
    /// <summary>
    /// Initialises the Counters
    /// </summary>
    public void Init()
    {
        time = 0;
        numbers = new Number[SIZE];
        timeNums = new Number[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            numbers[i] = Instantiate(number, new Vector3(-1f * i + 3, 18f, -1), Quaternion.identity);
            timeNums[i] = Instantiate(number, new Vector3(-1f * i + 14, 18f, -1), Quaternion.identity);
            numbers[i].number = (int)(numFlags / Math.Pow(10, i) % 10);
            timeNums[i].number = (int)(time / Math.Pow(10, i) % 10);
        }
    }
    /// <summary>
    /// Draws the Counter
    /// </summary>
    public void Draw()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].number = (int)(numFlags/Math.Pow(10, i) % 10);
            numbers[i].Draw();
            timeNums[i].number = (int)(time / Math.Pow(10, i) % 10);
            timeNums[i].Draw();
        }
    }
    /// <summary>
    /// Sets the TIme of the Counter to the specified time t
    /// </summary>
    /// <param name="t">Time</param>
    public void SetTime(int t)
    {
        time = t;
    }
    
    /// <summary>
    /// Gets the current Time of the timer
    /// </summary>
    /// <returns>current Timer time</returns>
    public int GetTime()
    {
        return time;
    }
}
