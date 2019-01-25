using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody2D mRigidBody;
    private bool faceLeft;
    [SerializeField]
    private float moveSpeed = 10f;
    private Vector2 velocity = Vector2.zero;

    void Awake() {
        mRigidBody = GetComponent<Rigidbody2D>();
    }

    public void move(float moveX) {
        var targetVelocity = new Vector2(moveX * moveSpeed, 0f);
        mRigidBody.velocity = Vector2.SmoothDamp(mRigidBody.velocity, targetVelocity, ref velocity, 0f);
        if (moveX < 0f && faceLeft) {
            flip();
        } else if (moveX > 0f && !faceLeft) {
            flip();
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void flip() {
        faceLeft = !faceLeft;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
