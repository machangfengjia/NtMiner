﻿using System;
using System.Collections.Generic;
using NTMiner.Gpus.Nvapi.Native.Display.Structures;

namespace NTMiner.Gpus.Nvapi.Native.Interfaces.Display
{
    /// <summary>
    ///     Interface for all PathInfo structures
    /// </summary>
    public interface IPathInfo : IDisposable
    {
        /// <summary>
        ///     Identifies sourceId used by Windows CCD. This can be optionally set.
        /// </summary>
        uint SourceId { get; }

        /// <summary>
        ///     Contains information about the source mode
        /// </summary>
        SourceModeInfo SourceModeInfo { get; }

        /// <summary>
        ///     Contains information about path targets
        /// </summary>
        IEnumerable<IPathTargetInfo> TargetsInfo { get; }
    }
}