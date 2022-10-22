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
    public GameObject afterAnswerPanel, wonScreen, ExitPanel;
    public Text afterAnswerPanelText;

    [Header ("UI For questions")]
    public GameObject questionLayout;
    public GameObject questionPanel;
    public GameObject currentPlayerDisplay;

    public GameObject boardGame;
    public GameObject diceButton;
    public GameObject playerInfo;

    public Text[] answerTextArray;
    private List<Text> answerTextList;

    Sprite[] avatars;

    void Start(){
        avatars = FindObjectOfType<PieceMovement>().avatars;
    }

    public void UpdateUI (int operation) {
        if (operation == 3) {
            //Updates the answers display (images and texts for each layout chosen)
            questionHolder.text = quizManager2Script.currentQuestion.questionName;
    
            /*if(quizManager2Script.currentQuestion.difficulty == 0){
                questionHolder.color = Color.blue;
            }else if(quizManager2Script.currentQuestion.difficulty == 1){
                questionHolder.color = Color.yellow;
            }else if(quizManager2Script.currentQuestion.difficulty == 2){
                questionHolder.color = Color.red;
            }*/

            answerTextList = answerTextArray.ToList<Text> ();
            quizManager2Script.currentQuestion.correctAnswerValue = Random.Range (0, 3);
            //cheatmode
                //quizManager2Script.currentQuestion.correctAnswerValue = 0;
                
            answerTextList[quizManager2Script.currentQuestion.correctAnswerValue].text = quizManager2Script.currentQuestion.correctAnswer;
            answerTextList.RemoveAt (quizManager2Script.currentQuestion.correctAnswerValue);
            answerTextList[0].text = quizManager2Script.currentQuestion.wrongAnswer1;
            answerTextList[1].text = quizManager2Script.currentQuestion.wrongAnswer2;
            answerTextList[2].text = quizManager2Script.currentQuestion.wrongAnswer3;
            
        } else if (operation == 17) {
            if (ExitPanel.activeInHierarchy == false) {
                ExitPanel.SetActive (true);
                timerScript.StopCoroutine ("Timer");
            } else {
                ExitPanel.gameObject.SetActive (false);    
                timerScript.StartCoroutine ("Timer",0);
            }
        }
    }

    public void ShowQuestion(){
        AudioManager.GetInstance().Play("ShowWindow");
        questionLayout.SetActive(true);
        questionPanel.SetActive(true);
        currentPlayerDisplay.GetComponent<Image>().sprite = avatars[this.GetComponent<PieceMovement>().currentPlayer];

        timerScript.StartCoroutine ("Timer",1);

        playerInfo.SetActive(false);
        boardGame.SetActive(false);
        diceButton.SetActive(false);
    }

    public void HideQuestion(){

        questionLayout.SetActive(false);
        questionPanel.SetActive(false);

        playerInfo.SetActive(true);
        boardGame.SetActive(true);
        diceButton.SetActive(true);
    }

    public void ShowAfterAnswerScreen(int helper){

        timerScript.StopCoroutine ("Timer");
        afterAnswerPanel.SetActive(true);

        if(helper == 0){
             afterAnswerPanelText.text = "Errado!";
            afterAnswerPanel.GetComponent<Image>().color = new Color32(255,0,0,255);
            AudioManager.GetInstance().Play("WrongAnswer");
        }else if (helper == 1){
            afterAnswerPanelText.text = "Correto!";
            afterAnswerPanel.GetComponent<Image>().color = new Color32(0,255,0,255);
            AudioManager.GetInstance().Play("CorrectAnswer");
        }else if (helper == 2){
            afterAnswerPanelText.text = "O tempo acabou!";
            afterAnswerPanel.GetComponent<Image>().color = new Color32(255,255,0,255);
            AudioManager.GetInstance().Play("WrongAnswer");
        }
    }

    public void HideAfterAnswerScreen(){
        afterAnswerPanel.SetActive(false);
    }

    public void ShowWonScreen(string playerWhoWon){
        wonScreen.SetActive(true);
        finishText.text = "O JOGADOR " + playerWhoWon + " GANHOU!"; 
        AudioManager.GetInstance().Pause("GameTheme");
        AudioManager.GetInstance().Play("WinSound");
    }
}