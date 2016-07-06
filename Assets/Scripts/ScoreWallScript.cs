using UnityEngine;
using System.Collections;

public class ScoreWallScript : MonoBehaviour {

	public int id;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Ball") {
			if (id == 1) {
				GameManager.instance.AddScore (2);

			} else if (id == 2) {
				GameManager.instance.AddScore (1);
			}
			Destroy (col.gameObject);

		} 

	}
}
