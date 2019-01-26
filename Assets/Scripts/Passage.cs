using UnityEngine;

public class Passage : MonoBehaviour {
    [SerializeField]
    protected bool canGo = false;

    public bool canWeGo() {
        return canGo;
    }

    public void unblock() {
        canGo = true;
    }
}
