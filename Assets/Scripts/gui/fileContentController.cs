using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class fileContentController:MonoBehaviour {

    public Sprite[] icons;
    public Image icon;
    public Text label;
    public betterButton button;
    public string path;

    public void populate(string set) {
        path = set;
        label.text = Path.GetFileName(path);
        if (label.text == "") label.text = path;
        if (path == "c:\\" || path == "C:\\" || path == "c:/" || path == "C:/") {
            icon.sprite = icons[1];
        } else if (path.Length == 3) {
            icon.sprite = icons[0];
        } else if (Directory.Exists(path)) {
            icon.sprite = icons[2];
        } else {
            string ext = Path.GetExtension(path);
            switch (ext) {
                case ".mid":
                case ".midi":
                    icon.sprite = icons[5];
                    break;
                case ".obj":
                    icon.sprite = icons[6];
                    break;
                default:
                    icon.sprite = icons[4];
                    break;
            }
        }
    }
}
