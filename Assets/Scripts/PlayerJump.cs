using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

	private Vector3 position = new Vector3(0,0,0);
	private bool isJumping = false;
	private float maxHeight = 10;

    // Start is called before the first frame update
    void Start()
    {
		this.gameObject.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
		Jump();
		if (isJumping)
		{
			if (position.y < maxHeight)
			{
				position.y += 1;
			}
			else
			{
				position.y -= 1;

				if(position.y == 0)
				{
					isJumping = false;
				}
			}
		}
	}

	private void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space) && isJumping == false)
		{
			Debug.Log("Hallo");
			isJumping = true;
		}
	}
}