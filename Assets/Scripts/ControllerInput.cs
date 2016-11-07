using Rewired;
using UnityEngine;

// TODO:
// Camera zoom controls?
// Movement is still tied to Input.GetAxisRaw instead of player.GetAxisRaw

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerController))]
public class ControllerInput : MonoBehaviour {
	
	PlayerController playerController;
	Player rewiredPlayer;
	CameraController cameraController;

	void Awake () {

		cameraController = Camera.main.GetComponentInChildren<CameraController> ();
		playerController = this.GetComponentInChildren<PlayerController> ();
		rewiredPlayer = ReInput.players.GetPlayer (0);
	}

	void Update () {

		// Jump
		if (rewiredPlayer.GetButtonDown ("Jump")) {

			playerController.StartCoroutine ("_Jump");
		}

		// Attack
		if (rewiredPlayer.GetButtonDown ("Attack")) {

			playerController.Attack (2);
		}

		// Camera rotation
		if (rewiredPlayer.GetAxisRaw ("RHorizontal") > 0) {

			cameraController.RotateRight ();
		}
		else if (rewiredPlayer.GetAxisRaw ("RHorizontal") < 0) {

			cameraController.RotateLeft ();
		}
		else {

			cameraController.RotateNone ();
		}
	}
}
