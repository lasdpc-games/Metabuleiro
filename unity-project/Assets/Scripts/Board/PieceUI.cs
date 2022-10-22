using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PieceUI : MonoBehaviour{
    public GameObject[] playerInfo;

    Sprite[] avatars;

    private void Awake() {
        avatars = FindObjectOfType<PieceMovement>().avatars;
    }

    public void InitializeUI(PlayerToken[] players){
        for(int i = 0; i < players.Length; i++){
            playerInfo[i].transform.Find("AvatarFrame").transform.Find("Avatar").GetComponent<Image>().sprite = avatars[i];
            playerInfo[i].transform.Find("Name").GetComponent<TMP_Text>().text = players[i].name;
            playerInfo[i].transform.Find("Points").GetComponent<TMP_Text>().text = "Pontos: "+  players[i].points;
            playerInfo[i].transform.Find("background").GetComponent<Image>().color = new Color32(100,100,100,255);
            playerInfo[i].SetActive(true);
            //Debug.Log("Initializing player " + (i+1));
        }
    }

    public void UpdateUI(PlayerToken[] players, int currentPlayer){
        for(int i = 0; i < players.Length; i++){
            /*Debug.Log("i: " + i);
            Debug.Log(playerInfo.Length);
            Debug.Log(playerInfo[i]);*/
            playerInfo[i].transform.Find("Points").GetComponent<TMP_Text>().text = "Pontos: "+  players[i].points;
            playerInfo[i].transform.Find("background").GetComponent<Image>().color = new Color32(100,100,100,255);
            playerInfo[i].SetActive(true);
        }
        playerInfo[currentPlayer].transform.Find("background").GetComponent<Image>().color = new Color32(255,255,255,255);

    }

    private void Update() {
        Debug.Log("Player 1: " + playerInfo[0]);
    }
}
