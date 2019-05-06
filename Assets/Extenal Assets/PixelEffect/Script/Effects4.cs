using System.Collections;
using UnityEngine;
public class effectTranss4 : MonoBehaviour
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
			var random = Random.Range(0, 3);
			if (random == 0)
				_effectTranss.TriggerSlash(Random.Range(0, 3), _t.localPosition);
			else if (random == 1)
				_effectTranss.TriggerClaw(Random.value > .5f, _t.localPosition);
			else if (random == 2)
				_effectTranss.TriggerSplatterBlood(_t.localPosition);
			yield return new WaitForSeconds(Random.Range(.5f, 1f));
		}
	}
}
