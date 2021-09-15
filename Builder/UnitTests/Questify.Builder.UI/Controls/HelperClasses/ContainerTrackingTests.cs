
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Controls;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls.HelperClasses
{
    [TestClass]
    public class ContainerTrackingTests
    {
        [TestMethod]
        public void Create3Objects_AllObjectsCanBeEnumeratedFromItemsTrackingTail()
        {
            //Arrange
            var someObjects = new List<string>(new[] { "a", "b", "c" });
            var itemsControl = new ItemsControlSimulator(someObjects);
            //Act
            itemsControl.Generate();
            
            //Assert
            
            //Get data
            var testList = new List<FakeControl>();
            var tracker = itemsControl.FakeControlTrackingTail;
            while(tracker!=null)
            {
                testList.Add(tracker.Container);
                tracker = tracker.Previous;
            }

            //Validate data
            Assert.AreEqual(itemsControl.Generated.Count, testList.Count, "Same objects thus same count!");

            var generated = itemsControl.Generated.Reverse().ToList();

            for (int i = 0; i < testList.Count; i++)
                Assert.IsTrue(object.ReferenceEquals(testList[i], generated[i]));
            
        }
        
        [TestMethod]
        public void Create3Objects_RemoveCAndCheckIfACanStillBeReachedViaItemsTrackerTail()
        {
            //Arrange
            var someObjects = new List<string>(new[] { "a", "b", "c" });
            var itemsControl = new ItemsControlSimulator(someObjects);
            itemsControl.Generate();
            //Act
            var control = itemsControl.GetControlById("c");
            itemsControl.Delete("c");            
            
            //Assert
            var controlA = itemsControl.GetControlById("a");

            var trackerA = itemsControl.FakeControlTrackingTail;
            while (trackerA != null && !trackerA.Container.Data.Equals(controlA.Data))
            {
                trackerA = trackerA.Previous;
            }

            Assert.IsNotNull(trackerA, "a still reachable via tracker after !");
        }



        [TestMethod]
        public void Create3Objects_RemoveAAndCheckIfItemsTrackerHasTwoRemainingNodes()
        {
            //Arrange
            var someObjects = new List<string>(new[] { "a", "b", "c" });
            var itemsControl = new ItemsControlSimulator(someObjects);
            itemsControl.Generate();
            //Act
            itemsControl.Delete("a");

            //Assert
            var tracker = itemsControl.FakeControlTrackingTail;
            int nodeCount = 0;
            while (tracker != null)
            {
                nodeCount++;
                tracker = tracker.Previous;
            }

            Assert.AreEqual(nodeCount, 2, "tracker still holds 2 nodes after tracking of its head element was stopped.");
        }

        [TestMethod]
        public void Create3Objects_RemoveBAndCheckIfItemsTrackerHasTwoRemainingNodes()
        {
            //Arrange
            var someObjects = new List<string>(new[] { "a", "b", "c" });
            var itemsControl = new ItemsControlSimulator(someObjects);
            itemsControl.Generate();

            //Act
            itemsControl.Delete("b");

            //Assert
            var tracker = itemsControl.FakeControlTrackingTail;
            int nodeCount = 0;
            while (tracker != null)
            {
                nodeCount++;
                tracker = tracker.Previous;
            }

            Assert.AreEqual(nodeCount, 2, "tracker still holds 2 nodes after tracking of an element in the middle was stopped.");
        }

        public class ItemsControlSimulator
        {

            List<string> _objects;
            List<FakeControl> _generated;

            private ContainerTracking<FakeControl> _TrackingTail; // Tail of a linked list of active cell containers

            public ItemsControlSimulator(List<string> objects)
            {
                _objects = objects;
            }

            internal ContainerTracking<FakeControl> FakeControlTrackingTail
            {
                get { return _TrackingTail; }
            }

            public ReadOnlyCollection<FakeControl> Generated
            {
                get { return _generated.AsReadOnly(); }
            }

            public void Generate()
            {
                _generated = new List<FakeControl>();
                foreach(var e in _objects )
                {
                    FakeControl control = CreateNewControl();
                    PrepareControl(control, e);
                    _generated.Add(control);
                }
            }

            private FakeControl CreateNewControl()
            {
                return new FakeControl();
            }

            private void PrepareControl(FakeControl control, string e)
            {
                control.setData(e);
                control.Tracker.StartTracking(ref _TrackingTail);
            }

            internal void Delete(string p)
            {
                var toDelete = GetControlById(p);
                _generated.Remove(toDelete);
                toDelete.Tracker.StopTracking(ref _TrackingTail );
                
            }

            internal FakeControl GetControlById(string p)
            {
                return _generated.First(e=> e.Data == p);
            }
        }

        [DebuggerDisplay("{Data}")]
        public class FakeControl
        {
            ContainerTracking<FakeControl> _root;
            string _data;

            public FakeControl()
            {
                _root = new ContainerTracking<FakeControl>(this);
            }


            internal ContainerTracking<FakeControl> Tracker
            {
                get { return _root; }
            }


            internal void setData(string e)
            {
                _data = e;
            }

            public string Data
            {
                get { return _data; }
            }
        }
    }
}
