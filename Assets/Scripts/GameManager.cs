using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance)
        {
            Debug.LogWarning("An instance of " + this + " already exists!", gameObject);
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


}
