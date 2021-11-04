using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ICanGiveDamage
{
    public Actor Shooter;
    [HideInInspector]
    public Rigidbody2D Rigidbody;
    [SerializeField]
    private float speed;
    private float fromEdgeDistance = 30f;
    private float damage = 1f;
    
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsOutOfBattleField())
        {
            Destroy(gameObject);
        }
     }

    public void SpeedUp()
    {
        Vector3 forceToAdd = transform.up * speed;
        Rigidbody.AddForce(forceToAdd);

    }

    private bool IsOutOfBattleField()
    {
        Vector3  screenPosition = DataHolder.MainCamera.WorldToScreenPoint(transform.position);
        if (screenPosition.x > DataHolder.MainCamera.pixelWidth + fromEdgeDistance || screenPosition.x < 0 - fromEdgeDistance) return true;
        if (screenPosition.y > DataHolder.MainCamera.pixelHeight+ fromEdgeDistance || screenPosition.y < 0 - fromEdgeDistance) return true; 
        return false;
    }

    public float GiveDamage()
    {
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(GiveDamage());
            Destroy(gameObject);
            DataHolder.EffectsHandler.MakeExplosion(transform.position);
        }
    }
}
