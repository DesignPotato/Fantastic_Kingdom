using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartManager : MonoBehaviour {

	public GameObject quitCheck;
	public Texture2D cursorTexture;

	public GameObject tutorial;
	public GameObject startButton;
	public GameObject quitButton;

	public GameObject yesButton;
	public GameObject noButton;

	private GameObject currentButton;
	private bool clicking = false;

	public void Start(){
        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        if (tutorial)
            tutorial.active = false;
	}

	void Update(){
		if (tutorial.activeSelf) {
			if (Input.GetButtonUp ("Jump")) {
				StartGame ();
			}
			return;
		}
			
		if(quitCheck.activeSelf && currentButton != null){
			if (!currentButton.Equals (noButton) && !currentButton.Equals (yesButton)) {
				currentButton = noButton;
				yesButton.GetComponent<Animator> ().SetBool ("Normal", true);
				noButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
			}
			if (!clicking && (Input.GetAxisRaw ("DPadLR") > 0 || Input.GetAxisRaw("Horizontal") > 0.2)) {
				clicking = true;
				if (currentButton.Equals (noButton)) {
					currentButton = yesButton;
					yesButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					noButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if (currentButton.Equals (yesButton)) {
					currentButton = noButton;
					yesButton.GetComponent<Animator> ().SetBool ("Normal", true);
					noButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				}
			} else if (!clicking && (Input.GetAxisRaw ("DPadLR") < 0 || Input.GetAxisRaw("Horizontal") < -0.2)) {
				clicking = true;
				if (currentButton.Equals (noButton)) {
					currentButton = yesButton;
					yesButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					noButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if (currentButton.Equals (yesButton)) {
					currentButton = noButton;
					yesButton.GetComponent<Animator> ().SetBool ("Normal", true);
					noButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				}
			} else if (clicking && Input.GetAxisRaw ("DPadLR") == 0 && Input.GetAxisRaw("Horizontal") == 0) {
				clicking = false;
			}

			if (Input.GetButtonUp ("Jump")) {
				if (currentButton.Equals (noButton)) {
					checkQuit(false);
					currentButton = quitButton;
					yesButton.GetComponent<Animator> ().SetBool ("Normal", true);
					noButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if(currentButton.Equals (yesButton)){
					Quit();
				}
			}

			return;
		}


		if (!clicking && (Input.GetAxisRaw ("DPadUD") > 0 || Input.GetAxisRaw("Vertical") > 0.2)) {
			clicking = true;
			if (currentButton == null) {
				currentButton = quitButton;
				quitButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				startButton.GetComponent<Animator> ().SetBool ("Normal", true);
			} else if (currentButton.Equals (startButton)) {
				currentButton = quitButton;
				quitButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				startButton.GetComponent<Animator> ().SetBool ("Normal", true);
			} else if (currentButton.Equals (quitButton)) {
				currentButton = startButton;
				quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				startButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
			}
		} else if (!clicking && (Input.GetAxisRaw ("DPadUD") < 0 || Input.GetAxisRaw("Vertical") < -0.2)) {
			clicking = true;
			if (currentButton == null) {
				currentButton = startButton;
				quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				startButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
			} else if (currentButton.Equals (startButton)) {
				currentButton = quitButton;
				quitButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				startButton.GetComponent<Animator> ().SetBool ("Normal", true);
			} else if (currentButton.Equals (quitButton)) {
				currentButton = startButton;
				quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				startButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
			}
		} else if (clicking && Input.GetAxisRaw ("DPadUD") == 0 && Input.GetAxisRaw("Vertical") == 0) {
			clicking = false;
		}

		if (currentButton != null && Input.GetButtonUp ("Jump")) {
			if (currentButton.Equals (startButton)) {
				LoadTutorial ();
			} else if(currentButton.Equals (quitButton)){
				checkQuit (true);
			}
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}

    public void LoadTutorial()
    {
        Debug.Log("Fuvk");
        Debug.Log(tutorial);
        if (tutorial)
        {
            Debug.Log("Fuyyyvk");
            tutorial.active = true;
        }
    }

	public void checkQuit(bool enabled){
		quitCheck.SetActive (enabled);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
