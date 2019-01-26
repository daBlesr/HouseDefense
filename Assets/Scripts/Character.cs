﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public float moveSpeed
	{
		get { return speed; }
		set { speed = value; }
	}

	private float speed;

    // Start is called before the first frame update
    private void Start()
    {
	
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

	public virtual void TakeDamage()
	{
		//Damage
	}
}
