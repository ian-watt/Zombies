using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : Enemy
{
    
    void Awake()
    {
        InitializeReferences();
    }

    void Update()
    {
        CheckAttackRange();
        Death();
    }
}
