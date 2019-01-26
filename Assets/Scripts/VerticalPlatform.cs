using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
	private PlatformEffector2D platformeffect;
	private float translation;

    // Start is called before the first frame update
    void Start()
    {
		platformeffect = GetComponent<PlatformEffector2D>();
    }

	// Update is called once per frame
	void Update()
	{
		translation = Input.GetAxis("Vertical");
		if(translation >= 1)
		{
			platformeffect.rotationalOffset = 180f;
		}

		if (Input.GetButtonDown("Jump"))
		{
			platformeffect.rotationalOffset = 0f;
		}
	}
}