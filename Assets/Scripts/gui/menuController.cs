#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class menuController : MonoBehaviour
{
    #region Internal variable declarations
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
    private Vector2 currentSize;
    private Vector2 targSize;
    private Vector2 fullSize;
    private bool animDone = false;
    private bool animToggle = true;
    private bool tabToggle = true;
    private string animState = "full";
    #endregion

    #region Configuration variable declarations
    public string title = "Menu window";
    #endregion

    // Start is called before the first frame update
    void Start() {
        #region Set variables
        backing = this.gameObject.transform.GetChild(0).gameObject;
        xpos = transform.position.x;
        ypos = transform.position.y;
        targx = transform.position.x;
        targy = transform.position.y;

        xcurpos = Input.mousePosition.x;
        ycurpos = Input.mousePosition.y;
        canvas = GameObject.Find("Canvas");

        currentSize = new Vector2(0, 0);
        fullSize = new Vector2(backing.GetComponent<RectTransform>().rect.width,
                               backing.GetComponent<RectTransform>().rect.height);
        targSize = fullSize;
        #endregion

        transform.SetParent(canvas.transform);
        backing.transform.GetChild(0).gameObject.GetComponent<Text>().text = title;
    }

    // Update is called once per frame
    void Update() {
        Movement();
        ActionAnimate();
        Visulization();
        controlButtons();
    }

    // Control the tab and close buttons
    void controlButtons() {
        if (animState != "close") {
            if (backing.transform.GetChild(1).gameObject.GetComponent<betterButton>().PubIsPressed()) {
                animState = "close";
                targSize = new Vector2(0, 0);
            } else if (backing.transform.GetChild(2).gameObject.GetComponent<betterButton>().PubIsPressed()) {
                if ((animState == "full") & (tabToggle == true)) {
                    animState = "tab";
                    targSize = new Vector2(80, 14f);
                } else {
                    animState = "full";
                    targSize = fullSize;
                }
                tabToggle = false;
            } else {
                tabToggle = true;
            }
        }
    }

    // Deal with the appearance of the menu
    void Visulization() {
        if (animToggle != animDone) {
            animToggle = animDone;
            if (animDone) {
                switch (animState) {
                    case "full":
                        foreach (Transform child in backing.transform) {
                            child.gameObject.SetActive(true);
                        }
                        backing.transform.GetChild(0).gameObject.GetComponent<Text>().text = title;
                        break;
                    case "tab":
                        if (title.Length >= 8) {
                            backing.transform.GetChild(0).gameObject.GetComponent<Text>().text = title.Substring(0, 6) + "...";
                        }
                        backing.transform.GetChild(0).gameObject.SetActive(true);
                        backing.transform.GetChild(1).gameObject.SetActive(true);
                        backing.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case "close":
                        Destroy(gameObject);
                        break;
                    }
            } else {
                foreach (Transform child in backing.transform) {
                    switch (animState) {
                        case "full":
                            child.gameObject.SetActive(false);
                            break;
                        case "tab":
                            child.gameObject.SetActive(false);
                            break;
                        case "close":
                            child.gameObject.SetActive(false);
                            break;
                    }
                }
            }
        }
    }

    // Smoothly animate opening, closing, and size changes
    void ActionAnimate() {
        if (currentSize != targSize) {
            animDone = true;

            if (currentSize.x < targSize.x - 5f) {
                currentSize.x += Time.deltaTime * 300;
                animDone = false;
            } else if (currentSize.x > targSize.x + 5f) {
                currentSize.x -= Time.deltaTime * 300;
                animDone = false;
            } else {
                currentSize.x = targSize.x;
            }

            if (currentSize.y < targSize.y - 5f) {
                currentSize.y += Time.deltaTime * 300;
                animDone = false;
            } else if (currentSize.y > targSize.y + 5f) {
                currentSize.y -= Time.deltaTime * 300;
                animDone = false;
            } else {
                currentSize.y = targSize.y;
            }

            backing.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize.x);
            backing.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize.y);
        }
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
            } else if (targy < 34) {
                targy = 34;
            }
        }
    }

    // Set up interactions
    public void Initalize(object[] elements) {
        foreach (object[] i in elements) {
            switch (i[0]) {
                case "test":
                    break;
            }
        }
    }
}
