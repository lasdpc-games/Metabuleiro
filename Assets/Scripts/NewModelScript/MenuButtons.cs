
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject levelChoicePanel, mainManuPanel;

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

    public void Exit(){
        Debug.Log("Exit");
        Application.Quit();
    }

    public void LevelChoice(int levelSelected)
    {
        QuizManager2.levelChosen = levelSelected;
        SceneManager.LoadScene("Quiz");
    }
}

