using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Signals/SignalItem")]
public class SignalItem : ScriptableObject
{
    public UnityEvent signal;

    public void Raise()
    {
        if (signal != null)
        {
            signal.Invoke();
        }
    }
}