using UnityEngine;
using System.Collections;

public class CardDummy
{
    string prefabName;

    public string PrefabName
    {
        get
        {
            return prefabName;
        }
    }

    public CardDummy (string prefabName)
    {
        this.prefabName = prefabName;
    }
}
