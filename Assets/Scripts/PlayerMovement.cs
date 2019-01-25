using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Movement movement;

    private float horizontalMove = 0f;
    // Start is called before the first frame update
    void Start() {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate() {
        movement.move(horizontalMove * Time.fixedDeltaTime);;
    }
}
