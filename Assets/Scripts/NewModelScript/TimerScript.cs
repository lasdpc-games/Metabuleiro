#region libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class TimerScript : MonoBehaviour {
    #region variables
    public Image timeBar;
    public UpdateUIScript updateUIScript;
    public QuizManager2 quizManager2Script;
    #endregion

    public IEnumerator Timer (int helper) {
        if ((QuizManager2.levelChosen != 4)&&(helper == 1)) {
            timeBar.fillAmount = 1;
        }
        float timerDelayToSubtract = 0;
        float subtractiveAmount = 0;
        switch (QuizManager2.levelChosen) {
            case 1:
                timerDelayToSubtract = 0.05f;
                subtractiveAmount = 0.01f;
                break;
            case 2:
                timerDelayToSubtract = 0.05f;
                subtractiveAmount = 0.005f;
                break;
            case 3:
                timerDelayToSubtract = 0.05f;
                subtractiveAmount = 0.0025f;
                break;
            case 4:
                timerDelayToSubtract = 0.005f;
                subtractiveAmount = 0.000125f;
                break;
        }
        while (timeBar.fillAmount > 0) {
            yield return new WaitForSeconds (timerDelayToSubtract);
            timeBar.fillAmount -= subtractiveAmount;
        }
        if (QuizManager2.levelChosen == 4) {
            updateUIScript.UpdateScreens (4);
        } else {
            updateUIScript.UpdateUI (18);
            quizManager2Script.combo = 0;
            updateUIScript.UpdateUI (2);
        }
    }
}