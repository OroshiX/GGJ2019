using UnityEngine;

public class Displayable : InteractableObject {

    [SerializeField]
    private GameObject toDisplay;
    [SerializeField]
    private GameObject toRemoveOnDisplay;

    public GameObject getToDisplay() {
        return toDisplay;
    }
}
