using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public static bool Paused = false;
	public GameObject resumeButton;
	public GameObject restartButton;
	public GameObject menuButton;
	public GameObject quitButton;

	private bool clicking = false;
	private GameObject currentButton;

    Canvas canvas;

    void Start()
    {
        Paused = false;
        canvas = GetComponent<Canvas>();
    }

    void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            canvas.enabled = !canvas.enabled;
            Pause();
        }

		if (Paused) {
			if (!clicking && (Input.GetAxisRaw ("DPadUD") > 0 || Input.GetAxisRaw("Vertical") > 0.2)) {
				clicking = true;
				if (currentButton == null) {
					currentButton = quitButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Normal", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				} else if (currentButton.Equals (resumeButton)) {
					currentButton = quitButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Normal", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				} else if (currentButton.Equals (restartButton)) {
					currentButton = resumeButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if (currentButton.Equals (menuButton)) {
					currentButton = restartButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Normal", true);
					restartButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if (currentButton.Equals (quitButton)) {
					currentButton = menuButton ;
					resumeButton.GetComponent<Animator> ().SetBool ("Normal", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				}
			} else if (!clicking && (Input.GetAxisRaw ("DPadUD") < 0 || Input.GetAxisRaw("Vertical") < -0.2)) {
				clicking = true;
				if (currentButton == null) {
					currentButton = quitButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if (currentButton.Equals (resumeButton)) {
					currentButton = restartButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Normal", true);
					restartButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if (currentButton.Equals (restartButton)) {
					currentButton = menuButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Normal", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				} else if (currentButton.Equals (menuButton)) {
					currentButton = quitButton;
					resumeButton.GetComponent<Animator> ().SetBool ("Normal", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
				} else if (currentButton.Equals (quitButton)) {
					currentButton = resumeButton ;
					resumeButton.GetComponent<Animator> ().SetBool ("Highlighted", true);
					restartButton.GetComponent<Animator> ().SetBool ("Normal", true);
					menuButton.GetComponent<Animator> ().SetBool ("Normal", true);
					quitButton.GetComponent<Animator> ().SetBool ("Normal", true);
				}
			} else if (clicking && Input.GetAxisRaw ("DPadUD") == 0 && Input.GetAxisRaw("Vertical") == 0) {
				clicking = false;
			}

			if (Input.GetButtonUp ("Jump") && currentButton != null) {
				if (currentButton.Equals (resumeButton)) {
					Pause ();
					canvas.enabled = !canvas.enabled;
				} else if(currentButton.Equals (restartButton)){
					Restart();
				} else if(currentButton.Equals (menuButton)){
					Menu();
				} else if(currentButton.Equals (quitButton)){
					Quit();
				}
			}
		}
    }

    public void Pause()
    {
        if (Paused)
            Paused = false;
        else
            Paused = true;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

    }

    public void Quit()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
		Application.Quit();
    #endif
    }

	public void Menu()
	{
		Debug.Log ("Menu");
		Time.timeScale = 1;
		SceneManager.LoadScene (0, LoadSceneMode.Single);
	}

	public void Restart()
	{
		Debug.Log ("Restart");
		Time.timeScale = 1;
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}
}
