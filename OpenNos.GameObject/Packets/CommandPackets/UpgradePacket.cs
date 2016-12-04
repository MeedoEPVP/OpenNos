﻿////<auto-generated <- Codemaid exclusion for now (PacketIndex Order is important for maintenance)

using OpenNos.Core;
using OpenNos.Domain;

namespace OpenNos.GameObject
{
    [PacketHeader("$Upgrade", PassNonParseablePacket = true)]
    public class UpgradePacket : PacketDefinition
    {
        #region Properties

        [PacketIndex(0)]
        public short Slot { get; set; }

        [PacketIndex(1)]
        public UpgradeMode Mode { get; set; }

        [PacketIndex(2)]
        public UpgradeProtection Protection { get; set; }

        #endregion
    }
}
