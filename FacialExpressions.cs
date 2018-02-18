using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class FacialExpressions : MonoBehaviour {

	//Script containing levels of emotions
	ImageResultsParser userEmotions;
	float dominateEmotion = 0;

	//player Transform used to obtain reference to UserEmotions script
	Transform player;

	//Used to change the face from smiling to frowning
	public Renderer faceRenderer;

	private Material faceMaterial;
	private Vector2 uvOffset;

	//Used for movements
	bool isUp = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Sailor").transform;
		userEmotions = player.GetComponent<ImageResultsParser> ();
		uvOffset = Vector2.zero;
		faceMaterial = faceRenderer.materials[1];
	}
	

	void Update () {
		dominateEmotion = Mathf.Max (userEmotions.joyLevel, userEmotions.sadnessLevel, userEmotions.surpriseLevel);

		if (dominateEmotion < 5) 
			setIdle ();
		else if (userEmotions.sadnessLevel == dominateEmotion)
			setSad ();
		else if (userEmotions.surpriseLevel == dominateEmotion)
			setSurprise ();
		else if (userEmotions.joyLevel == dominateEmotion && dominateEmotion > 50)
			setJoyful ();
		else
			setIdle ();

		faceMaterial.SetTextureOffset ("_MainTex", uvOffset);
	}
		
	void setIdle(){
		uvOffset = Vector2.zero;
		if (isUp) {
			player.Translate (Vector3.down);
			isUp = false;
		}
	}
		
	void setJoyful() {
		uvOffset = new Vector2(0.25f, 0);
		if (isUp)
			return;
		else {
			player.Translate (Vector3.up);
			isUp = true;
		}
	}
		
	void setSad() {
		uvOffset = new Vector2(0, -0.25f);
		if (isUp) {
			player.Translate (Vector3.down);
			isUp = false;
		}
	}
		
	void setSurprise() {
		uvOffset = new Vector2(0.25f, -0.25f);
		if (isUp) {
			player.Translate (Vector3.down);
			isUp = false;
		}
		player.RotateAround (Vector3.zero, Vector3.up, 360.0f * Time.deltaTime);
	}
		

}
