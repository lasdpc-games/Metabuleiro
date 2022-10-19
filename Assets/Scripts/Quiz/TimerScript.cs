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
        if (helper == 1) {
            timeBar.fillAmount = 1;
        }
        
        float timerDelayToSubtract = (float)quizManager2Script.sizeQuestion/20;
        Debug.Log(timerDelayToSubtract);
        float subtractiveAmount = 0.001f;
        while (timeBar.fillAmount > 0) {
            yield return new WaitForSeconds (timerDelayToSubtract);
            timeBar.fillAmount -= subtractiveAmount;
        }
        updateUIScript.ShowAfterAnswerScreen(2);
    }
}