using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject target;
    Vector3 offset;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - target.transform.position;
	}
	
	void Update () {
        Vector3 nextpos = target.transform.position + offset;
        transform.position = nextpos;
	}
}
