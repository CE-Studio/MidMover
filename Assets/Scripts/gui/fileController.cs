using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class fileController:MonoBehaviour {

    private static readonly string[] shortcuts = {"./",
                                                  "%userprofile%",
                                                  "%userprofile%/Downloads",
                                                  "%userprofile%/Documents",
                                                  "%userprofile%/Pictures",
                                                  "%userprofile%/Music",
                                                  "%userprofile%/Videos"};

    private static bool mustLoadPrefabs = true;
    private static UnityEngine.Object listitem;

    private bool focustrack = false;

    public InputField pathBox;
    public InputField fileBox;
    public betterButton openButton;
    public betterButton[] sideButtons;
    public Transform contentContainer;
    public List<fileContentController> content;
    public string path;
    public string filetype = "midi";

    void Awake() {
        if (mustLoadPrefabs) {
            listitem = Resources.Load("Prefabs/fileContent");
            mustLoadPrefabs = false;
        }
    }

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

        if (sideButtons[7].justPressed) {
            DriveInfo[] h = DriveInfo.GetDrives();
            List<string> names = new List<string>();
            foreach (DriveInfo i in h) {
                names.Add(i.Name);
            }
            populateContent(names.ToArray());
        }

        foreach (fileContentController i in content) {
            if (i.button.justPressed) {
                if (i.isfolder) {
                    cd(i.path);
                } else {
                    fileBox.text = i.filename;
                }
            }
        }

        if (openButton.justPressed) {
            if (filetype == "midi") {
                globalMidiController.loadFile(path + fileBox.text);
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

        string[] dirs;
        string[] files;

        try {
            dirs = Directory.GetDirectories(tp);
            files = Directory.GetFiles(tp);
        } catch {
            print("No access to " + tp);
            pathBox.text = path;
            return;
        }

        string[] h = { tp + "/.." };

        populateContent(h.Concat(dirs.Concat(files).ToArray()).ToArray());
        path = tp;
        pathBox.text = tp;
        settingsStorage.globalSettings.lastPath = tp;

    }

    private void populateContent(string[] items) {
        foreach (fileContentController i in content) {
            Destroy(i.gameObject);
        }
        content = new List<fileContentController>();
        int h = 0;
        foreach (string i in items) {
            GameObject j = Instantiate(listitem) as GameObject;
            j.transform.SetParent(contentContainer);
            j.transform.localScale = Vector3.one;
            j.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -h);
            content.Add(j.GetComponent<fileContentController>());
            content[content.Count - 1].populate(i);
            h += 12;
        }
        contentContainer.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
    }
}
