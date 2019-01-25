using UnityEngine;

public class JumpForce : MonoBehaviour
{
	private float jumpforce = 10.0f;
	private Rigidbody2D rigid;
	private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
		rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetButtonDown("JumpPs4"))
		{
			jumping = true;
			rigid.velocity = new Vector2(0, jumpforce);
		}
		else
		{
			jumping = false;
		}
    }
}
