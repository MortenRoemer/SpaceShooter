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
    public float health;
    public float bulletSpeed = 5.0f;
    public float maxHealth;

    private Rigidbody2D body;
    private new Transform transform;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform = base.transform;
        Reset();
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.TryGetComponent<BulletBehavior>(out var bulletBehavior))
            return;

        Damage(bulletBehavior.damage);
        Destroy(bulletBehavior.gameObject);
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
            gameObject.SetActive(false);
    }

    public void Shoot(Vector2 direction)
    {
        var bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
        var bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();

        bulletBehavior.direction = transform.up.IgnoreZ() * bulletSpeed + body.velocity;

        bulletBehavior.Move(transform.localScale.y / 2);
    }

    public void Reset()
    {
        health = maxHealth;
    }
}