using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperPattern : MonoBehaviour
{
    public Animator[] patternList;
    void Start()
    {
        StartCoroutine("RandomPattern");
    }

    IEnumerator RandomPattern()
    {
        while (true)
        {
            float randomSec = 3;//Random.Range(3f, 6f);
            int randNum = Random.Range(0, patternList.Length);

            Animator pattern = Instantiate(patternList[randNum], transform.position, Quaternion.identity);
            switch (randNum)
            {
                case 0:
                    pattern.SetFloat("Random", 1.0f);
                    pattern.SetTrigger("Trigger");
                    for(int i=0; i<=20;i++)
                    {
                        yield return new WaitForSeconds(0.3f);
                        pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y + Random.Range(-3f, 3f), transform.position.z)
                            , Quaternion.identity);
                        pattern.SetFloat("Random", 1.0f);
                        pattern.SetTrigger("Trigger");
                    }
                    break;
            }

            yield return new WaitForSeconds(randomSec);
        }
    }
}
