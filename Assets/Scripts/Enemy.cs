using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public float health = 100;

    public int value = 50;

    [SerializeField] private Transform target;
    private int wavepointIndex = 0;

    public Transform GetTarget()
    {
        return target;
    }

    public void SetTarget(Transform value)
    {
        target = value;
    }

    void Start()
    {
        target = waypoints.points[0];
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die ()
    {
     //   Debug.Log("die");
        PlayerStats.Money += value;
        Destroy(gameObject);
        //gameObject effect = (gameObject)Instantiate(DeathEffect, )
    }

    void Update ()
    {
       // Debug.Log("update!!");
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
        GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
       // Debug.Log("next wp!!");
        if (wavepointIndex >= waypoints.points.Length - 1)
        {
            //Destroy(gameObject);
            EndPath();
            return;
        }
        wavepointIndex++;
        SetTarget(waypoints.points[wavepointIndex]);
    }

    void EndPath ()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
