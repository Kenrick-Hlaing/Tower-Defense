using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public float explosionRadius = 0f;
    public int damage = 50;
    public GameObject impactEffect;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame){
            HitTarget();

            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if(explosionRadius > 0f){
            Explode();
        } else {
            Damage(target);
        }

        
        Destroy(gameObject);
    }

    void Damage(Transform enemy){
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null){
            e.TakeDamage(damage);
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders){
            if(collider.tag == "Enemy"){
                Damage(collider.transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
