using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public DoorTrigger[] doorTrigger;
	public bool sticky;

	private Animator animator;
	private bool down;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){

		down = true;

		animator.SetInteger ("AnimState", 1);

		foreach (DoorTrigger trigger in doorTrigger) {
			if(trigger != null)
				trigger.Toggle(true);
		}
	}

	void OnTriggerExit2D(Collider2D target){

		//Guard Clause
		if (sticky && down)
			return;

		down = false;

		animator.SetInteger ("AnimState", 2);

		foreach (DoorTrigger trigger in doorTrigger) {
			if(trigger != null)
				trigger.Toggle(false);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = sticky ? Color.red : Color.green;

		foreach (DoorTrigger trigger in doorTrigger) {
			if(trigger != null)
				Gizmos.DrawLine(transform.position, trigger.door.transform.position);
		}
	}
}
