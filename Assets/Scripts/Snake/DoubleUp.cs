using UnityEngine;

public class DoubleUp : MonoBehaviour
{
    public BoxCollider2D gridAllowedArea;
    /// <summary>
    /// Invoke a function call to randomise the position
    /// of the double up every 15 seconds
    /// </summary>
    private void Start()
    {
        InvokeRepeating("RandomePosition", 0.0f, 15.0f);
    }

    /// <summary>
    /// generate and set a rendom position with in th ebounds
    /// </summary>
    private void RandomePosition()
    {
        Bounds bounds = this.gridAllowedArea.bounds;

        //generate a random point within the grid
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    /// <summary>
    /// Handles the collision with the Player (Snake)
    /// </summary>
    /// <param name="other">Element that was collided (snake)</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //whenever it collides with an object, it checks, if it is the Player (Snake), with the tag the Player got
        if (other.tag == "Player")
        {
            this.transform.position = new Vector3(3000.0f, 3000.0f, 0.0f);
        }
    }
}