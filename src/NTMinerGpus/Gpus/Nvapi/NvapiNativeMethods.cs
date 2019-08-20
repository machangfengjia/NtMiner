﻿using System;
using System.Runtime.InteropServices;

namespace NTMiner.Gpus.Nvapi {
    public static class NvapiNativeMethods {
        #region Delegates
        private delegate IntPtr nvapi_QueryInterfaceDelegate(uint id);
        private delegate NvStatus NvAPI_InitializeDelegate();

        internal delegate NvStatus NvAPI_EnumPhysicalGPUsDelegate([Out] NvPhysicalGpuHandle[] physicalGpus, out int gpuCount);
        internal delegate NvStatus NvAPI_EnumTCCPhysicalGPUsDelegate([Out] NvPhysicalGpuHandle[] physicalGpus, out int gpuCount);
        internal delegate NvStatus NvAPI_GPU_GetBusIdDelegate(NvPhysicalGpuHandle physicalGpu, out int busID);
        internal delegate NvStatus NvAPI_GPU_GetTachReadingDelegate(NvPhysicalGpuHandle physicalGpu, out int value);
        internal delegate NvStatus NvAPI_GPU_GetPStatesDelegate(NvPhysicalGpuHandle physicalGpu, ref NvPStates nvPStates);

        internal delegate NvStatus NvAPI_DLL_ClientPowerPoliciesGetInfoDelegate(NvPhysicalGpuHandle physicalGpu, ref NvGPUPowerInfo info);
        internal delegate NvStatus NvAPI_DLL_ClientPowerPoliciesGetStatusDelegate(NvPhysicalGpuHandle physicalGpu, ref NvGPUPowerStatus status);
        internal delegate NvStatus NvAPI_DLL_ClientPowerPoliciesSetStatusDelegate(NvPhysicalGpuHandle physicalGpu, ref NvGPUPowerStatus status);

        internal delegate NvStatus NvSetGetPStateV1Delegate(NvPhysicalGpuHandle physicalGpu, ref NvGpuPerfPstates20InfoV1 pstate);
        internal delegate NvStatus NvSetGetPStateV2Delegate(NvPhysicalGpuHandle physicalGpu, ref NvGpuPerfPstates20InfoV2 pstate);
        internal delegate NvStatus NvGetAllClockFrequenciesV2Delegate(NvPhysicalGpuHandle physicalGpu, ref NvGpuClockFrequenciesV2 freq);

        internal delegate NvStatus NvApiClientThermalPoliciesGetInfoDelegate(NvPhysicalGpuHandle physicalGpu, ref NvGpuThermalInfo outThermalInfo);
        internal delegate NvStatus NvApiClientThermalPoliciesGetSetLimitDelegate(NvPhysicalGpuHandle physicalGpu, ref NvGpuThermalLimit outThermalLimit);

        internal delegate NvStatus NvApiClientPowerPoliciesGetSetStatusDelegate(NvPhysicalGpuHandle physicalGpu, ref NvGpuPowerStatus outPowerPolicy);
        internal delegate NvStatus NvApiClientPowerPoliciesGetInfoDelegate(NvPhysicalGpuHandle physicalGpu, ref NvGpuPowerInfo outPowerInfo);

        internal delegate NvStatus NvApiGetCoolerSettingsDelegate(NvPhysicalGpuHandle physicalGpu, NvCoolerTarget targetId, ref NvCoolerSettings outCoolerInfo);
        internal delegate NvStatus NvApiRestoreCoolerSettingsDelegate(NvPhysicalGpuHandle physicalGpu, IntPtr pCoolerIndex, NvCoolerTarget targetId);
        internal delegate NvStatus NvApiSetCoolerLevelsDelegate(NvPhysicalGpuHandle physicalGpu, NvCoolerTarget coolerIndex, ref NvCoolerLevel level);
        internal delegate NvStatus NvAPI_GPU_ClientFanCoolersSetControl(NvPhysicalGpuHandle physicalGpu, PrivateFanCoolersControlV1 control);

        private static readonly nvapi_QueryInterfaceDelegate nvapi_QueryInterface;
        private static readonly NvAPI_InitializeDelegate NvAPI_Initialize;
        private static readonly bool available;

        internal static readonly NvAPI_EnumPhysicalGPUsDelegate NvAPI_EnumPhysicalGPUs;
        internal static readonly NvAPI_EnumTCCPhysicalGPUsDelegate NvAPI_EnumTCCPhysicalGPUs;
        internal static readonly NvAPI_GPU_GetBusIdDelegate NvAPI_GPU_GetBusID;
        internal static readonly NvAPI_GPU_GetTachReadingDelegate NvAPI_GPU_GetTachReading;
        internal static readonly NvAPI_GPU_GetPStatesDelegate NvAPI_GPU_GetPStates;

