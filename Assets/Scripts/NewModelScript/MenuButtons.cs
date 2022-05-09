
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject helpPanel, levelChoicePanel, medalRoomPanel,mainManuPanel;

    public void OpenLevelChoicePanel()
    {
        if (levelChoicePanel.activeInHierarchy == false)
        {
            mainManuPanel.SetActive(false);
            levelChoicePanel.SetActive(true);
        }
        else
        {
            mainManuPanel.SetActive(true);
            levelChoicePanel.SetActive(false);
        } 
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Help()
    {
        if (helpPanel.activeInHierarchy == false)
        {
            mainManuPanel.SetActive(false);
            helpPanel.gameObject.SetActive(true);
        }
        else
        {
            mainManuPanel.SetActive(true);
            helpPanel.gameObject.SetActive(false);
        }
    }
    public void OpenMedalRoom()
    {
        if (medalRoomPanel.activeInHierarchy == false)
        {
            mainManuPanel.SetActive(false);
            medalRoomPanel.gameObject.SetActive(true);
        }
        else
        {
            mainManuPanel.SetActive(true);
            medalRoomPanel.gameObject.SetActive(false);
        }
    }

    public void LevelChoice(int levelSelected)
    {
        QuizManager2.levelChosen = levelSelected;
        SceneManager.LoadScene("Quiz");
    }
}

