using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] private Joystick joystick;
	[SerializeField] private Scylla Scylla;
	
	private Rigidbody playerRigidbody;
	private Vector3 moveVector;

	private void Start()
	{
		playerRigidbody = gameObject.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		moveVector = new Vector3();
		moveVector.x = joystick.Horizontal;
		moveVector.z = joystick.Vertical;
		playerRigidbody.velocity = new Vector3(moveVector.x * 5f, playerRigidbody.velocity.y, moveVector.z * 5f);
		Scylla.AnimationsSolution(moveVector);
		Scylla.SetThePosition(gameObject.transform.position);
	}

	public void SetAll()
	{
		playerRigidbody.useGravity = true;
		gameObject.GetComponent<BoxCollider>().enabled = true;
	}
}