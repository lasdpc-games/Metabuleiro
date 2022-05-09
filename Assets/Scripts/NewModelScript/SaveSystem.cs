using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem {
    public static void SavePlayer (VerifyAchivements achivementsToSave) {
        BinaryFormatter formatter = new BinaryFormatter ();

        string path = Application.persistentDataPath + "/achivements.txt";
        //string path = "C:/Users/gusta/Desktop/achivements.txt";
        FileStream stream = new FileStream (path, FileMode.Create);

        PlayerData data = new PlayerData (achivementsToSave);

        formatter.Serialize (stream, data);
        stream.Close ();
    }

    public static PlayerData LoadPlayer () {

        string path = Application.persistentDataPath + "/achivements.txt";
        if (File.Exists (path)) {
            BinaryFormatter formatter = new BinaryFormatter ();
            FileStream stream = new FileStream (path, FileMode.Open);

            PlayerData data = formatter.Deserialize (stream) as PlayerData;
            stream.Close ();
            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}