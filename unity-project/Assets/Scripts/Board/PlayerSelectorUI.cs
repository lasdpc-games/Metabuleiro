using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectorUI : MonoBehaviour{

    public Slider numberOfPlayerSelector;
    public GameObject playerCreatorHolder;
    public GameObject playerCreatorPrefab;
    public int numberOfPlayers = 1;

    public static PlayerToken[] players;

    private void Start() {
        QuantitySelector(1);
    }

    public void QuantitySelector(float value){
        numberOfPlayers = (int)value;
        players = new PlayerToken[numberOfPlayers];

        foreach (Transform child in playerCreatorHolder.transform) {
            GameObject.Destroy(child.gameObject);
            AudioManager.GetInstance().Play("Pop");
        }

        for(int i = 0; i < numberOfPlayers; i++){
            GameObject go = Instantiate(playerCreatorPrefab, playerCreatorHolder.transform);
            string playerName = "Player 0" + (i+1);

            go.transform.Find("Label").GetComponent<TMP_Text>().text = playerName;

            int i2 = i;
            TMP_InputField name = go.transform.Find("Name").GetComponent<TMP_InputField>();

            name.onValueChanged.AddListener(delegate {
               NameSetter(name, i2);
            });
            go.name = playerName + "Creator";
            players[i] = new PlayerToken();
            players[i].name = playerName;
        }
    }

    public void NameSetter(TMP_InputField name, int i2){
        players[i2].name = name.text;
        //?Debug.Log("Player0" + (i2+1) +": " + players[i2].name);
    }
}
