using System.Collections;
using UnityEngine;
public class effectTranss7 : MonoBehaviour
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
			var random = Random.Range(0, 4);
			if (random == 0)
				_effectTranss.TriggerFire(_t.localPosition);
			else if (random == 1)
				_effectTranss.TriggerEarth(_t.localPosition);
			else if (random == 2)
				_effectTranss.TriggerIce(_t.localPosition);
			else if (random == 3)
				_effectTranss.TriggerWater(_t.localPosition);
			yield return new WaitForSeconds(Random.Range(.5f, 1f));
		}
	}
}
