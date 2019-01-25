using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    private bool pausedMovement = false;

    [SerializeField]
    private Vector2 leftBottom, rightTop;

    private Vector2 forwardDirection;

    [SerializeField]
    private float maxTimeTillChangeDir = 5f;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
