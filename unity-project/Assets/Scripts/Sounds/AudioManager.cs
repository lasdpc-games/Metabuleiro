using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public static AudioManager GetInstance() {
        return instance;
    }

    public Sound[] sounds;

    Scene scene;

    void Awake() {
        instance = this;
        scene = SceneManager.GetActiveScene();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Music");

        if (objects.Length > 1){
            Destroy(this.gameObject);
        }
        
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch; 
            sound.source.loop = sound.loop;
        }
    }

    void Start(){
        if (scene.buildIndex == 0){
            Play("MenuTheme");
        } else {
            Play("GameTheme");
        }
    }

    public void Play(string name) {
        Sound foundSound = Array.Find(sounds, sound => sound.name == name);
        
        if(foundSound == null){
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        
        foundSound.source.Play();
    }

    public void Pause(string name) {
        Sound foundSound = Array.Find(sounds, sound => sound.name == name);
        
        if(foundSound == null){
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        
        foundSound.source.Pause();
    }
}
