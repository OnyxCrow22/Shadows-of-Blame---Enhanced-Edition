using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarSoundtrack : MonoBehaviour
{
    public string[] trackNames;
    public AudioClip[] songs;
    AudioClip currentSong;
    public AudioSource carRadio;
    public PlayerMovementSM playsm;
    public float currentTrack;
    int songSelect;
    public TextMeshProUGUI songName;
    public bool canPlayRadio = false;
    
    public PlayerControls pControls;

    private void Update()
    {
        if (playsm.inVehicle && pControls.Driving.PlaySong.IsPressed())
        {
            StartCoroutine(PlaySong());
            canPlayRadio = true;
        }

        if (playsm.inVehicle && pControls.Driving.SkipSong.IsPressed())
        {
            StartCoroutine(SkipSong());
        }

        if (playsm.inVehicle && pControls.Driving.StopSong.IsPressed())
        {
            StartCoroutine(StopSong());
            canPlayRadio = false;
        }

        else if (!playsm.inVehicle)
        {
            StartCoroutine(StopSong());
            canPlayRadio = false;
        }
    }

    void Awake()
    {
        pControls = new PlayerControls();
    }

    void OnEnable()
    {
        pControls.Enable();
    }

    void OnDisable()
    {
        pControls.Disable();   
    }

    IEnumerator PlaySong()
    {
        List<int> playedSongs = new List<int>();
        int songCount = 0;
        while (songCount < 7)
        {
            songSelect = Random.Range(0, songs.Length);
            playedSongs.Contains(songSelect);
            playedSongs.Add(songSelect);
            currentSong = songs[songSelect];
            carRadio.PlayOneShot(currentSong);
            trackNames[songSelect] = currentSong.name;
            songName.text = currentSong.name;
            yield return new WaitForSeconds(currentSong.length);
            songCount++;
        }
        if (songCount > 7)
        {
            yield break;
        }
    }

    IEnumerator SkipSong()
    {
        carRadio.Stop();
        songName.text = "";
        songSelect = Random.Range(0, songs.Length);
        currentSong = songs[songSelect];
        carRadio.PlayOneShot(currentSong);
        songName.text = currentSong.name;
        yield break;
    }

    IEnumerator StopSong()
    {
        carRadio.Stop();
        songName.text = "";
        canPlayRadio = false;
        yield break;
    }
}
