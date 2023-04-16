using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject WinPanel;
    private void Start()
    {
        WinPanel = GameObject.FindGameObjectWithTag("winPanel");
        WinPanel = WinPanel.transform.GetChild(1).gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            WinPanel.SetActive(true);
        }
    }
}
