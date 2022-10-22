using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public bool[] achivements = new bool[9];

    public PlayerData (VerifyAchivements achivementsToSave) {
        for (int i = 0; i < achivements.Length; i++) {
            achivements[i] = achivementsToSave.achivementConquered[i];
        }
    }
}