using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveCountdown : MonoBehaviour
{
    [SerializeField] private Text waveCountdown = null;

    // @todo Wave管理するscriptを作ってそこでこの変数を管理してね
    public int Count = 0;

    private void Start()
    {
        Count = 10;
    }

    // Update is called once per frame
    private void Update()
    {
        waveCountdown.text = ("次のWaveまであと" + Count + "体");
    }

    public void TextCountdown()
    {
        // 次のWaveまでの残り敵数
        Count -= 1;
        if (Count <= 0)
        {
            Count = 10;
        }
    }
}
