using System;
using System.Runtime.InteropServices;

namespace Questify.Builder.Security.ActiveDirectory.DsObjectPicker
{

    [StructLayout(LayoutKind.Sequential)]
    internal struct FORMATETC
    {
        internal int cfFormat;
        internal IntPtr ptd;
        internal int dwAspect;
        internal int lindex;
        internal int tymed;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct STGMEDIUM
    {
        internal int tymed;
        internal IntPtr hGlobal;
        internal IntPtr pUnkForRelease;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct DSOP_INIT_INFO
    {
        internal int cbSize;
        internal string pwzTargetComputer;
        internal int cDsScopeInfos;
        internal DSOP_SCOPE_INIT_INFO* aDsScopeInfos;
        internal int flOptions;
        internal int cAttributesToFetch;
        internal short* apwzAttributeNames;
    }

    [StructLayout(LayoutKind.Sequential), Serializable]
    internal unsafe struct DSOP_SCOPE_INIT_INFO
    {
        internal int cbSize;
        internal int flType;
        internal int flScope;
        internal DSOP_FILTER_FLAGS FilterFlags;
        internal short* pwzDcName;
        internal short* pwzADsPath;
        internal void* hr;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DSOP_UPLEVEL_FILTER_FLAGS
    {
        internal int flBothModes;
        internal int flMixedModeOnly;
        internal int flNativeModeOnly;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DSOP_FILTER_FLAGS
    {
        internal DSOP_UPLEVEL_FILTER_FLAGS Uplevel;
        internal int flDownlevel;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe internal struct DS_SELECTION
    {
        internal IntPtr pwzName;
        internal IntPtr pwzADsPath;
        internal IntPtr pwzClass;
        internal IntPtr pwzUPN;
        internal IntPtr pvarFetchedAttributes;
        internal int flScopeType;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct DS_SELECTION_LIST
    {
        internal int cItems;
        internal int cFetchedAttributes;
        internal DS_SELECTION aDsSelection;
    }
}