#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class menuMove : MonoBehaviour
{
    #region Variable declarations
    private GameObject backing;
    private float xpos;
    private float ypos;
    private float xvel;
    private float yvel;
    private float xcurpos;
    private float ycurpos;
    private float xcurvel;
    private float ycurvel;
    private float targx;
    private float targy;
    private GameObject canvas;
    #endregion

    // Start is called before the first frame update
    void Start() {
        backing = this.gameObject.transform.GetChild(0).gameObject;
        xpos = transform.position.x;
        ypos = transform.position.y;
        targx = transform.position.x;
        targy = transform.position.y;

        xcurpos = Input.mousePosition.x;
        ycurpos = Input.mousePosition.y;
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update() {
        Movement();
        ActionAnimate();
    }

    // Smoothly animate opening, closing, and size changes
    void ActionAnimate() {

    }

    // Move menus with warp animation
    void Movement() {
        xvel = xpos - transform.position.x;
        yvel = ypos - transform.position.y;
        xpos = transform.position.x;
        ypos = transform.position.y;

        xcurvel = xcurpos - Input.mousePosition.x;
        ycurvel = ycurpos - Input.mousePosition.y;
        xcurpos = Input.mousePosition.x;
        ycurpos = Input.mousePosition.y;

        transform.rotation = Quaternion.Euler(0, 0, (xvel * 0.2f) / -2f);
        backing.transform.rotation = Quaternion.Euler(0, 0, (xvel * 0.2f));
        backing.transform.localScale = new Vector3(0.5f, (-yvel * 0.01f) + 1, 0);

        transform.position += new Vector3((targx - transform.position.x) * 0.1f, (targy - transform.position.y) * 0.1f, 0);

        if (backing.GetComponent<betterButton>().PubIsPressed()) {
            targx -= xcurvel;
            targy -= ycurvel;
            if (targx > Camera.main.pixelRect.width) {
                targx = Camera.main.pixelRect.width;
            } else if (targx < 0) {
                targx = 0;
            }
            if (targy > Camera.main.pixelRect.height) {
                targy = Camera.main.pixelRect.height;
            } else if (targy < 27) {
                targy = 27;
            }
        }
    }
}
