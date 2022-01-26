using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transformController:MonoBehaviour {
    public GameObject target;
    private Transform targtransf;

    public InputField xpos;
    public InputField ypos;
    public InputField zpos;
    public InputField xsca;
    public InputField ysca;
    public InputField zsca;
    public InputField xrot;
    public InputField yrot;
    public InputField zrot;

    private Vector3 pos = new Vector3();
    private Vector3 sca = new Vector3();
    private Vector3 rot = new Vector3();

    // Start is called before the first frame update
    void Start() {
        targtransf = target.GetComponent<Transform>();

        pos = targtransf.localPosition;
        sca = targtransf.localScale;
        rot = targtransf.localEulerAngles;

        xpos.text = pos.x.ToString();
        ypos.text = pos.y.ToString();
        zpos.text = pos.z.ToString();
        xsca.text = sca.x.ToString();
        ysca.text = sca.y.ToString();
        zsca.text = sca.z.ToString();
        xrot.text = rot.x.ToString();
        yrot.text = rot.y.ToString();
        zrot.text = rot.z.ToString();
    }

    // Update is called once per frame
    void Update() {
        Vector3 poscheck = new Vector3(float.Parse(xpos.text), float.Parse(ypos.text), float.Parse(zpos.text));

        if (poscheck == pos) {
            if (pos != targtransf.localPosition) {
                pos = targtransf.localPosition;
                xpos.text = pos.x.ToString();
                ypos.text = pos.y.ToString();
                zpos.text = pos.z.ToString();
            }
        } else {
            pos = poscheck;
            targtransf.localPosition = poscheck;
        }

        Vector3 scacheck = new Vector3(float.Parse(xsca.text), float.Parse(ysca.text), float.Parse(zsca.text));

        if (scacheck == sca) {
            if (sca != targtransf.localScale) {
                sca = targtransf.localScale;
                xsca.text = sca.x.ToString();
                ysca.text = sca.y.ToString();
                zsca.text = sca.z.ToString();
            }
        } else {
            sca = scacheck;
            targtransf.localScale = scacheck;
        }

        Vector3 rotcheck = new Vector3(float.Parse(xrot.text), float.Parse(yrot.text), float.Parse(zrot.text));

        if (rotcheck == rot) {
            if (rot != targtransf.localEulerAngles) {
                rot = targtransf.localEulerAngles;
                xrot.text = rot.x.ToString();
                yrot.text = rot.y.ToString();
                zrot.text = rot.z.ToString();
            }
        } else {
            rot = rotcheck;
            targtransf.localEulerAngles = rotcheck;
        }
    }
}
