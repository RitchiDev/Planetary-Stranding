using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance { get; private set; }

    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private PoolAbleObject m_EnergyPack;
    [SerializeField] private PoolAbleObject m_Diamond;
    [SerializeField] private int m_MaxDiamonds = 3;

    [SerializeField] private Vector2 m_MaxPosition;
    [SerializeField] private Vector2 m_MinPosition;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartClassic();
    }

    private void StartClassic()
    {
        GameObject energyPack = PoolManager.Instance.GetObjectFromPool(m_EnergyPack);
        energyPack.transform.position = GetRandomSpawnPosition();

        for (int i = 0; i < m_MaxDiamonds; i++)
        {
            GameObject diamond = PoolManager.Instance.GetObjectFromPool(m_Diamond);
            diamond.transform.position = GetRandomSpawnPosition();
        }
    }

    private void StartEndless()
    {

    }

    public void SpawnEnergyPack()
    {
        GameObject energyPack = PoolManager.Instance.GetObjectFromPool(m_EnergyPack);
        energyPack.transform.position = GetRandomSpawnPosition();
    }

    public void SpawnDiamond()
    {
        GameObject diamond = PoolManager.Instance.GetObjectFromPool(m_Diamond);
        diamond.transform.position = GetRandomSpawnPosition();
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomSpawnPos = new Vector2(0, 3f);
        bool placed = false;
        int stopCount = 0;

        while (!placed)
        {
            if(stopCount++ > 1000)
            {
                Debug.Log("Reached Stop Count");
                randomSpawnPos = new Vector2(0, 3f);
                break;
            }

            randomSpawnPos.x = Random.Range(m_MinPosition.x, m_MaxPosition.x);
            randomSpawnPos.y = Random.Range(m_MinPosition.y, m_MaxPosition.y);

            if (Physics2D.OverlapCircle(randomSpawnPos, 1f, m_LayerMask) == null)
            {
                placed = true;
            }
        }

        return randomSpawnPos;
    }
}
