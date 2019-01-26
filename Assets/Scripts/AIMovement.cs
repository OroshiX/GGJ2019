using UnityEngine;

public class AIMovement : MonoBehaviour {

    private Movement movement;

    [SerializeField]
    private Vector2 leftBottom, rightTop;

    private Vector2 forwardDirection;

    void Start() {
        movement = GetComponent<Movement>();
        InvokeRepeating("AIMoving", 0f, 3.0f);
    }

    void AIMoving() {
        float random = Random.value;
        if (random < 0.3f)
            movement.move(60f, 0f);
            
        else if (random < 0.6f)
            movement.move(-60f, 0f);
        else
            movement.move(0f, 0f);
    }
}
