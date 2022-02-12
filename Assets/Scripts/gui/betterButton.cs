using UnityEngine.UI;

public class betterButton:Button {

    private bool edgeTracker = false;

    public bool PubIsPressed() {
        return IsPressed();
    }

    public bool justPressed {
        get {
            bool i = IsPressed();
            if (i == edgeTracker) {
                return (false);
            } else {
                edgeTracker = i;
                return (i);
            }
        }
    }
}
