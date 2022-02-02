#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class menuController:MonoBehaviour {
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
    private bool mustRunInit = true;
    private Camera maincam;

    public Transform content;
    public List<GameObject> items = new List<GameObject>();
    #endregion

    #region Configuration variable declarations
    public string title = "Menu window";
    public string mode = "none";
    public GameObject linkedobj;
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

        maincam = Camera.main;
        #endregion

        transform.SetParent(canvas.transform);
        transform.localScale = new Vector3(2, 1, 1);
        backing.transform.GetChild(0).gameObject.GetComponent<Text>().text = title;

        if (targx > maincam.pixelRect.width) {
            targx = maincam.pixelRect.width;
        } else if (targx < 0) {
            targx = 0;
        }
        if (targy > maincam.pixelRect.height) {
            targy = maincam.pixelRect.height;
        } else if (targy < 34) {
            targy = 34;
        }
    }

    // Update is called once per frame
    void Update() {
        if (mustRunInit) {
            Initalize();
            mustRunInit = false;
        }

        Movement();
        controlButtons();
        ActionAnimate();
        Visulization();
    }

    // Control the tab and close buttons
    void controlButtons() {
        switch (mode) {
            case "playbar":
            case "titlebar":
                if (targx >= maincam.pixelRect.width) {
                    reshapeVert();
                } else if (targx <= 0) {
                    reshapeVert();
                } else if (targy >= maincam.pixelRect.height) {
                    reshapeHoriz();
                } else if (targy <= 34) {
                    reshapeHoriz();
                }
                break;
            default:
                if (animState == "close") break;
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
                break;
        }
    }

    private void reshapeVert() {
        if (animState == "vert") return;
        animState = "vert";

        if (mode == "titlebar") {
            targSize = new Vector2(27, 63);
            backing.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(13.5f, -19.5f, 0);
            backing.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(13.5f, -31.5f, 0);
            backing.transform.GetChild(4).GetComponent<RectTransform>().anchoredPosition = new Vector3(7.5f, -43.5f, 0);
            backing.transform.GetChild(5).GetComponent<RectTransform>().anchoredPosition = new Vector3(19.5f, -43.5f, 0);
            backing.transform.GetChild(6).GetComponent<RectTransform>().anchoredPosition = new Vector3(13.5f, -55.5f, 0);
        } else {

        }
    }

    private void reshapeHoriz() {
        if (animState == "horiz") return;
        animState = "horiz";

        if (mode == "titlebar") {
            targSize = new Vector2(111, 15);
            backing.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(37.5f, -7.5f, 0);
            backing.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(61.5f, -7.5f, 0);
            backing.transform.GetChild(4).GetComponent<RectTransform>().anchoredPosition = new Vector3(79.5f, -7.5f, 0);
            backing.transform.GetChild(5).GetComponent<RectTransform>().anchoredPosition = new Vector3(91.5f, -7.5f, 0);
            backing.transform.GetChild(6).GetComponent<RectTransform>().anchoredPosition = new Vector3(103.5f, -7.5f, 0);
        } else {

        }
    }

    // Deal with the appearance of the menu
    void Visulization() {
        if (animToggle != animDone) {
            animToggle = animDone;
            if (animDone) {
                switch (animState) {
                    case "horiz":
                    case "vert":
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
                        case "horiz":
                        case "vert":
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
            if (targx > maincam.pixelRect.width) {
                targx = maincam.pixelRect.width;
            } else if (targx < 0) {
                targx = 0;
            }
            if (targy > maincam.pixelRect.height) {
                targy = maincam.pixelRect.height;
            } else if (targy < 34) {
                targy = 34;
            }
        }
    }

    // Set up interactions
    public void Initalize() {
        switch (mode) {
            case "config":
                items.Add(Instantiate(Resources.Load("Prefabs/transformPanel"), new Vector3(), Quaternion.identity) as GameObject);
                items[items.Count - 1].transform.SetParent(content, false);
                items[items.Count - 1].transform.localPosition = new Vector3(0, -68, 0);
                items[items.Count - 1].GetComponent<transformController>().target = linkedobj;

                content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 144.5f);
                content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 68);
                break;
            case "titlebar":
                targSize = new Vector2(111, 15);
                break;
        }
    }
}
