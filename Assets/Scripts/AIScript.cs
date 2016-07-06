using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

	public GameObject player;
	PlayerController script;
	ArrayList ball_list;
	float distination = 5;

	// Use this for initialization
	void Start () {
		ball_list = new ArrayList();
		script = player.GetComponent<PlayerController> ();
	}

	// Update is called once per frame
	void Update () {
		float min_time = 9999;
		distination = 5;
		if (Score.GAME || player != null) {
			if (ball_list.Count == 0) {
			} else {
				float distance = 0;
				if (script.yoko && script.player_mode == 2) {
					foreach (GameObject ball in ball_list) {
						distance = Abs (ball.transform.position.z - player.transform.position.z);
						Rigidbody rb = ball.GetComponent<Rigidbody> ();
						float v = rb.velocity.z;
						float time = Abs(distance / v);
						if (v > 0) {
							if (time < min_time) {

								float tmp = ball.transform.position.x + rb.velocity.x * time;

								if ((-0.5f < tmp && tmp < 0.5f) || (9.5f < tmp && tmp < 10.5f)) { //壁に当たる

								} else {
									if (time > 2.2f) {

									} else if (time + 0.2f < (Abs (tmp - player.transform.position.x) / script.speed)) { //追いつかない
										if (!player.GetComponent<PlayerController> ().attackmode) {
											distination = tmp;
											min_time = time;

										}
									} else {
										distination = tmp;
										min_time = time;
									}
								}
							}
						}

						if (distination > 10.0f) {
							float sa = distination- 10.0f;
							distination = 10.0f - sa;
						} else if (distination < 0) {
							float sa = 0 - distination;
							distination = 0 + sa;
						}
					}
					if (Abs (player.transform.position.x - distination) < 0.15f) {
						Stop ();
					} else if (player.transform.position.x <= distination) {
						MoveRight ();
					} else {
						MoveLeft ();
					}
				} else if(!script.yoko) {
					foreach (GameObject ball in ball_list) {
						distance = Abs (ball.transform.position.x - player.transform.position.x);
						Rigidbody rb = ball.GetComponent<Rigidbody> ();
						float v = rb.velocity.x;
						float time = Abs(distance / v);
						if ((v < 0 && script.player_mode == 3) || (v > 0 && script.player_mode == 4)) {
							if (time < min_time) {

								float tmp = ball.transform.position.z + rb.velocity.z * time;

								if ((-0.5f < tmp && tmp < 0.5f) || (9.5f < tmp && tmp < 10.5f)) { //壁に当たる

								} else {
									if (time > 2.2f) {

									} else if (time + 0.2f < (Abs (tmp - player.transform.position.z) / script.speed)) { //追いつかない
										if (!player.GetComponent<PlayerController> ().attackmode) {
											distination = tmp;
											min_time = time;

										}
									} else {
										distination = tmp;
										min_time = time;
									}
								}

							}
						}

						if (distination > 10.0f) {
							float sa = distination - 10.0f;
							distination = 10.0f - sa;
						} else if (distination < 0) {
							float sa = 0 - distination;
							distination = 0 + sa;
						}
					}
					if (Abs (player.transform.position.z - distination) < 0.15f) {
						Stop ();
					} else if (player.transform.position.z <= distination) {
						if (script.player_mode == 3) {

							MoveLeft ();
						} else {
							MoveRight ();
						}
					} else {
						if (script.player_mode == 3) {
							MoveRight ();
						} else {
							MoveLeft ();
						}
					}
				}
			}
		}

	}
	public void Add(GameObject ball){
		ball_list.Add (ball);
	}
	public void Remove(GameObject ball){
		ball_list.Remove(ball);
	}
	private float Abs(float n){
		if (n < 0)
			return -n;
		return n;
	}
	private void MoveLeft(){

		script.left = true;
		script.right = false;

	}
	private void MoveRight(){
		script.left = false;
		script.right = true;

	}
	private void Stop(){
		script.left = true;
		script.right = true;
	}
}
