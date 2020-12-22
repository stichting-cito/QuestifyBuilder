using System.Diagnostics;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor
{
    static class DebugItemEditorCommands
    {
        public static ICommand EnableAdvancedScoring
        {
            get { return _EnsureCommand(CommandId.EnableAdvancedScoring, false); }
        }

        private static ICommand _EnsureCommand(CommandId idCommand, bool IsRoutedCommand)
        {
            if (idCommand >= 0 && idCommand < CommandId.Last)
            {
                lock (_internalCommands.SyncRoot)
                {
                    if (_internalCommands[(int)idCommand] == null)
                    {
                        ICommand newCommand = null;

                        if (IsRoutedCommand)
                        {
                            newCommand = CreateCommand(idCommand);
                        }
                        else
                        {
                            newCommand = CreateSimpleCommand(idCommand);
                        }

                        Debug.Assert(newCommand != null);

                        _internalCommands[(int)idCommand] = newCommand;
                    }
                }
                return _internalCommands[(int)idCommand];
            }
            return null;
        }

        private static ICommand CreateSimpleCommand(CommandId idCommand)
        {
            switch (idCommand)
            {
                case CommandId.EnableAdvancedScoring:
                    return ViewModels.ScoreEditors.V2ScoringViewModel.EnableMeCommand;
                default:
                    return null;
            }
        }

        private static RoutedUICommand CreateCommand(CommandId idCommand)
        {
            var name = GetPropertyName(idCommand);
            var ret = new RoutedUICommand(name, name, typeof(DebugItemEditorCommands));
            return ret;
        }

        private static string GetPropertyName(CommandId idCommand)
        {
            return idCommand.ToString();
        }

        private enum CommandId : int
        {
            EnableAdvancedScoring = 0,

            Last = 1
        }

        private static ICommand[] _internalCommands = new ICommand[(int)CommandId.Last];
    }
}
