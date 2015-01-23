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

    private List<Transform> blocks;
    #endregion

    #region Main Methods
    #endregion

	void Start () 
    {
        blocks = new List<Transform>();
        Generate(null);
	}
	
	void FixedUpdate () 
    {
	    for (var i = 0; i < blocks.Count; i++)
	    {
	        var block = blocks[i];
            block.rigidbody.MovePosition(block.position - Vector3.left * Speed * Time.deltaTime);
	    }
    }

    public void Generate(Transform fixedBlock)
    {
        foreach (var b in blocks)
        {
            if (b != fixedBlock)
            {
                Destroy(b.gameObject);
            }
        }
        blocks.Clear();
        if (fixedBlock != null)
        {
            blocks.Add(fixedBlock);
        }
        for (int i = 0; i < Width; i++)
        {
            var go = Instantiate(Prefabs[Random.Range(0, Prefabs.Count)].Prefab) as GameObject;
            var t = go.transform;
            t.name = "Block_" + i;
            t.parent = transform;
            t.localPosition = new Vector3(i * BlockSize, Random.Range(HeightMin, HeightMax), 0);
            blocks.Add(t);
        }
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
