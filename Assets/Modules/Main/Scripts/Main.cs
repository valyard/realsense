using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    #region Type definitions
    public enum MainState
    {
        Init,
        Idle,
        Playing,
        Paused
    };
    #endregion



    #region Variables
    public PlayerTracker PlayerTracker;

    MainState _state;
    public MainState State
    {
        get
        {
            return _state;
        }
        private set
        {
            Debug.Log(string.Format("Transition from {0} to {1}", _state, value));
            _state = value; 
        }
    }
    #endregion

    #region Main Methods
    // Use this for initialization
	void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion

    #region Events
    void init()
    {
        if (State == MainState.Init)
        {
            State = MainState.Idle;
            PlayerTracker.OnTrackingStarted += onTrackingStarted;
            PlayerTracker.OnTrackingLost += onTrackingLost;
            PlayerTracker.OnBlink += onBlink;
        }
    }

    void onTrackingStarted(PlayerTracker pt)
    {
        start();
    }
    void onTrackingLost(PlayerTracker pt)
    {
        pause();
    }
    void onBlink(PlayerTracker pt)
    {
        blink();
    }
    #endregion

    #region Private Methods

    void start()
    {
        Debug.Log("Game should start");
    }

    void pause()
    {
        Debug.Log("Game should pause");
    }

    void blink()
    {
        Debug.Log("Game should update level");
    }
    #endregion
}
