// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.ApplicationLayer;
using TimeTracker.DomainLayer;
using UserActivity;

namespace TimeTracker.Tests.ApplicationLayer
{
    [TestFixture]
    public class When_exit_application_event_is_raised
    {
        private IApplication _application;
        private IApplicationExit _applicationExit;
        private IKeyboard _keyboard;

        [SetUp]
        public void SetUp()
        {
            ITaskEntryPresenter presenter = MockRepository.GenerateMock<ITaskEntryPresenter>();
            IHotKeySpecification hotKeySpecification = MockRepository.GenerateStub<IHotKeySpecification>();
            hotKeySpecification.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything)).Return(true);

            _keyboard = MockRepository.GenerateMock<IKeyboard>();
            _applicationExit = MockRepository.GenerateMock<IApplicationExit>();
            _application = MockRepository.GenerateMock<IApplication>();

            new ApplicationController(presenter, _applicationExit, _keyboard, _application, hotKeySpecification);

            _applicationExit.Raise(exit => exit.ExitApplication += null, this, EventArgs.Empty);
        }

        [Test]
        public void should_exit_application()
        {
            _application.AssertWasCalled(app=>app.Exit());
        }

        [Test]
        public void should_unsubscribe_exit_event()
        {
            _applicationExit.AssertWasCalled(exit=>exit.ExitApplication -= Arg<EventHandler>.Is.Anything);
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
        private ITaskEntryPresenter _presenter;
        private KeyboardEventArgs _keyboardEventArgs;

        [SetUp]
        public void SetUp()
        {
            IApplicationExit applicationExit = MockRepository.GenerateMock<IApplicationExit>();
            IApplication application = MockRepository.GenerateMock<IApplication>();
            IHotKeySpecification hotKeySpecification = MockRepository.GenerateStub<IHotKeySpecification>();
            hotKeySpecification.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything)).Return(true);

            IKeyboard keyboard = MockRepository.GenerateMock<IKeyboard>();
            keyboard.Stub(x => x.AltPressed).Return(true);
            keyboard.Stub(x => x.CtrlPressed).Return(true);
            keyboard.Stub(x => x.ShiftPressed).Return(true);
            keyboard.Stub(x => x.KeyPressed).Return(VirtualKeyCode.VK_T);

            _keyboardEventArgs = new KeyboardEventArgs{Handled = false};
            _presenter = MockRepository.GenerateMock<ITaskEntryPresenter>();

            new ApplicationController(_presenter, applicationExit, keyboard, application, hotKeySpecification);

            keyboard.Raise(x=>x.KeyDown += null, this, _keyboardEventArgs);
        }

        [Test]
        public void should_show_time_tracker_view()
        {
            _presenter.AssertWasCalled(presenter=>presenter.ShowView());
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
        private ITaskEntryPresenter _presenter;
        private KeyboardEventArgs _keyboardEventArgs;

        [SetUp]
        public void SetUp()
        {
            IApplicationExit applicationExit = MockRepository.GenerateMock<IApplicationExit>();
            IApplication application = MockRepository.GenerateMock<IApplication>();
            IHotKeySpecification _hotKeySpecification = MockRepository.GenerateStub<IHotKeySpecification>();
            _hotKeySpecification.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything)).Return(false);

            IKeyboard keyboard = MockRepository.GenerateMock<IKeyboard>();
            keyboard.Stub(x => x.AltPressed).Return(false);
            keyboard.Stub(x => x.CtrlPressed).Return(false);
            keyboard.Stub(x => x.ShiftPressed).Return(false);
            keyboard.Stub(x => x.KeyPressed).Return(VirtualKeyCode.VK_A);

            _keyboardEventArgs = new KeyboardEventArgs { Handled = false };
            _presenter = MockRepository.GenerateStrictMock<ITaskEntryPresenter>();

            new ApplicationController(_presenter, applicationExit, keyboard, application, _hotKeySpecification);

            keyboard.Raise(x=>x.KeyDown += null, this, _keyboardEventArgs);
        }

        [Test]
        public void should_not_handle_key_down_event()
        {
            Assert.IsFalse(_keyboardEventArgs.Handled);
        }
    }
}