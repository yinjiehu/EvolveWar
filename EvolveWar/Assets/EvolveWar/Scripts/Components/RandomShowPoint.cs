using Kit.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShowPoint : MonoBehaviour {

    [SerializeField]
    List<GameObject> _points;

	// Use this for initialization
	void Start () {

        StartCoroutine(InstantiatePoint());
        //InvokeRepeating("ShowPoint", 1f, 0.1f);
	}

    IEnumerator InstantiatePoint()
    {
        while(true)
        {
            ShowPoint();
            yield return new WaitForSeconds(0.02f);
        }
    }
	
    void ShowPoint()
    {
        var prefab = RandomUtility.RandomListMember(_points);
        var temp = Instantiate(prefab);
        temp.transform.SetParent(transform);
        temp.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        temp.SetActive(true);
        if(RandomUtility.Random0To1() > 0.5f)
        {
            if(RandomUtility.Random0To1() > 0.5f)
            {
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(RandomUtility.Random0ToMax(900), RandomUtility.Random0ToMax(550) * -1, 0);
            }
            else
            {
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(RandomUtility.Random0ToMax(900), RandomUtility.Random0ToMax(550), 0);
            }
            
        }
        else
        {
            if (RandomUtility.Random0To1() > 0.5f)
            {
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(RandomUtility.Random0ToMax(900) * -1, RandomUtility.Random0ToMax(550) * -1, 0);
            }
            else
            {
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(RandomUtility.Random0ToMax(900) * -1, RandomUtility.Random0ToMax(550), 0);
            }
            
        }
    }
}
