using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalRoom : MonoBehaviour {
    [Header ("MainMedalRoomLayout")]
    public Image[] medals;
    public GameObject medalsObject;
    [Header ("AchivementInfoPopUp")]
    public GameObject achivementInfoPanel;
    public Text achivementInfoText;
    public Image achivementInfoSprite;
    bool[] achivementConquered = new bool[9];
    public Achivement[] achivements = new Achivement[9];

    public void CheckMedals () {
        LoadPlayer ();
        for (int i = 0; i < medals.Length; i++) {
            if (achivementConquered[i] == true) {
                medals[i].color = new Color32 (255, 255, 255, 255);
            }
        }
    }
    void LoadPlayer () {

        PlayerData achievementsSaved = SaveSystem.LoadPlayer ();

        for (int i = 0; i < achivementConquered.Length; i++) {
            achivementConquered[i] = achievementsSaved.achivements[i];
        }
    }
    public void ShowAchivementInfo (int whatAchivement) {
        if (achivementInfoPanel.activeInHierarchy == false) {
            medalsObject.SetActive (false);
            achivementInfoPanel.SetActive (true);
            achivementInfoSprite.sprite = achivements[whatAchivement].achivementSprite;
            achivementInfoText.text = achivements[whatAchivement].achivementInfo;
            if (achivementConquered[whatAchivement] == true) {
                achivementInfoSprite.color = new Color32 (255, 255, 255, 255);
            } else {
                achivementInfoSprite.color = new Color32 (0, 0, 0, 130);
            }
        } else {
            medalsObject.SetActive (true);
            achivementInfoPanel.SetActive (false);
        }
    }
}