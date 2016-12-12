using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class AreaTransition : MonoBehaviour {

	[SerializeField] Transform forwardWaypoint;
	[SerializeField] Transform backwardWaypoint;
	[SerializeField] GameObject screenOverlay;

	GameObject player;
	Vector3 newPosition;

	void OnTriggerEnter (Collider other) {

		if(other.CompareTag("Player")) {

			player = other.gameObject;

			float distanceToForwardWaypoint = Vector3.Distance(player.transform.position, forwardWaypoint.position);
			float distanceToBackwardWaypoint = Vector3.Distance(player.transform.position, backwardWaypoint.position);
			newPosition = (distanceToForwardWaypoint > distanceToBackwardWaypoint) ? forwardWaypoint.position : backwardWaypoint.position;

			StartCoroutine(FadeInAndOut());
		}
	}

	IEnumerator FadeInAndOut () {

		screenOverlay.SetActive(true);
		Image overlayImage = screenOverlay.GetComponent<Image>();
		overlayImage.color = Color.clear;

		float fadeSpeed = 7 * Time.deltaTime;
		float newAlpha = 0;

		while(newAlpha < 1) {

			newAlpha += fadeSpeed;
			overlayImage.color = new Color(0, 0, 0, newAlpha);
			yield return new WaitForSeconds(fadeSpeed);
		}

		player.transform.position = newPosition;

		while(newAlpha > 0) {

			newAlpha -= fadeSpeed;
			overlayImage.color = new Color(0, 0, 0, newAlpha);
			yield return new WaitForSeconds(fadeSpeed);
		}

		screenOverlay.SetActive(false);
	}
}
