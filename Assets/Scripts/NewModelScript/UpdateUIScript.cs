using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIScript : MonoBehaviour {
    public QuizManager2 quizManager2Script;
    public TimerScript timerScript;
    [Header ("Generic UI")]
    public Text questionHolder, scoreText, correctionText, comboIndicator, finishText;
    public Image correctionImage;
    public GameObject correctPanel, wrongPanel, timeOverPanel, challengeFailedPanel, finishPanel, ExitPanel, retryButton;
    [Header ("UI For questions with Images")]
    public GameObject layout2;
    public Sprite[] allSpritesForAnswers;
    public Image[] answerImageArray;
    private List<Image> answerImageList;
    [Header ("UI For questions")]
    public GameObject layout1;
    public Text[] answerTextArray;
    private List<Text> answerTextList;
    public int layoutChosen;
    public void UpdateScreens (int operation) {
        if (operation == 1) {
            //Set all screens that stop the game to false
            if (correctPanel.activeInHierarchy == true) {
                correctPanel.SetActive (false);
            } else if (wrongPanel.activeInHierarchy == true) {
                wrongPanel.SetActive (false);
            } else if (timeOverPanel.activeInHierarchy == true) {
                timeOverPanel.SetActive (false);
            }
        } else if (operation == 2) {
            //Set the screen that shows the users answered correctly to true
            correctPanel.SetActive (true);
        } else if (operation == 3) {
            //Set the screen that shows the users answered incorrectly to true
            wrongPanel.SetActive (true);
        } else if (operation == 4) {
            //Set the screen that shows the user failed in challenge to true
            challengeFailedPanel.SetActive (true);
        } else if (operation == 5) {
            if (finishPanel.activeInHierarchy == false) {
                finishPanel.SetActive (true);
            } else {
                finishPanel.SetActive (false);
            }
        }
    }
    public void UpdateUI (int operation) {
        if (operation == 1) {
            //Updates the score display
            scoreText.text = "PONTOS: " + quizManager2Script.genericScore;
        } else if (operation == 2) {
            //Updates the "combo" display
            if (quizManager2Script.combo > 6) {
                comboIndicator.text = "3X";
            } else if (quizManager2Script.combo > 4) {
                comboIndicator.text = "2X";
            } else {
                comboIndicator.text = "1X";
            }
        } else if (operation == 3) {
            //Updates the answers display (images and texts for each layout chosen)
            if (layoutChosen == 1) {
                questionHolder.text = quizManager2Script.currentQuestion.questionName;
                answerTextList = answerTextArray.ToList<Text> ();
                quizManager2Script.currentQuestion.correctAnswerValue = Random.Range (0, 3);
                answerTextList[quizManager2Script.currentQuestion.correctAnswerValue].text = quizManager2Script.currentQuestion.correctAnswer;
                answerTextList.RemoveAt (quizManager2Script.currentQuestion.correctAnswerValue);
                answerTextList[0].text = quizManager2Script.currentQuestion.wrongAnswer1;
                answerTextList[1].text = quizManager2Script.currentQuestion.wrongAnswer2;
                answerTextList[2].text = quizManager2Script.currentQuestion.wrongAnswer3;
            } else if (layoutChosen == 2) {
                questionHolder.text = quizManager2Script.currentQuestionWithImage.questionName;
                answerImageList = answerImageArray.ToList<Image> ();
                quizManager2Script.currentQuestionWithImage.correctAnswerValue = Random.Range (0, 3);
                answerImageList[quizManager2Script.currentQuestionWithImage.correctAnswerValue].sprite = allSpritesForAnswers[quizManager2Script.currentQuestionWithImage.correctImageID];
                answerImageList.RemoveAt (quizManager2Script.currentQuestionWithImage.correctAnswerValue);
                List<int> randomIndex = new List<int> ();
                randomIndex.Add (quizManager2Script.currentQuestionWithImage.correctImageID);
                while (randomIndex.Count < 4) {
                    int indexSorted = Random.Range (0, allSpritesForAnswers.Length);
                    if (!randomIndex.Contains (indexSorted)) {
                        randomIndex.Add (indexSorted);
                    }
                }
                for (int i = 0; i < randomIndex.Count; i++) { }
                for (int i = 1; i < answerImageList.Count + 1; i++) {
                    answerImageList[i - 1].sprite = allSpritesForAnswers[randomIndex[i]];
                }
            }
        } else if (operation == 5) {
            //Set the timer display to false
            GameObject.Find ("TimerHolder").gameObject.SetActive (false);
        } else
        if (operation == 6) {
            //Set the combo display to false
            comboIndicator.gameObject.SetActive (false);
        } else if (operation == 7) {
            //Update the layout chosen on screen (with images or with texts)
            ChooseLayout ();
            switch (layoutChosen) {
                case 1:
                    layout1.SetActive (true);
                    layout2.SetActive (false);
                    break;
                case 2:
                    layout1.SetActive (false);
                    layout2.SetActive (true);
                    break;
            }
        } else if (operation == 10) {
            //Set the correction text on screen to the correction string that correspond to the current question
            if (layoutChosen == 1) {
                correctionText.gameObject.SetActive (true);
                correctionImage.gameObject.SetActive (false);
                correctionText.text = quizManager2Script.currentQuestion.correction;
            } else {
                correctionText.gameObject.SetActive (false);
                correctionImage.gameObject.SetActive (true);
                correctionImage.sprite = allSpritesForAnswers[quizManager2Script.currentQuestionWithImage.correctionImageID];
            }
        } else if (operation == 12) {
            //Set the "second chance button" to false
            retryButton.SetActive (false);
        } else if (operation == 13) {
            finishText.text = " Parabéns!Você Acertou todas as perguntas!";
        } else if (operation == 14) {
            finishText.text = "Você conseguiu " + quizManager2Script.genericScore + " pontos! Mas você pode tentar as perguntas que errou com metade do valor!";
        } else if (operation == 15) {
            finishText.text = "Sua pontuação foi para " + quizManager2Script.genericScore + "! Você consegue fazer melhor da próxima vez!!";
        } else if (operation == 17) {
            if (ExitPanel.activeInHierarchy == false) {
                ExitPanel.SetActive (true);
                if (quizManager2Script.secondChance == false) {
                    timerScript.StopCoroutine ("Timer");
                }
            } else {
                ExitPanel.gameObject.SetActive (false);
                if (quizManager2Script.secondChance == false) {
                    timerScript.StartCoroutine ("Timer",0);
                }
            }
        } else if (operation == 18) {
            timeOverPanel.SetActive (true);
        }
    }
    void ChooseLayout () {
        if ((quizManager2Script.secondChance == true)) {
            if (quizManager2Script.questionsToAnswerAgain.Count == 0) {
                layoutChosen = 2;
            } else if (quizManager2Script.questionsWithImagesToAnswerAgain.Count == 0) {
                layoutChosen = 1;
            } else {
                layoutChosen = Random.Range (1, 3);
            }
        } else {
            if (quizManager2Script.unansweredQuestions.Count == 0) {
                layoutChosen = 2;
            } else if (quizManager2Script.unansweredQuestionsWithImages.Count == 0) {
                layoutChosen = 1;
            } else {
                layoutChosen = Random.Range (1, 3);
            }
        }
    }
}