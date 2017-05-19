﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//At the beginning of each match, each player will be assigned an ID (Player1 or Player2) and
//WeaponManager will use these to make tags for all the weapons.

[RequireComponent (typeof (BaseShip))]
public class WeaponManager : MonoBehaviour {
	private int _maxWeapons;
	private BaseShip _myShip;
	private int maxHealth;
	//private int playerID;

	public Slider healthBarSlider;

	private Gun[] weaponsList;
	public Dictionary<Vector3, Gun> weaponDict;

	void Awake() {
		_myShip = GetComponent<BaseShip> ();
	}

	void Start () {
		weaponDict = new Dictionary<Vector3, Gun> ();
		AddWeapons ();
		//healthBarSlider = GetComponentInChildren<Slider> ();
		healthBarSlider.value = healthBarSlider.maxValue = maxHealth;
		Debug.Log ("dict " + weaponDict);
	}

	public void AddWeapons () {
		weaponsList = GetComponentsInChildren<Gun> ();
		foreach (Gun weapon in weaponsList)
		{
			weaponDict.Add (weapon.gameObject.transform.position, weapon);
			maxHealth += weapon.GetComponent<WeaponStatsBase> ().health;
			Debug.Log (maxHealth);
		}
	}

	public void RemoveWeapon(Vector3 v) {
		weaponDict.Remove (v);
	}

	//Call this when adding the WeaponManager script
	/*void initWeaponManager (int id) {
		playerID = id;
		_maxWeapons = _myShip.WeaponSpace_x * _myShip.WeaponSpace_y;
		AssignTags();
	}

	private void AssignTags() {
		foreach(KeyValuePair<Vector2, GameObject> entry in weaponDict)
		{
			entry.Value.tag = playerID+"-Weapon"; //playerID needs to be implemented later
		}
	}*/

	public void TakeDamage(int damage) {
		Debug.Log ("Taking damage");
		healthBarSlider.value -= damage;
	}



	//loop through, call fire() on all wpwns
	public void FireAll() {
		foreach(KeyValuePair<Vector3, Gun> weapon in weaponDict)
		{
			//weapon.Value.Fire();
			// do something with entry.Value or entry.Key

			if (weapon.Value.GetComponent<WeaponStatsBase> ().typeOfWeapon == WeaponType.FULLAUTO)
			{
				weapon.Value.Fire();
			}
			else
			{
				Debug.Log("Chaw haw haw, it's not FULLAUTO");
			}
		}
	}

	public void StartBoostAll() {
		//loop through, call boost() on all wpwns
		foreach(KeyValuePair<Vector3, Gun> entry in weaponDict)
		{
			entry.Value.StartBoost();
			// do something with entry.Value or entry.Key
		}
	}

	public void StopBoostAll() {
		//loop through, call boost() on all wpwns
		foreach(KeyValuePair<Vector3, Gun> entry in weaponDict)
		{
			entry.Value.StopBoost();
			// do something with entry.Value or entry.Key
		}
	}

	void Update () {
		FireAll ();
	}
}
