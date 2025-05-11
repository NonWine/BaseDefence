using System.Collections.Generic;
using Sirenix.OdinInspector;

[InlineProperty]
public class CardUpgradeInfo
{
    [Title("Description")]
    [HideLabel]
    [MultiLineProperty]
    [TabGroup("Description")]
    public string Description;
    [TabGroup("Data")] [PropertyOrder(-100)]
    public List<BonusInfo> Bonuses = new List<BonusInfo>();
    
    
}

[InlineProperty]
public class BonusInfo
{
    [LabelWidth(100)] [ProgressBar(0,100)] public int PercentBonus;
    [HideLabel] public StatName StatName;
    [LabelWidth(100)]  public bool isNegative;
}

