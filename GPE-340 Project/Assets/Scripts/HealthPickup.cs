using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupPawn
{
    [SerializeField]
    private float healthRestored;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(GameObject target)
    {
        target.GetComponent<PlayerPawn>().currentHealth += healthRestored;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Heal(col.gameObject);
        }
        Destroy(gameObject);
    }
}
