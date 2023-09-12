using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coins : MonoBehaviour
{
    public Text coinText;
    public int coinNumber = 1000;//�������

    private void Start()
    {
        // ��ʼ���ı���������ʾ��ʼ�Ľ������
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        // �����ı�������ʾ�Ľ������
        coinText.text = coinNumber.ToString();
    }

    public void AddCoins(Text coinText)
    {
        this.coinText = coinText;

        // ���ӽ������
        coinNumber += Random.Range(0, 10000); ;

        // ������ʾ����������ı�����
        UpdateCoinText();
    }

    /*public void Addcoins(Text coinText)
    {
        int.TryParse(coinText.text, out int coins);
        coins += Random.Range(0, 10000);
        ShowCoin(coinText, coins);
    }

    public void ShowCoin(Text coinText, int coins)
    {
        coinNumber = coins;
        coinText.text = coins.ToString();
    }
    */
}
