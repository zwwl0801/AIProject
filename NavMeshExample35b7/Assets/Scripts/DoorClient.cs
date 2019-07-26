using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class DoorClient : MonoBehaviour
{
	private UnityEngine.AI.NavMeshAgent navMeshAgent;

	void Start() {
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	public bool PassingThrough(Vector4 plane) {
		UnityEngine.AI.NavMeshHit hit;
		navMeshAgent.SamplePathPosition(-1, 2.5f, out hit);
		if(hit.distance==0.0f) return false;

		Vector3 next_pos = hit.position;
		Vector3 curr_pos = transform.position;

		// Test positions are on opposite sides of door plane.
		float s1 = Vector3.Dot(next_pos, plane) + plane.w;
		float s2 = Vector3.Dot(curr_pos, plane) + plane.w;
		return s1*s2<0.0f;
	}
}

