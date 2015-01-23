using UnityEngine;
using System.Collections;

public class BackgroundAnimation : MonoBehaviour {
    public float Speed = 0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position - Time.deltaTime * Speed * Vector3.right;
	}
}
