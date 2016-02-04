using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : Photon.MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public GameObject cylinder;

    private bool shuffled;
    TurnSwitch turnInteraction;

    public GameObject[] cards = new GameObject[52];
    private Stack<GameObject> cardStack;

    //cards[i] is fixed cards, like cards[0] always refers to Diamond A, the only thing changes all the time is its position
    private int[] cardNum = new int[52];
    private string[] cardPattern = new string[52];


    private GameObject[] cubeHands = new GameObject[2];
    private GameObject[] sphereHands = new GameObject[2];
    private GameObject[] communityCards = new GameObject[3];

    private Vector3 cardGap;

    public float time = 2.0f;
    private float rate = 0.0f;
    private float i = 0.0f;

    void Start()
    {
        cardGap = new Vector3(0.5f, 0, 0);


        cube = GameObject.Find("PlayerOneHand");
        sphere = GameObject.Find("PlayerTwoHand");
        cylinder = GameObject.Find("CommunityCards");

        List<GameObject> cardPrefabs = new List<GameObject>();

        string prefix = "Card_";
        string[] value = new string[]
        {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "J",
            "Q",
            "K",
            "A"
        };
        string[] suit = new string[]
        {
            "Club",
            "Diamond",
            "Heart",
            "Spade"
        };

        foreach (var v in value)
        {
            foreach (var s in suit)
            {
                cardPrefabs.Add(Resources.Load<GameObject>(prefix + v + s));
            }
        }

        cards = cardPrefabs.ToArray();

        Debug.Log("Hello from card manager(s)?");

    }
    //void Update()
    //{
    //    if (turnInteraction.RiverIsDealt == true)
    //    {

    //        DealRiver();
    //    }
    //}


    public void Shuffle()
    {
        Debug.Log("Everyday I'm Shuffling");

        var shuffledCards = new GameObject[cards.Length];

        for (int i = 0; i < cards.Length; i++)
        {
            shuffledCards[i] = cards[i];
        }

        for (int iteration = 0; iteration < 5; iteration++)
        {
            for (int i = 0; i < shuffledCards.Length; i++)
            {
                var c = shuffledCards[i];
                int j = Random.Range(0, 51);
                shuffledCards[i] = shuffledCards[j];
                shuffledCards[j] = c;
            }
        }


        cardStack = new Stack<GameObject>(shuffledCards);
        //foreach (GameObject item in cardStack)
        //{
        //    Debug.Log(item);
        //}
        shuffled = true;
    }
    public void Deal()
    {
        if (shuffled)
        {
            Debug.Log("Dealing");

            for (int j = 0; j < 4; j++) //j is the number of cards going to be dealed
            {

                if (j % 2 == 0)//if j is even(number)
                {
                    DealCardTo(cube);
                }
                else //if j is odd //if player.id == 2 
                {
                    DealCardTo(sphere);
                }
            }
        }
    }
    public void DealRiver()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("river contains card");
            DealCardTo(cylinder);
        }
    }


    private void DealCardTo(GameObject receiver)
    {
        var card = cardStack.Pop();
        //foreach (var item in cardStack)
        //{
        card = PhotonNetwork.Instantiate(card.name, receiver.transform.position, receiver.transform.rotation, 0);
        //}
        //card.transform.parent = receiver.transform;
        //card.transform.SetParent(receiver.transform, true);
        //card.transform.position = Vector3.zero;

        //if set parent kommer til at virke -
        //compare
    }

    //cube
    private GameObject[] cubeFiveCards;
    int[] cubeFiveNum = new int[5];
    string[] cubeFiveString = new string[5];

    private GameObject[] sphereFiveCards;
    int[] sphereFiveNum = new int[5];
    string[] sphereFiveString = new string[5];

    public void compareCards()
    {
        //this will have to be run after the River - or whenever the game ends - or when 
        //cubeFiveCards = new GameObject[5] { shuffledCards[0], shuffledCards[2], shuffledCards[4], shuffledCards[5], shuffledCards[6] };
        //sphereFiveCards = new GameObject[5] { shuffledCards[1], shuffledCards[3], shuffledCards[4], shuffledCards[5], shuffledCards[6] };

        //cube get children component
        //card holder klasse

        //#region Read the numbers and patterns of these five cards
        //for (int i = 0; i < 5; i++)
        //{
        //    cubeFiveNum[i] = cubeFiveCards[i].GetComponent<Cards>().cardNum;
        //    cubeFiveString[i] = cubeFiveCards[i].GetComponent<Cards>().cardPattern;
        //    sphereFiveNum[i] = sphereFiveCards[i].GetComponent<Cards>().cardNum;
        //    sphereFiveString[i] = sphereFiveCards[i].GetComponent<Cards>().cardPattern;
        //}
        //#endregion

        //pairs();
    }

    //int cubePairs = 0;
    //int cubePairNum = 0;

    //int spherePairs = 0;
    //int spherePairNum = 0;

    //private int[] largerCards(int[] cardList)
    //{
    //    //if (listI == null) throw new ArgumentNullException("listI");
    //    int temp = 0;
    //    for (int i = 0; i < cardList.Length - 1; i++)
    //    {
    //        for (int j = i + 1; j < cardList.Length; j++)
    //        {
    //            if (cardList[i] < cardList[j])
    //            {
    //                temp = cardList[i];
    //                cardList[i] = cardList[j];
    //                cardList[j] = temp;
    //            }
    //        }
    //    }
    //    return cardList;
    //}



    //void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{

    //}
    //private void pairs()
    //{
    //    for (int i = 0; i <= 3; i++)
    //    {
    //        for (int j = i + 1; j <= 4; j++)
    //        {
    //            if (cubeFiveNum[i] == cubeFiveNum[j])
    //            {
    //                cubePairs++;
    //                cubePairNum = cubeFiveNum[i];
    //            }
    //            if (sphereFiveNum[i] == sphereFiveNum[j])
    //            {
    //                spherePairs++;
    //                spherePairNum = sphereFiveNum[i];
    //            }
    //        }
    //    }

    //    if (cubePairs > spherePairs)
    //    {
    //        Debug.Log("Cube has one more pairs. So cube wins!");
    //    }
    //    else if (cubePairs < spherePairs)
    //    {
    //        Debug.Log("Sphere has one more pairs. So Sphere wins!");
    //    }
    //    else
    //    {
    //        if (cubePairs == 0 && spherePairs == 0)
    //        {
    //            int cubeTop = largerCards(cubeFiveNum)[0];
    //            int sphereTop = largerCards(sphereFiveNum)[0];
    //            for (int i = 0; i < 5; i++)
    //            {
    //                Debug.Log(cubeTop);
    //                Debug.Log(sphereTop);
    //            }

    //            if (cubeTop > sphereTop)
    //            {
    //                Debug.Log("No one has any pairs. Cube has the largest card: " + cubeTop + ". So cube wins!");
    //            }
    //            else if (cubeTop < sphereTop)
    //            {
    //                Debug.Log("No one has any pairs. Sphere has the largest card: " + sphereTop + ". So sphere wins!");
    //            }
    //            else
    //            {
    //                Debug.Log("No one has any pairs. They both have largest cards. So it's draw game!");
    //            }
    //        }
    //        else
    //        {
    //            if (cubePairNum > spherePairNum)
    //            {
    //                Debug.Log("Cube has a pair of " + cubePairNum + ". Sphere has a pair of " + spherePairNum + " . So cube wins!");
    //            }
    //            else if (cubePairNum < spherePairNum)
    //            {
    //                Debug.Log("Cube has a pair of " + cubePairNum + ". Sphere has a pair of " + spherePairNum + " . So sphere wins!");
    //            }
    //            else
    //            {
    //                Debug.Log("Cube has a pair of " + cubePairNum + ". Sphere has a pair of " + spherePairNum + " . So draw game!");
    //            }
    //        }
    //    }
    //}

}
