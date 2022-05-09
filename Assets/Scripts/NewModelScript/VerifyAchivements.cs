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
    public void VerifyAchivement () {
        switch (QuizManager2.levelChosen) {
            case 1:
                if (quizManager2.easyScore >= 10 && (achivementConquered[0] == false)) {
                    if (achivementHelper[0] == false) {
                        achivementHelper[0] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                if (quizManager2.easyScore >= 22 && (achivementConquered[1] == false)) {
                    if (achivementHelper[1] == false) {
                        achivementHelper[1] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                if (quizManager2.easyScore >= 40 && (achivementConquered[2] == false)) {
                    if (achivementHelper[2] == false) {
                        achivementHelper[2] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                break;
            case 2:
                if (quizManager2.mediumScore >= 10 && (achivementConquered[3] == false)) {
                    if (achivementHelper[3] == false) {
                        achivementHelper[3] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                if (quizManager2.mediumScore >= 22 && (achivementConquered[4] == false)) {
                    if (achivementHelper[4] == false) {
                        achivementHelper[4] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                if (quizManager2.mediumScore >= 40 && (achivementConquered[5] == false)) {
                    if (achivementHelper[5] == false) {
                        achivementHelper[5] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                break;
            case 3:
                if (quizManager2.hardScore >= 10 && (achivementConquered[6] == false)) {
                    if (achivementHelper[6] == false) {
                        achivementHelper[6] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                if (quizManager2.hardScore >= 22 && (achivementConquered[7] == false)) {
                    if (achivementHelper[7] == false) {
                        achivementHelper[7] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                if (quizManager2.hardScore >= 40 && (achivementConquered[8] == false)) {
                    if (achivementHelper[8] == false) {
                        achivementHelper[8] = true;
                        StartCoroutine ("ShowAchivementIndicator");
                    }
                }
                break;
        }
    }
    IEnumerator ShowAchivementIndicator () {
        newAchivementIndicator.SetActive (true);
        yield return new WaitForSeconds (2f);
        newAchivementIndicator.SetActive (false);
    }
    public void ConfirmAchivements () {
        for (int i = 0; i < achivementHelper.Length; i++) {
            achivementConquered[i] = achivementHelper[i];
        }
        SaveSystem.SavePlayer (this);
    }
}