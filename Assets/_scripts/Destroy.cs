using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    public float countdown  = 10.0f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, countdown);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
