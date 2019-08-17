﻿using NTMiner.Gpus.Nvapi.Native.GPU;

namespace NTMiner.Gpus.Nvapi.Native.Interfaces.GPU
{
    /// <summary>
    ///     Holds information regarding a voltage domain of a performance state
    /// </summary>
    public interface IPerformanceStatesVoltage
    {
        /// <summary>
        ///     Gets the voltage domain identification
        /// </summary>
        PerformanceVoltageDomain DomainId { get; }

        /// <summary>
        ///     Gets the voltage in mV
        /// </summary>
        uint Value { get; }
    }
}