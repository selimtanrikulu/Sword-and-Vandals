using UnityEngine;

public class FillBar : MonoBehaviour
{
    [SerializeField] private GameObject pivotBar;
    
    public void UpdateBar(float currentValue, float maxValue)
    {
        Vector3 localScale = pivotBar.transform.localScale;
        localScale.y = currentValue / maxValue;
        pivotBar.transform.localScale = localScale;
    }
}
