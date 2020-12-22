using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(IWinFormsWindowService))]
    public class WinFormsWindowService : IWinFormsWindowService
    {
        public IEnumerable<Guid> OpenSelectDependentResourceDialog(IPropertyEntity propertyEntity)
        {
            using (var dialog = new SelectDependenceResourceDialog(propertyEntity))
            {

                var result = dialog.ShowDialog();

                if (result != System.Windows.Forms.DialogResult.Cancel)
                    return dialog.SelectedEntities.Select(s => s.ResourceId);
            }

            return new List<Guid>();
        }

        public IEnumerable<AspectResourceDto> OpenSelectAspectDialog(int bankId, List<String> currentAspects, bool enableMultipleSelect, bool showDisabledRowsAsGray)
        {
            using (var dialog = new SelectAspectResourceDialog(bankId, currentAspects, enableMultipleSelect, showDisabledRowsAsGray))
            {
                var result = dialog.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.Cancel)
                {
                    return dialog.SelectedEntities;
                }
            }

            return null;
        }
    }
}
