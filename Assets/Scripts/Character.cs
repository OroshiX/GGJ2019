using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public AudioSource source_light;
    public AudioSource source_dark;
    public AudioSource source_buzz;
    public AudioChorusFilter filter_buzz;
    public GameObject obj_transition1;
    public GameObject obj_transition2;
    public float sanity;

    [SerializeField]
    private Takeable inHand;
    private InteractableObject inCollision;
    private ReleaseArea releaseArea;
    [SerializeField]
    private GameObject whichRoom;
    private Camera mCamera;
    private Transform nextRoom;
    private bool soundside = true;

    [SerializeField]
    private GameObject panelFade;

    public float transitionDuration = 0.5f;

    public float deltaY;

    public Animator animator;

    public GameObject displayed;

    void Start() {
        mCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
    }

    public void release() {
        if (releaseArea) {
            releaseArea.put(inHand);
            inHand = null;
        } else {
            Debug.LogError("Can't release here");
            // TODO Can't release here : play sound?
        }
    }

    public void undisplay() {
        //        displayed.SetActive(false); // TODO destroy?
        Destroy(displayed);
        displayed = null;
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
    /*
        private Color currentColor;
        private float fadeInDuration = 1f;
        private Image fadeImage;
        private bool hasBegun = false;
    //*/
    private void endGame() {
        //        fadeImage = GetComponent<Image>();
        //        currentColor = fadeImage.color;
        //        panelFade.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }


    void calc_buzz_kill() {
        if (!soundside)
        {
            if (!filter_buzz.gameObject.activeSelf)
                filter_buzz.gameObject.SetActive(true);

        }
        else
        if (filter_buzz.gameObject.activeSelf)
            filter_buzz.gameObject.SetActive(false);

        if (sanity > 25)
            source_buzz.pitch = 1.0f;
        else
        {
            source_buzz.pitch = 1.0f + (25 - sanity) / 50.0f;
        }
    }

    void Update() {


        sanity = gameObject.GetComponent<Sanity>().get_Sanity();
        calc_buzz_kill();
        if (Input.GetKeyDown(KeyMapping.mainAction)) {
            if (inHand) {
                release();
            } else if (displayed) {
                undisplay();
            } else if (inCollision) {
                if ((bool) inCollision.GetComponent<Takeable>()) {
                    take(inCollision.GetComponent<Takeable>());
                } else if (inCollision.GetComponent<Openable>()) {
                    openOrClose(inCollision.GetComponent<Openable>());
                } else if (inCollision.GetComponent<Displayable>()) {
                    display(inCollision.GetComponent<Displayable>());
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
        inHand.transform.localPosition = new Vector2(0.69f, 0f);
        var type = whichRoom.GetComponent<TypeRoom>().getTypeRoom();
        inHand.GetComponent<SpriteRenderer>().sprite = type == Lightness.LIGHT ? inHand.getCarriedLight() : inHand.getCarriedDark();
    }

    private void display(Displayable displayable) {
        displayed = Instantiate(displayable.getToDisplay());

        // TODO play sound
        FindObjectOfType<PlaySound>().playPhoneCall();
        FindObjectOfType<PlaySound>().lastMusic();
        Invoke(nameof(destroyDisplay), Constants.TIME_PHONE_CALL);

        Invoke(nameof(endGame), Constants.TIME_PHONE_CALL);
    }

    private void destroyDisplay() {
        Destroy(displayed);
        Destroy(displayed.gameObject);
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

    private void swap_sounds() {
        soundside = !soundside;
        if (soundside == false)
        {
            source_light.volume = 0;
            source_dark.volume = 100;
            if (obj_transition1.activeSelf)
                obj_transition1.SetActive(false);
            obj_transition2.SetActive(true);
        }
        else
        {
            source_light.volume = 100;
            source_dark.volume = 0;
            if (obj_transition2.activeSelf)
                obj_transition2.SetActive(false);
            obj_transition1.SetActive(true);
        }
    }

    private void travelOpposite(Transform targetTransform) {
        nextRoom = targetTransform;
        playSoundOpposite();
        swap_sounds();

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

    public GameObject getWhichRoom() {
        return whichRoom;
    }

    private void playSoundUpDown() {
        // TODO sound

    }

    private void playSoundOpposite() {
        // TODO sound
    }


}
