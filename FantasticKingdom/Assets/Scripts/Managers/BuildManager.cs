using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildManager : MonoBehaviour {

	public Image icon1;
	public Image icon2;
	public Image icon3;
	public Image icon4;

	private bool building = false;
	private Canvas canvas;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckBuilding ()) {
			CheckSelect ();
		}

	}

	bool CheckBuilding(){
		if (Input.GetKeyDown (KeyCode.B)) {
			building = !building;
			if (building) {
				canvas.enabled = true;
				Debug.Log ("Building Mode");
			} else {
				canvas.enabled = false;
				Debug.Log ("Exit Building Mode");
			}
		}
		return building;
	}

	void CheckSelect(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Debug.Log ("SELECT ONE");
			icon1.color = new Color (100, 0, 0);
			icon2.color = new Color (255, 255, 255);
			icon3.color = new Color (255, 255, 255);
			icon4.color = new Color (255, 255, 255);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Debug.Log ("SELECT TWO");
			icon1.color = new Color (255, 255, 255);
			icon2.color = new Color (100, 0, 0);
			icon3.color = new Color (255, 255, 255);
			icon4.color = new Color (255, 255, 255);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			Debug.Log ("SELECT THREE");
			icon1.color = new Color (255, 255, 255);
			icon2.color = new Color (255, 255, 255);
			icon3.color = new Color (100, 0, 0);
			icon4.color = new Color (255, 255, 255);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			Debug.Log ("SELECT FOUR");
			icon1.color = new Color (255, 255, 255);
			icon2.color = new Color (255, 255, 255);
			icon3.color = new Color (255, 255, 255);
			icon4.color = new Color (100, 0, 0);
		}
	}
}
