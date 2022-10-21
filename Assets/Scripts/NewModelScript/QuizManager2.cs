using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class QuizManager2 : MonoBehaviour {
    
    #region variables
    [HideInInspector]
    public PieceMovement pieceMovementScript;
    public bool hasWon;
    public QuestionCSV[] questions;
    [HideInInspector]
    public List<QuestionCSV> unansweredQuestions, questionsToAnswerAgain = new List<QuestionCSV> ();
    [HideInInspector]
    public QuestionCSV currentQuestion;
    [HideInInspector]
    public static int levelChosen;
    [HideInInspector]
    public int genericScore, answeredCount;
    VerifyAchivements verifyAchivements;
    TimerScript timerScript;
    UpdateUIScript updateUIScript;
    GetQuestionsFromCSV getQuestionsFromCSVScript;
    #endregion
    void Start () {
        updateUIScript = GetComponent<UpdateUIScript> ();
        timerScript = GetComponent<TimerScript> ();
        getQuestionsFromCSVScript = GetComponent<GetQuestionsFromCSV> ();
        questions = getQuestionsFromCSVScript.GetQuestions (1);
        FillAnswers();
        updateUIScript.UpdateUI (7);
        GetRandomQuestion ();
        timerScript.StartCoroutine ("Timer", 1);
    }
    void FillAnswers () {
        unansweredQuestions = questions.ToList<QuestionCSV> ();
    }
    public void GetRandomQuestion () {
        int questionIndex = Random.Range (0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[questionIndex];
        updateUIScript.UpdateUI (3);
    }

    public delegate void Answered(int v);
    public static event Answered OnAnswer;

    public void AnswerButtonSelected (int answerSelected) {
        timerScript.StopCoroutine ("Timer");
        bool correct = false;
        if (answerSelected == currentQuestion.correctAnswerValue){
            correct = true;
            unansweredQuestions.Remove (currentQuestion);
            OnAnswer(1);
        }else{
            OnAnswer(0);
        }
        updateUIScript.ShowAfterAnswerScreen(correct);
    }

    public void ReloadQuestion () {
        if(!hasWon){
            if ((unansweredQuestions.Count == 0)){
                //PegaMaisPerguntas
            }else{
                updateUIScript.UpdateUI (7);
                timerScript.StartCoroutine ("Timer", 1);
                GetRandomQuestion ();
            }
        }
    }
    public void BackToMenu (int operation) {
        switch (operation) {
            case 1:
                updateUIScript.UpdateUI (17);
                break;
            case 2:
                SceneManager.LoadScene ("Menu");
                AudioManager.GetInstance().Play("MenuTheme");
                break;
        }
    }
}