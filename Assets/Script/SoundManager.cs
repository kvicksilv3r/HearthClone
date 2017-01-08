using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//By Johanna Pettersson

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    [SerializeField] private AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched
    public float highPitchRange = 1.05f;

    public Slider volumeSlider;

    public AudioSource volumeAudio;
    PlayerSettings playerSettings;

    public AudioSource VolumeSlider {
        get {return volumeAudio; }
        set { volumeAudio = value; }
        }

    void Awake()
    {
        volumeSlider.value = volumeAudio.volume; 
            
    }


    public void MasterVolumeControl()
    {
        volumeAudio.volume = volumeSlider.value;
        

    }

    public void OnMouseEnter()
    {
        volumeAudio.Play();
    }
}
