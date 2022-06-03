using UnityEngine;

public class Poisen : MonoBehaviour
{
    public BoxCollider2D gridAllowedArea;
    /// <summary>
    /// Invoke a function call to randomise the position
    /// of the poison every ^5 seconds
    /// </summary>
    private void Start()
    {
        InvokeRepeating("RandomePosition", 0.0f, 5.0f);
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
}
