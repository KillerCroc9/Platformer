using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Controller : MonoBehaviour
{
    Rigidbody rb;
    private float speed = 100f;
    float maxSpeed = 100f;
    public GameObject[] Cams;
    int currentCameraIndex;
    public GameObject particle;
    public GameObject particle1;
    Animator animator;
    bool isPlaying;
    bool transition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed; // Normalize the velocity and multiply it by maxSpeed to cap the speed
        }
      
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Physics.gravity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(-speed*1.5f, 0, 0), ForceMode.Acceleration);
            this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, -90, 0);
            animator.SetBool("isRunning", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Physics.gravity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(speed * 1.5f, 0, 0), ForceMode.Acceleration);
            this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("isRunning", true);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!isPlaying) { 
            particle1.SetActive(true);
            particle.SetActive(true);
            GetComponent<AudioSource>().Play();
                isPlaying = true;
            }
            Physics.gravity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(0, speed, 0), ForceMode.Acceleration);
            animator.SetBool("isRunning", false);

        }
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isRunning", false);
        }
        if (!Input.GetKey(KeyCode.UpArrow))
        {
            Physics.gravity = new Vector3(0, -100f, 0);
            if (isPlaying) {
                GetComponent<AudioSource>().Stop();
                particle1.SetActive(false);
            particle.SetActive(false);
                isPlaying = false;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(new Vector3(0, -speed, 0), ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.Tab) && !transition) // Switch camera
        {
            transition = true;
            StartCoroutine(Transitions());
            currentCameraIndex++;
            if (currentCameraIndex >= Cams.Length)
            {
                currentCameraIndex = 0;
            }
            for (int i = 0; i < Cams.Length; i++)
            {
                if (i == currentCameraIndex)
                {
                    Cams[i].SetActive(true);
                    
                }
                else
                {
                    Cams[i].SetActive(false);
                }
            }
        }
        
    }
    IEnumerator Transitions()
    {
        yield return new WaitForSeconds(1.5f);
        transition = false;
    }
}
