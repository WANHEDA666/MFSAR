using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private Joystick joystick;

	private Animator animator;
	private CharacterController characterController;
	private Vector3 moveVector;
	//private Rigidbody playerRigidbody;

	private void Awake() {
		animator = gameObject.GetComponent<Animator>();
		characterController = gameObject.GetComponent<CharacterController>();
		//playerRigidbody = gameObject.GetComponent<Rigidbody>();
	}

	private void Update() {
		moveVector = new Vector3();
		moveVector.x = joystick.Horizontal;
		moveVector.z = joystick.Vertical;
		if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0) {
			Vector3 direction = Vector3.RotateTowards(Vector3.forward, moveVector, 3f, 0f);
			gameObject.transform.rotation = Quaternion.LookRotation(direction);
		}
		characterController.Move(moveVector * 3f * Time.deltaTime);
		AnimationsSolution();
	}

	private void AnimationsSolution() {
		if (moveVector.x == 0 && moveVector.z == 0) {
			animator.SetInteger("State", 1);
		}
		else {
			animator.SetInteger("State", 2);
		}
	}
}
