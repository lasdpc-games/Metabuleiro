using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceMovement : MonoBehaviour{

    [Range(1,4)]
    public int numberOfPlayers = 1;
    public Sprite[] avatars;

    int turnsPlayed = 0, currentPlayer;
    int diceValue;

    Board gameBoard;

    PlayerToken[] players;

    public UpdateUIScript updateUIscript;
    public PieceUI pieceUI;

    void Start(){
        gameBoard = GetComponent<Board>();
        QuizManager2.OnAnswer += Answer;

        players = new PlayerToken[numberOfPlayers];

        for (int i = 0; i < numberOfPlayers; i++){
            players[i] = new PlayerToken();
            players[i].avatar = new GameObject("player" + i, typeof(Image));
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
        PopQuestion();
        turnsPlayed++;
    }

    void PopQuestion(){
        diceValue = DiceRoll();
        Debug.Log(diceValue);
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
        Debug.Log("Current player: " + currentPlayer);
        pieceUI.UpdateUI(players, currentPlayer);
    }

    int DiceRoll(){
        System.Random rnd = new System.Random();
        int diceValue = rnd.Next(1, 7);
        //animate dice roll
        //show result
        return diceValue;
    }

    public void MovePiece(){
        PlayerToken player = players[currentPlayer];
        player.pos += diceValue;

        if(player.pos >= gameBoard.pathPos.Length){
            //win
            Debug.Log("Player " + currentPlayer + " won the game");
        }
        Vector2 newPathPos = gameBoard.pathPos[player.pos];
        player.avatar.transform.SetParent(gameBoard.board[(int)newPathPos.x - 1][(int)newPathPos.y - 1].transform);
    }
}
