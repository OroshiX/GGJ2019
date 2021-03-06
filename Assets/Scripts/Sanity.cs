﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sanity : MonoBehaviour {
    [SerializeField]
    [RangeAttribute(0f, 100f)]
    private float sanity = 50f;
    private float basicLosePerSecond = 5.0f / 60f; // 20min pour perdre depuis 50%
    private float baseMultiplier = 1f;
    public AudioChorusFilter filter_music_hi;
    public AudioChorusFilter filter_music_low;
    public AudioSource asource_music_hi;
    public AudioSource asource_music_low;
    //    bool low_switched = false;
    //   bool hi_switched = true;


    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start() {
        updateDisplay();
    }

    void high_sanity_distorder() {
        if (filter_music_hi.dryMix > 0)
            filter_music_hi.dryMix -= 0.002f;
        if (filter_music_hi.rate < 0.6f)
            filter_music_hi.rate += 0.001f;
        if (filter_music_hi.depth < 0.35f)
            filter_music_hi.depth += 0.0002f;
    }

    void high_atmosphere_music_distorder() {
        filter_music_hi.rate = 0.3f + ((-(sanity - 25)) * 0.012f);
        filter_music_hi.depth = 0.03f + ((-(sanity - 25)) * 0.0132f);
        filter_music_hi.dryMix = sanity * 0.02f;
    }

    void reset_high_sanity_music() {
        filter_music_hi.rate = 0.3f;
        filter_music_hi.depth = 0.03f;
        filter_music_hi.dryMix = 0.5f;
    }



    // Update is called once per frame
    void Update() {
        checkDeath();
        sanity -= basicLosePerSecond * baseMultiplier * Time.deltaTime;
        updateDisplay();

        if (sanity < 25)
            high_atmosphere_music_distorder();
        else
            reset_high_sanity_music();
    }

    void checkDeath() {
        if (sanity <= 0) {
            die();
        }
    }

    public void goDark() {
        baseMultiplier = 2f;
    }

    public void goLight() {
        baseMultiplier = 1f;
    }

    private void updateDisplay() {
        slider.value = sanity;
    }

    public void loseSanity(float amount) {
        if (amount > 0f) {
            sanity -= amount;
            updateDisplay();
        } else {
            Debug.LogError("Amount should be >0: " + amount);
        }
    }

    public void winSanity(float amount) {
        if (amount > 0f) {
            sanity += amount;
            updateDisplay();
        } else {
            Debug.LogError("Amount should be >0: " + amount);
        }
    }

    public void darkCatMultiplier() {
        baseMultiplier = 4f;
    }

    private void die() {
        //
        var panel = FindObjectOfType<FadeOut>();
        panel.gameObject.SetActive(true);
        panel.startFadeOut();
        Invoke("mainMenu", 3f);


    }

    private void mainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public float get_Sanity() {
        return (this.sanity);
    }

}
