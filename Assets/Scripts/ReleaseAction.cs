using UnityEngine;

public class ReleaseAction : MonoBehaviour {
    [SerializeField]
    private GameObject phone;

    public void doOnRelease() {
        displayPhone();
    }

    // Update is called once per frame
    void Update() {

    }

    private void displayPhone() {
        phone.SetActive(true);
    }
}
