using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardState cardState;
    public CardPattern cardPattern;
    public GameManager gameManager;
    void Start()
    {
        cardState = CardState.未翻牌;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//找有這個標籤的遊戲物件，裡面的這個組件
    }
    private void OnMouseUp()
    {
        if (cardState.Equals(CardState.已翻牌))//卡牌狀態
        {
            return;//後面以下都不會執行
        }

        if (cardState.Equals(CardState.配對成功))//卡牌狀態，自行新增
        {
            return;//後面以下都不會執行
        }

        if (gameManager.ReadyToCompareCards)//已經可以比對卡牌的情況下，List有兩張牌
        {
            return;
        }

        OpenCard();
        gameManager.AddCardInCardComparison(this);
        gameManager.CompareCardsInList();
    }
    void OpenCard()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        cardState = CardState.已翻牌;
    }
}

public enum CardState//卡牌狀態
{
    未翻牌, 已翻牌, 配對成功
}

public enum CardPattern
{
     無,奇異果,柳橙,橘子,水蜜桃,芭樂,葡萄,蘋果,西瓜
}
