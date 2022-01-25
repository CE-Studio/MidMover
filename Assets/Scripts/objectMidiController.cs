using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

public class objectMidiController:MonoBehaviour {
    public static readonly System.Type[] typelist = new System.Type[] {typeof(bool), typeof(int), typeof(uint), typeof(float), typeof(Vector2), typeof(Vector2Int), typeof(Vector3), typeof(Vector3Int), typeof(Vector4), typeof(string)};

    private GameObject parent;
    private Component[] parentComponents;
    private List<PropertyInfo> keys = new List<PropertyInfo>();
    private List<string> readableKeyNames = new List<string>();
    private List<int> componentRefs = new List<int>();

    //Get every (writable) field from every component of the parent object
    void scrapeFields() {
        int j = 0;
        foreach (Component i in parentComponents) {
            PropertyInfo[] testinfo = i.GetType().GetProperties();
            foreach (PropertyInfo h in testinfo) {
                if (h.CanWrite && typelist.Contains(h.PropertyType)) {
                    readableKeyNames.Add(i.GetType().ToString() + " -> " + h.Name);
                    keys.Add(h);
                    componentRefs.Add(j);
                }
            }
            j++;
        }
    }

    // Start is called before the first frame update
    void Start() {
        parent = transform.gameObject;
        parentComponents = parent.GetComponents(typeof(Component));

        scrapeFields();
        
    }

    // Update is called once per frame
    void Update() {

    }
}
