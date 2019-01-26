using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour {
    [SerializeField]
    [RangeAttribute(0f, 100f)]
    private float sanity = 50f;

    private float basicLosePerSecond = 2.5f / 60f; // 20min pour perdre depuis 50%
    private float baseMultiplier = 1f;


    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start() {
        updateDisplay();
    }

    // Update is called once per frame
    void Update() {
        checkDeath();
        sanity -= basicLosePerSecond * baseMultiplier * Time.deltaTime;
        updateDisplay();
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

    private void die() {
        //

    }
}
