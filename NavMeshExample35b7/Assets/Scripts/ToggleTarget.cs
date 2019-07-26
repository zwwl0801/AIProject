using UnityEngine;
using System.Collections;

public class ToggleTarget : MonoBehaviour {
	
	public Transform target1;
	public Transform target2;

	// Use this for initialization
	void Start () {
		target1.gameObject.active = true;
		target2.gameObject.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow) && !target2.gameObject.active)
			ToggleTargets();
		if(Input.GetKey(KeyCode.RightArrow) && !target1.gameObject.active)
			ToggleTargets();
	}
	
	void ToggleTargets() {
		target1.gameObject.active = !target1.gameObject.active;
		target2.gameObject.active = !target2.gameObject.active;
	}

	void OnGUI() {
		if(target1.gameObject.active)
			if(GUI.Button(new Rect(0, Screen.height-20, 100,20), "<"))
				ToggleTargets();
		if (target2.gameObject.active)
			if(GUI.Button(new Rect(Screen.width-100, Screen.height-20, 100,20), ">"))
				ToggleTargets();
	}
}
