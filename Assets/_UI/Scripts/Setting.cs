using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        GameManager.Ins.Continue();
        Close();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }
}
