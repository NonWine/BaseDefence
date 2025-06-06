﻿using System;
using System.Collections.Generic;
using ModestTree;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private PointType[] _points;

    [ShowInInspector]
    public Dictionary<Team, Queue<Transform>> RoadTemplateContainer { get; private set; }

    private Transform parent1;

    [ShowInInspector] public bool bool1 => true;

    private void OnValidate()
    {
        _points = GetComponentsInChildren<PointType>();
    }

    private void Awake()
    {
        RoadTemplateContainer = new Dictionary<Team, Queue<Transform>>();

        foreach (var pointType in _points)
        {
            if (!RoadTemplateContainer.ContainsKey(pointType.Team))
            {
                RoadTemplateContainer.TryAdd(pointType.Team, new Queue<Transform>());
            }
            RoadTemplateContainer[pointType.Team].Enqueue(pointType.transform);
        }
        
    }
    
    [Button]
    public void Show()
    {
        Debug.Log("1");
    }
}

public class UnitRoadTemplate
{
   [Inject] private RoadManager _roadManager;
   private  Queue<Transform> _road;
   public Transform LastPoint { get; private set; }

   public UnitRoadTemplate()
   {
       _road = new Queue<Transform>();
   }

   public void SetRoad(Team team)
   {
       _road = new Queue<Transform>(_roadManager.RoadTemplateContainer[team]);
        TryGetPoint();
   } 
       

   public bool TryGetPoint()
   {
       if (_road.IsEmpty())
           return false;
       LastPoint = _road.Dequeue();
       return true;
   }

}