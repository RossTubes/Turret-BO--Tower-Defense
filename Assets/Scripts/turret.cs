using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    //private is limited tot 1 script
    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform PartToRotate;
    public float turnspeed = 10f;
    public GameObject bulletprefab;
    public Transform firepoint;

    // Start is called before the first frame update
    public void Start()
    { 
      InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    // Update is called once per frame
    public void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float ShortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < ShortestDistance)
            {
                ShortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && ShortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    void Update ()
    {
        if (target == null)
            return;
        //target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation,Time.deltaTime * turnspeed).eulerAngles;
        //lerp grafiek hoe dichter bij hoe sneller die gaat
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        //euler stelt rotatie in als vector3
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot ()
    {
       GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
       bullet bullet = bulletGO.GetComponent<bullet>();
        //instantiate makes clones

        if (bullet != null)
            bullet.Seek(target);
    }
    public void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
