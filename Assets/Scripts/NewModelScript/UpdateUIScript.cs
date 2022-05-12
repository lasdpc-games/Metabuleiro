using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIScript : MonoBehaviour {
    public QuizManager2 quizManager2Script;
    public TimerScript timerScript;
    [Header ("Generic UI")]
    public Text questionHolder, scoreText, correctionText, finishText;
    public Image correctionImage;
    public GameObject afterAnswerPanel, finishPanel, ExitPanel, retryButton;
    public Text afterAnswerPanelText;

    [Header ("UI For questions")]
    public GameObject questionLayout;
    public GameObject boardGame;
    public GameObject diceButton;
    public Text[] answerTextArray;
    private List<Text> answerTextList;
    public void UpdateScreens (int operation) {
        if (operation == 1) {
            //Set all screens that stop the game to false
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
        } else if (operation == 3) {
            //Updates the answers display (images and texts for each layout chosen)
            questionHolder.text = quizManager2Script.currentQuestion.questionName;
            answerTextList = answerTextArray.ToList<Text> ();
            quizManager2Script.currentQuestion.correctAnswerValue = Random.Range (0, 3);
            answerTextList[quizManager2Script.currentQuestion.correctAnswerValue].text = quizManager2Script.currentQuestion.correctAnswer;
            answerTextList.RemoveAt (quizManager2Script.currentQuestion.correctAnswerValue);
            answerTextList[0].text = quizManager2Script.currentQuestion.wrongAnswer1;
            answerTextList[1].text = quizManager2Script.currentQuestion.wrongAnswer2;
            answerTextList[2].text = quizManager2Script.currentQuestion.wrongAnswer3;

        } else if (operation == 5) {
            //Set the timer display to false
            GameObject.Find ("TimerHolder").gameObject.SetActive (false);
        } else if (operation == 10) {
            //Set the correction text on screen to the correction string that correspond to the current question
                correctionText.gameObject.SetActive (true);
                correctionImage.gameObject.SetActive (false);
                correctionText.text = quizManager2Script.currentQuestion.correction;
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
        }
    }

    public void ShowQuestion(){
        questionLayout.SetActive(true);
        boardGame.SetActive(false);
        diceButton.SetActive(false);
    }

    public void HideQuestion(){
        questionLayout.SetActive(false);
        boardGame.SetActive(true);
        diceButton.SetActive(true);
    }

    public void ShowAfterAnswerScreen(bool correct){
        afterAnswerPanel.SetActive(true);
        if(correct){
            afterAnswerPanelText.text = "Correto!";
            afterAnswerPanel.GetComponent<Image>().color = new Color32(0,255,0,255);
        }else{
            afterAnswerPanelText.text = "Errado!";
            afterAnswerPanel.GetComponent<Image>().color = new Color32(255,0,0,255);
        }
    }

    public void HideAfterAnswerScreen(){
        afterAnswerPanel.SetActive(false);
    }
}