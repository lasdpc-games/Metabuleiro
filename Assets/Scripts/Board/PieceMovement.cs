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

        for (int i = 0; i < numberOfPlayers; i++){
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

        dice.SetActive(true);
        Image rend = dice.GetComponent<Image>();

        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++){
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        rend.sprite = diceSides[diceValue - 1];
        yield return new WaitForSeconds(0.5f);

        dice.SetActive(false);

        players[currentPlayer].questionsAsked++;
        updateUIscript.ShowQuestion();
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
        int d1Upper = (pos + 1);
        int d2Upper = ((155 + 22 * pos)/31) + d1Upper;
        int d3Upper = ((310 + 12 * pos)/31) + d2Upper;
        int d4Upper = ((682 - 12 * pos)/31) + d3Upper;
        int d5Upper = ((837 - 22 * pos)/31) + d4Upper;
        int d6Upper = (-pos + 32) + d5Upper;

        //Debug.Log("0, " + d1Upper + ", " + d2Upper + ", " + d3Upper + ", " + d4Upper + ", " + d5Upper + ", " + d6Upper);

        System.Random rnd = new System.Random();
        int randVal = rnd.Next(0, d6Upper);
        randVal -= (players[currentPlayer].points/(players[currentPlayer].questionsAsked+1))*10;
        randVal = Mathf.Clamp(randVal, 0, d6Upper);

        //Debug.Log(randVal);

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

        if(player.pos >= gameBoard.pathPos.Length){
            updateUIscript.ShowWonScreen(players[currentPlayer].name);
            return;
        }
        Vector2 newPathPos = gameBoard.pathPos[player.pos];
        player.avatar.transform.SetParent(gameBoard.board[(int)newPathPos.x - 1][(int)newPathPos.y - 1].transform);
    }
}
