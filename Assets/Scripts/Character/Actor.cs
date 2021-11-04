using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour,ICanTakeDamage
{
    [SerializeField]
    protected float maxSpeed,healthPoints,minSpeed = 0f;
    protected Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        SetRigidbody();
    }
    public void Start()
    {
        SetRigidbody();
    }
    public void SetRigidbody() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedUp(Vector3 direction)
    {
        rigidbody.velocity *= 0f;
        float speed = Random.Range(minSpeed, maxSpeed);
        rigidbody.AddForce(direction * speed);
    }



    public void AddRandomMoveForce()
    {
        Vector3 direction = transform.up;
        float angle = Random.Range(1, 360);
        Quaternion quaternion = Quaternion.EulerAngles(0, 0, angle);
        direction = quaternion * direction;
        SpeedUp(direction);

    }

    public virtual void TakeDamage(float damage)
    {
        healthPoints -= damage;
      
    }
}
