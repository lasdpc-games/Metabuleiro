using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceMovement : MonoBehaviour{

    [Range(1,4)]
    public int numberOfPlayers = 1;
    public Sprite[] avatars;

    public UpdateUIScript updateUIscript;
    public PieceUI pieceUI;
    public GameObject dice;

    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites

    int turnsPlayed = 0, currentPlayer;
    int diceValue;

    Board gameBoard;

    PlayerToken[] players;

    void Start(){
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
        Debug.Log(diceValue);

        dice.SetActive(true);
        Image rend = dice.GetComponent<Image>();

        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++){
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        rend.sprite = diceSides[diceValue - 1];
        yield return new WaitForSeconds(3f);

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
        System.Random rnd = new System.Random();
        int diceValue = rnd.Next(1, 7);
        return diceValue;
    }

    public void MovePiece(){
        PlayerToken player = players[currentPlayer];
        player.pos += diceValue;

        if(player.pos >= gameBoard.pathPos.Length){
            Debug.Log("Player " + currentPlayer + " won the game");
            
        }
        Vector2 newPathPos = gameBoard.pathPos[player.pos];
        player.avatar.transform.SetParent(gameBoard.board[(int)newPathPos.x - 1][(int)newPathPos.y - 1].transform);
    }
}
