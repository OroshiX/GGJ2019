using UnityEngine;

public class Takeable : InteractableObject {
    [SerializeField]
    private TypeObject type;

    public TypeObject getTheType() {
        return type;
    }

}
