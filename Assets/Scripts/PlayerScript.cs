using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	Rigidbody rb;
	Vector3 pre_pos;
	Vector3 vel;

	Vector3 distination;

	public float power = 0.7f;
	public float speed = 0.1f;

	void Awake(){
		rb = GetComponent<Rigidbody> ();


	}

	// Use this for initialization
	void Start () {
		pre_pos = transform.position;
		vel = Vector3.zero;
		distination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {



		transform.position = Vector3.Lerp(transform.position, distination ,Time.deltaTime*speed);

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			distination += Vector3.forward;

		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			distination += Vector3.left;

		}if (Input.GetKeyDown (KeyCode.RightArrow)) {
			distination += Vector3.right;

		}if (Input.GetKeyDown (KeyCode.DownArrow)) {
			distination += Vector3.back;

		}

	}

	void LateUpdate(){
		Vector3 v = transform.position - pre_pos;
		//rb.velocity = v / Time.deltaTime;
		vel = v / Time.deltaTime;
		pre_pos = transform.position;
	}

	void FixUpdate(){
		


	}

	void OnCollisionEnter(Collision col){
		
		if (col.gameObject.tag == "Ball") {
			
			Vector3 normal = (col.gameObject.transform.position-col.contacts [0].point).normalized;
			Debug.Log ("normal:"+normal);
			Vector3 new_Vec;
			if (col.rigidbody.velocity.sqrMagnitude > 0.01f) {
				new_Vec = col.rigidbody.velocity + 2.0f * Vector3.Dot (col.rigidbody.velocity, normal) * normal;
			} else {
				new_Vec = normal;
			}
			Debug.Log ("new_Vec:"+new_Vec);
			new_Vec = new_Vec.normalized;
			float vec2 = Vector3.Dot (vel,new_Vec);
			col.rigidbody.velocity = new_Vec * vec2 * power;
		}

	}
}
