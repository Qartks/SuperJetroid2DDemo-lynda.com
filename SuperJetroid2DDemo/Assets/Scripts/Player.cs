using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 5);
	public bool standing = false;
	public float jetSpeed = 15f;
	public float airSpeedMultiplier = 0.3f;
	public AudioClip leftFootSound;
	public AudioClip rightFootSound;
	public AudioClip thudSound;
	public AudioClip rocketSound;

	private PlayerController controller;
	private Animator animator;

	void Start(){
	
		controller = GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();

	}

	void PlayLeftFootSound(){
		if (leftFootSound) {
			AudioSource.PlayClipAtPoint(leftFootSound,transform.position);
		}
	}

	void PlayRightFootSound(){
		if (rightFootSound) {
			AudioSource.PlayClipAtPoint(rightFootSound,transform.position);
		}
	}

	void PlayRocketSound(){
		if (!rocketSound || GameObject.Find ("RocketSound"))
				return;

		GameObject go = new GameObject ("RocketSound");
		AudioSource rockSource = go.AddComponent<AudioSource> ();
		rockSource.clip = rocketSound;
		rockSource.volume = 0.5f;
		rockSource.Play ();

		Destroy (go, rocketSound.length);
	}

	void OnCollisionEnter2D(Collision2D target){
		if (!standing) {
			var absVelX = Mathf.Abs(rigidbody2D.velocity.x);
			var absVelY = Mathf.Abs(rigidbody2D.velocity.y);

			if(absVelX <= 0.1f || absVelY<=0.1f){
				if(thudSound){
					AudioSource.PlayClipAtPoint(thudSound,transform.position);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		var forceX = 0f;
		var forceY = 0f;

		var absVelocityX = Mathf.Abs (rigidbody2D.velocity.x);
		var absVelocityY = Mathf.Abs (rigidbody2D.velocity.y);

		if (absVelocityY < 0.2f) {
			standing = true;
		} else
			standing = false;

//		if (Input.GetKey ("right")) {
//
//			if(absVelocityX < maxVelocity.x)
//				forceX = standing ? speed : (speed * airSpeedMultiplier) ;
//
//			transform.localScale = new Vector3(1 , 1, 1);
//
//		} else if (Input.GetKey("left")){
//
//			if(absVelocityX < maxVelocity.x)
//				forceX = standing ? -speed : (-speed * airSpeedMultiplier) ;
//
//			transform.localScale = new Vector3(-1 , 1, 1);
//
//		}

		if (controller.moving.x != 0) {

			if (absVelocityX < maxVelocity.x) {
				forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x * airSpeedMultiplier);
				transform.localScale = new Vector3 (forceX > 0 ? 1 : -1, 1, 1);
			}

			animator.SetInteger ("AnimState", 1);

		} else {

			animator.SetInteger ("AnimState", 0);
		}

		if (controller.moving.y > 0) {
			PlayRocketSound();
			if (absVelocityY < maxVelocity.y) {
	
					forceY = (jetSpeed * controller.moving.y);
	
			}
			animator.SetInteger ("AnimState", 2);
		} else if (absVelocityY > 0) {
			animator.SetInteger ("AnimState", 3);
		}
		if (Input.GetKey ("up")) {
				if (absVelocityY < maxVelocity.y)
						forceY = jetSpeed;
		}

		rigidbody2D.AddForce(new Vector2 (forceX, forceY));
	}
}
