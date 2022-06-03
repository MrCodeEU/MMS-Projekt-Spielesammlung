using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Difficulty difficulty;

    [SerializeField]
    private Block block;

    private int w;
    private int h;

    private Block[,] blocks;

    /// <summary>
    /// Initializes the field 
    /// Places all Bombs depending on the difficulty
    /// </summary>
    /// <param name="size">With and Height of the field</param>
    /// <param name="difficulty">THe selected difficulty</param>
    public void Init(int size, Difficulty difficulty)
    {
        w = size;
        h = size;
        blocks = new Block[w, h];

        SetDifficulty(difficulty);

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                var blockType = GetBlockType();
                blocks[i, j] = Instantiate(block, new Vector2(i, j), Quaternion.identity);
                blocks[i, j].type = blockType;
                blocks[i, j].i = i;
                blocks[i, j].j = j;
            }
        }
        UpdateNearbyBlock();
        //HintAtStart();
    }
    /// <summary>
    /// Show a field that is not bomb
    /// </summary>
    public void HintAtStart()
    {
        foreach (Block block in blocks)
        {
            if (block.type == Type.Free)
            {
                block.PlaceFlag();
                break;
            }
        }
    }
    /// <summary>
    /// Depending on the amount of adjancent Bombs 
    /// the field gets a diffrent Type
    /// </summary>
    void UpdateNearbyBlock()
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                int n = 0;

                if (blocks[i, j].type == Type.Mine)
                {
                    continue;
                }

                var list = GetAllNearbyBlocks(i, j);
                // Check how many adjancent fields are bombs 
                foreach(var block in list)
                {
                    if (block.type == Type.Mine)
                    {
                        n++;
                    }
                }
                if (n > 0)
                {
                    blocks[i, j].type = (Type)(n + 7);
                }
            }
        }
    }
    /// <summary>
    /// Randomly returns a Empty field or a Mine depending the difficulty
    /// </summary>
    /// <returns>Type of the field</returns>
    Type GetBlockType()
    {
        if (Random.Range(1, 1000)%(int)difficulty == 0)
        {
            return Type.Mine;
        }
        else
        {
            return Type.Free;
        }
    }
    /// <summary>
    /// Gets a Block coordinats i and j 
    /// </summary>
    /// <param name="i">x coordinate</param>
    /// <param name="j">y coordinate</param>
    /// <returns>The Block and i and j</returns>
    Block GetBlock(int i, int j)
    {
        if (i < 0 || j < 0 || i >= w || j >= h)
        {
            return null;
        }
        return blocks[i, j];
    }
    /// <summary>
    /// Gets all adjancent Blocks
    /// </summary>
    /// <param name="i">x coordinate</param>
    /// <param name="j">y cooridnate</param>
    /// <param name="includeCorner">Check the corner if true</param>
    /// <returns> list of all adjancent fields</returns>
    List<Block> GetAllNearbyBlocks(int i, int j, bool includeCorner=true)
    {
        List<Block> list = new List<Block>();
        list.Add(GetBlock(i + 1, j));
        list.Add(GetBlock(i, j + 1));
        list.Add(GetBlock(i - 1, j));
        list.Add(GetBlock(i, j - 1));

        if (includeCorner)
        {
            list.Add(GetBlock(i + 1, j + 1));
            list.Add(GetBlock(i + 1, j - 1));
            list.Add(GetBlock(i - 1, j + 1));
            list.Add(GetBlock(i - 1, j - 1));
        }
        return list
            .Where(x => x != null)
            .ToList();
    }
    /// <summary>
    /// Revelas all Fields
    /// </summary>
    void RevealAll()
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                blocks[i, j].Reveal();
            }
        }
    }
    /// <summary>
    /// Reveals all Empty field that are a complete area
    /// </summary>
    /// <param name="i">x coordinate for where to start searching</param>
    /// <param name="j">y coordinate from where to start searching</param>
    public void RevealEmpty(int i, int j)
    {
        List<Block> list = GetAllNearbyBlocks(i, j, true); // False if corners should not be included
        list = list.Where(x => !x.visible).ToList();
        
        foreach(var block in list)
        {
            block.Reveal();
            if (block.type == Type.Free)
            {
                RevealEmpty(block.i, block.j);
            }
        }
    }

    /// <summary>
    /// Sets game state to game over and 
    /// Play game over sound plus explosion sound
    /// and reval all Fields
    /// </summary>
    public void Lose()
    {
        Table.gameOver = true;
        FindObjectOfType<AudioManager>().Play("Explosion");
        FindObjectOfType<AudioManager>().Play("GameOver");
        RevealAll();
    }
    /// <summary>
    /// gets the amount of fields that are not visible yet
    /// </summary>
    /// <returns>number of fields</returns>
    public int GetBlockedFields()
    {
        int n = 0;
        foreach (Block block in blocks)
        {
            if (!block.visible)
            {
                n++;
            }
        }
        return n;
    }
    /// <summary>
    /// Gets the number of Mines on the fields
    /// </summary>
    /// <returns>number of mines</returns>
    public int GetNumMines()
    {
        int n = 0;
        foreach(Block block in blocks)
        {
            if (block.type == Type.Mine)
            {
                n++;
            }
        }
        return n;
    }
    /// <summary>
    /// sets the selected difficulty
    /// </summary>
    /// <param name="d">ENmú difficulty</param>
    public void SetDifficulty(Difficulty d)
    {
        difficulty = d;
    }
}
