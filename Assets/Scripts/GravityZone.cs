using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GravityZone : MonoBehaviour
{
    [SerializeField] private float m_ShrinkTime = 1.5f;
    public Vector2 m_InitialScale;
    private IEnumerator m_ShrinkCoroutine;
    private Collider2D m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
        m_InitialScale = transform.localScale;
    }

    private void OnEnable()
    {
        m_Collider.enabled = true;
        transform.localScale = m_InitialScale;
    }

    public void ShrinkAndDeactivate()
    {
        m_Collider.enabled = false;
        m_ShrinkCoroutine = ShrinkCoroutine();
        StartCoroutine(m_ShrinkCoroutine);
    }

    private IEnumerator ShrinkCoroutine()
    {
        float currentTime = 0;

        while (currentTime < m_ShrinkTime)
        {
            currentTime += Time.deltaTime;

            float progress = currentTime / m_ShrinkTime;

            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, progress);

            yield return null;
        }

        transform.localScale = Vector2.zero;

        gameObject.SetActive(false);
    }
}
