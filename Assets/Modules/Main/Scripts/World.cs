using UnityEngine;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    #region Variables
    public Transform[] BlockPrefabs;

    public int BlocksCount = 1000;
    public float BlockSpacing = 1.0f;

    public float HeightMin = -1;
    public float HeightMax = 3;

    private List<Transform> blocks;
    #endregion

    #region Main Methods
    #endregion
    // Use this for initialization
	void Start () {
        blocks = new List<Transform>();
        Generate(null);
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Generate(Transform fixedBlock)
    {
        foreach (var b in blocks)
        {
            if (b != fixedBlock)
            {
                GameObject.Destroy(b.gameObject);
            }
        }
        blocks.Clear();
        if (fixedBlock != null)
        {
            blocks.Add(fixedBlock);
        }
        for (int i = 0; i < BlocksCount; i++)
        {
            Transform newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]) as Transform;
            newBlock.name = "Block_" + i;
            newBlock.parent = transform;
            newBlock.position = new Vector3(i * BlockSpacing, Random.Range(HeightMin, HeightMax), 0);
            blocks.Add(newBlock);
        }
    }

    #region Utility Methods
    #endregion
}
