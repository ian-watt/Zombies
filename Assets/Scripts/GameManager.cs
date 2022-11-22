using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Player> CurrentPlayers;
    public List<Interactable> InteractablesInMap;


    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }




}
