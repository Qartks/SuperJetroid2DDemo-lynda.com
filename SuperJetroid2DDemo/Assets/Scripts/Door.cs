using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public const int IDEL = 0;
	public const int OPENING = 1;
	public const int CLOSING = 3;
	public const int OPEN = 2;

	public float closeDelay = 0.5f;

	private int state = IDEL;
	private Animator animator; 

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnOpenStart(){
		state = OPENING;
	}

	void OnOpenEnd(){
		state = OPEN;
	}

	void OnCloseStart(){
		state = CLOSING;
	}

	void OnCloseEnd(){
		state = IDEL;
	}

	void DisableCollider2D(Collider2D target){
		collider2D.enabled = false;
	}

	void EnableCollider2D(Collider2D target){
		collider2D.enabled = true;
	}

	public void Open(){
		animator.SetInteger ("AnimState", 1);
	}

	public void Close(){
		StartCoroutine (CloseNow ());
	}

	private IEnumerator CloseNow(){
		yield return new WaitForSeconds(closeDelay);
		animator.SetInteger ("AnimState", 2);
	}
}
