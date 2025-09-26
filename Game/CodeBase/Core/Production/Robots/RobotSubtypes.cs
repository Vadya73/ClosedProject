using System;

namespace Core
{
    [Flags]
    public enum RobotSubtypes
    {
        None = 0,
        // RobotTypes.Toy 
        Dog = 1 << 0,
        Repeater = 1 << 1,
        Transformer = 1 << 2,
        Companion = 1 << 3,
        ToyCategory = Dog | Transformer | Repeater | Companion,
        // RobotTypes.Utilitarian
        SmartVacuumCleaner = 1 << 4,
        DeliveryDrone = 1 << 5,
        SmartSpeaker = 1 << 6,
        Officiant = 1 << 7,
        SidelkaRobot = 1 << 8,
        UtilitarianCategory = SmartVacuumCleaner | DeliveryDrone | SmartSpeaker | Officiant | SidelkaRobot,
        // RobotTypes.Humanoid
        Humanoid = 1 << 9,      // 512
    }
}