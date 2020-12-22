using System.Collections.Generic;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface ISelectDialogFactory
    {
        ISelectIltDialog GetSelectItemLayoutTemplate(int bankId, List<ItemTypeEnum> itemTypes, bool exclude, string currentIltName);
    }
}
