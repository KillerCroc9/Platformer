using UnityEngine;
using System.Collections;

public class SphereRaycast : MonoBehaviour
{
    public float raycastLength;
    private void Start()
    {
        // Invoke the Raycast() method after 2 seconds from the start of the game
        Invoke("Raycast", 2);
    }
    void Raycast()
    {
        // Define a new Ray starting from the position and facing direction of the SphereRaycast object

        Ray ray = new Ray(transform.position, transform.forward);
        // If the hit object has a TrapHolder , set the first child object to inactive and the second child object to active
        if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
        {
            if (hit.collider.gameObject.tag == "TrapHolder") { 
            hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        // If the hit object has a Wall, set the first child object to active

            if (hit.collider.gameObject.tag == "Wall")
            {
                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
