using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("Questions")]
    public Question[] easyQuestions;
    public Question[] mediumQuestions;
    public Question[] hardQuestions;
    public List<Question> unansweredQuestions, questionsToAnswerAgain = new List<Question>();
    Question currentQuestion;

    [Header("Questions with Image")]
    public QuestionWithImage[] questionsWithImages;
    QuestionWithImage currentQuestionWithImage;
    public List<QuestionWithImage> unansweredQuestionsWithImages, questionWithImagesToAnswerAgain = new List<QuestionWithImage>();

    [Header("UI For questions")]
    public GameObject layout1;
    public Text[] answerTextArray;
    private List<Text> answerTextList;

    [Header("UI For questions with Images")]
    public GameObject layout2;
    public Image[] answerImageArray;
    private List<Image> answerImageList;

    [Header("Generic UI")]
    public Image timeBar;
    public Text questionHolder, scoreText, correctionText, comboIndicator, finishText;
    public GameObject correctPanel, wrongPanel, timeOverPanel, finishPanel;
    public Image backGround;
    public GameObject retryButton;


    public static int difficultyChosen;
    public int layoutChosen;
    int correctAnsweredValue, genericScore, combo, answeredCount;
    bool secondChance;

    void Start()
    {
        correctAnsweredValue = 2;
        switch (difficultyChosen)
        {
            case 1:
                unansweredQuestions = easyQuestions.ToList<Question>();
                unansweredQuestionsWithImages = questionsWithImages.ToList<QuestionWithImage>();
                break;
            case 2:
                unansweredQuestions = mediumQuestions.ToList<Question>();
                break;
            case 3:
                unansweredQuestions = hardQuestions.ToList<Question>();
                break;
        }
        layoutChosen = Random.Range(1, 3);
        if (layoutChosen == 1)
        {
            layout1.SetActive(true);
        }
        else
        {
            layout2.SetActive(true);
        }
        GetRandomQuestion();
        StartCoroutine("Timer");
    }

    public void GetRandomQuestion()
    {
        if (layoutChosen == 1)
        {
            if (secondChance == false)
            {
                int questionIndex = Random.Range(0, unansweredQuestions.Count);
                currentQuestion = unansweredQuestions[questionIndex];
            }
            else
            {

                int questionIndex = Random.Range(0, questionsToAnswerAgain.Count);
                currentQuestion = questionsToAnswerAgain[questionIndex];

            }
        }
        else
        {
            if (secondChance == false)
            {
                int questionIndex = Random.Range(0, unansweredQuestionsWithImages.Count);
                currentQuestionWithImage = unansweredQuestionsWithImages[questionIndex];
            }
            else
            {

                int questionIndex = Random.Range(0, questionWithImagesToAnswerAgain.Count);
                currentQuestion = questionsToAnswerAgain[questionIndex];

            }
        }
        UpdateUI(3);
    }

    public void UpdateUI(int operation)
    {
        if (operation == 1)
        {
            scoreText.text = "PONTOS: " + genericScore;
        }
        else if (operation == 2)
        {
            if (combo > 6)
            {
                comboIndicator.text = "3X";
            }
            else if (combo > 4)
            {
                comboIndicator.text = "2X";
            }
            else
            {
                comboIndicator.text = "1X";
            }
        }
        else if (operation == 3)
        {
            if (layoutChosen == 1)
            {
                questionHolder.text = currentQuestion.questionName;
                answerTextList = answerTextArray.ToList<Text>();
                currentQuestion.correctAnswerValue = Random.Range(0, 3);
                answerTextList[currentQuestion.correctAnswerValue].text = currentQuestion.correctAnswer;
                answerTextList.RemoveAt(currentQuestion.correctAnswerValue);
                for (int i = 0; i <= 2; i++)
                {
                    answerTextList[i].text = currentQuestion.wrongAnswers[i];
                }
            }
            else
            {
                questionHolder.text = currentQuestionWithImage.questionName;
                answerImageList = answerImageArray.ToList<Image>();
                currentQuestionWithImage.correctAnswerValue = Random.Range(0, 3);
                answerImageList[currentQuestionWithImage.correctAnswerValue].sprite = currentQuestionWithImage.correctAnswer;
                answerImageList.RemoveAt(currentQuestionWithImage.correctAnswerValue);
                for (int i = 0; i <= 2; i++)
                {
                    answerImageList[i].sprite = currentQuestionWithImage.wrongAnswers[i];
                }
            }
        }
        else if (operation == 4)
        {
            if (correctPanel.activeInHierarchy == true)
            {
                correctPanel.SetActive(false);
            }
            else if (wrongPanel.activeInHierarchy == true)
            {
                wrongPanel.SetActive(false);
            }
            else if (timeOverPanel.activeInHierarchy == true)
            {
                timeOverPanel.SetActive(false);
            }
        }
        else if (operation == 5)
        {
            comboIndicator.gameObject.SetActive(false);
            timeBar.gameObject.SetActive(false);
        }
    }

    public void AnswerButtonSelected(int answerSelected)
    {
        StopCoroutine("Timer");
        if (layoutChosen == 1)
        {
            if (answerSelected == currentQuestion.correctAnswerValue)
            {
                if (secondChance == false)
                {
                    combo++;
                    if (combo > 6)
                    {
                        genericScore += correctAnsweredValue * 3;
                    }
                    else if (combo > 4)
                    {
                        genericScore += correctAnsweredValue * 2;
                    }
                    else
                    {
                        genericScore += correctAnsweredValue;
                    }
                    UpdateUI(2);
                }
                else
                {
                    genericScore += correctAnsweredValue;
                }
                correctPanel.SetActive(true);
                UpdateUI(1);
            }
            else
            {
                if (secondChance == false)
                {
                    questionsToAnswerAgain.Add(currentQuestion);
                    combo = 0;
                    UpdateUI(2);
                }
                correctionText.text = currentQuestion.correction;
                wrongPanel.SetActive(true);
            }
        }
        else
        {
            if (answerSelected == currentQuestionWithImage.correctAnswerValue)
            {
                if (secondChance == false)
                {
                    combo++;
                    if (combo > 6)
                    {
                        genericScore += correctAnsweredValue * 3;
                    }
                    else if (combo > 4)
                    {
                        genericScore += correctAnsweredValue * 2;
                    }
                    else
                    {
                        genericScore += correctAnsweredValue;
                    }
                    UpdateUI(2);
                }
                else
                {
                    genericScore += correctAnsweredValue;
                }
                correctPanel.SetActive(true);
                UpdateUI(1);
            }
            else
            {
                if (secondChance == false)
                {
                    questionWithImagesToAnswerAgain.Add(currentQuestionWithImage);
                    combo = 0;
                    UpdateUI(2);
                }
                correctionText.text = currentQuestionWithImage.correction;
                wrongPanel.SetActive(true);
            }
        }
    }

    public void ReloadQuestion()
    {
        answeredCount++;
        if ((secondChance == true) && ((questionsToAnswerAgain.Count == 0)|| (questionWithImagesToAnswerAgain.Count == 0)))
        {
            if (questionsToAnswerAgain.Count == 0)
            {
                layoutChosen = 2;
            }
            else if (questionWithImagesToAnswerAgain.Count == 0)
            {
                layoutChosen = 1;
            }
        }
        else
        {
            layoutChosen = Random.Range(1, 3);
        }
        if (layoutChosen == 1)
        {
            layout1.SetActive(true);
            layout2.SetActive(false);
        }
        else
        {
            layout2.SetActive(true);
            layout1.SetActive(false);
        }
        if ((answeredCount == 10) && (secondChance == false))
        {
            Finish();
        }
        else
        {
            if (secondChance == false)
            {
                if (layoutChosen == 1)
                {
                    unansweredQuestions.Remove(currentQuestion);
                }
                else
                {
                    unansweredQuestionsWithImages.Remove(currentQuestionWithImage);
                }
                timeBar.fillAmount = 1;
                StartCoroutine("Timer");
                GetRandomQuestion();
            }
            else
            {
                if (layoutChosen == 1)
                {
                    questionsToAnswerAgain.Remove(currentQuestion);
                }
                else
                {
                    questionWithImagesToAnswerAgain.Remove(currentQuestionWithImage);
                }
                if (questionsToAnswerAgain.Count + questionWithImagesToAnswerAgain.Count == 0)
                {
                    finishPanel.SetActive(true);
                    retryButton.SetActive(false);
                    finishText.text = "Sua pontuação foi para " + genericScore + "! Você consegue fazer melhor da próxima vez!!";
                }
                else
                {
                    GetRandomQuestion();
                }
            }
        }
        UpdateUI(4);
    }

    void Finish()
    {
        finishPanel.SetActive(true);
        StopCoroutine("Timer");
        if (genericScore == 40)
        {
            retryButton.SetActive(false);
            finishText.text = " Parabéns!Você Acertou todas as perguntas!";
        }
        else
        {
            finishText.text = "Você conseguiu " + genericScore + " pontos! Mas você pode tentar as perguntas que errou com metade do valor!";
        }
    }

    IEnumerator Timer()
    {
        while (timeBar.fillAmount > 0)
        {
            yield return new WaitForSeconds(0.125f);
            timeBar.fillAmount -= 0.006125f;
        }
        timeOverPanel.SetActive(true);
    }

    public void Retry()
    {
        correctAnsweredValue = 1;
        finishPanel.SetActive(false);
        backGround.color = new Color32(255, 0, 0, 255);
        secondChance = true;
        UpdateUI(5);
        ReloadQuestion();
    }
    public void BackToMenu()
    {
        //switch (levelChoosed)
        //{
        //    case 1:
        //        easyScore = genericScore;
        //        break;
        //    case 2:
        //        mediumScore = genericScore;
        //        break;
        //    case 3:
        //        hardScore = genericScore;
        //        break;
        //}
        SceneManager.LoadScene("Menu");
    }
}
