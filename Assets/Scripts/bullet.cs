using UnityEngine;

public class bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public int damage = 50;
    public float ExplosionRadius = 0f;
    public GameObject impactEffect;

    public float arcHeight = 70f;
    public float height = 70f;

    public bool isMortar;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        if (!isMortar)
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
        else
        {
            float distanceThisFrame = speed * Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            arcHeight = 2f * Mathf.PI / arcHeight;
            dir += new Vector3(dir.x,arcHeight * Mathf.Sin(Time.deltaTime),dir.z);


            Debug.Log(dir.y);
            GameObject myObj;
          //  myObj.transform.localScale = new Vector3(Mathf.Sin(Time.time), Mathf.Sin(Time.time), Mathf.Sin(Time.time));
            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if (ExplosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        // Destroy(target.gameObject); 
        //  Debug.Log("we hit something");
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);

        }

        // Destroy(enemy.gameObject);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
