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
    private bool darkside = false;
    private bool brightside = false;

    public float transitionDuration = .5f;

    // Start is called before the first frame update
    void Start() {
        mCamera = FindObjectOfType<Camera>();
    }

    void TeleportDark_right()
    {
        Debug.Log("DRIGHT");
        player.transform.position = new Vector3(-351.4f, -51, 0);
        mainCamera.SetActive(false);
        darkRoomCamera.SetActive(true);
    }
    void TeleportDark_left()
    {
        Debug.Log("DLEFT");
        player.transform.position = new Vector3(-368.43f, -50.47f, 0);
        mainCamera.SetActive(false);
        darkRoomCamera.SetActive(true);
    }

    /*    void TeleportDarkRoom_right()
        {
            Debug.Log("ENTERRIGHT");
            enter_left = true;
            player.transform.position = new Vector3(-351.4f, -51, 0);
            mainCamera.SetActive(false);
            darkRoomCamera.SetActive(true);
        }

        void TeleportLightCorridor_right()
        {
            player.transform.position = new Vector3(10.44f, 2.7f, 0);
            mainCamera.SetActive(true);
            darkRoomCamera.SetActive(false);
        }

        void TeleportDarkRoom_left()
        {
            Debug.Log("ENTERLEFT");
            enter_left = true;
            player.transform.position = new Vector3(-368.43f, -50.47f, 0);
            mainCamera.SetActive(false);
            darkRoomCamera.SetActive(true);
        }

        void TeleportLightCorridor_left()
        {
            player.transform.position = new Vector3(10.15f, 2.5f, 0);
            mainCamera.SetActive(true);
            darkRoomCamera.SetActive(false);
        }*/

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        Debug.Log("Collision");

        if (collisionInfo.gameObject.CompareTag(Tags.PLAYER)) {
            if (canGo)
            {
                changeRoom(direction);
            }
            else if (!darkRoomCamera.activeSelf)
            {
                /*if (direction == Direction.LEFT)
                {
                    TeleportDark_right();
                    Debug.Log("Teleport dark coté droit");
                }
                else if (direction == Direction.RIGHT) */
                    if (direction == Direction.RIGHT)
                    {
                        TeleportDark_right();
                        Debug.Log("Teleport dark coté gauche");
                    }
            }
            else if (darkRoomCamera.activeSelf)
            {
                if (direction == Direction.LEFT)
                    Debug.Log("Teleport light coté droit");
                else if (direction == Direction.RIGHT)
                    Debug.Log("Teleport light coté gauche");
            }


            /*else if (!darkRoomCamera.activeSelf && direction == Direction.LEFT)
            {
                Debug.Log("PASSRIGHT");
                TeleportDarkRoom_right();
                enter_right = true;
                enter_left = false;
                Debug.Log(enter_right);
                Debug.Log(enter_left);
                Debug.Log("------------------------------------");
            }
            else if (darkRoomCamera.activeSelf && direction ==  Direction.LEFT && enter_right == true)
            {
                enter_right = false;
                Debug.Log("EXITLEFT");
                TeleportLightCorridor_left();
            }
            else if (!darkRoomCamera.activeSelf && direction == Direction.RIGHT)
            {
                Debug.Log("PASSLEFT");
                TeleportDarkRoom_left();
            }
            else if (darkRoomCamera.activeSelf && direction == Direction.RIGHT && enter_left == true)
            {
                Debug.Log("EXITRIGHT");
                TeleportLightCorridor_right();
            }
            else
            {
                Debug.Log(darkRoomCamera.activeSelf);
                Debug.Log(direction == Direction.LEFT);
                Debug.Log(enter_right);
                Debug.Log(enter_left);
                Debug.Log("------------------");
            }*/
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
