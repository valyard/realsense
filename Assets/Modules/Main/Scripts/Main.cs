using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public Text PointsText;

    private MainState _state;
    private int points = 0;

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
                    Time.timeScale = 0;
                    points = 0;
                    break;
                case MainState.Paused:
                    MainMenu.SetActive(true);
                    GameOver.SetActive(false);
                    Time.timeScale = 0;
                    break;
                case MainState.Playing:
                    MainMenu.SetActive(false);
                    GameOver.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case MainState.GameOver:
                    MainMenu.SetActive(false);
                    GameOver.SetActive(true);
                    Time.timeScale = 0;
                    break;
            }
        }
    }
    #endregion

    #region Main Methods

    public void Restart()
    {
        Application.LoadLevel(0);
    }

	void Start () 
    {
        init();
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        checkLosingConditions();

	    points = (int)Time.time;
	    PointsText.text = "Points: " + points;
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

        if (State == MainState.Idle || State == MainState.Paused)
        {
            State = MainState.Playing;
        }
    }

    void pause()
    {
        Debug.Log("Game should pause");

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
