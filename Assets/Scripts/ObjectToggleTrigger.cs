using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class ObjectToggleTrigger : MonoBehaviour {

	[SerializeField] GameObject objectToToggle;

	void Awake () {

		if(!objectToToggle) {

			objectToToggle = this.transform.GetChild(0).gameObject;
		}

		objectToToggle.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		
		if(other.CompareTag("Player")) {

			objectToToggle.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other) {

		if(other.CompareTag("Player")) {

			objectToToggle.SetActive(false);
		}
	}
}
