using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {
        private float speed = 20f;
        private bool crawling = true;
    GameObject go;

// Use this for initialization
void Start () {

   

        }
	
	// Update is called once per frame
	void Update () {

            if (!crawling)
                return;
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (gameObject.transform.position.y > 1500)
            {
                crawling = false;
            }
        }
    
}
