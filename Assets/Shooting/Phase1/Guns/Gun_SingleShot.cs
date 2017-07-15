﻿using UnityEngine;
using System.Collections;

public class Gun_SingleShot : Gun_Childing
{
    public override void Fire()
    {
		if (nextFireTime < Time.time) {
			FireOneShot ();
			nextFireTime = Time.time + fireRate;
		}
    }   
}