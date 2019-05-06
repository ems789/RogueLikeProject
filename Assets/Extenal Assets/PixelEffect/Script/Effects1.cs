using System.Collections;
using UnityEngine;
public class effectTranss1 : MonoBehaviour
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
			var random = Random.Range(0, 6);
			if (random == 0)
				_effectTranss.TriggerStar(_t.localPosition);
			else if (random == 1)
				_effectTranss.TriggerCircle(_t.localPosition);
			else if (random == 2)
				_effectTranss.TriggerGlint(_t.localPosition);
			else if (random == 3)
				_effectTranss.TriggerPuff(_t.localPosition);
			else if (random == 4)
				_effectTranss.TriggerWeb(_t.localPosition);
			else if (random == 5)
				_effectTranss.TriggerBlock(_t.localPosition);
			yield return new WaitForSeconds(Random.Range(.5f, 1f));
		}
	}
}
