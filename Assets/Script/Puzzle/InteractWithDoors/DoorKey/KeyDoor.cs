using UnityEngine;

public class KeyDoor : MonoBehaviour 
{
    [SerializeField] private Key.KeyType keyType;
   
    public Key.KeyType GetKeyType(){
        return keyType;
    }

    
    public void OpenDoor(){
        gameObject.SetActive(false);
    }

   
}
