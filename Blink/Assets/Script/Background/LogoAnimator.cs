using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject backgroundObject;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            animator.speed = 0f;
            StartCoroutine(EnableBackground());
        }
    }

    private IEnumerator EnableBackground()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        backgroundObject.SetActive(true);
    }
}
