// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using TimeTracker.ApplicationLayer;
using TimeTracker.DomainLayer;
using TimeTracker.PresentationLayer;
using UserActivity;

namespace TimeTracker.Tests.ApplicationLayer
{
    [TestFixture]
    public class When_exit_application_event_is_raised
    {
        private IApplication _application;
        private IKeyboard _keyboard;
        private IPresentationController _presentationController;

        [SetUp]
        public void SetUp()
        {
        	var container = new RhinoAutoMocker<ApplicationController>();
        	var hotKeySpecification = container.Get<IHotKeySpecification>();
        	
        	hotKeySpecification
        		.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything))
        		.Return(true);

        	_presentationController = container.Get<IPresentationController>();
            _keyboard = container.Get<IKeyboard>();
            _application = container.Get<IApplication>();

            var systemUnderTest = container.ClassUnderTest;

            _presentationController.Raise(exit => exit.ExitApplication += null, this, EventArgs.Empty);
        }

        [Test]
        public void should_exit_application()
        {
            _application.AssertWasCalled(app=>app.Exit());
        }

        [Test]
        public void should_unsubscribe_exit_event()
        {
            _presentationController.AssertWasCalled(exit=>exit.ExitApplication -= Arg<EventHandler>.Is.Anything);
        }

        [Test]
        public void should_unsubribe_key_downn_event()
        {
            _keyboard.AssertWasCalled(keyboard=>keyboard.KeyDown -= Arg<EventHandler<KeyboardEventArgs>>.Is.Anything);  
        }
    }

    [TestFixture]
    public class When_Ctrl_Alt_T_is_pressed
    {
        private IPresentationController _presentationController;
        private KeyboardEventArgs _keyboardEventArgs;

        [SetUp]
        public void SetUp()
        {
        	var container = new RhinoAutoMocker<ApplicationController>();
            var application = container.Get<IApplication>();
            var hotKeySpecification = container.Get<IHotKeySpecification>();
            var reportPesenter = container.Get<IReportPresenter>();
            var keyboard = container.Get<IKeyboard>();

            _presentationController = container.Get<IPresentationController>();
            _keyboardEventArgs = new KeyboardEventArgs{Handled = false};
            
            hotKeySpecification
            	.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything))
            	.Return(true);

            keyboard
            	.Stub(x => x.AltPressed)
            	.Return(true);
            
            keyboard
            	.Stub(x => x.CtrlPressed)
            	.Return(true);
            
            keyboard
            	.Stub(x => x.ShiftPressed)
            	.Return(true);
            
            keyboard
            	.Stub(x => x.KeyPressed)
            	.Return(VirtualKeyCode.VK_T);

            var systemUnderTest = container.ClassUnderTest;
            
            keyboard.Raise(x=>x.KeyDown += null, this, _keyboardEventArgs);
        }

        [Test]
        public void should_show_time_tracker_view()
        {
            _presentationController
            	.AssertWasCalled(presenter=>presenter.ShowEntryView());
        }

        [Test]
        public void should_handle_key_down_event()
        {
            Assert.IsTrue(_keyboardEventArgs.Handled);
        }
    }

    [TestFixture]
    public class When_something_other_than_Ctrl_Alt_T_is_pressed
    {
        private IPresentationController _presentationController;
        private KeyboardEventArgs _keyboardEventArgs;

        [SetUp]
        public void SetUp()
        {
        	var container = new RhinoAutoMocker<ApplicationController>();
            var application = container.Get<IApplication>();
            var hotKeySpecification = container.Get<IHotKeySpecification>();
            var keyboard = container.Get<IKeyboard>();
            
            _keyboardEventArgs = new KeyboardEventArgs { Handled = false };
            _presentationController = container.Get<IPresentationController>();
                                    
            hotKeySpecification
            	.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything))
            	.Return(false);

            keyboard
            	.Stub(x => x.AltPressed)
            	.Return(false);
            
            keyboard
            	.Stub(x => x.CtrlPressed)
            	.Return(false);
            
            keyboard
            	.Stub(x => x.ShiftPressed)
            	.Return(false);
            
            keyboard
            	.Stub(x => x.KeyPressed)
            	.Return(VirtualKeyCode.VK_A);

            var systemUnderTest = container.ClassUnderTest;

            keyboard.Raise(x=>x.KeyDown += null, this, _keyboardEventArgs);
        }

        [Test]
        public void should_not_handle_key_down_event()
        {
            Assert.IsFalse(_keyboardEventArgs.Handled);
        }
    }
    
    [TestFixture]
    public class When_Disposed
    {
        private IPresentationController _presentationController;
                
        [SetUp]
        public void SetUp()
        {
        	var container = new RhinoAutoMocker<ApplicationController>();
            var application = container.Get<IApplication>();
            var hotKeySpecification = container.Get<IHotKeySpecification>();
            var keyboard = container.Get<IKeyboard>();
            var keyboardEventArgs = new KeyboardEventArgs { Handled = false };
            
            _presentationController = container.Get<IPresentationController>();

            container.ClassUnderTest.Dispose();
        }
    	
        [Test]
        public void Should_Dispose_PresentationController()
        {
        	_presentationController
        		.AssertWasCalled(x=>x.Dispose());
        }
    }
}