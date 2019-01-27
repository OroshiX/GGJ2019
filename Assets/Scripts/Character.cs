using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField]
    private Takeable inHand;
    private InteractableObject inCollision;
    private ReleaseArea releaseArea;
    [SerializeField]
    private GameObject whichRoom;
    private Camera mCamera;
    private Transform nextRoom;

    public float transitionDuration = 0.5f;

    public float deltaY;

    void Start() {
        mCamera = FindObjectOfType<Camera>();
    }

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
        }
        if (other.gameObject.CompareTag(Tags.AREA_RELEASE)) {
            releaseArea = other.gameObject.GetComponent<ReleaseArea>();
        }
        if (other.gameObject.CompareTag(Tags.ROOM)) {
            Debug.Log("We are in room!!!!!!! " + other.name);
            whichRoom = other.gameObject;
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
        } else if (Input.GetKeyDown(KeyMapping.secondaryAction)) {
            // TODO which actions possible?
            if (inHand) {
                // Launch?
            } else if (inCollision) {
                if (inCollision.GetComponent<Pushable>()) {
                    push(inCollision.GetComponent<Pushable>());
                }
            }
        } else if (Input.GetKeyDown(KeyMapping.changeWorld)) {
            // TODO check can change world
            // Check which room we are in
            if (whichRoom) {
                whichRoom.GetComponentInChildren<Opposite>();
            }
            // Check if in that
        } else if (Input.GetAxisRaw("Vertical") > 0f) {
            var top = whichRoom.GetComponentInChildren<Top>();
            Debug.Log("AAAAAAAAAA >0, top: " + top + " can we go: ");
            if (top.canWeGo()) {
                Debug.Log("We can go!");
                travelFloor(top.getNextRoom().transform);
            }
        } else if (Input.GetAxisRaw("Vertical") < 0f) {
            Debug.Log("AAAAAAAAAA <0");
            var bottom = whichRoom.GetComponentInChildren<Bottom>();
            if (bottom.canWeGo()) {
                travelFloor(bottom.getNextRoom().transform);
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

    private void push(Pushable pushable) {
        // TODO
    }

    private void travelFloor(Transform floor) {
        Debug.Log("Traveling!");
        nextRoom = floor;
        deltaY = floor.position.y - whichRoom.transform.position.y;
        StartCoroutine("transitionCamera");
        StartCoroutine("transitionCharacter");
    }

    IEnumerator transitionCamera() {
        float t = 0f;
        var startingPos = mCamera.transform.position;
        var endPos = nextRoom.position;
        endPos.z = -10f;
        while (t < 1f) {
            t += Time.deltaTime * Time.timeScale / transitionDuration;
            mCamera.transform.position = Vector3.Lerp(startingPos, endPos, t);
            yield return 0;
        }
    }

    IEnumerator transitionCharacter() {
        float t = 0f;
        var startingPos = transform.position;
        var endPos = new Vector2(startingPos.x, startingPos.y);
        endPos.y += deltaY;
        
        while (t < 1.0f) {
            t += Time.deltaTime * Time.timeScale / transitionDuration;
            transform.position = Vector3.Lerp(startingPos, endPos, t);
            yield return 0;
        }

    }

}
