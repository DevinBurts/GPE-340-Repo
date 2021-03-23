using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePickup : MonoBehaviour
{
    public GameObject pickupType;
    public float timer;
    public float timerReset;
    private GameObject currentPickup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the pickup is destroyed
        if (currentPickup == null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnPickup();
            }
        }
    }

    private void SpawnPickup()
    {
        currentPickup = Instantiate(pickupType, transform.position, transform.rotation) as GameObject;
        currentPickup.transform.parent = transform.parent;
        timer = timerReset;
    }
}
