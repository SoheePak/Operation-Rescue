using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : LivingEntity
{
    private Animator animator;
    private Tutorial tutorial;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tutorial = FindObjectOfType<Tutorial>();
    }

    public override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
        tutorial.KillText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
