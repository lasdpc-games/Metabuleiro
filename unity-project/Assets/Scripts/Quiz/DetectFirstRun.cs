using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFirstRun : MonoBehaviour {
    public GameObject firstRunPanel;
    int hasPlayed;
    void Start () {
        hasPlayed = PlayerPrefs.GetInt ("HasPlayed");
        if(hasPlayed != 0){
            Destroy (firstRunPanel);
        }
    }
    public void PressedHelpForTheFirstTime () {
        if (hasPlayed == 0) {
            Destroy (firstRunPanel);
            PlayerPrefs.SetInt ("HasPlayed", 1);
        }
    }
    private void Update () {
        if (Input.GetKeyDown (KeyCode.P)) {
            PlayerPrefs.DeleteAll ();
        }
    }
}