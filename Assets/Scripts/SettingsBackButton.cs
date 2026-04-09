using System;
using UnityEngine;

public class SettingsBackButton : MonoBehaviour
{
    public Action onBack;
    
    public void OnBackPressed() => onBack?.Invoke();
}
