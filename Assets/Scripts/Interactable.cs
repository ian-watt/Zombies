using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum Type {Vend};
    public Type type;
    public UnityEvent<Player> interactEvent = new UnityEvent<Player>();

    private void Start()
    {
        interactEvent.AddListener(Interact);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

        }
    }

    public void Interact(Player player)
    {
       
        Debug.Log("Interacted");
       
    }
}
