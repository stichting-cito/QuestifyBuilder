using System;
using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    interface IWinFormsWindowService
    {
        IEnumerable<Guid> OpenSelectDependentResourceDialog(IPropertyEntity propertyEntity);

        IEnumerable<AspectResourceDto> OpenSelectAspectDialog(int bankId, List<String> currentAspects, bool enableMultipleSelect, bool showDisabledRowsAsGray);
    }
}
