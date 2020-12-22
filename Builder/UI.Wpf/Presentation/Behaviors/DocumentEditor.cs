using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class DocumentEditor
    {
        private static readonly List<Type> _registeredDocumentEditorTypes = new List<Type>();


        internal static void RegisterCommandHandlers(Type controlType)
        {
            Debug.Assert(controlType != null);

            lock (_registeredDocumentEditorTypes)
            {
                var shouldQuit = _registeredDocumentEditorTypes.Any(t => t.IsAssignableFrom(controlType));

                if (shouldQuit) return;

                _registeredDocumentEditorTypes.ForEach(t =>
                {
                    if (controlType.IsAssignableFrom((Type)t)) throw new ArgumentException("Should not be called");
                });

                _registeredDocumentEditorTypes.Add(controlType);
            }

            DocumentEditorCommands.RegisterClassHandlers(controlType);
        }

    }

}