using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {
    [SerializeField]
    private GameObject ringDisplay;
    // Start is called before the first frame update
    void Start() {
        FindObjectOfType<PlaySound>().playPhone();
        InvokeRepeating(nameof(ring), 2f, 1f);
    }

    private void ring() {
        ringDisplay.SetActive(true);
        Invoke(nameof(screenOff), 0.5f);
    }

    private void screenOff() {
        ringDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }
}
