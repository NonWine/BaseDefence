using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private Window[] _windows;
    [ShowInInspector, ReadOnly] private Dictionary<Type, Window> _windowsContainer = new Dictionary<Type, Window>();
    private Window _currentWindow;
    [SerializeField] private Button waveButton;
    [SerializeField] private Button upgradePlayerButton;
    [SerializeField] private Button upgradeWeaponButton;
    
    private void OnValidate()
    {
        Init();
    }

    private void Init()
    {
        foreach (var window in _windows)
        {
            if (!_windowsContainer.ContainsKey(window.GetType()))
            {
                _windowsContainer.Add(window.GetType(), window);
            }
        }
    }

    private void Start()
    {
        Init();
        for (var i = 0; i < _windows.Length; i++)
        {
            _windows[i].Initialize(this);
        }
        ChangeWindow(typeof(MenuWindow));
        waveButton.onClick.AddListener(ToWave);
        upgradePlayerButton.onClick.AddListener(ToPlayerUpgrade);
        upgradeWeaponButton.onClick.AddListener(ToWeaponUpgrade);
    }

    private void OnDestroy()
    {
        waveButton.onClick.RemoveListener(ToWave);
        upgradePlayerButton.onClick.RemoveListener(ToPlayerUpgrade);
        upgradeWeaponButton.onClick.RemoveListener(ToWeaponUpgrade);
    }


    private void Update()
    { 
         _currentWindow.Tick();
    }
    

    public Window ChangeWindow(Type type)
    {
        if(_currentWindow != null)
            _currentWindow.CloseWindow();
        
        if(_windowsContainer.TryGetValue(type, out _currentWindow))
            _currentWindow.OpenWindow();
        
        return _currentWindow;
    }

    public Window GetWindow(Type key) => _windowsContainer[key];
    
    public void ToPlayerUpgrade() => ChangeWindow(typeof(PlayerUpgradeView));

    public void ToWeaponUpgrade() => ChangeWindow(typeof(WeaponUpgradeView));

    public void ToWave() => ChangeWindow(typeof(MenuWindow));

}