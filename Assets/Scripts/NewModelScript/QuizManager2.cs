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
    public QuestionCSV[] questions;
    [HideInInspector]
    public List<QuestionCSV> unansweredQuestions, questionsToAnswerAgain = new List<QuestionCSV> ();
    [HideInInspector]
    public QuestionCSV currentQuestion;
    [HideInInspector]
    public QuestionCSVWithImages[] questionsWithImages;
    [HideInInspector]
    public QuestionCSVWithImages currentQuestionWithImage;
    [HideInInspector]
    public List<QuestionCSVWithImages> unansweredQuestionsWithImages, questionsWithImagesToAnswerAgain = new List<QuestionCSVWithImages> ();
    [HideInInspector]
    public static int levelChosen;
    [HideInInspector]
    public int genericScore, combo, answeredCount;
    [HideInInspector]
    public int easyScore, mediumScore, hardScore, championshipScore;
    [HideInInspector]
    public bool secondChance;
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
            questionsWithImages = getQuestionsFromCSVScript.GetQuestionsWithImages (1);
            verifyAchivements = GetComponent<VerifyAchivements> ();
            FillAnswers ();
            updateUIScript.UpdateUI (7);
            GetRandomQuestion ();
            timerScript.StartCoroutine ("Timer", 1);
    }
    void FillAnswers () {
        unansweredQuestions = questions.ToList<QuestionCSV> ();
        unansweredQuestionsWithImages = questionsWithImages.ToList<QuestionCSVWithImages> ();
    }
    public void GetRandomQuestion () {
        if (secondChance == false) {
            int questionIndex = Random.Range (0, unansweredQuestions.Count);
            currentQuestion = unansweredQuestions[questionIndex];
        } else {
            int questionIndex = Random.Range (0, questionsToAnswerAgain.Count);
            currentQuestion = questionsToAnswerAgain[questionIndex];
            
        }
        updateUIScript.UpdateUI (3);
    }

    public delegate void Answered(int v);
    public static event Answered OnAnswer;

    public void AnswerButtonSelected (int answerSelected) {
        unansweredQuestions.Remove (currentQuestion);
        timerScript.StopCoroutine ("Timer");
        bool correct = false;
        if (answerSelected == currentQuestion.correctAnswerValue){
            correct = true;
            OnAnswer(1);
            //FindObjectOfType<PieceMovement>().MovePiece();
        }else{
            OnAnswer(0);
        }
        updateUIScript.ShowAfterAnswerScreen(correct);
    }
    public void ReloadQuestion () {
        ConvertScore ();
        verifyAchivements.VerifyAchivement ();
        if (levelChosen == 4) {
            if ((unansweredQuestions.Count + unansweredQuestionsWithImages.Count) == 0) {
                Finish (1);
            }
        }
        if ((secondChance == false) && (levelChosen != 4)) {
            answeredCount++;
        }
        if ((answeredCount == 10) && (secondChance == false)) {
            Finish (1);
        } else if ((secondChance == true) && (questionsToAnswerAgain.Count + questionsWithImagesToAnswerAgain.Count == 0)) {
            Finish (2);
        } else if ((unansweredQuestions.Count + unansweredQuestionsWithImages.Count != 0)) {
            updateUIScript.UpdateUI (7);
            if (secondChance == false) {
                timerScript.StartCoroutine ("Timer", 1);
                GetRandomQuestion ();
            } else {
                GetRandomQuestion ();
            }
            updateUIScript.UpdateScreens (1);
        }
    }
    void Finish (int whatFinal) {
        if (whatFinal == 1) {
            timerScript.StopCoroutine ("Timer");
            if ((genericScore == 40) || ((levelChosen == 4) && ((unansweredQuestionsWithImages.Count + unansweredQuestions.Count) == 0))) {
                updateUIScript.UpdateUI (12);
                updateUIScript.UpdateUI (13);
            } else {
                updateUIScript.UpdateUI (14);
            }
        } else if (whatFinal == 2) {
            updateUIScript.UpdateUI (12);
            updateUIScript.UpdateUI (15);
        }
        updateUIScript.UpdateScreens (5);
    }
    public void Retry () {
        updateUIScript.UpdateScreens (5);
        secondChance = true;
        updateUIScript.UpdateUI (5);
        updateUIScript.UpdateUI (6);
        ReloadQuestion ();
    }
    public void BackToMenu (int operation) {
        switch (operation) {
            case 1:
                updateUIScript.UpdateUI (17);
                break;
            case 2:
                SceneManager.LoadScene ("Menu");
                break;
            case 3:
                verifyAchivements.ConfirmAchivements ();
                SceneManager.LoadScene ("Menu");
                break;
        }
    }
    void ConvertScore () {
        switch (levelChosen) {
            case 1:
                easyScore = genericScore;
                break;
            case 2:
                mediumScore = genericScore;
                break;
            case 3:
                hardScore = genericScore;
                break;
            case 4:
                championshipScore = genericScore;
                break;
        }
    }
}