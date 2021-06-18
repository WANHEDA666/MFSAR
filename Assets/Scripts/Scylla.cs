using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scylla : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void SetThePosition(Vector3 pos)
    {
	    gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y, pos.z);
    }

    public void AnimationsSolution(Vector3 moveVector) {
    	if (moveVector.x == 0 && moveVector.z == 0) animator.SetInteger("State", 0);	
    	else animator.SetInteger("State", 1);
        
		if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0) {
			Vector3 direction = Vector3.RotateTowards(Vector3.forward, moveVector, 3f, 0f);
			gameObject.transform.rotation = Quaternion.LookRotation(direction);
		}
    }
}
