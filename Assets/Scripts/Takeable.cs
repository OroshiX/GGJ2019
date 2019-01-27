using UnityEngine;

public class Takeable : InteractableObject {
    [SerializeField]
    private TypeObject type;

    [SerializeField]
    private Sprite carriedDark, carriedLight;

    public Sprite getCarriedDark() {
        return carriedDark;
    }

    public Sprite getCarriedLight() {
        return carriedLight;
    }

    public TypeObject getTheType() {
        return type;
    }

}
