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
        Paused,
        GameOver
    };
    #endregion

    #region Variables
    public PlayerTracker PlayerTracker;

    public World World;
    public Player Player;
    public float LeftBorder = -14;
    public float BottomBorder = -10;

    public GameObject MainMenu;
    public GameObject GameOver;

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

            switch (_state)
            {
                case MainState.Idle:
                    MainMenu.SetActive(true);
                    GameOver.SetActive(false);
                    break;
                case MainState.Paused:
                    MainMenu.SetActive(true);
                    GameOver.SetActive(false);
                    break;
                case MainState.Playing:
                    MainMenu.SetActive(false);
                    GameOver.SetActive(false);
                    break;
                case MainState.GameOver:
                    MainMenu.SetActive(false);
                    GameOver.SetActive(true);
                    break;
            }
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
        checkLosingConditions();
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
            Time.timeScale = 0;

            World.Generate();

            //Debug.Log("Device: " + PXCMSenseManager.CreateInstance().QueryCaptureManager().QueryDevice());

           // PXCMSenseManager.CreateInstance().QueryCaptureManager().QueryDevice().SetColorAutoExposure(false);
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

        Time.timeScale = 1;
        if (State == MainState.Idle || State == MainState.Paused)
        {
            State = MainState.Playing;
        }
    }

    void pause()
    {
        Debug.Log("Game should pause");

        Time.timeScale = 0;
        if (State == MainState.Playing)
        {
            State = MainState.Paused;
        }
    }

    void blink()
    {
        if (State == MainState.Playing)
        {
            Debug.Log("Game should update level");
            World.Generate(Player.transform.position.x);
        }
    }

    void gameOver()
    {
        if (State == MainState.Playing)
        {
            State = MainState.GameOver;
        }
    }

    void checkLosingConditions()
    {
        if (Player.transform.position.y < BottomBorder || Player.transform.position.x < LeftBorder)
        {
            gameOver();
        }
    }
    #endregion
}
