using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerBehavior : MonoBehaviour
{
    private ShipBehavior ship;
    private float deltaSinceLastShoot;
    public float shootFrequency = 1.0f;
    public Vector3 lookTarget;


    void Start()
    {
        ship = GetComponent<ShipBehavior>();
    }

    void Update()
    {
        HandlePlayerActions();
    }

    private void HandlePlayerActions()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        ship.Move(new Vector2(horizontal, vertical));

        var isShooting = Input.GetButton("Submit");

        if (isShooting && Mathf.Abs(Time.time - deltaSinceLastShoot) >= shootFrequency)
        {
            ship.Shoot(Vector2.right);
            deltaSinceLastShoot = Time.time;
        }

        var mousePosition = Input.mousePosition;
        mousePosition.z = 0;

        var position = transform.position;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(position);

        mousePosition.x = mousePosition.x - objectPos.x;
        mousePosition.y = mousePosition.y - objectPos.y;

        transform.rotation = Quaternion.LookRotation(Vector3.back, mousePosition - position);
    }
}
