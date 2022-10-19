
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject helpPanel, levelChoicePanel, medalRoomPanel,mainManuPanel;

    public void Exit()
    {
        Application.Quit();
    }
    
    public void Play()
    {
        SceneManager.LoadScene("Quiz");
    }
}

