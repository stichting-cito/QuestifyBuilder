using System.Diagnostics;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    public static class ScoreEditorCommands
    {
        public static ICommand GroupInteractions
        {
            get { return _EnsureCommand(CommandId.GroupInteractions); }
        }

        public static ICommand AddInteractionToGroup
        {
            get { return _EnsureCommand(CommandId.AddInteractionToGroup); }
        }

        public static ICommand Ungroup
        {
            get { return _EnsureCommand(CommandId.RemoveInteractionFromGroup); }
        }

        public static ICommand AddSet
        {
            get { return _EnsureCommand(CommandId.AddSet); }
        }

        public static ICommand RemoveSet
        {
            get { return _EnsureCommand(CommandId.RemoveSet); }
        }

        public static ICommand AutoScoringOn
        {
            get { return _EnsureCommand(CommandId.AutoScoringOn); }
        }

        public static ICommand AutoScoringOff
        {
            get { return _EnsureCommand(CommandId.AutoScoringOff); }
        }


        private static ICommand _EnsureCommand(CommandId idCommand)
        {
            if (idCommand >= 0 && idCommand < CommandId.Last)
            {
                lock (_internalCommands.SyncRoot)
                {
                    if (_internalCommands[(int)idCommand] == null)
                    {
                        ICommand newCommand = null;

                        newCommand = CreateCommand(idCommand);

                        Debug.Assert(newCommand != null);

                        _internalCommands[(int)idCommand] = newCommand;
                    }
                }
                return _internalCommands[(int)idCommand];
            }
            return null;
        }

        private static RoutedUICommand CreateCommand(CommandId idCommand)
        {
            var name = GetPropertyName(idCommand);
            var ret = new RoutedUICommand(name, name, typeof(ScoreEditorCommands));
            return ret;
        }

        private static string GetPropertyName(CommandId idCommand)
        {
            return idCommand.ToString();
        }



        private enum CommandId : byte
        {
            GroupInteractions = 0,
            AddInteractionToGroup = 1,
            RemoveInteractionFromGroup = 2,
            AddSet = 3,
            RemoveSet = 4,
            AutoScoringOn = 5,
            AutoScoringOff = 6,
            Last = 7
        }

        private static ICommand[] _internalCommands = new ICommand[(int)CommandId.Last];
    }
}
