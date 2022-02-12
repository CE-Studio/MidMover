using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class fileController:MonoBehaviour {

    private static readonly string[] shortcuts = {"./",
                                                  "%userprofile%",
                                                  "%userprofile%/Downloads",
                                                  "%userprofile%/Documents",
                                                  "%userprofile%/Pictures",
                                                  "%userprofile%/Music",
                                                  "%userprofile%/Videos"};

    private bool focustrack = false;

    public InputField pathBox;
    public InputField fileBox;
    public betterButton openButton;
    public betterButton[] sideButtons;
    public string path;

    // Start is called before the first frame update
    void Start() {
        cd(settingsStorage.globalSettings.lastPath);
    }

    // Update is called once per frame
    void Update() {
        if (!(pathBox.isFocused == focustrack)) {
            if (pathBox.isFocused) {
                focustrack = true;
            } else {
                focustrack = false;
                cd(pathBox.text);
            }
        }

        for (int i = 0; i < 7; i++) {
            if (sideButtons[i].justPressed) {
                cd(shortcuts[i]);
            }
        }
    }

    private void cd(string toGo) {
        string tp = Path.GetFullPath(System.Environment.ExpandEnvironmentVariables(toGo));
        if (!Directory.Exists(tp)) { 
            print("Invalid path " + tp);
            pathBox.text = path;
            return; 
        }
        path = tp;
        pathBox.text = tp;
        settingsStorage.globalSettings.lastPath = tp;
    }
}
