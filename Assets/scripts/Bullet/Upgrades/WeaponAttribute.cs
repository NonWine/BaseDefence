using System;
using UnityEngine;

[System.Serializable]
public class WeaponAttribute
{
    public ValueAttribute valueAttribute;
    [SerializeField] private float _value;
    public float MultyplyForUpgrade;
    private int _level;
    private  string Save_KEY;
    private float _debugAttribute;
    [NonSerialized] public float Debuf = 0f;
    public void Init(string SaveKey)
    {
        Save_KEY = SaveKey;
        _level = CurrentLevel;
        _debugAttribute = Value;
    }

    public float Value
    {
        get
        {
            float tValue = _value + (MultyplyForUpgrade * (_level ));
            tValue -= Debuf;
            return tValue;
        }
    }
    
    public int CurrentLevel
    {
        get { return PlayerPrefs.GetInt(Save_KEY + valueAttribute, _level); }

        set
        {
            _level = value;
            PlayerPrefs.SetInt(Save_KEY + valueAttribute, _level);
            _debugAttribute = Value;
        }
    }
    
}