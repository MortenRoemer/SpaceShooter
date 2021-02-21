using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipBehavior : MonoBehaviour
{
    public GameObject bullet;
    public float accelerationPower = 1.0f;
    public float health = 100.0f;
    public float bulletSpeed = 5.0f;
        
    private Rigidbody2D body;
    private new Transform transform;
    
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform = base.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.TryGetComponent<BulletBehavior>(out var bulletBehavior))
            return;
        
        Damage(bulletBehavior.damage);
        GameObject.Destroy(bulletBehavior.gameObject);
    }

    public void Move(Vector2 direction)
    {
        var accForce = direction * (accelerationPower * Time.deltaTime);
        body.AddForce(accForce);
    }

    public void Damage(float damage)
    {
        health -= damage;
        
        if (health <= 0.0f)
            GameObject.Destroy(gameObject);
    }

    public void Shoot(Vector2 direction)
    {
        var bulletInstance = GameObject.Instantiate(bullet, transform.position + new Vector3(transform.localScale.x / 2, 0, 0), Quaternion.identity);
        bulletInstance.GetComponent<BulletBehavior>().direction = Vector2.right * bulletSpeed;
    }
}
