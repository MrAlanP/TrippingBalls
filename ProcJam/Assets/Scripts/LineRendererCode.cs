using UnityEngine;
using System.Collections;

public class LineRendererCode : MonoBehaviour {

	//Line Renderer
	public LineRenderer myLineRenderer;
	public Transform myPoint1;
	public Transform myPoint2;

	//Material
	public Material myMaterial;
	public Texture myTexture;
	public float xScaleFactor;
	private float adjustedXSize;

	// Update is called once per frame
	void Update () {
		myMaterial.SetTexture(0, myTexture);

		myLineRenderer.SetPosition(0,myPoint1.position);
		myLineRenderer.SetPosition(1,myPoint2.position);

		adjustedXSize = xScaleFactor * (Mathf.Abs(Vector2.Distance(myPoint1.position,myPoint2.position)));

		myMaterial.mainTextureScale = new Vector2(adjustedXSize, myMaterial.mainTextureScale.y);
	}
}
