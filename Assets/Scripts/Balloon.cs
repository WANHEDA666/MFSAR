using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player>() != null) {
			//Event about the ballon collect
		}
	}
}
