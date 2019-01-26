using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ReleaseArea : MonoBehaviour {
    private Takeable takeable;
    [SerializeField]
    private TypeObject typeObjectToGoHere;
    [SerializeField]
    private Vector3 positionReleasedObject;

    [SerializeField]
    private Sprite renderedArea;

    public void put(Takeable takeable) {
        Debug.Log("Putting takeable " + takeable.name + " in release area");
        if (takeable && takeable.getTheType() == typeObjectToGoHere) {
            takeable.transform.parent = null;
            takeable.GetComponent<SpriteRenderer>().sprite = renderedArea;
            this.takeable = takeable;
            takeable.transform.position = positionReleasedObject;
        }

    }

}
