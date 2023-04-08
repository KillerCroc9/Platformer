using UnityEngine;
using System.Collections;

public class SphereRaycast : MonoBehaviour
{
    public float raycastLength;
    private void Start()
    {
        Invoke("Raycast", 2);
    }
    void Raycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
        {
            if (hit.collider.gameObject.tag == "TrapHolder") { 
            hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            if (hit.collider.gameObject.tag == "Wall")
            {
                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
