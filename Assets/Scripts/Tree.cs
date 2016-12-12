using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Tree : MonoBehaviour {

	bool playerIn = false;
	bool growing = false;
	bool shrinking = false;
	float minY = 1;
	float maxY = 25;

	void Update () {

		if(Input.GetKeyDown(KeyCode.Space) && playerIn) {

			growing = true;
		}

		if(growing) {

			float newY = this.transform.localScale.y;
			newY += 3 * Time.deltaTime;
			this.transform.localScale = new Vector3(this.transform.localScale.x, newY, this.transform.localScale.z);

			if(newY >= maxY) {

				growing = false;
			}
		}
		else if(shrinking) {

			float newY = this.transform.localScale.y;
			newY -= 3 * Time.deltaTime;
			this.transform.localScale = new Vector3(this.transform.localScale.x, newY, this.transform.localScale.z);

			if(newY <= 1) {

				shrinking = false;
			}
		}
	}

	void OnTriggerEnter (Collider other) {

		if(other.CompareTag("Player")) {

			if(this.transform.localScale.y < 2) {
			
				playerIn = true;
			}
			else if(this.transform.localScale.y > 20) {

				shrinking = true;
			}
		}
	}

	void OnTriggerExit (Collider other) {

		if(other.CompareTag("Player")) {

			playerIn = false;
		}
	}
}
