using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MortarShell : MonoBehaviour
{
    public float speed;

    private float explosiveRange = 1.5f;

    [SerializeField]
    private int shellDamage = 4;

    private Transform target;

    public Transform startMarker;
    public Transform endMarker;

    private float startTime;

    public float height;

    public float journeyLength = 30.2f;

    public float enemyHitDistance = 0.1f;

    [SerializeField] private LayerMask _layer;

    public void Seek(Transform _target, Transform _startMarker)
    {
        target = _target;
        startMarker = _startMarker;
        startTime = Time.time;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (Vector3.Distance(transform.position, target.position) <= enemyHitDistance)
        {
            Enemy e = target.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(0);
                Destroy(gameObject);
            }
            return;
        }

        float distCovered = (Time.time - startTime) * speed;


        float fractionOfJourney = distCovered / journeyLength;


        transform.position = Parabola(startMarker.position, target.position, height, fractionOfJourney);
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, explosiveRange, _layer);
            for (int i = 0; i < cols.Length; i++)
            {
                Enemy health = cols[i].GetComponent<Enemy>();
                if (health != null)
                {
                    health.TakeDamage(shellDamage);
                    PlayerStats.Money += collision.gameObject.GetComponent<Enemy>().value;
                }
            }
            //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(shellDamage);
            //FindObjectOfType<PlayerMoney>().playerMoney += collision.gameObject.GetComponent<Enemy>().enemyWorth;
            Destroy(gameObject);
        }
    }*/

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, explosiveRange);
    }
}