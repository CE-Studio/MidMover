using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class selectionController:MonoBehaviour {
    Camera cam;
    GameObject selected;
    bool lastclick = false;
    GameObject latestMenu;
    Renderer selrend;

    public LineRenderer line;

    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Ray clickray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            lastclick = Physics.Raycast(clickray, out hit);
            line.enabled = lastclick;
            if (lastclick) {
                selected = hit.transform.gameObject;
                selrend = selected.GetComponent<Renderer>();
            }
            print(lastclick);
        } else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()) {
            if (lastclick) {
                latestMenu = Instantiate(Resources.Load("Prefabs/menubox"), Input.mousePosition, Quaternion.identity) as GameObject;
                menuController menu = latestMenu.GetComponent<menuController>();
                menu.title = selected.name;
                menu.linkedobj = selected;
            } else {
                latestMenu = Instantiate(Resources.Load("Prefabs/menubox"), Input.mousePosition, Quaternion.identity) as GameObject;
                menuController menu = latestMenu.GetComponent<menuController>();
                menu.title = "Scene Menu";
            }
        }

        if (lastclick) {
            Bounds box = selrend.bounds;
            print(box.extents);

            #region selection box
            Vector3 point00 = new Vector3(-1, -1, -1);
            Vector3 point01 = new Vector3(-1, -1, 1);
            Vector3 point02 = new Vector3(-1, 1, 1);
            Vector3 point03 = new Vector3(1, 1, 1);
            Vector3 point04 = new Vector3(1, 1, -1);
            Vector3 point05 = new Vector3(1, -1, -1);
            Vector3 point06 = new Vector3(1, -1, 1);
            Vector3 point07 = new Vector3(-1, -1, 1);
            Vector3 point08 = new Vector3(1, -1, -1);
            Vector3 point09 = new Vector3(-1, -1, -1);
            Vector3 point10 = new Vector3(-1, 1, -1);
            Vector3 point11 = new Vector3(-1, 1, 1);
            Vector3 point12 = new Vector3(1, 1, -1);
            Vector3 point13 = new Vector3(-1, 1, -1);
            Vector3 point14 = new Vector3(1, -1, -1);
            Vector3 point15 = new Vector3(1, 1, 1);
            Vector3 point16 = new Vector3(1, -1, 1);
            Vector3 point17 = new Vector3(-1, 1, 1);
            Vector3 point18 = new Vector3(-1, -1, -1);

            point00 = Vector3.Scale(point00, box.extents);
            point01 = Vector3.Scale(point01, box.extents);
            point02 = Vector3.Scale(point02, box.extents);
            point03 = Vector3.Scale(point03, box.extents);
            point04 = Vector3.Scale(point04, box.extents);
            point05 = Vector3.Scale(point05, box.extents);
            point06 = Vector3.Scale(point06, box.extents);
            point07 = Vector3.Scale(point07, box.extents);
            point08 = Vector3.Scale(point08, box.extents);
            point09 = Vector3.Scale(point09, box.extents);
            point10 = Vector3.Scale(point10, box.extents);
            point11 = Vector3.Scale(point11, box.extents);
            point12 = Vector3.Scale(point12, box.extents);
            point13 = Vector3.Scale(point13, box.extents);
            point14 = Vector3.Scale(point14, box.extents);
            point15 = Vector3.Scale(point15, box.extents);
            point16 = Vector3.Scale(point16, box.extents);
            point17 = Vector3.Scale(point17, box.extents);
            point18 = Vector3.Scale(point18, box.extents);

            point00 = point00 + box.center;
            point01 = point01 + box.center;
            point02 = point02 + box.center;
            point03 = point03 + box.center;
            point04 = point04 + box.center;
            point05 = point05 + box.center;
            point06 = point06 + box.center;
            point07 = point07 + box.center;
            point08 = point08 + box.center;
            point09 = point09 + box.center;
            point10 = point10 + box.center;
            point11 = point11 + box.center;
            point12 = point12 + box.center;
            point13 = point13 + box.center;
            point14 = point14 + box.center;
            point15 = point15 + box.center;
            point16 = point16 + box.center;
            point17 = point17 + box.center;
            point18 = point18 + box.center;

            line.SetPosition(00, point00);
            line.SetPosition(01, point01);
            line.SetPosition(02, point02);
            line.SetPosition(03, point03);
            line.SetPosition(04, point04);
            line.SetPosition(05, point05);
            line.SetPosition(06, point06);
            line.SetPosition(07, point07);
            line.SetPosition(08, point08);
            line.SetPosition(09, point09);
            line.SetPosition(10, point10);
            line.SetPosition(11, point11);
            line.SetPosition(12, point12);
            line.SetPosition(13, point13);
            line.SetPosition(14, point14);
            line.SetPosition(15, point15);
            line.SetPosition(16, point16);
            line.SetPosition(17, point17);
            line.SetPosition(18, point18);
            #endregion

        }
    }
}
