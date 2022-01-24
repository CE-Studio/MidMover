using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class selectionController:MonoBehaviour {
    Camera cam;
    GameObject selected;
    bool lastclick = false;
    GameObject latestMenu;

    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Ray clickray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            lastclick = Physics.Raycast(clickray, out hit);
            if (lastclick) {
                selected = hit.transform.gameObject;
            }
            print(lastclick);
        } else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()) {
            if (lastclick) {
                latestMenu = Instantiate(Resources.Load("Prefabs/menubox"), Input.mousePosition, Quaternion.identity) as GameObject;
            }
        }
    }
}
