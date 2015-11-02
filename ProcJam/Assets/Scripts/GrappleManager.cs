using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrappleManager : MonoBehaviour {


	public GrapplePoint[] grapplePoints;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GrapplePoint GetClosestGrapple(Vector3 position, float maxDistance){

		if (grapplePoints.Length == 0) {
			return null;
		}
		int closestIndex = 0;
		float closestDistance = 20000;
		for (int i = 0; i<grapplePoints.Length; i++) {
			float distance = Vector3.Distance(grapplePoints[i].gameObject.transform.position, position);
			if(distance<closestDistance){
				closestDistance = distance;
				closestIndex = i;
			}
		}

		if (closestDistance > maxDistance) {
			return null;
		}

		return grapplePoints [closestIndex];
	}
}
