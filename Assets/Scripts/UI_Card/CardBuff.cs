using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBuff : MonoBehaviour
{
    // 自分の隣のボックスと結ぶ
    [SerializeField] public GameObject NextBox = null;

    public int CardBuffType;
    public int NextCardBuffType;


    // 何よりも早く
    private void Awake()
    {
        CardBuffType = 0;
        NextCardBuffType = 0;
    }

    private void Start()
    {
        CardBuffType = Random.Range(0, 3);
    }

    private void Update()
    {
        if(NextBox.tag == "Box")
        {
            NextCardBuffType = NextBox.GetComponent<CardBuff>().CardBuffType;
        }
        else
        {
            NextCardBuffType = NextBox.GetComponent<ThirdCard>().CardBuffType;
        }

        this.gameObject.GetComponentInChildren<Text>().text = CardBuffType.ToString();
    }

    public void ChangeType()
    {
        CardBuffType = NextCardBuffType;

        // 再帰的に数字を変えたい
        if(NextBox.tag == "Box")
        {
            NextBox.GetComponent<CardBuff>().ChangeType();
        }
        else
        {
            NextBox.GetComponent<ThirdCard>().ChangeType();
        }
    }
}
