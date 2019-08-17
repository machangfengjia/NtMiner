﻿using System.Linq;
using System.Runtime.InteropServices;
using NTMiner.Gpus.Nvapi.Native.Attributes;
using NTMiner.Gpus.Nvapi.Native.General.Structures;
using NTMiner.Gpus.Nvapi.Native.Interfaces;

namespace NTMiner.Gpus.Nvapi.Native.GPU.Structures
{
    /// <summary>
    ///     Holds information regarding illumination zones
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct IlluminationZoneInfoParametersV1 : IInitializable
    {
        private const int MaximumNumberOfReserved = 64;
        private const int MaximumNumberOfZones = 32;
        internal StructureVersion _Version;
        internal uint _NumberOfZones;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfReserved)]
        internal byte[] _Reserved;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaximumNumberOfZones)]
        internal IlluminationZoneInfoV1[] _Zones;

        /// <summary>
        ///     Gets the list of illumination zones.
        /// </summary>
        public IlluminationZoneInfoV1[] Zones
        {
            get => _Zones.Take((int) _NumberOfZones).ToArray();
        }
    }
}