using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ReleaseArea : MonoBehaviour {
    private Takeable takeable;
    [SerializeField]
    private TypeObject typeObjectToGoHere;
    [SerializeField]
    private Vector2 positionReleasedObject;

    public void put(Takeable takeable) {
        if (!takeable && takeable.getTheType() == typeObjectToGoHere) {
            takeable.transform.parent = null;
            this.takeable = takeable;
            takeable.transform.position = positionReleasedObject;
        }

    }

}
