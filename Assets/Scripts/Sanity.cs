using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour {
    [SerializeField]
    [RangeAttribute(0f, 100f)]
    private float sanity;

    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start() {
        updateDisplay();
    }

    // Update is called once per frame
    void Update() {

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
}
