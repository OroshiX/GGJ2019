using System.Collections.Generic;
using UnityEngine;

public class Unblock : MonoBehaviour {
    [SerializeField]
    private List<Passage> toUnblock;

    public void unblock() {
        foreach (var passage in toUnblock) {
            passage.unblock();
        }
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
