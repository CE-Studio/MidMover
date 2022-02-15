using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi;

public class globalMidiController:MonoBehaviour {

    public static string midiPath;
    public static MidiFile midi;

    public static bool loadFile(string path) { 
        if (File.Exists(path)) {
            string h = Path.GetExtension(path);
            if (h == ".mid" || h == ".midi") {
                midiPath = path;
                midi = MidiFile.Read(midiPath);
                return (true);
            }
        }
        return (false);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
