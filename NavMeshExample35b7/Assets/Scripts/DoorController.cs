using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Collider))]
public class DoorController : MonoBehaviour
{
	public Transform hinge;
	private float open = 0.0f;
	private Vector4 passPlane;
	private List<DoorClient> clients = new List<DoorClient>();

	void Start() {
		// Plane equation for side test.
		passPlane = transform.forward;
		passPlane.w = -Vector3.Dot(transform.forward, transform.position);
	}

	// Add/remove clients using door
	void Register(DoorClient client, bool register) {
		DoorClient found = null;
		foreach (DoorClient c in clients) {
			if(c==client) {
				found = c;
				break;
			}
		}
		if(register && !found && client)
			clients.Add(client);
		else if(found && !register)
			clients.Remove(client);
	}

	void OnTriggerEnter (Collider other) {
		DoorClient client = other.GetComponent<DoorClient>();
		if(!client) return;

		Rigidbody rigid = other.GetComponent<Rigidbody>();
		if(rigid) rigid.isKinematic = false;

		if(client.PassingThrough(passPlane)) {
			Register(client, true);	
			float dir = Mathf.Sign( Vector3.Dot(passPlane, other.transform.position) + passPlane.w);
			OpenDoor(dir);
		}
	}

	void OnTriggerExit (Collider other) {
		DoorClient client = other.GetComponent<DoorClient>();
		if(!client) return;

		Rigidbody rigid = other.GetComponent<Rigidbody>();
		if(rigid) rigid.isKinematic = true;

		Register(client, false);
		CloseDoor();
	}
	
	void OpenDoor(float openDirection) {
		open = openDirection;
	}
	
	void CloseDoor() {
		if(clients.Count == 0)
			open = 0.0f;
	}

	void Update() {
		Quaternion wantedRotation = new Quaternion();
		wantedRotation.SetLookRotation(passPlane);
		if(open != 0.0f) {
			Vector3 openDir = new Vector3(passPlane[2], passPlane[1], passPlane[0]);
			wantedRotation.SetLookRotation(open*openDir);
		}

		hinge.rotation = Quaternion.RotateTowards(hinge.rotation, wantedRotation, 180.0f*Time.deltaTime);
	}
}
