using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coins : MonoBehaviour
{
    public Text coinText;
    public int coinNumber = 1000;//金币数量

    private void Start()
    {
        // 初始化文本对象以显示初始的金币数量
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        // 更新文本对象显示的金币数量
        coinText.text = coinNumber.ToString();
    }

    public void AddCoins(Text coinText)
    {
        this.coinText = coinText;

        // 增加金币数量
        coinNumber += Random.Range(0, 10000); ;

        // 更新显示金币数量的文本对象
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
