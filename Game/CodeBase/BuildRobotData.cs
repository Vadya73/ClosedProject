using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

[Serializable]
public class BuildRobotData
{
    [SerializeField] private RobotSubtypes _playerHaveRobotSubtypes;
    [SerializeField] private List<RobotComponentConfig> _allHaveMaterials;
    [SerializeField] private List<RobotComponentConfig> _allHaveChip;
    [SerializeField] private List<RobotComponentConfig> _allHaveEngine;
    [SerializeField] private List<RobotComponentConfig> _allHaveBattery;
    [SerializeField] private List<RobotAddonConfig> _addons;
        
    public List<RobotComponentConfig> AllHaveMaterials => _allHaveMaterials;
    public List<RobotComponentConfig> AllHaveChip => _allHaveChip;
    public List<RobotComponentConfig> AllHaveEngine => _allHaveEngine;
    public List<RobotComponentConfig> AllHaveBattery => _allHaveBattery;
    public List<RobotAddonConfig> Addons => _addons;
    public RobotSubtypes PlayerHaveRobotSubtypes => _playerHaveRobotSubtypes;

    public void Initialize()
    {
        _allHaveMaterials ??= new List<RobotComponentConfig>();
        _allHaveChip ??= new List<RobotComponentConfig>();
        _allHaveEngine ??= new List<RobotComponentConfig>();
        _allHaveBattery ??= new List<RobotComponentConfig>();
        _addons ??= new List<RobotAddonConfig>();
    }
        
    public void AddRobotSubtype(RobotSubtypes subtypesToAdd)
    {
        _playerHaveRobotSubtypes |= subtypesToAdd;
    }
        
    public void DeleteRobotSubtype(RobotSubtypes subtypesToRemove)
    {
        _playerHaveRobotSubtypes &= ~subtypesToRemove;
    }

    public void AddRobotComponent(ISkillPoint skillPoint)
    {
        Debug.Log("AddSkillpoint");
        switch (skillPoint)
        {
            case RobotComponentConfig { Type: RobotComponentType.Material } materialComponent:
                _allHaveMaterials.Add(materialComponent);
                break;
            case RobotComponentConfig { Type: RobotComponentType.Chip } chipComponent:
                _allHaveChip.Add(chipComponent);
                break;
            case RobotComponentConfig { Type: RobotComponentType.Engine } engineComponent:
                _allHaveEngine.Add(engineComponent);
                break;
            case RobotComponentConfig { Type: RobotComponentType.Battery } batteryComponent:
                _allHaveBattery.Add(batteryComponent);
                break;
        }
    }

    public void AddRobotAddon(ISkillPoint skillPointData)
    {
        if (skillPointData is RobotAddonConfig aiComponent)
            _addons.Add(aiComponent);
    }
}