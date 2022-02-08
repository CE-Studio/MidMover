using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsStorage:MonoBehaviour {

    [Serializable]
    public struct settingsGlobal {
        public Vector2 playbarPos;
        public bool playbarShape;
        public Vector2 titlebarPos;
        public bool titlebarShape;
        public float camPanScale;
        public float camTransScale;
    }

    public static settingsGlobal globalSettings;

    void Awake() {
        if (File.Exists("Settings.json")) {
            string savedata = File.ReadAllText("Settings.json");
            globalSettings = JsonUtility.FromJson<settingsGlobal>(savedata);
        } else {

        }
    }

    // Update is called once per frame
    void Update() {

    }

    void OnApplicationQuit() {
        string savedata = JsonUtility.ToJson(globalSettings);
        File.WriteAllText("Settings.json", savedata);
    }
}
