using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private float m_TimeActive;
    [SerializeField] private PoolAbleObject m_GravityZonePrefab;
    private GravityZone m_ActiveGravityZone;
    private IEnumerator m_GravityZoneTimer;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(m_ActiveGravityZone)
            {
                RemoveActiveGravityZone();
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = Camera.main.nearClipPlane;

            m_ActiveGravityZone = PoolManager.Instance.GetObjectFromPool(m_GravityZonePrefab).GetComponent<GravityZone>();
            m_ActiveGravityZone.transform.position = mousePos;

            m_GravityZoneTimer = DeactivateGravityZone();
            StartCoroutine(m_GravityZoneTimer);
        }
    }

    private void RemoveActiveGravityZone()
    {
        if (m_ActiveGravityZone)
        {
            if (m_ActiveGravityZone.gameObject.activeInHierarchy)
            {
                m_ActiveGravityZone.gameObject.SetActive(false);
                m_ActiveGravityZone = null;

                StopCoroutine(m_GravityZoneTimer);
                m_GravityZoneTimer = DeactivateGravityZone();
            }
        }
    }

    private IEnumerator DeactivateGravityZone()
    {
        yield return new WaitForSeconds(m_TimeActive);
        RemoveActiveGravityZone();
    }
}
