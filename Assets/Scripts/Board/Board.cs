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

            int helper = (int)((pathPos[i].y - 1)%2);
            byte colorB = 0;
            Debug.Log(helper);
            switch (helper){
                case 0:
                    colorB = 80;
                    break;
                case 1:
                    colorB = 255;
                    break;
            }

            helper = (int)((pathPos[i].x - 1)%2);
            byte colorG = 0;
            Debug.Log(helper);
            switch (helper){
                case 0:
                    colorG = 80;
                    break;
                case 1:
                    colorG = 255;
                    break;
            }

            board[(int)pathPos[i].x - 1][(int)pathPos[i].y - 1].GetComponent<Image>().color = new Color32(0,colorG,colorB,255);
            //?Debug.Log(board[(int)pathPos[i].x - 1][(int)pathPos[i].y - 1].name);
        }
    }
}
