using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInSpace player = collision.GetComponent<PlayerInSpace>();
        if(player)
        {
            ItemSpawner.Instance.SpawnDiamond();
            gameObject.SetActive(false);
        }
    }
}
