using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpace : MonoBehaviour
{
    public InputField inputField;
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //Listen for Input on Inputfield and call RemoveSpaces
        inputField.onValueChanged.AddListener(delegate { RemoveSpaces(); });
    }

    /// <summary>
    /// Replace all WHitespaces with empty strings
    /// </summary>
    void RemoveSpaces()
    {
        inputField.text = inputField.text.Replace(" ", "");
    }
}
