using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    #region State def
    public enum PlayerState {
        OnGround,
        Jumping
    }

    private PlayerState _state;
    public PlayerState State
    {
        get { return _state; }
        private set {
            Debug.Log(string.Format("PlayerState transition from {0} to {1}", _state, value));
            _state = value;
        }
    }
    #endregion

    #region Variables
    public float JumpForce = 1;
    #endregion

    #region Main Methods
    void Start () {
	    
	}
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            jump();
	    }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(string.Format("{0} {1}", contact.point, contact.normal));
            if (Mathf.Approximately(contact.normal.y, 1.0f))
            {
                contactGround();
            }
        }
    }
    #endregion

    #region Utility Methods
    void jump()
    {
        if (State == PlayerState.OnGround)
        {
            rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
            State = PlayerState.Jumping;
        }
    }

    void contactGround()
    {
        if (State == PlayerState.Jumping)
        {
            State = PlayerState.OnGround;
        }
    }
    #endregion
}
