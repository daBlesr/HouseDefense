using System;
using UnityEngine;

public class HitBoxTrigger : MonoBehaviour
{
	public static Action HitPlayerEvent;
	public static Action HitHomeEvent;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (HitPlayerEvent != null)
			{
				HitPlayerEvent();
			}
		}

		if(collision.gameObject.tag == "Home")
		{
			if(HitHomeEvent != null)
			{
				HitHomeEvent();
			}
		}
	}
}