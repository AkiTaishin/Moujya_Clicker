using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// 作り方がアスペ @todo ねむい焼きそば食べたい
public class ThirdCard : MonoBehaviour
{
    public int CardBuffType;

    private void Start()
    {
        CardBuffType = Random.Range(0, 3);
    }

    private void Update()
    {        
        this.gameObject.GetComponentInChildren<Text>().text = CardBuffType.ToString();
    }

    public void ChangeType()
    {
        CardBuffType = Random.Range(0, 3);
    }
}
