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
    UpdateUIScript updateUIScript;
    GetQuestionsFromCSV getQuestionsFromCSVScript;

    public int sizeQuestion;
    #endregion

    void Start () {
        updateUIScript = GetComponent<UpdateUIScript> ();
        getQuestionsFromCSVScript = GetComponent<GetQuestionsFromCSV> ();
        questions = getQuestionsFromCSVScript.GetQuestions();
        FillAnswers();
        GetRandomQuestion();
    }

    void FillAnswers () {
        unansweredQuestions = questions.ToList<QuestionCSV> ();
    }

    public void GetRandomQuestion () {
        int questionIndex = Random.Range (0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[questionIndex];
        updateUIScript.UpdateUI (3);
        
        sizeQuestion = CountQuestion(currentQuestion.questionName);
    }

    int CountQuestion(string question){
        int count = 0;
        foreach (char c in question){
            if( c == ' '){
                count++;
            }   
        }
        return count;
    }

    public delegate void Answered(int v);
    public static event Answered OnAnswer;

    public void AnswerButtonSelected (int answerSelected) {
        int correct = 0;
        if (answerSelected == currentQuestion.correctAnswerValue){
            correct = 1;
            unansweredQuestions.Remove (currentQuestion);
            OnAnswer(1);
        }else{
            OnAnswer(0);//MUDAR!
        }
        updateUIScript.ShowAfterAnswerScreen(correct);
    }

    public void ReloadQuestion () {
        if(!hasWon){
            if ((unansweredQuestions.Count == 0)){
                FillAnswers();
            }else{
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
                OnAnswer = null;
                SceneManager.LoadScene ("Menu");
                AudioManager.GetInstance().Play("MenuTheme");
                break;
        }
    }
}