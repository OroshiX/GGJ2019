using UnityEngine;

public class InteractableObject : MonoBehaviour {

    [SerializeField]
    private float sanityEffect = 0f;


    // Start is called before the first frame update
    void Start() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(Tags.PLAYER)) {
            if (Input.GetKeyDown(KeyMapping.mainAction)) {
                // TODO open/close/take/release

            } else if (Input.GetKeyDown(KeyMapping.secondaryAction)) {
                // TODO launch, ...
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
