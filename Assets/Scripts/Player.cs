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

    public float CenterAttractorForce = 0.1f;

    public float JumpingDelay = 0.5f;
    private float jumpingStartTime;
    #endregion

    #region Main Methods
    void Start () {
	    
	}
	
	void FixedUpdate () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            jump();
	    }


        rigidbody.AddForce(-Vector3.right * rigidbody.velocity.x, ForceMode.VelocityChange);
        /*
        if (Mathf.Abs(transform.position.x) > 1)
        {
            rigidbody.AddForce(-Vector3.right * CenterAttractorForce * Mathf.Sign(transform.position.x), ForceMode.VelocityChange);
        }
        */
       // rigidbody.velocity = new Vector3(0.5f * rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
    }

    void OnCollisionStay(Collision collision)
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
            Debug.Log("Jump: " + (Time.time - (jumpingStartTime + JumpingDelay)));
            rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
            State = PlayerState.Jumping;
            jumpingStartTime = Time.time;
        }
    }

    void contactGround()
    {
        if (State == PlayerState.Jumping && Time.time > jumpingStartTime + JumpingDelay)
        {
            State = PlayerState.OnGround;
        }
    }
    #endregion
}
