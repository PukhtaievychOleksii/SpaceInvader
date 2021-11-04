using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Actor,ICanGiveDamage
{
    public bool Intelligent;
    public float DefaultTimeBetweenShots;
    public float DefaultShotDispercion;
    [HideInInspector]
    public float timeBetweenShots;
    [HideInInspector]
    public float shotDispertion;
    [HideInInspector]
    public Actor target;
    [SerializeField]
    private float damage;
    [SerializeField]
    private int value;
    void Start()
    {
        AddRandomMoveForce();
        target = DataHolder.SpaceShip;
        StartPermanentFireing();
        timeBetweenShots = DefaultTimeBetweenShots;
        shotDispertion = DefaultShotDispercion;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null && bullet.Shooter != this) TakeDamage(bullet.GiveDamage());
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(healthPoints <= 0)
        {
            int increment = value;
            if (Intelligent) increment *= 4;
            if (DataHolder.SpaceShip != null) DataHolder.SpaceShip.UpdatePower();
            DataHolder.Game.Match.UpdateScore(increment);
            DataHolder.EffectsHandler.MakeExplosion(transform.position);
            DataHolder.Game.Aliens.Remove(this);
            Destroy(gameObject);
        }
    }

    public void StartPermanentFireing()
    {
        StartCoroutine(FireCoroutine());
    }


    private IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        FireOnTarget();
        if (Intelligent) FollowTarget();
        StartCoroutine(FireCoroutine());
    }

    public void FireOnTarget()
    {
        if (target == null) return;
        Vector3 shootDirection = ApplyDispertion(target.transform.position);
        Bullet bullet = DataHolder.Factory.AddBullet(transform.position, shootDirection);
        bullet.SpeedUp();
        bullet.Shooter = this;
    }

    private Vector3 ApplyDispertion(Vector3 direction)
    {
        float dispertion = UnityEngine.Random.Range(shotDispertion / 3, shotDispertion);
        if (Time.deltaTime % 2 == 0) dispertion *= -1;
        return VectorHelper.RotateVector(direction, dispertion);
    }

    private void FollowTarget()
    {
        rigidbody.velocity = rigidbody.velocity.magnitude * (target.transform.position - transform.position).normalized;
    }

    public float GiveDamage()
    {
        return damage;
    }

}
