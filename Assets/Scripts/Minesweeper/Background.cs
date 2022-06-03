using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Sprite backgroundSprite;
    private SpriteRenderer sr;
    /// <summary>
    /// Gets the sprite renderer of the this component
    /// </summary>
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
}
