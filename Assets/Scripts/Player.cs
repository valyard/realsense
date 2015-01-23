using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    #region Variables
    public float JumpForce = 1;
    public float RunForce = 1;
    public float MaxSpeed = 1;
    #endregion

    #region Main Methods
    void Start () {
	
	}
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
	    }
        if (rigidbody.velocity.x < MaxSpeed)
        {
            rigidbody.AddForce(Vector3.right * RunForce, ForceMode.VelocityChange);
        }

    }
    #endregion

    #region Utility Methods
    #endregion
}
