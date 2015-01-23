using UnityEngine;
using System.Collections;

public class PlayerTracker : MonoBehaviour
{
    public delegate void PlayerTrackerAction(PlayerTracker pt);
    public event PlayerTrackerAction OnTrackingStarted;
    public event PlayerTrackerAction OnTrackingLost;
    public event PlayerTrackerAction OnBlink;


    bool isTrackingFace = false;
    float _lastTrackingUpdateTime = 0;

    #region Variables
    #endregion

    #region Main Methods
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - _lastTrackingUpdateTime > 1)
        {
            stopTracking();
        }
    }
    #endregion

    #region Realsense Callbacks
    void OnFaceDetected()
    {
   //     startTracking();
    }

    void OnFaceLost()
    {
    //   stopTracking();
    }

    void OnEyeClosed()
    {
        Debug.Log("Blink");
        if (isTrackingFace)
        {
            blink();
        }
    }

    void OnEyeDetect()
    {
        if (!isTrackingFace)
        {
            startTracking();
        }
    }
    #endregion

    #region Private Methods
    void startTracking()
    {
        isTrackingFace = true;
        _lastTrackingUpdateTime = Time.time;
        Debug.Log("Face detected");
        if (OnTrackingStarted != null)
        {
            OnTrackingStarted(this);
        }
    }

    void stopTracking()
    {
        isTrackingFace = false;
        Debug.Log("Face lost");
        if (OnTrackingLost != null)
        {
            OnTrackingLost(this);
        }
    }

    void blink()
    {
        if (OnBlink != null)
        {
            OnBlink(this);
        }
    }
    #endregion
}
