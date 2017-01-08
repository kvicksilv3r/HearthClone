using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {

    private AudioSource volumeSlider;
    SoundManager soundManager;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
        volumeSlider = soundManager.VolumeSlider;

	}
	
}
