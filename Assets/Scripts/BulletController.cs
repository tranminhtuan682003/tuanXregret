using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;
    [SerializeField] private string bulletTag;
    [SerializeField] private float speed = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        StartCoroutine(DeActive());
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        rb.velocity = transform.forward * speed;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);
            gameObject.SetActive(false);
        }
        else if (other.gameObject.layer == 9)
        {
            other.GetComponent<IHealth>().TakeDamage(10);
        }
    }
}
