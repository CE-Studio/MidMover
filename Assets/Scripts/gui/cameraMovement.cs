﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class cameraMovement:MonoBehaviour {
    private float lastmousex;
    private float lastmousey;
    private float mousemovex;
    private float mousemovey;

    public float mousePanScale = 1;
    public float mouseTranslateScale = 1;

    // Start is called before the first frame update
    void Start() {
        lastmousex = Input.mousePosition.x;
        lastmousey = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update() {
        mousemovex = (lastmousex - Input.mousePosition.x) / -4;
        mousemovey = (lastmousey - Input.mousePosition.y) / 4;
        lastmousex = Input.mousePosition.x;
        lastmousey = Input.mousePosition.y;

        if (!Application.isFocused) return;

        if (Input.GetMouseButton(2)) {
            if (Input.GetKey("left shift")) {
                transform.position += transform.up * ((mousemovey * mouseTranslateScale) / 16);
                transform.position += transform.right * ((mousemovex * mouseTranslateScale) / -16);
            } else {
                transform.Rotate(0, (mousemovex * mousePanScale), 0, Space.World);
                transform.Rotate((mousemovey * mousePanScale), 0, 0);
            }
        }

        if (EventSystem.current.IsPointerOverGameObject()) return;

        transform.position += transform.forward * (Input.mouseScrollDelta.y * 0.1f);

        if (Input.GetKey("w")) {
            transform.position += transform.forward * (Time.deltaTime * 4f);
        }
        if (Input.GetKey("a")) {
            transform.position += transform.right * (Time.deltaTime * -4f);
        }
        if (Input.GetKey("s")) {
            transform.position += transform.forward * (Time.deltaTime * -4f);
        }
        if (Input.GetKey("d")) {
            transform.position += transform.right * (Time.deltaTime * 4f);
        }
        if (Input.GetKey("q")) {
            transform.Rotate(0, 0, Time.deltaTime * 16f);
        }
        if (Input.GetKey("e")) {
            transform.Rotate(0, 0, Time.deltaTime * -16f);
        }
        if (Input.GetKey("r")) {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
