using System.Diagnostics;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    static class ParameterSetRoutedCommands
    {
        public static ICommand ClearValue
        {
            get { return _EnsureCommand(CommandId.ClearValue); }
        }

        public static ICommand PickValue
        {
            get { return _EnsureCommand(CommandId.PickValue); }
        }

        public static ICommand ViewValue
        {
            get { return _EnsureCommand(CommandId.ViewValue); }

        }

        public static ICommand ToggleValue
        {
            get { return _EnsureCommand(CommandId.ToggleValue); }
        }

        public static ICommand AddToCollection
        {
            get { return _EnsureCommand(CommandId.AddToCollection); }
        }

        public static ICommand RemoveFromCollection
        {
            get { return _EnsureCommand(CommandId.RemoveFromCollection); }
        }

        private static ICommand _EnsureCommand(CommandId idCommand)
        {
            if (idCommand >= 0 && idCommand < CommandId.Last)
            {
                lock (InternalCommands.SyncRoot)
                {
                    if (InternalCommands[(int)idCommand] == null)
                    {
                        ICommand newCommand = null;

                        newCommand = CreateCommand(idCommand);

                        Debug.Assert(newCommand != null);

                        InternalCommands[(int)idCommand] = newCommand;
                    }
                }
                return InternalCommands[(int)idCommand];
            }
            return null;
        }

        private static RoutedUICommand CreateCommand(CommandId idCommand)
        {
            var name = GetPropertyName(idCommand);
            var ret = new RoutedUICommand(name, name, typeof(ParameterSetRoutedCommands));
            return ret;
        }

        private static string GetPropertyName(CommandId idCommand)
        {
            return idCommand.ToString();
        }


        private enum CommandId : byte
        {
            ClearValue = 0, PickValue = 1, ViewValue = 2, ToggleValue = 3, AddToCollection = 4, RemoveFromCollection = 5,
            Last = 6
        }

        private static readonly ICommand[] InternalCommands = new ICommand[(int)CommandId.Last];
    }
}
