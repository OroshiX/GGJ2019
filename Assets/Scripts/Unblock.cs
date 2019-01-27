using System.Collections.Generic;
using UnityEngine;

public class Unblock : MonoBehaviour {
    [SerializeField]
    private List<Passage> toUnblock;

    public void unblock() {
        foreach (var passage in toUnblock) {
            passage.unblock();
        }
        toUnblock.Clear();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tags.PLAYER) && toUnblock != null && toUnblock.Count > 0) {
            unblock();
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
