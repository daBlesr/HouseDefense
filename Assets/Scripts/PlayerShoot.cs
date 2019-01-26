using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Renderer renderer;
    private SpriteRenderer rendererForSprite;
    private float aimSensitivity = 80f;
    private int lookDirection = 1;
    public Rigidbody2D bullet;
    private float bulletVelocity = 20f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        rendererForSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        changeLookDirection();
        rotateArm();
        shoot();   
    }

    // change direction player is facing.
    private void changeLookDirection()
    {
        float horizontalAxis = Input.GetAxis("RightStickHorizontal");

        if (Mathf.Abs(horizontalAxis) > 0.3f && -Mathf.Sign(horizontalAxis) != lookDirection)
        {
            lookDirection = -lookDirection;
            this.rendererForSprite.flipY = lookDirection == 1 ? false : true;
        }
    }

    // change arm rotation
    private void rotateArm()
    {
        float verticalAxis = Input.GetAxis("RightStickVertical");
        Vector3 pivotPoint = this.gameObject.transform.parent.position;

        if (Mathf.Abs(verticalAxis) > 0)
        {
            this.gameObject.transform.RotateAround(
                pivotPoint, // rotate around..
                new Vector3(0, 0, 1), // using axis..
                lookDirection * verticalAxis * aimSensitivity * Time.deltaTime // with angle..
            );
        }
    }

    // spawn bullet if shot.
    private void shoot()
    {
        bool shot = Input.GetButtonDown("Fire1");
        if (shot)
        {
            Rigidbody2D newBullet = Instantiate(bullet, transform.position, transform.rotation);
            float rotation = Mathf.Deg2Rad * (this.transform.localEulerAngles.z - 90f);
            Vector2 horizontalShot = new Vector2(bulletVelocity, 0);
            float length = horizontalShot.magnitude;
            newBullet.velocity = new Vector2(
                length * Mathf.Cos(rotation),
                length * Mathf.Sin(rotation)
            );
        }
    }
}
