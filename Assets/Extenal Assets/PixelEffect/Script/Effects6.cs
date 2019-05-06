﻿using System.Collections;
using UnityEngine;
public class effectTranss6 : MonoBehaviour
{
	Transform _t;
	effectTranss _effectTranss;
	void Awake()
	{
		_t = transform;
		_effectTranss = GetComponentInParent<effectTranss>();
	}
	void Start()
	{
		StartCoroutine(Test());
	}
	IEnumerator Test()
	{
		yield return new WaitForSeconds(.333f);
		while (true)
		{
			if (Random.value > .5f)
				_effectTranss.TriggerPoison(_t.localPosition);
			else
				_effectTranss.TriggerDark(_t.localPosition);
			yield return new WaitForSeconds(Random.Range(.5f, 1f));
		}
	}
}
