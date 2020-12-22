using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Questify.Builder.Security.ActiveDirectory.DsObjectPicker
{

    public class DirectoryObjectPicker
    {


        private DsObjectPicker _dialog;
        private IDataObject _dataObj;
        private List<DirectoryObjectSelection> _selected = new List<DirectoryObjectSelection>();


        public List<DirectoryObjectSelection> Selected
        {
            get { return _selected; }
        }


        public DirectoryObjectPicker()
        {
            _dialog = new DsObjectPicker();
            Init();
        }

        public DialogResult ShowDialog(IntPtr hwnd)
        {
            ((IDsObjectPicker)_dialog).InvokeDialog(hwnd, out _dataObj);
            if (MarshalReturnedValues(_dataObj))
            {
                return DialogResult.OK;
            }

            return DialogResult.Cancel;
        }

        private void Init()
        {
            DSOP_INIT_INFO initInfo = new DSOP_INIT_INFO();
            DSOP_SCOPE_INIT_INFO scopeInfo = CreateDomainScope();

            unsafe
            {
                initInfo.aDsScopeInfos = &scopeInfo;

                string[] attrName = { "samAccountName" }; IntPtr refArr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
                IntPtr refStr = Marshal.StringToHGlobalUni(attrName[0]);
                Marshal.WriteIntPtr(refArr, refStr);

                initInfo.cAttributesToFetch = attrName.Length;
                initInfo.apwzAttributeNames = (short*)refArr;
            }

            initInfo.cbSize = Marshal.SizeOf(initInfo);
            initInfo.pwzTargetComputer = null;
            initInfo.cDsScopeInfos = 1;
            initInfo.flOptions = (int)DSOP_INIT_INFO_FLAGS.DSOP_FLAG_MULTISELECT;

            ((IDsObjectPicker)_dialog).Initialize(ref initInfo);
        }

        private DSOP_SCOPE_INIT_INFO CreateDomainScope()
        {
            DSOP_SCOPE_INIT_INFO scope = new DSOP_SCOPE_INIT_INFO();

            scope.cbSize = Marshal.SizeOf(typeof(DSOP_SCOPE_INIT_INFO));

            scope.flType = (int)(DSOP_SCOPE_TYPE_FLAGS.DSOP_SCOPE_TYPE_ENTERPRISE_DOMAIN
    | DSOP_SCOPE_TYPE_FLAGS.DSOP_SCOPE_TYPE_EXTERNAL_UPLEVEL_DOMAIN
    | DSOP_SCOPE_TYPE_FLAGS.DSOP_SCOPE_TYPE_GLOBAL_CATALOG
    | DSOP_SCOPE_TYPE_FLAGS.DSOP_SCOPE_TYPE_UPLEVEL_JOINED_DOMAIN);

            scope.flScope = (int)(DSOP_SCOPE_INIT_INFO_FLAGS.DSOP_SCOPE_FLAG_STARTING_SCOPE |
                DSOP_SCOPE_INIT_INFO_FLAGS.DSOP_SCOPE_FLAG_WANT_PROVIDER_LDAP);

            scope.FilterFlags = new DSOP_FILTER_FLAGS();

            scope.FilterFlags.Uplevel = new DSOP_UPLEVEL_FILTER_FLAGS();
            scope.FilterFlags.Uplevel.flBothModes = (int)DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_USERS;

            scope.FilterFlags.flDownlevel = (int)DSOP_DOWNLEVEL_FLAGS.DSOP_DOWNLEVEL_FILTER_USERS;

            return scope;
        }

        private bool MarshalReturnedValues(IDataObject obj)
        {
            if (obj == null)
                return false;

            STGMEDIUM stg;

            int format = DataFormats.GetFormat(CLIPBOARD_FORMAT.CFSTR_DSOP_DS_SELECTION_LIST).Id;

            FORMATETC fe = new FORMATETC();
            fe.cfFormat = (ushort)format;
            fe.ptd = IntPtr.Zero;
            fe.dwAspect = (int)DVASPECT.DVASPECT_CONTENT;
            fe.lindex = -1; fe.tymed = (int)TYMED.TYMED_HGLOBAL;

            int rc = obj.GetData(ref fe, out stg);

            if (rc != 0)
                throw new System.ComponentModel.Win32Exception();

            unsafe
            {
                IntPtr pList = NativeMethods.GlobalLock(stg.hGlobal);

                if (pList != IntPtr.Zero)
                {

                    DS_SELECTION_LIST list = (DS_SELECTION_LIST)Marshal.PtrToStructure(pList, typeof(DS_SELECTION_LIST));

                    try
                    {
                        DS_SELECTION selection = new DS_SELECTION();
                        int pFirstSelection = pList.ToInt32() + Marshal.SizeOf(list) - Marshal.SizeOf(selection);
                        for (int i = 0; i < list.cItems; i++)
                        {
                            selection = (DS_SELECTION)Marshal.PtrToStructure(new IntPtr(pFirstSelection + (Marshal.SizeOf(selection) * i)), typeof(DS_SELECTION));
                            object[] fetched = Marshal.GetObjectsForNativeVariants(selection.pvarFetchedAttributes, list.cFetchedAttributes);
                            _selected.Add(new DirectoryObjectSelection(fetched[0] as string, Marshal.PtrToStringUni(selection.pwzName)));
                        }

                    }
                    catch
                    {
                    }
                }
                NativeMethods.GlobalUnlock(stg.hGlobal);
                Marshal.FreeHGlobal(stg.hGlobal);
            }

            return _selected.Count > 0;
        }
    }
}
