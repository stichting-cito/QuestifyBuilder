using System;
using System.Collections.Generic;
using Cito.Tester.Common;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Versioning;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface IHistoryService
    {
        List<object> GetOpenWindows();
        void ShowDifferencesWindow(MetaDataCompareResult metaDataCompareResult, string oldVersion, string newVersion);
        void ShowDifferencesWindow(ResourceHistoryEntity resourceHistoryEntity1, ResourceHistoryEntity resourceHistoryEntity2, Type type, ResourceManagerBase resourceManager);
    }
}
