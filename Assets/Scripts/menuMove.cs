using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuMove : MonoBehaviour
{
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
    public GameObject canvas;
    private RectTransform canvbs;
    // Start is called before the first frame update
    void Start() {
        backing = this.gameObject.transform.GetChild(0).gameObject;
        xpos = transform.position.x;
        ypos = transform.position.y;
        targx = transform.position.x;
        targy = transform.position.y;

        xcurpos = Input.mousePosition.x;
        ycurpos = Input.mousePosition.y;
        canvbs = canvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        xvel = xpos - transform.position.x;
        yvel = ypos - transform.position.y;
        xpos = transform.position.x;
        ypos = transform.position.y;

        xcurvel = xcurpos - Input.mousePosition.x;
        ycurvel = ycurpos - Input.mousePosition.y;
        xcurpos = Input.mousePosition.x;
        ycurpos = Input.mousePosition.y;

        transform.rotation = Quaternion.Euler(0, 0, xvel / -2);
        backing.transform.rotation = Quaternion.Euler(0, 0, xvel);
        backing.transform.localScale = new Vector3(0.5f, (-yvel * 0.1f) + 1, 0);

        transform.position += new Vector3(Time.deltaTime * (targx - transform.position.x) * 8, Time.deltaTime * (targy - transform.position.y) * 8, 0);

        if (backing.GetComponent<betterButton>().PubIsPressed()) {
            targx -= xcurvel;
            targy -= ycurvel;
            if (targx > canvbs.rect.width * 2) {
                targx = canvbs.rect.width * 2;
            } else if (targx < 0) {
                targx = 0;
            }
            if (targy > canvbs.rect.height * 2) {
                targy = canvbs.rect.height * 2;
            } else if (targy < 0) {
                targy = 0;
            }
        }
    }
}
