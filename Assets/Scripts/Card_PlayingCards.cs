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
        cardState = CardState_PC.��½�P;
        gameManager = GameObject.FindGameObjectWithTag("GameManager_PC").GetComponent<GameManager_PlayingCards>();//�䦳�o�Ӽ��Ҫ��C������A�̭����o�Ӳե�
    }
    private void OnMouseUp()
    {
        if (cardState.Equals(CardState_PC.�w½�P))//�d�P���A
        {
            return;//�᭱�H�U�����|����
        }

        if (cardState.Equals(CardState_PC.�t�令�\))//�d�P���A�A�ۦ�s�W
        {
            return;//�᭱�H�U�����|����
        }

        if (gameManager.ReadyToCompareCards)//�w�g�i�H���d�P�����p�U�AList����i�P
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
        cardState = CardState_PC.�w½�P;
    }
}
public enum CardState_PC//�d�P���A
{
    ��½�P, �w½�P, �t�令�\
}

public enum CardPattern_PC
{
    �L, �±�3, �±�7, ����4, ����J, ����A, ����9, �®�2, �®�K
}
