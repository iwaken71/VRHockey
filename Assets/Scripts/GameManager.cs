using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	int score1,score2;
	GameObject ball;
	public bool VRmode;

	void Awake(){
		if (instance == null) {
			init ();
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	void init(){
		ball = Resources.Load ("ball") as GameObject;
	}

	public void AddScore(int input){
		if (input == 1) {
			score1++;
		}
		if (input == 2) {
			score2++;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			CreateBall ();

		}
	
	}

	void CreateBall(){
		
		GameObject obj = Instantiate (ball) as GameObject;
		obj.transform.position = new Vector3 (-4,0.5f,0);
		float angle = Random.Range (-85.0f,-45.0f)*(Mathf.PI/180.0f);
		float x = Mathf.Cos (angle);
		float z = Mathf.Sin (angle);
		obj.GetComponent<SphereScript> ().SetVelocity (new Vector3(x,0,z)*4);
		//obj.GetComponent<Rigidbody> ().velocity = new Vector3 (2,0,-2);


	}
}
