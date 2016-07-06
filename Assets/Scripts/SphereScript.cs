using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

	public Vector3 v,a; //速度、加速度
	Vector3 f = Vector3.zero; //外力
	public float boundness = 1; //反発係数
	public float mass; //質量
	Vector3 init_v; //初速度
	public Vector3 gravity = Vector3.down*9.8f; //重力加速度
	public float myu = 0.1f;//動摩擦度係数
	bool is_update = false;
	float radius;

	float const_y;

	// Use this for initialization
	void Start () {
		init_v = v; //初速度
		f = Vector3.zero;
		const_y = transform.position.y;
		radius = transform.localScale.x/2.0f;
	}

	// Update is called once per frame
	void Update () {
		//a = gravity;

	}
	void FixedUpdate(){
		if (!is_update) {
			f += mass * gravity;
			is_update = true;
		}
		a = f/mass; //運動方程式
		v += a* Time.fixedDeltaTime; //加速度の定義より
		transform.position += v * Time.fixedDeltaTime; //速度の定義より
		transform.position += Vector3.up*(const_y - transform.position.y);

		is_update = false;
		f = Vector3.zero;
	}

	// 衝突した時
	void OnCollisionEnter(Collision collision){
		if (!is_update) {
			f += mass * gravity;
			is_update = true;
		}
		for (int i = 0; i < collision.contacts.Length; i++) {
			Vector3 point = collision.contacts [i].point;
			Vector3 n = point - transform.position;
			Vector3 v2 = (Vector3.Dot (v, n) / n.magnitude) * n.normalized;
			Vector3 v1 = v - v2;
			v2 = -v2 * boundness;
			v = v1 + v2;
		}
	}
	void OnCollisionStay(Collision collision){
		if (!is_update) {
			f += mass * gravity;
			is_update = true;
		}



		Vector3 point = collision.contacts [0].point;
		Vector3 n_vec = point - transform.position;

		//重力を面の垂直方向だけ取り出す
		Vector3 ori_f = n_vec.normalized * (Vector3.Dot (mass * gravity, n_vec) / n_vec.magnitude);

		//垂直抗力
		Vector3 N_f = -ori_f;

		// 接触しているので垂直抗力を力に加える。
		f += N_f;

		//次に摩擦力を計算する
		Vector3 vertical_v = (Vector3.Dot (v, n_vec) / n_vec.magnitude) * n_vec.normalized; //接触面に垂直方向のベクトル
		Vector3 horizontal_v = v - vertical_v; //接触面に水平方向のベクトル

		// 動いている場合、摩擦力を加える
		if (Abs (horizontal_v.magnitude) >= 0.001f) {
			f += -horizontal_v.normalized * N_f.magnitude * myu;

		}
		Vector3 distance = (collision.contacts [0].point - transform.position);
		if (distance.magnitude < radius - 0.01f) {
			transform.position = collision.contacts [0].point + collision.contacts [0].normal.normalized * radius;
		}

	}
	void OnCollisionExit(Collision collision){
		if (!is_update) {
			f += mass * gravity;
			is_update = true;
		}
	}

	float Abs(float x){
		return (x >= 0) ? x : -x;
	}

	public void SetVelocity(Vector3 input){
		v = input;
	}
}
