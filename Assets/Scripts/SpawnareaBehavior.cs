using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnareaBehavior : MonoBehaviour
{
    public float width;
    public float height;

    public GameObject spawnTemplate;
    public int enemyLimit;
    public float spawnDuration;
    public float spawnIntensity;


    private GameObject[] enemies;
    private float lastSpawn;

    private float left => transform.position.x - width / 2;
    private float right => transform.position.x + width / 2;
    private float top => transform.position.y + height / 2;
    private float bottom => transform.position.y - height / 2;

    private void OnDrawGizmos()
    {
        var topLeft = new Vector3(left, top, 0);
        var topRight = new Vector3(right, top, 0);

        var bottomRight = new Vector3(right, bottom, 0);
        var bottomLeft = new Vector3(left, bottom, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        OnDrawGizmos();
    }

    void Start()
    {
        enemies = new GameObject[enemyLimit];

        for (int i = 0; i < enemyLimit; i++)
        {
            var enemy = Instantiate(spawnTemplate);

            enemies[i] = enemy;
        }
    }

    private GameObject GetAvailableEnemyObject()
    {
        for (int i = 0; i < enemyLimit; i++)
        {
            var enemy = enemies[i];

            if (!enemy.activeInHierarchy)
                return enemy;
        }

        throw new InvalidOperationException("Das war falsch, es gibt kein available Gegner.");
    }

    private void Spawn()
    {
        var availableEnemy = GetAvailableEnemyObject();

        availableEnemy.GetComponent<ShipBehavior>().Reset();

        var spawnPosX = Random.Range(left, right);
        var spawnPosY = Random.Range(bottom, top);
        var spawnPosZ = 0;

        availableEnemy.transform.position = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

        availableEnemy.SetActive(true);

        lastSpawn = Time.time;
    }

    private bool ShouldSpawn()
    {
        if (Time.time - lastSpawn < spawnDuration)
            return false;

        if (enemies.Any(x => x.activeInHierarchy))
            return false;

        return Random.Range(0, 1) < spawnIntensity;
    }


    void Update()
    {
        if (ShouldSpawn())
            Spawn();
    }
}