using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpRoomScript : MonoBehaviour {
    public Sprite[] helpSprites;
    public string[] helpStrings;
    public Image helpImageUI;
    public Text helpTextUI;
    public int actualLayer;
    // Start is called before the first frame update
    void Start () {
        actualLayer = 0;
        helpTextUI.text = helpStrings[actualLayer];
        helpImageUI.sprite = helpSprites[actualLayer];
    }
    public void ChangeActualLayer (int helper) {
        if (helper == 1) {
            if(actualLayer + 1 < helpSprites.Length){
            actualLayer++;
            }
        } else if (helper == -1) {
            if(actualLayer - 1 >= 0)
            {
            actualLayer--;
            }
        }
        ChangeLayout();
    }
    void ChangeLayout () {
        helpTextUI.text = helpStrings[actualLayer];
        helpImageUI.sprite = helpSprites[actualLayer];
    }
}