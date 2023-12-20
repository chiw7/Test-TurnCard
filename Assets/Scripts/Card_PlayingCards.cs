using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_PlayingCards : MonoBehaviour
{
    public CardState_PC cardState;
    public CardPattern_PC cardPattern;
    public GameManager_PlayingCards gameManager;
    void Start()
    {
        cardState = CardState_PC.未翻牌;
        gameManager = GameObject.FindGameObjectWithTag("GameManager_PC").GetComponent<GameManager_PlayingCards>();//找有這個標籤的遊戲物件，裡面的這個組件
    }
    private void OnMouseUp()
    {
        if (cardState.Equals(CardState_PC.已翻牌))//卡牌狀態
        {
            return;//後面以下都不會執行
        }

        if (cardState.Equals(CardState_PC.配對成功))//卡牌狀態，自行新增
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
        cardState = CardState_PC.已翻牌;
    }
}
public enum CardState_PC//卡牌狀態
{
    未翻牌, 已翻牌, 配對成功
}

public enum CardPattern_PC
{
    無, 黑梅3, 黑梅7, 紅方4, 紅方J, 紅心A, 紅心9, 黑桃2, 黑桃K
}
