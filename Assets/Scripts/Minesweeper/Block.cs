using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enum of the diffrent states a Block can have
/// </summary>
public enum Type
{
	Block,
	Free,
	Flag,
	QuestionMark,
	PressedQuestionMark,
	Mine,
	ExplodingMine,
	CancelledMine,
	One,
	Two,
	Three,
	Four,
	Five,
	Six,
	Seven,
	Eight
}

public class Block : MonoBehaviour
{
	[SerializeField]
	private List<Sprite> blockSprites;

	public Type type = 0;

	public int i = 0;
	public int j = 0;

	public bool visible = false;
	public bool hasFlag = false;

	private SpriteRenderer sr;

	/// <summary>
	/// Gets the sprite renderer of this component
	/// </summary>
	void Awake()
    {
		sr = GetComponent<SpriteRenderer>();
    }
	/// <summary>
	/// Check for Mouse movent 
	/// is triggered if Mouse is moving ontop of this field
	/// </summary>
	void OnMouseOver()
    {
		if (!Table.gameOver)
        {
			if (Input.GetMouseButtonDown(0)) //Left Click
			{
				Reveal(); 
			}

			if (Input.GetMouseButtonDown(1)) // Right Click
			{
				PlaceFlag();
			}
		}
    }
	/// <summary>
	/// Revels if field is Empty, has adjacent Bombs or is a bomb
	/// </summary>
	public void Reveal()
    {
		if (!visible) // doe not trigger on previously reveled fields
        {
			if (!Table.gameOver) // So it just triggers once after stepping on mine
            {
				FindObjectOfType<AudioManager>().Play("RemoveFlag");
			}
			if (hasFlag)
			{
				Counter.numFlags++;
				hasFlag = false;
			}
			visible = true;
			sr.sprite = blockSprites[(int)type];
        }
		if (type == Type.Mine)
        {
			type = Type.ExplodingMine;
			FindObjectOfType<Table>().board.Lose();
        }
		else if (type == Type.Free)
        {
			FindObjectOfType<Table>().board.RevealEmpty(i, j);
        }
    }
	/// <summary>
	/// Toggles the field between no flag and flag
	/// </summary>
	public void PlaceFlag()
    {
		if (visible) return;
		if (!hasFlag && Counter.numFlags > 0)
        {
			Counter.numFlags--;
			sr.sprite = blockSprites[(int)Type.Flag];
			FindObjectOfType<AudioManager>().Play("PlaceFlag");
			hasFlag = !hasFlag;
        } 
		else if (hasFlag)
        {
			Counter.numFlags++;
			sr.sprite = blockSprites[(int)Type.Block];
			FindObjectOfType<AudioManager>().Play("RemoveFlag");
			hasFlag = !hasFlag;
        }
		
    }
}
