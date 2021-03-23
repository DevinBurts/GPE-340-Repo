using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignRotation : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        // Make the target the parent
        target = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the weapon towards the target direction
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, speed * Time.deltaTime);
    }
}
