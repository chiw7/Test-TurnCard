using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("比對卡牌的清單")]
    public List<Card> cardComparison;

    [Header("卡牌種類清單")]
    public List<CardPattern> cardsToBePutIn;//裝8張牌

    public Transform[] positions;

    [Header("已配對的卡牌數量")]
    public int matchedCardsCount = 0; 

    void Start()
    {
        //SetupCardsToBePutIn();
        //AddNewCard(CardPattern.水蜜桃);
        GenerateRandomCards();
    }
    void SetupCardsToBePutIn()//Enum轉List
    {
        Array array = Enum.GetValues(typeof(CardPattern));
        foreach (var item in array)
        {
            cardsToBePutIn.Add((CardPattern)item);//強制轉
        }
        cardsToBePutIn.RemoveAt(0);//刪掉Cardpattern.無
    }
    void GenerateRandomCards()//發牌
    {
        int positionIndex = 0;//發牌位置

        for(int i = 0; i < 2; i++)//8個一組，做兩組出來
        {
            //準備8張牌
            SetupCardsToBePutIn();

            //最大亂數不超過8
            int maxRandomNumber = cardsToBePutIn.Count;

            for(int j = 0; j < maxRandomNumber; maxRandomNumber--)
            {
                //0到8之間產生亂數 最小是0最大是7
                int randomNumber = UnityEngine.Random.Range(0, maxRandomNumber);//Unity裡面有一個Random類別的Range方法

                //抽牌
                AddNewCard(cardsToBePutIn[randomNumber], positionIndex);
                cardsToBePutIn.RemoveAt(randomNumber);
                positionIndex++;
            }
        }

    }

    void AddNewCard(CardPattern cardPattern, int positionIndex)//positionIndex位置編號
    {
        GameObject card = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));//讀取為GameObject類型的物件，載入牌的物件
        card.GetComponent<Card>().cardPattern = cardPattern;//設定是哪種水果
        card.name = "Card_" + cardPattern.ToString();//改名稱
        card.transform.position = positions[positionIndex].position;//陣列裡面的位置編號，的物件的位置

        GameObject graphic = Instantiate(Resources.Load<GameObject>("Prefabs/圖"));//載入圖的物件
        graphic.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/" + cardPattern.ToString());//設定圖樣
        graphic.transform.SetParent(card.transform);//變成牌的子物件
        graphic.transform.localPosition = new Vector3(0, 0, 0.1f);//設定座標
        graphic.transform.eulerAngles = new Vector3(0, 180, 0);//順著Y軸轉180度 翻牌時不會左右顛倒
    }

    public void AddCardInCardComparison(Card card)
    {
        cardComparison.Add(card);
    }
    public bool ReadyToCompareCards//屬性
    {
        get
        {
            if (cardComparison.Count == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public void CompareCardsInList()
    {
        if (ReadyToCompareCards)
        {
            //Debug.Log("可以比對卡牌了");
            if (cardComparison[0].cardPattern == cardComparison[1].cardPattern)
            {
                Debug.Log("兩張牌一樣");
                foreach (var card in cardComparison)//清單裡的每一個項目
                {
                    card.cardState = CardState.配對成功;
                }
                ClearCardComparison();
                matchedCardsCount = matchedCardsCount + 2;
                if (matchedCardsCount >= positions.Length)//已配對的卡牌數量，超過或等於16張
                {
                    //代表全部都成功配對了
                    StartCoroutine(ReloadScene());
                }
            }
            else 
            {
                Debug.Log("兩張牌不一樣");
                StartCoroutine(MissMatchCards());
                //TurnBackCards();
                //ClearCardComparison();
            }
        }
    }
    void ClearCardComparison()
    {
        cardComparison.Clear();
    }
    void TurnBackCards()
    {
        foreach (var card in cardComparison)//比對清單裡面的每一張牌
        {
            card.gameObject.transform.eulerAngles = Vector3.zero;// = new Vector3(0, 0, 0) 角度歸零
            card.cardState = CardState.未翻牌;
        }
    }
    IEnumerator MissMatchCards()//IEnumerator類型的方法，需要用協程來進行
    {
        yield return new WaitForSeconds(1.5f);//到這段延遲1.5秒，再接著執行以下
        TurnBackCards();
        ClearCardComparison();
    }
    IEnumerator ReloadScene()//重新洗牌
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新讀取場景。讀取目前執行中的場景
    }
}
