using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform oldParent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(1,1,1);
        transform.parent = oldParent;
    }
}
