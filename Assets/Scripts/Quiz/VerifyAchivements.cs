#region libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class VerifyAchivements : MonoBehaviour {

    #region variables
    QuizManager2 quizManager2;
    public GameObject newAchivementIndicator;
    [HideInInspector]
    public bool[] achivementConquered = new bool[9];
    bool[] achivementHelper = new bool[9];
    #endregion
    void Start () {
        quizManager2 = GetComponent<QuizManager2> ();
        for (int i = 0; i < achivementHelper.Length; i++) {
            achivementHelper[i] = achivementConquered[i];
        }
    }
    public void ConfirmAchivements () {
        for (int i = 0; i < achivementHelper.Length; i++) {
            achivementConquered[i] = achivementHelper[i];
        }
        SaveSystem.SavePlayer (this);
    }
}