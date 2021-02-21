using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float damage = 1.0f;
    public Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var moveVector = new Vector3(direction.x, direction.y, 0);
        transform.position += moveVector * Time.deltaTime;
    }
}
