using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2DFreeformD : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;

        animator.SetFloat("Horizontal", horizontal * offset);
        animator.SetFloat("Vertical", vertical * offset);

    }
}
