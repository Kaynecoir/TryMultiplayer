using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public float width, height;
    public GameObject coin;
    public float speedGenerate, curSpeedGenerate = 0;
    public bool isPause;

    void Update()
    {
        if (!isPause) curSpeedGenerate -= Time.deltaTime;
        //curSpeedGenerate -= Time.deltaTime;
        if (curSpeedGenerate <= 0)
		{
            Vector3 pos = new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2), 0);

            GameObject go = Instantiate(coin, pos, Quaternion.identity);

            curSpeedGenerate = speedGenerate;
		}
    }
}
