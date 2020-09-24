using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalNoteTracker : MonoBehaviour
{
    public static List<int> Track1 = new List<int>();
    public float BPMinute;
    public float BPMeasure;
    public float timeUntilBeat;
    public float timer;
    public static int beat;
    
    void Start()
    {
        BPMinute = 128f;
        BPMeasure = 8f;
        Track1.Add(0); //
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0); //
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0); //
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0); //
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(15); //
        Track1.Add(0);
        Track1.Add(15);
        Track1.Add(0);
        Track1.Add(43);
        Track1.Add(43);
        Track1.Add(39);
        Track1.Add(39);
        Track1.Add(0); //
        Track1.Add(0);
        Track1.Add(15);
        Track1.Add(0);
        Track1.Add(43);
        Track1.Add(43);
        Track1.Add(39);
        Track1.Add(39);
        Track1.Add(0); //
        Track1.Add(0);
        Track1.Add(15);
        Track1.Add(0);
        Track1.Add(43);
        Track1.Add(43);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(45); //
        Track1.Add(45);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(46);
        Track1.Add(46);
        Track1.Add(0);
        Track1.Add(0);
        Track1.Add(0); //
        Track1.Add(9999);
        timeUntilBeat = 60 / BPMinute / BPMeasure;
    }

    void FixedUpdate()
    {
        timer = timer + Time.deltaTime;
        if (timer >= timeUntilBeat)
        {
            timer = timer - timeUntilBeat;
            beat++;
        }
        //print(beat + ", " + timer + "/" + timeUntilBeat);
    }
}
