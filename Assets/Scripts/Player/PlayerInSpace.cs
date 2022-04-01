using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInSpace : MonoBehaviour
{
    [SerializeField] private LayerMask m_GravityZoneLayer;
    [SerializeField] private float m_GravityTreshold = 0.8f;
    [SerializeField] private float m_MaxZoneVelocity = 20f;
    [SerializeField] private float m_MaxSpaceVelocity = 5f;
    [SerializeField] private float m_GravityStrength = 5f;
    private Rigidbody2D m_Rigidbody;
    private CircleCollider2D m_Collider;
    private SpriteRenderer m_SpriteRenderer;
    private float m_ScaledRadius;
    private bool m_InZone;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();   
        m_Collider = GetComponent<CircleCollider2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_ScaledRadius = Mathf.Max(m_Rigidbody.transform.localScale.x, m_Rigidbody.transform.localScale.y) - m_Collider.radius;
    }

    private void Start()
    {
        float randomY = Random.Range(-1f, 1f);
        float randomX = Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY);
        m_Rigidbody.AddForce(randomDirection, ForceMode2D.Impulse);
    }

    private void Update()
    {
        CheckForGravityZone();
        ClampVelocity();
        RotatePlayer();
        //FlipSprite();
    }

    private void CheckForGravityZone()
    {
        Collider2D[] zoneCheck = Physics2D.OverlapCircleAll(transform.position, 1f, m_GravityZoneLayer);

        if (zoneCheck.Length != 0)
        {
            //Debug.Log("Hoi");
            for (int i = 0; i < zoneCheck.Length; i++)
            {
                GravityZone gravityZone = zoneCheck[i].GetComponent<GravityZone>();
                if (gravityZone)
                {
                    if(Vector2.Distance(gravityZone.transform.position, m_Rigidbody.position) > m_GravityTreshold)
                    {
                        Vector2 pullDirection = (gravityZone.transform.position - m_Rigidbody.transform.position).normalized;
                        m_Rigidbody.AddForce(pullDirection * m_GravityStrength, ForceMode2D.Impulse);
                    }

                    m_InZone = true;
                    break;
                }
            }
        }
        else
        {
            m_InZone = false;
        }
    }

    private void FlipSprite()
    {
        float xVelocity = m_Rigidbody.velocity.x;
        m_SpriteRenderer.flipX = xVelocity < 0;
    }

    private void RotatePlayer()
    {
        Vector2 velocity = m_Rigidbody.velocity;
        float angle = (Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ClampVelocity()
    {
        if(m_InZone)
        {
            m_Rigidbody.velocity = new Vector2(Mathf.Clamp(m_Rigidbody.velocity.x, -m_MaxZoneVelocity, m_MaxZoneVelocity), Mathf.Clamp(m_Rigidbody.velocity.y, -m_MaxZoneVelocity, m_MaxZoneVelocity));
        }
        else
        {
            m_Rigidbody.velocity = new Vector2(Mathf.Clamp(m_Rigidbody.velocity.x, -m_MaxSpaceVelocity, m_MaxSpaceVelocity), Mathf.Clamp(m_Rigidbody.velocity.y, -m_MaxSpaceVelocity, m_MaxSpaceVelocity));
        }
    }
}
