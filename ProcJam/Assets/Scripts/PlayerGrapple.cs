using UnityEngine;
using System.Collections;

public class PlayerGrapple : MonoBehaviour {

	//Line Renderer
	public GrappleManager grappleManager;
	LineRenderer myLineRenderer;
	public Player player;
	[HideInInspector]
	public bool grappleActive;
	float grappleStrength = 20.0f;


	void Awake(){
		grappleActive = false;
		myLineRenderer = gameObject.GetComponent<LineRenderer> ();
		myLineRenderer.sortingLayerName = "Foreground";
	}

	// Update is called once per frame
	void Update () {

	}

	public void UseGrapple(){
		GrapplePoint closestGrapple = grappleManager.GetClosestGrapple (player.transform.position, 5);

		if (closestGrapple == null) {
			myLineRenderer.enabled = false;
			return;
		}
		grappleActive = true;
		myLineRenderer.enabled = true;
	
		myLineRenderer.SetPosition(0,player.transform.position - new Vector3(0,0,0.01f));
		myLineRenderer.SetPosition(1,closestGrapple.transform.position- new Vector3(0,0,0.01f));

		Vector3 offset = closestGrapple.transform.position - player.transform.position;
		offset.Normalize ();
		Vector3 grappleForce = offset * grappleStrength;

		float xScale = 0.1f * Vector3.Distance (closestGrapple.transform.position, player.transform.position);
		myLineRenderer.material.mainTextureScale = new Vector2 (xScale, 1);

		player.AddForce(grappleForce);
	}

	public void DisableGrapple(){
		grappleActive = false;
		myLineRenderer.enabled = false;
	}


}
