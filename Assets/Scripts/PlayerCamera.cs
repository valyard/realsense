using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
    public Vector3 Offset;
    public Transform Target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Target.position + Offset;
	}
}
