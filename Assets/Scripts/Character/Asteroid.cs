using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Actor,ICanTakeDamage,ICanGiveDamage
{
    [HideInInspector]
    public int sizeIndex = 0;
    [SerializeField]
    private float chanceToChangeDirection ;
    [SerializeField]
    private float splitDispertion;
    [SerializeField]
    private float speedMultiplyer; // used to Increse the speed of smaller asteroids; 
    [SerializeField]
    private int value;
    private float damage = 1f;
    

   

    private void TryToChangeDirection()
    {
        int value = Random.Range(0, 100);
        if(value < chanceToChangeDirection)
        {
            float angle = Random.Range(0, 360);
            Quaternion quaternion = Quaternion.EulerAngles(0, 0, angle);
            rigidbody.velocity = quaternion * rigidbody.velocity;
        }
    }

    private IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(3f);
        TryToChangeDirection();
        StartCoroutine(ChangeDirection());
    }

    public void DivideAsteroid()
    {
        Vector3 DivAsteroidVelocity2 = -VectorHelper.RotateVector(rigidbody.velocity, splitDispertion);
        Vector3 DivAsteroidVelocity1 = -VectorHelper.RotateVector(rigidbody.velocity, -splitDispertion);
        if(sizeIndex < DataHolder.NumberOfAsteroidTypes - 1)
        {
            Asteroid asteroid1 = DataHolder.Factory.AddAsteroid(transform.position,sizeIndex + 1);
            asteroid1.rigidbody.velocity = DivAsteroidVelocity1 * asteroid1.speedMultiplyer;
            Asteroid asteroid2 = DataHolder.Factory.AddAsteroid(transform.position,sizeIndex + 1);
            asteroid2.rigidbody.velocity = DivAsteroidVelocity2 * asteroid2.speedMultiplyer;
        }
        DataHolder.Game.Asteroids.Remove(this);
        DataHolder.Game.LevelGenarator.TryToGoToNextLevel();
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (healthPoints <= 0)
        {
            DivideAsteroid();
            if (DataHolder.SpaceShip != null)
            {
                DataHolder.SpaceShip.UpdatePower();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if(bullet != null && bullet.Shooter is SpaceShip)
        {
            DataHolder.EffectsHandler.MakeExplosion(gameObject.transform.position);
            DataHolder.Game.Match.UpdateScore(value);
            Destroy(collision.gameObject);
            TakeDamage(bullet.GiveDamage());
            
            
        }
    }

    public float GiveDamage()
    {
        return damage;
    }
}
