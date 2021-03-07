using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float damage = 1.0f;
    public Vector2 direction;
    public float SpawnTime { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        if (Time.time > SpawnTime + 5F)
        {
            gameObject.SetActive(false);
        }
    }

    public void Move(float minDistance = 0)
    {
        var moveVector = new Vector3(direction.x, direction.y, 0) * Time.deltaTime;

        if (moveVector.magnitude < minDistance)
        {
            moveVector = moveVector.normalized * minDistance;
        }

        transform.position += moveVector;
    }
}