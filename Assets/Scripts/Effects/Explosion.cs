using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator Animator;
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;
        Animator = child.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) Destroy(gameObject);
    }
}
