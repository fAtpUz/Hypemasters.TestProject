using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UI : MonoBehaviour
{
    #region Fields

    float timePassed;
    Text timeCounter;
    Button restart;
    Button pause;
    Button play;
    Scrollbar timeBar;

    GoToTimeState timeChangeEvent;

    #endregion

    #region Methods

    void Awake()
    {
        timeCounter = GameObject.FindGameObjectWithTag("TimeCounter").GetComponent<Text>();
        restart = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Button>();
        pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>();
        play = GameObject.FindGameObjectWithTag("Play").GetComponent<Button>();
        timeBar = GameObject.FindGameObjectWithTag("ScrollBar").GetComponent<Scrollbar>();

        play.gameObject.SetActive(false);
        timeBar.gameObject.SetActive(false);

        restart.onClick.AddListener(OnRestart);
        pause.onClick.AddListener(OnPause);
        play.onClick.AddListener(OnPlay);
        timeBar.onValueChanged.AddListener(OnTimeBarChangeValue);
    }

    void Start() {
        timeChangeEvent = new GoToTimeState();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        timeCounter.text = "Playback time: " + timePassed.ToString("0.00");
    }

    /// <summary>
    /// Restart the scene
    /// </summary>
    void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Pauses the gameplay
    /// Shows pause menu
    /// </summary>
    void OnPause()
    {
        restart.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
        timeBar.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resumes the gameplay
    /// Hides pause menu
    /// </summary>
    void OnPlay() {
        restart.gameObject.SetActive(true);
        pause.gameObject.SetActive(true);
        play.gameObject.SetActive(false);
        timeBar.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// What happens when we change the value in the scrollbar
    /// </summary>
    /// <param name="value">float</param>
    void OnTimeBarChangeValue(float value) {
        print("Hello world");
        timeChangeEvent.Invoke(value);
        timeChangeEvent = new GoToTimeState();
    }

    /// <summary>
    /// Add the listener to time change event
    /// </summary>
    /// <param name=""></param>
    public void AddTimeChangeListener(UnityAction<float> listener) {
        timeChangeEvent.AddListener(listener);
    }

    #endregion
}
