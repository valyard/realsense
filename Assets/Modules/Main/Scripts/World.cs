using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class World : MonoBehaviour
{
    #region Variables
    public List<BlockPrefab> Prefabs;

    public float Speed = 1;
    
    public int Width = 22;
    public float BlockSize = 1.0f;

    public float HeightMin = -1;
    public float HeightMax = 3;

    private Transform[] blocks;
    #endregion

    #region Main Methods
    #endregion

	void Start () 
    {
        blocks = new Transform[Width];
        Generate();
	}
	
	void FixedUpdate ()
	{
	    for (var i = 0; i < Width; i++)
	    {
	        var block = blocks[i];
	        if (block == null) continue;
            block.rigidbody.MovePosition(block.position + Vector3.left * Speed * Time.deltaTime);
	    }

	    if (blocks[0] == null) return;
	    if (blocks[0].localPosition.x < -BlockSize)
	    {
            Destroy(blocks[0].gameObject);
	        for (var i = 1; i < Width; i++) blocks[i - 1] = blocks[i];
	        var xPos = (Width - 1) * BlockSize;
	        if (blocks[Width - 2] != null) xPos = blocks[Width - 2].localPosition.x + BlockSize;
            AddBlock(Width - 1, xPos);
	    }
    }

    public void Generate(float fixedPosition = -100)
    {
        for (int i = 0; i < Width; i++)
        {
            var block = blocks[i];
            var xPos = i * BlockSize;
            if (block != null)
            {
                if (block.position.x > fixedPosition - BlockSize*2 && block.position.x < fixedPosition + BlockSize*2) continue;
                xPos = block.localPosition.x;
                Destroy(block.gameObject);
            }
            AddBlock(i, xPos);
        }
    }

    private void AddBlock(int index, float xPos)
    {
        var go = Instantiate(Prefabs[Random.Range(0, Prefabs.Count)].Prefab) as GameObject;
        var t = go.transform;
        t.name = "Block";
        t.parent = transform;
        t.localPosition = new Vector3(xPos, Random.Range(HeightMin, HeightMax), 0);
        blocks[index] = t;
    }

    #region Utility Methods
    #endregion
}

[Serializable]
public struct BlockPrefab
{
    public GameObject Prefab;
    public float Probability;
}
