using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float JumpForce = 1;

	void Start () {
	
	}
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
	    }
	}
}
