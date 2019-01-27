using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private GameObject secondLayer;
    private float timeAlive;

    void Start()
    {
        this.timeAlive = Time.time;
    }

    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward, - Time.deltaTime * 30);

        if (timeAlive > 30.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        }
	}
}
