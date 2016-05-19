using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

    public bool Paused;

    Canvas canvas;

    void Start()
    {
        Paused = false;
        canvas = GetComponent<Canvas>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            Pause();
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
}
