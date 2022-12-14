using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    private static GameState gameState = GameState.OnInit;
    public static GameState GameState => gameState;
    public List<Material> materials;

    // Start is called before the first frame update
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        gameState = GameState.OnInit;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            StartWave();
        }
        else
        {
            UIManager.Ins.OpenUI<MianMenu>();
        }
    }

    public void StartWave()
    {
        if (gameState == GameState.End) return;
        UIManager.Ins.OpenUI<Bonus>();
        Wave.instance.OnInit();
    }

    public void Win()
    {
        UIManager.Ins.OpenUI<Win>();
        gameState = GameState.End;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        StopTime();
        UIManager.Ins.OpenUI<Setting>();
    }

    public void Continue()
    {
        RunTime();
    }

    public void Lose()
    {
        UIManager.Ins.OpenUI<Lose>();
        StopTime();
        gameState = GameState.End;
    }


    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state)
    {
        return gameState == state;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void RunTime()
    {
        Time.timeScale = 1;
    }
}
