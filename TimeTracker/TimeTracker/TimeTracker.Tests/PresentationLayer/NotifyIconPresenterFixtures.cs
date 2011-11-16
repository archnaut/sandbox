// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.PresentationLayer;

namespace TimeTracker.Tests.PresentationLayer
{
    [TestFixture]
    public class When_exit_application_is_subscribed
    {
        private INotifyIcon _notifyIcon;

        [SetUp]
        public void SetUp()
        {
            _notifyIcon = MockRepository.GenerateMock<INotifyIcon>();
            new NotifyIconPresenter(_notifyIcon).ExitApplication += delegate { };
        }

        [Test]
        public void should_subscribe_to_notify_icon_exit_application_event()
        {
            _notifyIcon.AssertWasCalled(icon=>icon.ExitApplication += Arg<EventHandler>.Is.Anything);
        }
    }

    [TestFixture]
    public class When_exit_application_is_unsubscribed
    {
        private INotifyIcon _notifyIcon;

        [SetUp]
        public void SetUp()
        {
            _notifyIcon = MockRepository.GenerateMock<INotifyIcon>();
            new NotifyIconPresenter(_notifyIcon).ExitApplication -= delegate { };
        }

        [Test]
        public void should_unsubscribe_from_notify_icon_exit_application_event()
        {
            _notifyIcon.AssertWasCalled(icon => icon.ExitApplication -= Arg<EventHandler>.Is.Anything);
        }
    }    
    
    [TestFixture]
    public class When_disposed
    {
        private INotifyIcon _notifyIcon;

        [SetUp]
        public void SetUp()
        {
            _notifyIcon = MockRepository.GenerateMock<INotifyIcon>();
            new NotifyIconPresenter(_notifyIcon).Dispose();
        }

        [Test]
        public void should_hide_notify_icon()
        {
            _notifyIcon.AssertWasCalled(icon => icon.Hide());
        }

        [Test]
        public void should_dispose_notify_icon()
        {
            _notifyIcon.AssertWasCalled(icon=>icon.Dispose());
        }
    }
}