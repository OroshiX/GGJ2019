using UnityEngine;

public class ChangeRoom : MonoBehaviour {
    private bool changePossible = true;
    [SerializeField]
    private Direction direction;
    [SerializeField]
    private float deltaPosChangeRoom = 5f;
    [SerializeField]
    private GameObject character;
    // Start is called before the first frame update
    void Start() {

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

    private void changeRoom(Direction direction) {
        // TODO lerp camera

        Vector2 positionCharacterEnd = character.transform.position;
        switch (direction) {
            case Direction.LEFT:
                positionCharacterEnd.x -= deltaPosChangeRoom;
                break;
            case Direction.BOTTOM:
                break;
            case Direction.RIGHT:
                positionCharacterEnd.x += deltaPosChangeRoom;
                break;
            case Direction.TOP:
                break;
        }

        character.transform.position = positionCharacterEnd;
    }
}
