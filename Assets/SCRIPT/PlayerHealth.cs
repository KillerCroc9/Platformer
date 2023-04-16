using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public bool Hit;
    public SkinnedMeshRenderer PlayerMesh;
    public MeshRenderer PlayerGunMesh;
    [SerializeField]
    private float blinkDuration = 2f; // how long to blink for in seconds
    public GameObject lose;
    public Image Health;
    private int maxHealth;

    void Start()
    {
        maxHealth = health;
    }

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
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        StartCoroutine(Delay());
        StartCoroutine(Blink());
        StartCoroutine(UpdateHealthWithDelay(-10, blinkDuration));
    }
    public void OnFire()
    {
        StartCoroutine(UpdateHealthWithDelay(-1, blinkDuration));
    }

    IEnumerator Delay()
    {
        Hit = true;
        yield return new WaitForSeconds(blinkDuration);
        Hit = false;
    }

    IEnumerator Blink()
    {
        while (Hit == true)
        {
            PlayerMesh.enabled = !PlayerMesh.enabled;
            PlayerGunMesh.enabled = !PlayerGunMesh.enabled;
            yield return new WaitForSeconds(.2f);
        }
        PlayerMesh.enabled = true;
        PlayerGunMesh.enabled = true;
    }

    IEnumerator UpdateHealthWithDelay(int delta, float duration)
    {
        float elapsed = 0f;
        int startHealth = health;
        health += delta;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            float startHealthNormalized = (float)startHealth / maxHealth;
            float currentHealthNormalized = (float)health / maxHealth;
            Health.fillAmount = Mathf.Lerp(startHealthNormalized, currentHealthNormalized, normalizedTime);
            yield return null;
        }

        Health.fillAmount = (float)health / maxHealth;
    }
}
