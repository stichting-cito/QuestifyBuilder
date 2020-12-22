using System;
using System.Windows.Input;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class DocumentEditorCommands
    {
        public static void RegisterClassHandlers(Type controlType)
        {
            CommandManager.RegisterClassCommandBinding(controlType,
                new CommandBinding(ApplicationCommands.New, new ExecutedRoutedEventHandler(OnNewDocument), new CanExecuteRoutedEventHandler(OnCanNewDocument)));
        }

        private static void OnNewDocument(object target, ExecutedRoutedEventArgs args)
        {
            var itemEditorContext = Factory.GetExport<ICurrentItemEditorContext>();
            var x = Factory.GetExport<IItemEditorService>();
            x.Make_NewItem(itemEditorContext.ItemLayoutTemplateId, itemEditorContext.BankIdentifier, true, true);
        }

        private static void OnCanNewDocument(object target, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

    }
}