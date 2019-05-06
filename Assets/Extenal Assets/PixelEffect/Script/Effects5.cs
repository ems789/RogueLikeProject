using System.Collections;
using UnityEngine;
public class effectTranss5 : MonoBehaviour
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
				_effectTranss.TriggerSparks(_t.localPosition);
			else
			{
				var type = Random.value > .5f ? (Random.value > .5f ? 0 : 1) : 2;
				_effectTranss.TriggerConsume(type, _t.localPosition);
			}
			yield return new WaitForSeconds(Random.Range(.5f, 1f));
		}
	}
}
