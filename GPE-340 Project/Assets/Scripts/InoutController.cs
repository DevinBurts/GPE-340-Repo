using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InoutController : MonoBehaviour
{
    private Animator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        characterAnimator.SetFloat("Forward", Input.GetAxis("Vertical"));
        characterAnimator.SetFloat("Right", Input.GetAxis("Horizontal"));
    }
}
