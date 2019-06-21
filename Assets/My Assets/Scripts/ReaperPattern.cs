using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperPattern : MonoBehaviour
{
    public Animator[] patternList;
    public Monster Me;

    void Start()
    {
        StartCoroutine("RandomPattern");
    }

    IEnumerator RandomPattern()
    {
        while (true)
        {
            int i = 0;           
            int sw = 0, sign = 1;
            int x = 0, y = 0;

            float randomSec = Random.Range(3f, 5f);
            int randNum = 2;// Random.Range(0, patternList.Length);

            Me.isPattern = false;
            yield return new WaitForSeconds(randomSec);
            Me.isPattern = true;

            Animator pattern;
            switch (randNum)
            {
                case 0:
                    for (i = 0; i <= 20; i++) 
                    {
                        yield return new WaitForSeconds(0.3f);
                        pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y + Random.Range(-3f, 3f), transform.position.z)
                            , Quaternion.identity);
                        pattern.SetTrigger("Trigger");
                    }
                    break;
                case 1:
                    // 달팽이 모양 패턴
                    i = 0; sign = -1;
                    float curX = transform.position.x;
                    float curY = transform.position.y;

                    while (i < 5)
                    {
                        i++;
                        for (int j = 0; j < i; j++)
                        {
                            curX += sign;
                            pattern = Instantiate(patternList[randNum]                                
                            , new Vector3(curX, curY, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.04f);
                        }

                        sign *= -1;

                        for (int j = 0; j < i; j++)
                        {
                            curY += sign;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(curX, curY, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.04f);
                        }
                    }
                    break;

                case 2:
                    // 십자 + 엑스자 모양 패턴
                    i = 0; x = 0; y = 0; sign = 1;
                    if (sw == 0)
                    {
                        while (i < 8)
                        {
                            i++;
                            y = i * sign;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x, transform.position.y + y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);

                            x = y;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + x, transform.position.y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);

                            sign *= -1;

                            y = i * sign;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x, transform.position.y + y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);

                            x = y;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + x, transform.position.y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);
                        }
                        sw = 1;
                    }

                    i = 0; x = 0; y = 0; sign = 1;
                    if (sw == 1)
                    {
                        while (i < 10)
                        {
                            i++;
                            y = i;
                            x = i;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);

                            sign *= -1;
                            x = i * sign;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);

                            y = i * sign;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);

                            sign *= -1;
                            x = i * sign;
                            pattern = Instantiate(patternList[randNum]
                            , new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z)
                            , Quaternion.identity);
                            pattern.SetTrigger("Trigger");
                            yield return new WaitForSeconds(0.03f);
                        }
                        sw = 0;
                    }
                    break;
            }
        }
    }
}
