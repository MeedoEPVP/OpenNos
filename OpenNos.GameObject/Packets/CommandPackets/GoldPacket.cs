﻿////<auto-generated <- Codemaid exclusion for now (PacketIndex Order is important for maintenance)

using OpenNos.Core;

namespace OpenNos.GameObject
{
    [PacketHeader("$Gold", PassNonParseablePacket = true)]
    public class GoldPacket : PacketDefinition
    {
        #region Properties

        [PacketIndex(0)]
        public long Amount { get; set; }

        #endregion
    }
}