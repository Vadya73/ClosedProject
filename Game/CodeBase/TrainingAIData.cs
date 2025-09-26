using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

[Serializable]
public class TrainingAIData
{
    [SerializeField] private List<AiComponentConfig> _allHaveDatasets;
    [SerializeField] private List<AiComponentConfig> _allHaveNetworkArchitecture;
    [SerializeField] private List<AiComponentConfig> _allHaveModelSize;
    [SerializeField] private List<AiComponentConfig> _allHaveTrainingMethods;
    [SerializeField] private List<AiAddonConfig> _addons;
        
    public List<AiComponentConfig> AllHaveDatasets => _allHaveDatasets;
    public List<AiComponentConfig> AllHaveNetworkArchitecture => _allHaveNetworkArchitecture;
    public List<AiComponentConfig> AllHaveModelSize => _allHaveModelSize;
    public List<AiComponentConfig> AllHaveTrainingMethods => _allHaveTrainingMethods;
    public List<AiAddonConfig> Addons => _addons;

    public void Initialize()
    {
        _allHaveDatasets ??= new List<AiComponentConfig>();
        _allHaveNetworkArchitecture ??= new List<AiComponentConfig>();
        _allHaveModelSize ??= new List<AiComponentConfig>();
        _allHaveTrainingMethods ??= new List<AiComponentConfig>();
        _addons ??= new List<AiAddonConfig>();
    }

    public void AddAiComponent(ISkillPoint skillPointData)
    {
        switch (skillPointData)
        {
            case AiComponentConfig { Type: AiComponentType.Dataset } component:
                _allHaveDatasets.Add(component);
                break;
            case AiComponentConfig { Type: AiComponentType.NetworkArchitecture } component:
                _allHaveNetworkArchitecture.Add(component);
                break;
            case AiComponentConfig { Type: AiComponentType.ModelSize } component:
                _allHaveModelSize.Add(component);
                break;
            case AiComponentConfig { Type: AiComponentType.TrainingMethod } component:
                _allHaveTrainingMethods.Add(component);
                break;
        }
    }

    public void AddAiAddon(ISkillPoint skillPointData)
    {
        if (skillPointData is AiAddonConfig aiComponent)
            _addons.Add(aiComponent);
    }
}