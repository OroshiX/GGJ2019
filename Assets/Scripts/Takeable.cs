using UnityEngine;

public class Takeable : InteractableObject {
    [SerializeField]
    private TypeObject type;

    [SerializeField]
    private Sprite carriedDark, carriedLight;

    public TypeObject getTheType() {
        return type;
    }

}
