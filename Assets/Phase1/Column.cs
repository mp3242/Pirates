﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {
	public int gunsRemaining { get; set; }
    private Gun[] guns;

    public Gun this[int row]
	{
		get
		{
			if (row >= 0 && row < guns.Length)
				return guns[row];
			throw new System.IndexOutOfRangeException();
		}
		set
		{
			if (row >= 0 && row < guns.Length)
				guns [row] = value;
			throw new System.IndexOutOfRangeException();
		}
	}

	public int GetLen() {
		return guns.Length;
	}

	public void RemoveWeaponFromColumn(int row) {
		guns [row] = null;
		gunsRemaining--;
		if (gunsRemaining == 0) {
			KillColumn ();
		}
	}

	public void KillColumn() {
		for(int i = 0; i < guns.Length; i++) {
			if (guns [i] != null) {
				gunsRemaining--;
                Gun deadGun = guns[i];
				guns [i] = null;
				deadGun.GetComponent<Health> ().Die (); //statement must come last
			}
		}
		Rigidbody2D myRigidbody = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        myRigidbody.gravityScale = 1.8f;
		gameObject.transform.parent = null;
	}

	public void initColumn() {
		guns = GetComponentsInChildren<Gun> ();

		gunsRemaining = guns.Length;
	}

}
