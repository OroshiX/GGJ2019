using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {
    private bool hasBegun;
    private bool fadeIn;
    private Image fadeImage;
    private Color currentColor;
    [SerializeField]
    private float fadeInDuration = 3f;
    [SerializeField]
    private float autoFadeOutStart;
    private float timer;

    private void Awake() {
        fadeImage = GetComponent<Image>();
        currentColor = fadeImage.color;
        //        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    private void Start() {
        startFadeIn();

        if (autoFadeOutStart > 0f) {
            Invoke(nameof(startFadeOut), autoFadeOutStart);
        }
    }

    public void startFadeOut() {
        hasBegun = true;
        fadeIn = false;
        currentColor = Color.black;
        currentColor.a = 0;
        //        gameObject.SetActive(true);
    }

    public void startFadeIn() {
        hasBegun = true;
        fadeIn = true;
        currentColor = Color.black;
        //        gameObject.SetActive(true);
    }


    // Update is called once per frame
    private void Update() {
        if (!hasBegun) return;

        timer += Time.deltaTime;
        currentColor.a = fadeIn ? 1 - timer / fadeInDuration : timer / fadeInDuration;
        fadeImage.color = currentColor;
        if (timer >= fadeInDuration) {
            hasBegun = false;
            timer = 0;
            //            gameObject.SetActive(false);
        }
    }
}
