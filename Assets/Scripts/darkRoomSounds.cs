using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkRoomSounds : MonoBehaviour
{
    public AudioSource source_high_music;
    public AudioSource source_low_music;
    public AudioChorusFilter filter_low_music;
    public GameObject son_inquietants;
    public GameObject buzz_kill;

    void OnEnable()
    {
        source_high_music.volume = 0f;
        source_low_music.volume = 0.47f;
        filter_low_music.depth = 0.8f;
        filter_low_music.rate = 0.7f;
        son_inquietants.SetActive(true);
        buzz_kill.SetActive(true);
    }

    private void OnDisable()
    {

        source_high_music.volume = 0.70f;
        source_low_music.volume = 0.0f;
        filter_low_music.depth = 0.03f;
        filter_low_music.rate = 0.3f;
        son_inquietants.SetActive(false);
        buzz_kill.SetActive(false);
    }
}
