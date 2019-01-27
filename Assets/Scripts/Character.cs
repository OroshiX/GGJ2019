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

    public Animator animator;

    void Start() {
        mCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
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
        if (other.gameObject.CompareTag(Tags.OBJECT)) {
            inCollision = other.gameObject.GetComponent<InteractableObject>();
        }
        if (other.gameObject.CompareTag(Tags.AREA_RELEASE)) {
            releaseArea = other.gameObject.GetComponent<ReleaseArea>();
        }
        if (other.gameObject.CompareTag(Tags.ROOM)) {
            whichRoom = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag(Tags.OBJECT)) {
            inCollision = null;
        } else if (other.gameObject.CompareTag(Tags.AREA_RELEASE)) {
            releaseArea = null;
        }
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
            // Check which room we are in
            if (whichRoom) {
                var opposite = whichRoom.GetComponentInChildren<Opposite>();
                // check if we can change world
                if (opposite.canWeGo()) {
                    travelOpposite(opposite.getNextRoom().transform);
                }
            }
            // Check if in that
        } else if (Input.GetAxisRaw("Vertical") > 0f) {
            var top = whichRoom.GetComponentInChildren<Top>();
            if (top.canWeGo()) {
                travelFloor(top.getNextRoom().transform);
            }
        } else if (Input.GetAxisRaw("Vertical") < 0f) {
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
        deltaY = nextRoom.position.y - mCamera.transform.position.y;
        //floor.position.y - whichRoom.transform.position.y;
        playSoundUpDown();
        var startingPosCharacter = transform.position;
        var endPosCharacter = new Vector3(startingPosCharacter.x, startingPosCharacter.y + deltaY, 0f);
        StartCoroutine(transitionCharacter(startingPosCharacter, endPosCharacter));

        StartCoroutine("transitionCamera");
    }

    private void travelOpposite(Transform targetTransform) {
        nextRoom = targetTransform;
        playSoundOpposite();

        var rotation = transform.rotation;
        rotation.x = -rotation.x;
        transform.rotation = rotation;
        var startingPosCharacter = transform.position;
        var endPosCharacter = new Vector2(startingPosCharacter.x, -startingPosCharacter.y);

        StartCoroutine("transitionCamera");
        StartCoroutine(transitionCharacter(startingPosCharacter, endPosCharacter));
        animator.SetTrigger(Params.SWITCH);
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

    IEnumerator transitionCharacter(Vector3 startingPos, Vector3 endPos) {
        float t = 0f;

        while (t < 1.0f) {
            t += Time.deltaTime * Time.timeScale / transitionDuration;
            transform.position = Vector3.Lerp(startingPos, endPos, t);
            yield return 0;
        }

    }

    private void playSoundUpDown() {
        // TODO sound
    }

    private void playSoundOpposite() {
        // TODO sound
    }


}
