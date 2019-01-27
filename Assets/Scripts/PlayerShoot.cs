using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private SpriteRenderer rendererForSprite;
    private float aimSensitivity = 80f;
    private int lookDirection = 1;
    public Rigidbody2D bullet;
    private float bulletVelocity = 20f;
    private LineRenderer trajectory;
    bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {
        rendererForSprite = GetComponent<SpriteRenderer>();
        trajectory = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        changeLookDirection();
        rotateArm();

        float rad = Mathf.Deg2Rad * (this.transform.localEulerAngles.z - 90f); ;
        Vector2 velocity = new Vector2(
            lookDirection * bulletVelocity * Mathf.Cos(rad),
            bulletVelocity * Mathf.Sin(rad)
        );

        shoot(velocity);
        aim(rad, velocity);
    }

    // change direction player is facing.
    private void changeLookDirection()
    {
        float horizontalAxis = Input.GetAxis("RightStickHorizontal");
        Vector3 pivotPoint = this.gameObject.transform.parent.position;

        if (Mathf.Abs(horizontalAxis) > 0.3f && -Mathf.Sign(horizontalAxis) != lookDirection)
        {
            lookDirection = -lookDirection;

            // flip sprite.
            this.rendererForSprite.flipX = lookDirection == 1 ? false : true;
            this.gameObject.transform.parent.GetComponent<SpriteRenderer>().flipX = lookDirection == 1 ? false : true;

            // flip arm 180 degrees around Y axis.
            this.gameObject.transform.RotateAround(
                pivotPoint,
                new Vector3(0, 1, 0),
                180
            );
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
    private void shoot(Vector2 velocity)
    {
        bool shot = Input.GetButtonDown("Fire1");
        if (shot)
        {
            Rigidbody2D newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.velocity = velocity;
        }
    }

    // show aim trajectory
    private void aim(float rad, Vector2 velocity)
    {

        if (Input.GetButtonDown("Aim"))
        {
            isAiming = true;
        }

        if (Input.GetButtonUp("Aim"))
        {
            isAiming = false;
            trajectory.positionCount = 0;
        }

        if (isAiming)
        {
            trajectory.startWidth = 0.3f;
            trajectory.endWidth = 0.3f;
            trajectory.positionCount = 100;
            trajectory.material.shader = Shader.Find("Transparent/Diffuse");
            trajectory.material.color = new Color(1f, 0f, 0f, 0.15f);

            Vector3[] points = Trajectory.Compute(
                bulletVelocity,
                rad,
                transform.position,
                trajectory.positionCount,
                lookDirection
            );

            trajectory.SetPositions(points);
        }
    }
}
