using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField]
    private Takeable inHand;
    private InteractableObject inCollision;
    private ReleaseArea releaseArea;

    public void release() {
        if (releaseArea) {
            releaseArea.put(inHand);
        } else {
            Debug.LogError("Can't release here");
            // TODO Can't release here : play sound?
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("We are colliding with " + other.name);
        if (other.gameObject.CompareTag(Tags.OBJECT)) {
            inCollision = other.gameObject.GetComponent<InteractableObject>();
        } else if (other.gameObject.CompareTag(Tags.AREA_RELEASE)) {
            releaseArea = other.gameObject.GetComponent<ReleaseArea>();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("We are EXITING collision with " + other.name);
        if (other.gameObject.CompareTag(Tags.OBJECT)) {
            inCollision = null;
        } else if (other.gameObject.CompareTag(Tags.AREA_RELEASE)) {
            releaseArea = null;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyMapping.mainAction)) {
            Debug.Log("Pressed main action!");
            if (inHand) {
                Debug.Log("In hand, so release");
                release();
            } else if (inCollision) {
                Debug.Log("In collision");
                if ((bool) inCollision.GetComponent<Takeable>()) {
                    take(inCollision.GetComponent<Takeable>());
                } else if (inCollision.GetComponent<Openable>()) {
                    openOrClose(inCollision.GetComponent<Openable>());
                }
            }
        }
    }

    private void take(Takeable theObject) {
        inHand = theObject;
        inHand.transform.parent = gameObject.transform;
        inHand.transform.localPosition = new Vector2();
    }

    private void openOrClose(Openable openable) {
        openable.openOrClose();
    }
    //    public void

}
