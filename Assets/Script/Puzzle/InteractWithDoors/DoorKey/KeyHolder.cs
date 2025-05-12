using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> KeyList;

    private void Awake(){
        KeyList = new List<Key.KeyType>();
    }

     public void AddKey(Key.KeyType keyType){
        Debug.Log("Added Key:" + keyType);
        KeyList.Add(keyType);
    
    }

    public void RemoveKey(Key.KeyType keyType){
        KeyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType){
        return KeyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
  

    KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
           if(ContainsKey(keyDoor.GetKeyType()));
           //currently holding key to open this door
           KeyDoor.OpenDoor
        }
  }
}
