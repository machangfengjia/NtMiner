﻿using System.Runtime.InteropServices;
using NTMiner.Gpus.Nvapi.Native.Interfaces;

namespace NTMiner.Gpus.Nvapi.Native.General.Structures
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct LongString : IInitializable
    {
        public const int LongStringLength = 256;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LongStringLength)]
        private readonly string _Value;

        public string Value
        {
            get => _Value;
        }

        public LongString(string value)
        {
            _Value = value ?? string.Empty;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}