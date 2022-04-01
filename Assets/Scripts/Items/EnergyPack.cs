using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInSpace player = collision.GetComponent<PlayerInSpace>();
        if (player)
        {
            ItemSpawner.Instance.SpawnEnergyPack();
            gameObject.SetActive(false);
        }
    }
}
