using UnityEditor.Animations;

public class Launchable : InteractableObject {
    private AnimatorController animatorController;

    private float speed = 3f;

    new void Start() {
        animatorController = GetComponent<AnimatorController>();
    }

    public void launch() {
        //TODO
    }
}
