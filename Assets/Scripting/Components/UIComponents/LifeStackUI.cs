using System.Collections;
using UnityEngine;

public class LifeStackUI : MonoBehaviour
{
    public LifeStack lifeStack;
    
    public void OnEnable()
    {
        lifeStack.Events.OnLifeStackIndexChanged += UpdateLifeStackUI;
    }

    public void OnDisable()
    {
        lifeStack.Events.OnLifeStackIndexChanged -= UpdateLifeStackUI;
    }

    private void UpdateLifeStackUI(int newIndex)
    {
        if (newIndex <= 0)
        {
            Debug.Log("Murió definitivamente");
        }
        else
        {
            Debug.Log("Murió... pero regresará");
        }
    }
}