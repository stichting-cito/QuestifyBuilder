using System;
using System.Collections.Generic;
using Cito.Tester.Common.WeakEventHandler;

namespace Questify.Builder.UnitTests.HelperFunctions
{
    class WeakEventHandlerTester
    {


        private IList<IWeakGenericEventHandler<EventArgs>> myHandlers;
        public event EventHandler<EventArgs> MyEvent
        {
            add
            {
                WeakEventUtils.AddWeakGenericEventHandler<EventArgs>(ref this.myHandlers, value, delegate (EventHandler<EventArgs> e)
                {
                    this.MyEvent -= e;
                });
            }
            remove
            {
                WeakEventUtils.RemoveWeakGenericEventHandler<EventArgs>(ref this.myHandlers, value);
            }
        }

        private void raise_MyEvent(object sender, EventArgs e)
        {
            if (this.myHandlers != null)
            {
                foreach (var evnt in myHandlers)
                {
                    if (evnt.IsAlive)
                    {
                        if (evnt.Handler != null)
                        {
                            evnt.Handler(sender, e);
                        }
                    }
                }
            }
        }



    }
}
