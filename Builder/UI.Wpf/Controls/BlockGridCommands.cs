using System.Diagnostics;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Controls
{

    public static class BlockGridCommands
    {
        public static ICommand InsertBlockGridElement
        {
            get { return _EnsureCommand(CommandId.InsertBlockGridElement); }
        }

        public static ICommand DeleteBlockGridElement
        {
            get { return _EnsureCommand(CommandId.DeleteBlockGridElement); }
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
            var ret = new RoutedUICommand(name, name, typeof(BlockGridCommands));
            return ret;
        }

        private static string GetPropertyName(CommandId idCommand)
        {
            return idCommand.ToString();
        }


        private enum CommandId : byte
        {
            InsertBlockGridElement = 0,
            DeleteBlockGridElement = 1,

            Last = 2
        }

        private static ICommand[] _internalCommands = new ICommand[(int)CommandId.Last];
    }
}
