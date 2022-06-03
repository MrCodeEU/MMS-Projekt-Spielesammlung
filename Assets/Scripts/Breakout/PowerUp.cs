using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Speed of the powerup falling down
    public float speed;

    /// <summary>
    /// Update is called once per frame
    /// Handles movement of Powerup
    /// </summary>
    void Update()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed); // Move Powerup down
        if (transform.position.y < -6f)
        {
            Destroy(gameObject); // Destroy game Object if of Screen
        }

    }
}
