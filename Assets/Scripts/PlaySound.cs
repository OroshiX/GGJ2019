using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    [SerializeField]
    private AudioSource audioSourceTelephone, audioSourcePhoneCall, musicFin;

    void Start() {
        //        playPhoneCall();
    }

    public void lastMusic() {
        musicFin.gameObject.SetActive(true);
    }

    public void playPhoneCall() {
        var audioSources = GetComponentsInChildren<AudioSource>();
        foreach (var audio in audioSources) {
            audio.gameObject.SetActive(false);
        }
        audioSourcePhoneCall.gameObject.SetActive(true);
    }

    public void playPhone() {
        var audioSources = GetComponentsInChildren<AudioSource>();
        foreach (var audio in audioSources) {
            audio.gameObject.SetActive(false);
        }

        audioSourceTelephone.gameObject.SetActive(true);
        //        AudioSource audioSource = GetComponent<AudioSource>();
        //        AudioClip clip_telephone = (AudioClip)Resources.Load("sounds/telephone.wav");
        //        audioSource.PlayOneShot(clip_telephone);
    }
}
