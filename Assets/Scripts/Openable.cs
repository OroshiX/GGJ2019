public class Openable : InteractableObject {
    private bool open;

    void Start() {
        updateDisplay();
    }

    public void openOrClose() {
        open = !open;
        updateDisplay();
    }

    private void updateDisplay() {
        // TODO
    }

}