        internal static readonly NvAPI_DLL_ClientPowerPoliciesGetInfoDelegate NvAPI_DLL_ClientPowerPoliciesGetInfo;
        internal static readonly NvAPI_DLL_ClientPowerPoliciesGetStatusDelegate NvAPI_DLL_ClientPowerPoliciesGetStatus;
        internal static readonly NvAPI_DLL_ClientPowerPoliciesSetStatusDelegate NvAPI_DLL_ClientPowerPoliciesSetStatus;

        internal static readonly NvSetGetPStateV1Delegate NvGetPStateV1;
        internal static readonly NvSetGetPStateV2Delegate NvGetPStateV2;
        internal static readonly NvSetGetPStateV1Delegate NvSetPStateV1;
        internal static readonly NvSetGetPStateV2Delegate NvSetPStateV2;
        internal static readonly NvGetAllClockFrequenciesV2Delegate NvGetAllClockFrequenciesV2;

        internal static readonly NvApiClientThermalPoliciesGetInfoDelegate NvApiClientThermalPoliciesGetInfo;
        internal static readonly NvApiClientThermalPoliciesGetSetLimitDelegate NvApiClientThermalPoliciesGetLimit;
        internal static readonly NvApiClientThermalPoliciesGetSetLimitDelegate NvApiClientThermalPoliciesSetLimit;

        internal static readonly NvApiClientPowerPoliciesGetSetStatusDelegate NvApiClientPowerPoliciesGetStatus;
        internal static readonly NvApiClientPowerPoliciesGetSetStatusDelegate NvApiClientPowerPoliciesSetStatus;
        internal static readonly NvApiClientPowerPoliciesGetInfoDelegate NvApiClientPowerPoliciesGetInfo;

        internal static readonly NvApiGetCoolerSettingsDelegate NvApiGetCoolerSettings;
        internal static readonly NvApiSetCoolerLevelsDelegate NvApiSetCoolerLevels;
        internal static readonly NvApiRestoreCoolerSettingsDelegate NvApiRestoreCoolerSettings;

        #endregion

        private static void GetDelegate<T>(uint id, out T newDelegate)
            where T : class {
            IntPtr ptr = nvapi_QueryInterface(id);
            if (ptr != IntPtr.Zero) {
                newDelegate = Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;
            }
            else {
                newDelegate = null;
            }
        }

        static NvapiNativeMethods() {
            DllImportAttribute attribute = new DllImportAttribute("nvapi64.dll");
            attribute.CallingConvention = CallingConvention.Cdecl;
            attribute.PreserveSig = true;
            attribute.EntryPoint = "nvapi_QueryInterface";
            PInvokeDelegateFactory.CreateDelegate(attribute, out nvapi_QueryInterface);

            try {
                GetDelegate(0x0150E828, out NvAPI_Initialize);
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return;
            }

            if (NvAPI_Initialize() == NvStatus.OK) {
                GetDelegate(0x5F608315, out NvAPI_GPU_GetTachReading);
                GetDelegate(0x60DED2ED, out NvAPI_GPU_GetPStates);
                GetDelegate(0xE5AC921F, out NvAPI_EnumPhysicalGPUs);
                GetDelegate(0xD9930B07, out NvAPI_EnumTCCPhysicalGPUs);
                GetDelegate(0x1BE0B8E5, out NvAPI_GPU_GetBusID);
                GetDelegate(0x34206D86, out NvAPI_DLL_ClientPowerPoliciesGetInfo);
                GetDelegate(0x70916171, out NvAPI_DLL_ClientPowerPoliciesGetStatus);
                GetDelegate(0xAD95F5ED, out NvAPI_DLL_ClientPowerPoliciesSetStatus);

                GetDelegate(0x6FF81213, out NvGetPStateV1);
                GetDelegate(0x6FF81213, out NvGetPStateV2);
                GetDelegate(0x0F4DAE6B, out NvSetPStateV1);
                GetDelegate(0x0F4DAE6B, out NvSetPStateV2);
                GetDelegate(0xDCB616C3, out NvGetAllClockFrequenciesV2);

                GetDelegate(0x0D258BB5, out NvApiClientThermalPoliciesGetInfo);
                GetDelegate(0xE9C425A1, out NvApiClientThermalPoliciesGetLimit);
                GetDelegate(0x34C0B13D, out NvApiClientThermalPoliciesSetLimit);

                GetDelegate(0x70916171, out NvApiClientPowerPoliciesGetStatus);
                GetDelegate(0xAD95F5ED, out NvApiClientPowerPoliciesSetStatus);
                GetDelegate(0x34206D86, out NvApiClientPowerPoliciesGetInfo);

                GetDelegate(0xDA141340, out NvApiGetCoolerSettings);
                GetDelegate(0x8F6ED0FB, out NvApiRestoreCoolerSettings);
                GetDelegate(0x891FA0AE, out NvApiSetCoolerLevels);
            }

            available = true;
        }

        public static bool IsAvailable { get { return available; } }

        public static uint MakeNVAPIVersion(object param, uint version) {
            return 72 | (version << 16);
        }
    }
}
