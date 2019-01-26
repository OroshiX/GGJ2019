using System.Collections;
using UnityEngine;

public class ChangeRoom : MonoBehaviour {
    [SerializeField]
    private bool changePossible = true;
    [SerializeField]
    private Direction direction;
    [SerializeField]
    private float deltaPosChangeRoom = 5f;
    [SerializeField]
    private GameObject character;
    private Camera camera;

    public float transitionDuration = .5f;

    public Transform nextRoom;

    public float heightRoom;
    // Start is called before the first frame update
    void Start() {
        camera = FindObjectOfType<Camera>();
        heightRoom = transform.parent.GetComponent<SpriteRenderer>().size.y;

    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        Debug.Log("Collision");
        if (collisionInfo.gameObject.CompareTag(Tags.PLAYER)) {
            if (changePossible) {
                // TODO change room
                changeRoom(direction);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator transitionCamera() {
        float t = 0f;
        var startingPos = camera.transform.position;
        var endPos = nextRoom.position;
        endPos.z = -10f;
        while (t < 1.0f) {
            t += Time.deltaTime * Time.timeScale / transitionDuration;
            camera.transform.position = Vector3.Lerp(startingPos, endPos, t);
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
            case Direction.TOP:
                endPos.y += heightRoom;
                break;
            case Direction.BOTTOM:
                endPos.y -= heightRoom;
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
