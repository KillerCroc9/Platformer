using UnityEngine;
using System.Collections;
public class GunRaycast : MonoBehaviour
{
    ParticleSystem[] particleSystems;
    bool playerInRange = false;
    bool raycastStopped = false;
    float raycastTimer = 0f;
    bool damageDealt = false;

    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (!raycastStopped)
        {
            Ray ray = new Ray(transform.position, -transform.forward);
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
            if (Physics.Raycast(ray, out RaycastHit hit, 110f) && hit.collider.CompareTag("Player"))
            {
                playerInRange = true;

                if (!damageDealt)
                {
                    print("Damage dealt");
                    StartCoroutine(DealDamage());
                    damageDealt = true;
                }
            }
            else
            {
                playerInRange = false;
                StopCoroutine(DealDamage());
                damageDealt = false;
                print("Damage not dealt");

            }

            raycastTimer += Time.deltaTime;
            if (raycastTimer >= 4f)
            {
                raycastStopped = true;
                foreach (ParticleSystem particleSystem in particleSystems)
                {
                    particleSystem.Stop();
                }
                StartCoroutine(RestartRaycast());
            }
        }
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(.5f);

        if (playerInRange)
        {
            PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.OnFire();
            }
        }

        damageDealt = false;
    }

    IEnumerator RestartRaycast()
    {
        yield return new WaitForSeconds(4f);

        raycastStopped = false;
        raycastTimer = 0f;
    }
}
