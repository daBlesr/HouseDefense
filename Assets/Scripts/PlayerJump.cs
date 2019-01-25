using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

	[SerializeField] private Vector2 position;
	private bool isJumping = false;
    private bool isFalling = false;
	private float maxHeight = 10;

	// Start is called before the first frame update
	void Start()
	{
		this.gameObject.transform.SetPositionAndRotation(position, new Quaternion());
	}
	
    // Update is called once per frame
    void Update()
    {
		Jump();
		if (isJumping)
		{
			if (position.y < maxHeight && !isFalling)
			{
				position.y += 1;
			}
			else
			{
                isFalling = true;
				position.y -= 1;

				if(position.y == 0)
				{
					isJumping = false;
                    isFalling = false;
				}
			}
		}

        this.gameObject.transform.SetPositionAndRotation(position, new Quaternion());
    }

	private void Jump()
	{
        // if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
			Debug.Log("Hallo");
			isJumping = true;
		}
	}
}