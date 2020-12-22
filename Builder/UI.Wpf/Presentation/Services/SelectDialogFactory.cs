using System.Collections.Generic;
using System.ComponentModel.Composition;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(ISelectDialogFactory))]
    class SelectDialogFactory : ISelectDialogFactory
    {
        public ISelectIltDialog GetSelectItemLayoutTemplate(int bankId, List<ItemTypeEnum> itemTypes, bool exclude, string currentIltName)
        {
            return new SelectItemLayoutTemplateResourceDialog(bankId, itemTypes, exclude, currentIltName);
        }
    }
}
