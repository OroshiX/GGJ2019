using UnityEngine;

public class Passage : MonoBehaviour {
    [SerializeField]
    protected bool canGo = false;

    [SerializeField]
    protected Transform nextRoom;

    public bool canWeGo() {
        return canGo;
    }

    public Transform getNextRoom() {
        return nextRoom;
    }

    public void unblock() {
        canGo = true;
    }
}
