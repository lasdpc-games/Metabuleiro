using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour{
    public Vector2[] pathPos;
    public int boardDimension = 8;
    public GameObject cellTemplate, boardHolder;

    [HideInInspector]
    public GameObject[][] board;

    void Awake(){
        foreach (Transform child in boardHolder.transform) {
            GameObject.Destroy(child.gameObject);
        }

        board = new GameObject[boardDimension][];
        for (int x = 0; x < boardDimension; x++){
            board[x] = new GameObject[boardDimension];
            for (int y = 0; y < boardDimension; y++){
                GameObject go = Instantiate(cellTemplate, boardHolder.transform);
                go.name = "Cell(" + x + ")(" + y + ")";
                board[x][y] = go;
            }
        }

        for (int i = 0; i < pathPos.Length; i++){
            byte colorG = Convert.ToByte(255 - 8*i);
            byte colorB = Convert.ToByte(255 - 8*i);

            board[(int)pathPos[i].x - 1][(int)pathPos[i].y - 1].GetComponent<Image>().color = new Color32(255,colorG,colorB,255);
            //?Debug.Log(board[(int)pathPos[i].x - 1][(int)pathPos[i].y - 1].name);
        }
    }
}
