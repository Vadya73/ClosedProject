using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct RobotСharacters
    {
        [Range(-100,100)] public int Intellect;
        [Range(-100,100)] public int Responsiveness;
        [Range(-100,100)] public int Longevity;
        [Range(-100,100)] public int Speed;
        [Range(-100,100)] public int Weight;
    }
}