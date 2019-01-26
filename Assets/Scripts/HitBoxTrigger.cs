using System;
using UnityEngine;

public class HitBoxTrigger : MonoBehaviour
{
	public static Action<bool> HitEvent;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Home")
		{
			if (HitEvent != null)
			{ 
				HitEvent(true);
			}
		}
	}
}