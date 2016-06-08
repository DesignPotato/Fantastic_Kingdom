using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildManager : MonoBehaviour {

	public Image icon1;
	public Image icon2;
	public Image icon3;
	public Image icon4;
	public Camera cam;

	public GameObject wallGhost;
	public GameObject gateGhost;
	public GameObject towerGhost;
	public GameObject barracksGhost;

	private GameObject currentObject;
	private bool building = false;
	private Canvas canvas;
	// Create mat
	private Material correctBuildMat;
	private Material failBuildMat;

	// Use this for initialization
	void Start () {
		// Init mats
		correctBuildMat = new Material(Shader.Find("Legacy Shaders/Diffuse"));
		correctBuildMat.color = Color.green;
		failBuildMat = new Material(Shader.Find("Legacy Shaders/Diffuse"));
		failBuildMat.color = Color.red;

		// Apply mats to all objects and disable physics
		wallGhost.SetActive(false);
		BoxCollider[] bcs = wallGhost.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider bc in bcs) {
			bc.isTrigger = true;
		}

		gateGhost.SetActive(false);
		bcs = gateGhost.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider bc in bcs) {
			bc.isTrigger = true;
		}

		towerGhost.SetActive(false);
		bcs = towerGhost.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider bc in bcs) {
			bc.isTrigger = true;
		}

		barracksGhost.SetActive(false);
		bcs = barracksGhost.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider bc in bcs) {
			bc.isTrigger = true;
		}


		currentObject = wallGhost;
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(PauseManager.Paused){
			canvas.enabled = false;
			building = false;
			return;
		}

		if (CheckBuilding ()) {
			CheckSelect ();
			ShowGhost ();
		}

	}

	bool CheckBuilding(){
		if (Input.GetKeyDown (KeyCode.B)) {
			if (!building) {
				building = true;
				canvas.enabled = true;
				currentObject.SetActive(true);
				Debug.Log ("Building Mode");

			} else {
				building = false;
				canvas.enabled = false;
				currentObject.SetActive(false);
				Debug.Log ("Exit Building Mode");
			}
		}
		return building;
	}

	void CheckSelect(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			currentObject.SetActive(false);
			currentObject = wallGhost;
			currentObject.SetActive(true);
			icon1.color = new Color (100, 0, 0);
			icon2.color = new Color (255, 255, 255);
			icon3.color = new Color (255, 255, 255);
			icon4.color = new Color (255, 255, 255);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			currentObject.SetActive(false);
			currentObject = gateGhost;
			currentObject.SetActive(true);
			icon1.color = new Color (255, 255, 255);
			icon2.color = new Color (100, 0, 0);
			icon3.color = new Color (255, 255, 255);
			icon4.color = new Color (255, 255, 255);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			currentObject.SetActive(false);
			currentObject = towerGhost;
			currentObject.SetActive(true);
			icon1.color = new Color (255, 255, 255);
			icon2.color = new Color (255, 255, 255);
			icon3.color = new Color (100, 0, 0);
			icon4.color = new Color (255, 255, 255);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			currentObject.SetActive(false);
			currentObject = barracksGhost;
			currentObject.SetActive(true);
			icon1.color = new Color (255, 255, 255);
			icon2.color = new Color (255, 255, 255);
			icon3.color = new Color (255, 255, 255);
			icon4.color = new Color (100, 0, 0);
		}
	}

	void ShowGhost(){
		int layerMask = 1 << 9;
		layerMask = ~layerMask;

		// Set color
		GhostBuilding gb = currentObject.GetComponent<GhostBuilding>();
		if (gb.IsTriggered()) {
			wallGhost.GetComponent<Renderer>().material = failBuildMat;

			gateGhost.GetComponent<Renderer>().material = failBuildMat;

			Renderer[] rends = towerGhost.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in rends) {
				Material[] mats = rend.materials;
				for (int i = 0; i < mats.Length; i++) {
					mats [i] = failBuildMat;
				}
				rend.materials = mats;
			}

			rends = barracksGhost.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in rends) {
				Material[] mats = rend.materials;
				for (int i = 0; i < mats.Length; i++) {
					mats [i] = failBuildMat;
				}
				rend.materials = mats;
			}
		} else {
			wallGhost.GetComponent<Renderer>().material = correctBuildMat;

			gateGhost.GetComponent<Renderer>().material = correctBuildMat;

			Renderer[] rends = towerGhost.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in rends) {
				Material[] mats = rend.materials;
				for (int i = 0; i < mats.Length; i++) {
					mats [i] = correctBuildMat;
				}
				rend.materials = mats;
			}

			rends = barracksGhost.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in rends) {
				Material[] mats = rend.materials;
				for (int i = 0; i < mats.Length; i++) {
					mats [i] = correctBuildMat;
				}
				rend.materials = mats;
			}
		}

		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
			if (currentObject == barracksGhost) {
				currentObject.transform.position = new Vector3 (hit.point.x, hit.point.y - 0.5f, hit.point.z);
			} else {
				currentObject.transform.position = hit.point;
			}
			currentObject.transform.position = new Vector3(currentObject.transform.position.x, currentObject.transform.position.y + 0.5f, currentObject.transform.position.z);
		}
	}
}
