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
        public string lastPath;
    }

    public static settingsGlobal globalSettings;

    public menuController titlebar;
    public menuController playbar;

    void Awake() {
        if (File.Exists("Settings.json")) {
            string savedata = File.ReadAllText("Settings.json");
            globalSettings = JsonUtility.FromJson<settingsGlobal>(savedata);

            playbar.transform.position = globalSettings.playbarPos;
            titlebar.transform.position = globalSettings.titlebarPos;
        } else {
            globalSettings.lastPath = "./";
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void OnApplicationQuit() {

        globalSettings.playbarPos = new Vector2(playbar.targx, playbar.targy);

        globalSettings.titlebarPos = new Vector2(titlebar.targx, titlebar.targy);

        if (globalSettings.lastPath == "") {
            globalSettings.lastPath = "./";
        }

        string savedata = JsonUtility.ToJson(globalSettings, true);
        File.WriteAllText("Settings.json", savedata);
    }
}
