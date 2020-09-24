using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float originRotate;
    private int noteRangeMin = 13;
    private int noteRangeMax = 49;
    private float angleRangeMin = -75;
    private float angleRangeMax = 75;
    private int currentNote;
    private float noteRange;
    private float angleRange;
    private float finalRange;
    private float degreesToRotate;
    private bool done;
    private float smooth = 30.0f;
    private Quaternion target;
    public TextMesh text;
    private int currentNoteValue;
    private int octave;
    private string noteDisplay;
    public GameObject laser;
    private Vector3 laserActive;
    private Vector3 laserInactive;

    void Start()
    {
        originRotate = transform.rotation.z;
        noteRange = noteRangeMax - noteRangeMin;
        angleRange = angleRangeMax + Mathf.Abs(angleRangeMin);
        finalRange = angleRange / noteRange;
        laserActive = new Vector3(0.25f, 5, 0.25f);
        laserInactive = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (currentNote == 9999)
        {
            done = true;
        }
        if (GlobalNoteTracker.beat != 0 && !done)
        {
            currentNote = GlobalNoteTracker.Track1[GlobalNoteTracker.beat - 1];
            smooth = 30f;
            laser.transform.localScale = laserActive;
        }
        if (GlobalNoteTracker.beat == 0 || currentNote == 0)
        {
            target = Quaternion.Euler(0, 180, originRotate);
            text.text = "Not playing";
            smooth = 10f;
            laser.transform.localScale = laserInactive;
        }
        else if (done)
        {
            target = Quaternion.Euler(0, 180, originRotate);
            text.text = "Not playing";
            smooth = 10f;
            laser.transform.localScale = laserInactive;
        }
        else
        {
            if (currentNote != 9999)
            {
                degreesToRotate = angleRangeMin + (finalRange * (currentNote - noteRangeMin));
                target = Quaternion.Euler(0, 180, degreesToRotate);
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        if (!done)
        {
            currentNoteValue = currentNote;
            octave = 0;
            while (currentNoteValue > 12)
            {
                currentNoteValue = currentNoteValue - 12;
                octave++;
            }
            switch (currentNoteValue)
            {
                case 1:
                    noteDisplay = "C" + octave;
                    break;
                case 2:
                    noteDisplay = "C#" + octave;
                    break;
                case 3:
                    noteDisplay = "D" + octave;
                    break;
                case 4:
                    noteDisplay = "D#" + octave;
                    break;
                case 5:
                    noteDisplay = "E" + octave;
                    break;
                case 6:
                    noteDisplay = "F" + octave;
                    break;
                case 7:
                    noteDisplay = "F#" + octave;
                    break;
                case 8:
                    noteDisplay = "G" + octave;
                    break;
                case 9:
                    noteDisplay = "G#" + octave;
                    break;
                case 10:
                    noteDisplay = "A" + octave;
                    break;
                case 11:
                    noteDisplay = "A#" + octave;
                    break;
                case 12:
                    noteDisplay = "B" + octave;
                    break;
            }
            if (currentNote != 0)
            {
                text.text = noteDisplay;
            }
        }
    }
}
