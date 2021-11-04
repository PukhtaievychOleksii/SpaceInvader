using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : Actor,ICanTakeDamage,ICanGiveDamage
    
{

    public float MaxPower;
    [SerializeField]
    private float acceletartion,breakAcceleration;
    [SerializeField]
    private float maxVelocity,minVelocity, angularVelocity;
    
    private float speed = 0;
    private Transform muzzle;
    private float damage = 1;
    private float power;

    private CapsuleCollider2D collider;
    private GameObject turboEffect;

    void Awake()
    {
        muzzle = transform.GetChild(0);
        DataHolder.SetSpaceShip(this);
        turboEffect = transform.GetChild(1).gameObject;
        turboEffect.GetComponent<SpriteRenderer>().enabled = false;
        collider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        
    }

    public void Accelerate(bool ForwardAccelerate)
    {
        Vector3 forceToAdd = transform.up * speed;
        if (speed < maxSpeed && ForwardAccelerate)
        {
            turboEffect.GetComponent<SpriteRenderer>().enabled = true;
            speed += acceletartion * Time.deltaTime;
        }

        if (!ForwardAccelerate )
        {
            rigidbody.velocity = rigidbody.velocity * breakAcceleration;
            return;
        } 

        rigidbody.AddForce(forceToAdd);
        if (rigidbody.velocity.magnitude > maxVelocity) rigidbody.velocity *= 0.99f;
     }

    public void RestrartAccelerator()
    {
        speed = minSpeed;
        turboEffect.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void StartRotate(bool ClockWise)
    {
        float torque = angularVelocity;
        if (ClockWise) torque *= -1;
        rigidbody.AddTorque(torque);
    }

    public void StopRotate()
    {
        rigidbody.angularVelocity = 0;
    }

    public void FireForward()
    {
       Bullet bullet =  DataHolder.Factory.AddBullet(muzzle.position, transform.position +  transform.up * 5f);
       bullet.Shooter = this;
       bullet.SpeedUp();
        DataHolder.EffectsHandler.PlaySound(DataHolder.EffectsHandler.LaserSound);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (healthPoints <= 0)
        {
            DataHolder.Game.Match.OnLost();
            DataHolder.SetSpaceShip(null);
            Destroy(gameObject);
        }
    }

    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        Alien alien = collision.gameObject.GetComponent<Alien>();
        if(asteroid != null)
        {
            TakeDamage(asteroid.GiveDamage());
             asteroid.TakeDamage(GiveDamage());
            DataHolder.UIWorldHandler.UpdateHPImages();
            DataHolder.EffectsHandler.PlaySound(DataHolder.EffectsHandler.CrashSound);
            StartCoroutine(SwitchCollider());
        }
        if (bullet != null && bullet.Shooter != this)
        {
            TakeDamage(bullet.GiveDamage());
            Destroy(bullet.gameObject);
            DataHolder.UIWorldHandler.UpdateHPImages();
            DataHolder.EffectsHandler.MakeExplosion(transform.position);
        }
        if(alien != null)
        {
            TakeDamage(alien.GiveDamage());
            alien.TakeDamage(GiveDamage());
            DataHolder.UIWorldHandler.UpdateHPImages();
            DataHolder.EffectsHandler.PlaySound(DataHolder.EffectsHandler.CrashSound);
        }
    }

    public IEnumerator SwitchCollider()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(1.5f);
        collider.enabled = true;
    }

    public float GetHealthPoints()
    {
        return healthPoints;
    }

    public float GiveDamage()
    {
        return damage;
    }

    public void UpdatePower()
    {
        if(power < MaxPower)
        {
            power++;
            DataHolder.UIWorldHandler.UpdatePowerSlider(power);
        }
    }

    public void SuperShot()
    {
        if(power == MaxPower)
        {
            power = 0;
            DataHolder.UIWorldHandler.UpdatePowerSlider(power);
            int shotsNumber = 60;
            float angleAmong = 360f / shotsNumber;
            Vector3 startDirection = transform.up;
            for(int i = 0;i < shotsNumber; i++)
            {
                Vector3 target = VectorHelper.RotateVector(startDirection, angleAmong * i);
                Bullet bullet  = DataHolder.Factory.AddBullet(transform.position, target.normalized * 5);
                bullet.Shooter = this;
                bullet.SpeedUp();
                DataHolder.EffectsHandler.PlaySound(DataHolder.EffectsHandler.LaserSound);
            }
        }
    }

    
}
