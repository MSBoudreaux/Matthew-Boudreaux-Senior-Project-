using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item Database", menuName = "InventorySystem/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetId.Add(Items[i], i);
        }
    }

    public void OnBeforeSerialize()
    {
        GetId = new Dictionary<ItemObject, int>();

    }
}
