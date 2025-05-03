using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuWindow : Window
{
    [SerializeField] private Button _buttonDay;
    [SerializeField] private Button _buttonGarage;
    [SerializeField] private Button _buttonShop;
    [SerializeField] private Button _buttonRent;
    [SerializeField] private TMP_Text _currentDay;


    public override void Initialize(WindowsController windowsController)
    {
        base.Initialize(windowsController);
    }



    public override void CloseWindow()
    {
        base.CloseWindow();
        
    }

    private void SetDay(int day) => _currentDay.text = "Day " + day.ToString();

    
}