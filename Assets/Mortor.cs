using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class Mortor : MonoBehaviour
{
    public Transform TargetEnemy;
    public float Range = 0f;
    public GameObject _startMarker;
    public string enemyTag = "Enemy";

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject mortarShellPrefab;
    public Transform mortarShellInstantiatePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length == 0) return;

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = enemies[0];
        for (int i = 0; i < enemies.Length; ++i)
        {
            float dist = Vector3.Distance(transform.position, enemies[i].transform.position);
            if(dist < shortestDistance)
            {
                shortestDistance = dist;
                nearestEnemy = enemies[i];
            }
        }
        if (shortestDistance <= Range)
        {
            TargetEnemy = nearestEnemy.transform;
            Shoot();
        }
            
        /*
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)// && TargetEnemy == null)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (nearestEnemy != null && shortestDistance <= Range) //&& TargetEnemy == null)
            {
                TargetEnemy = nearestEnemy.transform;
            }
            else
            {
                TargetEnemy = null;
            }
        }*/
    }

    void Update()
    {
        /*
        if (TargetEnemy == null)
        {
            return;
        }

        Vector3 dir = TargetEnemy.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = LookRotation.eulerAngles;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    */
    }

    void Shoot()
    {
        GameObject BulletGO = (GameObject)Instantiate(mortarShellPrefab, mortarShellInstantiatePoint.position, mortarShellInstantiatePoint.rotation);
        MortarShell shell = BulletGO.GetComponent<MortarShell>();

        if (shell != null)
            shell.Seek(TargetEnemy, _startMarker.transform);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
