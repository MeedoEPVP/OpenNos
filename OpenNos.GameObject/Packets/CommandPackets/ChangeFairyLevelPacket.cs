﻿////<auto-generated <- Codemaid exclusion for now (PacketIndex Order is important for maintenance)

using OpenNos.Core;

namespace OpenNos.GameObject
{
    [PacketHeader("$FLvl", PassNonParseablePacket = true)]
    public class ChangeFairyLevelPacket : PacketDefinition
    {
        #region Properties

        [PacketIndex(0)]
        public short FairyLevel { get; set; }

        #endregion
    }
}