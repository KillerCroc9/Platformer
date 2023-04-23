using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCountDown : MonoBehaviour
{
    [SerializeField]
    private float duration = 10f; // the duration of the countdown in seconds
    //public TextMeshProUGUI countdownText; // the Text component that displays the countdown
    public GameObject lose;
    private float startTime; // the time when the countdown started
    bool called = false;
    public Image image;
    public GameObject Player;
    public GameObject TimeUpCam;
    public AudioClip GOW;

    void Start()
    {
        startTime = Time.time;
        
    }
    void Update()
    {
        float timeElapsed = Time.time - startTime; // calculate the time elapsed since the countdown started
        float timeRemaining = Mathf.Max(duration - timeElapsed, 0f); // calculate the time remaining, clamped at 0

        int minutes = Mathf.FloorToInt(timeRemaining / 60f); // calculate the number of minutes remaining
        int seconds = Mathf.FloorToInt(timeRemaining % 60f); // calculate the number of seconds remaining
        float fillAmount = Mathf.Clamp01(Mathf.Max(duration - timeElapsed, 0f) / duration);
        image.fillAmount = fillAmount;
        //countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // update the Text component with the remaining time
        if (timeRemaining <= 0 && called == false)
        {
            called = true;
            StartCoroutine(DeathPlayer());
            
        }
    }
    IEnumerator DeathPlayer()
    {
        TimeUpCam.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Player = GameObject.FindWithTag("Player");
        Player.GetComponentInChildren<Animator>().SetBool("isRunning", false);
        this.GetComponent<AudioSource>().clip = GOW;
        this.GetComponent<AudioSource>().loop = false;
        this.GetComponent<AudioSource>().Play();
        Player.GetComponent<Controller>().enabled = false;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Player.GetComponent<Rigidbody>().AddForce(new Vector3(10, 10, 10), ForceMode.Impulse);
        yield return new WaitForSeconds(9f);
        Time.timeScale = 0;
        lose.SetActive(true);
    }
}
