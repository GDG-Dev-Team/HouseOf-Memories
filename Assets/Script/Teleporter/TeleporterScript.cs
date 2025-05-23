using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    [SerializeField] private Transform Destination; 
   
   public Transform GetDestination()
    {
        return Destination;
    }

}
