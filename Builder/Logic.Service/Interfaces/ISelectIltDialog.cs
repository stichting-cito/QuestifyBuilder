using ItemLayoutTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemLayoutTemplateResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{

    public interface ISelectIltDialog
    {
        System.Windows.Forms.DialogResult ShowDialog();
        ItemLayoutTemplateResourceDto SelectedEntity { get; }
    }
}
