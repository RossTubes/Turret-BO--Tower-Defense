using System.Collections;
using System.Collections.Generic;
using UnityEngine;

     public class LaserTurret : MonoBehaviour
    {

        private Transform target;

        [Header("General")]
        public float range = 15f;

        [Header ("Use Bullets (Default)")]
        public GameObject bulletprefab;
        public float fireRate = 1f;
        private float fireCountdown = 0f;


        [Header("Use Laser")]
        public bool useLaser = false;
        public LineRenderer lineRenderer;
        public GameObject ImpactEffect;
        public Light impactLight;

        public int damegeOverTime = 30;

        [Header("Unity Setup Fields")]

        public string enemyTag = "enemy";

        public Transform PartToRotate;
        public float turnspeed = 10f;
        public Transform firepoint;

        // Start is called before the first frame update
        public void Start()
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }
        // Update is called once per frame
        public void UpdateTarget()
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
        void Update()
        {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    ImpactEffect.GetComponent<ParticleSystem>().Stop();
                    //impactLight.enabled = false;
                    //ImpactEffect.GetComponent<ParticleSystem>().Stop();
                }
            }
            return;
        }
            
            LockOnTarget();
            if (useLaser)
            {
                Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
            }
        }
        
        void LockOnTarget ()
    {
        //target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
        void Laser ()
        {
        target.GetComponent<Enemy>().TakeDamage(damegeOverTime * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            //GameObject laserEffect = Instantiate(ImpactEffect, target.position, Quaternion.identity);
            lineRenderer.enabled = true;
            //ImpactEffect.Play();
            //laserEffect.transform.parent = target.transform;
            //laserEffect.Play();
            //this.ImpactEffect.Play();
            //impactLight.enabled = true;
            //keeps the particles from not dying
        }
            ImpactEffect.GetComponent <ParticleSystem>().Play();

        lineRenderer.SetPosition(0, firepoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firepoint.position - target.position;
 

        ImpactEffect.transform.position = target.position + dir.normalized * .1f;
        ImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
 

        }
        void Shoot()
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
            bullet bullet = bulletGO.GetComponent<bullet>();

            if (bullet != null)
                bullet.Seek(target);
        }
         void Damage(Transform enemy)
            {
                Destroy(enemy.gameObject);
            }
    public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
