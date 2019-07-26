using UnityEngine;
using System.Collections;

public class DrawBridge : MonoBehaviour {
	private float open = 0.0f;
	private Transform hinge;
	private UnityEngine.AI.NavMeshAgent[] agents;

	// Use this for initialization
	void Start () {
		hinge = transform;
		agents = FindObjectsOfType(typeof(UnityEngine.AI.NavMeshAgent)) as UnityEngine.AI.NavMeshAgent[];
		foreach(UnityEngine.AI.NavMeshAgent agent in agents) {
			agent.walkableMask = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) 
		|| (open != 0.0f && Input.GetKeyDown(KeyCode.DownArrow))
		|| (open == 0.0f && Input.GetKeyDown(KeyCode.UpArrow)) )
			ToggleBridge();
		

		Quaternion wantedRotation = new Quaternion();
		wantedRotation.SetLookRotation(Vector3.forward);
		if(open != 0.0f) {
			Vector3 openDir = new Vector3(0.0f, -0.7f, 0.7f);
			wantedRotation.SetLookRotation(open*openDir);
		}
		hinge.rotation = Quaternion.RotateTowards(hinge.rotation, wantedRotation, 180.0f*Time.deltaTime);
	}
	
	bool CanOpen() {
		if(open == 1.0f)
			return false;
		// Is occupied?
		UnityEngine.AI.NavMeshHit hit = new UnityEngine.AI.NavMeshHit();
		foreach(UnityEngine.AI.NavMeshAgent agent in agents) {
			agent.SamplePathPosition(-1, 0.0f, out hit);
			if((hit.mask & 8) != 0)
				return false;
		}
		return true;
	}

	void ToggleBridge() {
		if(open == 0.0f && !CanOpen())
			return;
		open = (open==0.0f) ? 1.0f : 0.0f;
		foreach(UnityEngine.AI.NavMeshAgent agent in agents) {
			agent.walkableMask = (open==0.0f) ? -1 : 1;
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(0,0,500,200), "Click mouse to place target. Use left/right arrows to rotate scene.");
		if(open != 0.0f) {
			if(GUI.Button(new Rect(Screen.width/2-50, Screen.height-20, 100,20), "Close Bridge")) {
				ToggleBridge();
				return;
			}
		}
		if(CanOpen()) {
			if(GUI.Button(new Rect(Screen.width/2-50, Screen.height-20, 100,20), "Open Bridge")) {
				ToggleBridge();
				return;
			}
		}
	}
}
