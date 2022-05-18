using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceMovement : MonoBehaviour{

    public int numberOfPlayers;
    public Sprite[] avatars;

    public UpdateUIScript updateUIscript;
    public QuizManager2 quizManagerScript;
    public PieceUI pieceUI;
    public GameObject dice;

    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites

    int turnsPlayed = 0;
    public int currentPlayer;
    int diceValue;

    Board gameBoard;

    PlayerToken[] players;

    void Start(){
        if(PlayerSelectorUI.players == null){
            quizManagerScript.BackToMenu(2);
            return;
        }
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        gameBoard = GetComponent<Board>();
        QuizManager2.OnAnswer += Answer;

        players = PlayerSelectorUI.players;
        numberOfPlayers = players.Length;
        /*new PlayerToken[numberOfPlayers];*/

        for (int i = 0; i < numberOfPlayers; i++){
            //players[i] = new PlayerToken();
            players[i].avatar = new GameObject("player0" + i+1, typeof(Image));
            players[i].avatar.transform.SetParent(FindObjectOfType<Canvas>().transform);
            players[i].avatar.transform.localScale = Vector3.one;
            Image av = players[i].avatar.GetComponent<Image>();
            av.sprite = avatars[i];
            diceValue = 0;
            currentPlayer = i;
            MovePiece();
        }

        currentPlayer = 0;
        pieceUI.InitializeUI(players);
        pieceUI.UpdateUI(players, currentPlayer);
    }

    private void Update() {
    }

    public void Turn(){
        currentPlayer = turnsPlayed%numberOfPlayers;
        StartCoroutine(RollDice());
        turnsPlayed++;
    }

    IEnumerator RollDice(){
        diceValue = DiceValue();
        //?Debug.Log(diceValue);

        dice.SetActive(true);
        Image rend = dice.GetComponent<Image>();

        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++){
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        rend.sprite = diceSides[diceValue - 1];
        yield return new WaitForSeconds(2f);

        dice.SetActive(false);

        players[currentPlayer].questionsAsked++;
        updateUIscript.ShowQuestion();
        //if right
        //correct answer animation
        //MovePiece
    }

    void Answer(int v){
        if(v != 0){
            MovePiece();
            players[currentPlayer].points += 1;
            currentPlayer = turnsPlayed%numberOfPlayers;
        }else{
            currentPlayer = turnsPlayed%numberOfPlayers;
        }
        pieceUI.UpdateUI(players, currentPlayer);
    }


    int DiceValue(){
        int pos = players[currentPlayer].pos;
        int d1Upper = ((32-1)/(31-0))*pos + 1; //(0, 1) e (31, 32)
        int d2Upper = (pos <= 31-2 ? ((27-5)/(29-0))*pos + 5 : 0) + d1Upper; //(0, 5) e (29,27)
        int d3Upper = (pos <= 31-3 ? ((22-10)/(28-0))*pos + 10 : 0) + d2Upper; //(0, 10) e (28, 22)
        int d4Upper = (pos <= 31-4 ? ((10-22)/(27-0))*pos + 22 : 0) + d3Upper; //(0, 22) e (27, 10)
        int d5Upper = (pos <= 31-5 ? ((5-27)/(26-0))*pos + 27 : 0) + d4Upper; //(0, 27) e (26, 5)
        int d6Upper = (pos <= 31-6 ? ((1-32)/(25-0))*pos + 32 : 0) + d5Upper; //(0, 32) e (25, 1)

        Debug.Log("0, " + d1Upper + ", " + d2Upper + ", " + d3Upper + ", " + d4Upper + ", " + d5Upper + ", " + d6Upper);

        System.Random rnd = new System.Random();
        int randVal = rnd.Next(0, d6Upper);
        randVal -= (players[currentPlayer].points/(players[currentPlayer].questionsAsked+1))*10;
        randVal = Mathf.Clamp(randVal, 0, d6Upper);

        Debug.Log(randVal);

        if(0 <=randVal && randVal <= d1Upper) return 1;
        if(d1Upper < randVal && randVal <= d2Upper) return 2;
        if(d2Upper < randVal && randVal <= d3Upper) return 3;
        if(d3Upper < randVal && randVal <= d4Upper) return 4;
        if(d4Upper < randVal && randVal <= d5Upper) return 5;
        if(d5Upper < randVal && randVal <= d6Upper) return 6;

        return 0;
    }

    public void MovePiece(){
        PlayerToken player = players[currentPlayer];
        player.pos += diceValue;
        Debug.Log("player.pos = " + player.pos);

        if(player.pos >= gameBoard.pathPos.Length){
            updateUIscript.ShowWonScreen(players[currentPlayer].name);
            return;
        }
        Vector2 newPathPos = gameBoard.pathPos[player.pos];
        player.avatar.transform.SetParent(gameBoard.board[(int)newPathPos.x - 1][(int)newPathPos.y - 1].transform);
    }
}
