using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotManager : MonoBehaviour
{
	List<GameObject> players;

	[SerializeField]
	public int chipValue;

	public int ChipValue
	{
		get
		{
			return chipValue;
		}

	}

	// Use this for initialization
	void Start()
	{
		chipValue = 0; 

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void AddChips(int player, int chips)
	{
		chipValue += chips;

	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(chipValue);
		}
		else
		{
			chipValue = (int)stream.ReceiveNext();
		}
	}
}
