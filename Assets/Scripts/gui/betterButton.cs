using UnityEngine.UI;

public class betterButton : Button 
 {
    public bool PubIsPressed() {
        return IsPressed();
    }
}
