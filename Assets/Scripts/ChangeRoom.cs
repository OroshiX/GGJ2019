using System.Collections;
using UnityEngine;

public class ChangeRoom : Passage {

    [SerializeField]
    private Direction direction;
    [SerializeField]
    private float deltaPosChangeRoom = 5f;
    [SerializeField]
    private GameObject character;
    private Camera mCamera;

    public GameObject player;
    public GameObject mainCamera;
    public GameObject darkRoomCamera;

    public float transitionDuration = .5f;

    // Start is called before the first frame update
    void Start() {
        mCamera = FindObjectOfType<Camera>();
    }

    void TeleportDarkRoom()
    { 
        player.transform.position = new Vector3(-358, -51, 0);
        mainCamera.SetActive(false);
        darkRoomCamera.SetActive(true);
    }


    void OnCollisionEnter2D(Collision2D collisionInfo) {
        Debug.Log("Collision");
        if (collisionInfo.gameObject.CompareTag(Tags.PLAYER)) {
            if (canGo)
            {
                changeRoom(direction);
            }
            else
                TeleportDarkRoom();
        }
    }


    IEnumerator transitionCamera() {
        float t = 0f;
        var startingPos = mCamera.transform.position;
        var endPos = nextRoom.position;
        endPos.z = -10f;
        while (t < 1.0f) {
            t += Time.deltaTime * Time.timeScale / transitionDuration;
            mCamera.transform.position = Vector3.Lerp(startingPos, endPos, t);
            yield return 0;
        }
    }

    IEnumerator transitionCharacter() {
        float t = 0f;
        var startingPos = character.transform.position;

        var endPos = new Vector2(startingPos.x, startingPos.y);
        switch (direction) {
            case Direction.LEFT:
                endPos.x -= deltaPosChangeRoom;
                break;
            case Direction.RIGHT:
                endPos.x += deltaPosChangeRoom;
                break;
        }
        while (t < 1.0f) {
            t += Time.deltaTime * Time.timeScale / transitionDuration;
            character.transform.position = Vector3.Lerp(startingPos, endPos, t);
            yield return 0;
        }
    }

    private void enable() {
        foreach (var change in toReenable) {
            change.gameObject.SetActive(true);
        }
    }

    private ChangeRoom[] toReenable;

    private void changeRoom(Direction direction) {
        Debug.Log("Changing room");
        toReenable = nextRoom.transform.GetComponentsInChildren<ChangeRoom>();
        foreach (var change in toReenable) {
            change.gameObject.SetActive(false);
        }
        StartCoroutine(transitionCamera());
        StartCoroutine(transitionCharacter());
        Invoke("enable", transitionDuration);
    }
}
