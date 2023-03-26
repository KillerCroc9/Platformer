using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public bool Hit;
    public SkinnedMeshRenderer PlayerMesh;
    public MeshRenderer PlayerGunMesh;
    [SerializeField]
    private float blinkDuration = 2f; // how long to blink for in seconds
    public GameObject lose;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            lose.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap" && Hit == false)
        {
            StartCoroutine(Delay());
            StartCoroutine(Blink());
        }
       
    }
    IEnumerator Delay()
    {
        Hit = true;
        health -= 25;
        yield return new WaitForSeconds(blinkDuration);
        Hit = false;
    }
    IEnumerator Blink()
    {
        while (Hit==true)
        {
            PlayerMesh.enabled = !PlayerMesh.enabled;
            PlayerGunMesh.enabled = !PlayerGunMesh.enabled;
            yield return new WaitForSeconds(.2f);
        }
        PlayerMesh.enabled = true; 
        PlayerGunMesh.enabled = true;
    }
}
