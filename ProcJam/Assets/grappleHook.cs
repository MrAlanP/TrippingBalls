using UnityEngine;
using System.Collections;

public class grappleHook : MonoBehaviour {

//Line Renderer
 public grappleHook GrappleHook;
 public Transform myPoint1;
 public Transform myPoint2;

 //Material
 public Material myMaterial;
 public float xScaleFactor;
 private float adjustedXSize;
 public Texture myTexture;

 // Update is called once per frame
 void Update()
 {
     myMaterial.SetTexture(0, myTexture);
     GrappleHook.transform.position = new Vector3 (0, myPoint1.position.y,0);
     GrappleHook.transform.position = new Vector3 (1, myPoint2.position.y,0);

     adjustedXSize = xScaleFactor * (Mathf.Abs(Vector2.Distance(myPoint1.position, myPoint2.position)));

     myMaterial.mainTextureScale = new Vector2(adjustedXSize, myMaterial.mainTextureScale.y);
 }
	// Use this for initialization
	void Start () {
	
	}
	
	
}
