using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField]
    private InteractableObject inHand;
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


    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.CompareTag(Tags.OBJECT)) {
            inCollision = collisionInfo.gameObject.GetComponent<InteractableObject>();
        } else if (collisionInfo.gameObject.CompareTag(Tags.AREA_RELEASE)) {
            releaseArea = collisionInfo.gameObject.GetComponent<ReleaseArea>();
        }
    }

    void OnCollisionExit2D(Collision2D collisionInfo) {
        inCollision = null;
    }

    void Update() {
        if (Input.GetKeyDown(KeyMapping.mainAction)) {
            if (inHand) {
                release();
            } else if (inCollision) {
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
    }

    private void openOrClose(Openable openable) {
        openable.openOrClose();
    }
    //    public void

}
