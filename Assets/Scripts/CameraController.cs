﻿using UnityEngine;
using System.Collections;

// TODO:
// - Completely fucks up if target doesn't start at Vector3.zero
// - Have it more dynamic (e.g. camera zooms out when moving character moves towards it)

[DisallowMultipleComponent]
public class CameraController : MonoBehaviour
{
	GameObject cameraTarget;
	public float rotateSpeed;
	float rotate;
	public float offsetDistance;
	public float offsetHeight;
	public float smoothing;
	Vector3 offset;
	bool following = true;
	Vector3 lastPosition;

	void Start()
	{
		cameraTarget = GameObject.FindGameObjectWithTag("Player");
		lastPosition = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
		offset = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.F))
		{
			if(following)
			{
				following = false;
			} 
			else
			{
				following = true;
			}
		} 

		if(following)
		{
			offset = Quaternion.AngleAxis(rotate * rotateSpeed, Vector3.up) * offset;
			transform.position = cameraTarget.transform.position + offset; 
			transform.position = new Vector3(Mathf.Lerp(lastPosition.x, cameraTarget.transform.position.x + offset.x, smoothing * Time.deltaTime), 
				Mathf.Lerp(lastPosition.y, cameraTarget.transform.position.y + offset.y, smoothing * Time.deltaTime), 
				Mathf.Lerp(lastPosition.z, cameraTarget.transform.position.z + offset.z, smoothing * Time.deltaTime));
		} 
		else
		{
			transform.position = lastPosition; 
		}
		transform.LookAt(cameraTarget.transform.position + new Vector3(0, offsetHeight - 1, 0));
	}

	public void RotateLeft () {

		rotate = -1;
	}

	public void RotateRight () {

		rotate = 1;
	}

	public void RotateNone () {

		rotate = 0;
	}

	void LateUpdate()
	{
		lastPosition = transform.position;
	}
}