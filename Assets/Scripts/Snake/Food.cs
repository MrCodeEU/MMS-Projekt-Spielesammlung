using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridAllowedArea;
    /// <summary>
    /// Invoke a function call to randomise the position
    /// of the food every 15 seconds
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
    /// gets called automatically whenever game-opjects collide with each other 
    /// gives a reference to the object it collidet with
    /// </summary>
    /// <param name="other">Element that was collided</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //whenever it collides with an object, it checks, if it is the Player (Snake), with the tag the Player got
        if (other.tag == "Player")
        {
            RandomePosition();
        }
    }

}
