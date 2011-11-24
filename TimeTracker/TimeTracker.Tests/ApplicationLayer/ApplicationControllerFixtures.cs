// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;
using Rhino.Mocks;
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
            IHotKeySpecification hotKeySpecification = MockRepository.GenerateStub<IHotKeySpecification>();
            hotKeySpecification.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything)).Return(true);

            _presentationController = MockRepository.GenerateMock<IPresentationController>();
            _keyboard = MockRepository.GenerateMock<IKeyboard>();
            _application = MockRepository.GenerateMock<IApplication>();

            new ApplicationController(_presentationController, _keyboard, _application, hotKeySpecification);

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
            var application = MockRepository.GenerateMock<IApplication>();
            var hotKeySpecification = MockRepository.GenerateStub<IHotKeySpecification>();
            var reportPesenter = MockRepository.GenerateStub<IReportPresenter>();
            hotKeySpecification.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything)).Return(true);

            IKeyboard keyboard = MockRepository.GenerateMock<IKeyboard>();
            keyboard.Stub(x => x.AltPressed).Return(true);
            keyboard.Stub(x => x.CtrlPressed).Return(true);
            keyboard.Stub(x => x.ShiftPressed).Return(true);
            keyboard.Stub(x => x.KeyPressed).Return(VirtualKeyCode.VK_T);

            _keyboardEventArgs = new KeyboardEventArgs{Handled = false};
            _presentationController = MockRepository.GenerateMock<IPresentationController>();

            new ApplicationController(_presentationController, keyboard, application, hotKeySpecification);

            keyboard.Raise(x=>x.KeyDown += null, this, _keyboardEventArgs);
        }

        [Test]
        public void should_show_time_tracker_view()
        {
            _presentationController.AssertWasCalled(presenter=>presenter.ShowEntryView());
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
            var application = MockRepository.GenerateMock<IApplication>();
            var _hotKeySpecification = MockRepository.GenerateStub<IHotKeySpecification>();
            _hotKeySpecification.Stub(spec => spec.IsSatisfiedBy(Arg<IKeyboard>.Is.Anything)).Return(false);

            IKeyboard keyboard = MockRepository.GenerateMock<IKeyboard>();
            keyboard.Stub(x => x.AltPressed).Return(false);
            keyboard.Stub(x => x.CtrlPressed).Return(false);
            keyboard.Stub(x => x.ShiftPressed).Return(false);
            keyboard.Stub(x => x.KeyPressed).Return(VirtualKeyCode.VK_A);

            _keyboardEventArgs = new KeyboardEventArgs { Handled = false };
            _presentationController = MockRepository.GenerateStrictMock<IPresentationController>();

            new ApplicationController(_presentationController, keyboard, application, _hotKeySpecification);

            keyboard.Raise(x=>x.KeyDown += null, this, _keyboardEventArgs);
        }

        [Test]
        public void should_not_handle_key_down_event()
        {
            Assert.IsFalse(_keyboardEventArgs.Handled);
        }
    }
}