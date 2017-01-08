using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class Music : MonoBehaviour
{

    [SerializeField]
    private Text MusicListText;
    private string display = "";
    [SerializeField]
    private Object[] mMusic;
    [SerializeField]
    private int currentTrack;
    [SerializeField]
    private int totalTracks;
    [SerializeField]
    private AudioSource source;


    void Awake()
    {
        //mMusic = 
        source = GetComponent<AudioSource>();
        source.clip = mMusic[0] as AudioClip;
        totalTracks = mMusic.Length;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SwitchTrack();
    }

    void SwitchTrack()
    {
        if (source.isPlaying)
            source.Stop();

        if (currentTrack >= totalTracks)
            currentTrack = 0;

        source.clip = mMusic[currentTrack++] as AudioClip;
        source.Play();
    }

}