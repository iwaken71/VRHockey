using UnityEngine;
using System.Collections;

public class ballScript : MonoBehaviour {

	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody> ();

	}

	// Use this for initialization
	void Start () {
		//rb.velocity = new Vector3 (0,0,-5);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
