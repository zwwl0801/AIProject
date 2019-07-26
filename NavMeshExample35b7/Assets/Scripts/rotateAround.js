#pragma strict

var speed : float = 0.0f;
var ds : float = 4.0f;

function Start() {
}

function Update() {

	if(Input.GetKeyDown(KeyCode.RightArrow)) {
		speed += ds;
	}
	if(Input.GetKeyDown(KeyCode.LeftArrow)) {
		speed -= ds;
	}
	if(Mathf.Abs(speed) < 0.001f) {
		speed = 0.0f;
	}
	else {
		transform.RotateAround (Vector3.zero, Vector3.up, speed*Time.deltaTime);
	}
}
