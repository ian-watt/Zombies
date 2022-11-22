using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VendingMachine : Interactable
{
    

    private void Awake()
    {
        type = Type.Vend;
    }

    private void Start()
    {
        GameManager.Instance.InteractablesInMap.Add(this);
    }

}
