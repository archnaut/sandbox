// ReSharper disable InconsistentNaming
using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Rhino.Mocks;

namespace UserActivity.Tests
{
    [TestFixture]
    public class When_no_observers_are_registered_and_an_observer_registers_for_events
    {
        private IUser32 _user32;

        [SetUp]
        public void SetUp()
        {
            _user32 = MockRepository.GenerateMock<IUser32>();

            _user32.Stub(user32 => user32.SetWindowsHook(
                                       Arg<int>.Is.Equal(Constants.WH_KEYBOARD_LL),
                                       Arg<HookProc>.Is.Anything,
                                       Arg<IntPtr>.Is.Anything,
                                       Arg<int>.Is.Anything)).Return(1);


            Keyboard keyboard = new Keyboard(_user32);
            keyboard.KeyUp += delegate { };
        }

        [Test]
        public void Should_hook_keyboard_events()
        {
            _user32.AssertWasCalled(user32 => user32.SetWindowsHook(
                                                  Arg<int>.Is.Equal(Constants.WH_KEYBOARD_LL),
                                                  Arg<HookProc>.Is.Anything,
                                                  Arg<IntPtr>.Is.Anything,
                                                  Arg<int>.Is.Anything));
        }
    }

    [TestFixture]
    public class When_key_down_events_occurs
    {
        private EventHandler<KeyboardEventArgs> _keyDownHandler;

        [SetUp]
        public void SetUp()
        {
            HookProc callback = delegate { return 1; };

            IUser32 user32 = MockRepository.GenerateStub<IUser32>();
            user32.Stub(usr32 => usr32.SetWindowsHook(Arg<int>.Is.Anything, Arg<HookProc>.Is.Anything, Arg<IntPtr>.Is.Anything, Arg<int>.Is.Anything))
                .WhenCalled(invocation=>callback = (HookProc)invocation.Arguments[1])
                .Return(1);

            _keyDownHandler = MockRepository.GenerateStub<EventHandler<KeyboardEventArgs>>();

            Keyboard keyboard = new Keyboard(user32);
            keyboard.KeyDown += _keyDownHandler;

            KeyboardHookStruct keyboardData = new KeyboardHookStruct();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(keyboardData));
            Marshal.StructureToPtr(keyboardData, ptr, true);

            callback(Constants.HC_ACTION, Constants.WM_KEYDOWN, ptr);
        }

        [Test]
        public void should_publish_keyboard_event()
        {
            _keyDownHandler.AssertWasCalled(
                handler => handler(null, null), options => options.IgnoreArguments());
        }
    }
 
    [TestFixture]
    public class When_key_up_events_occurs
    {
        private EventHandler<KeyboardEventArgs> _keyUpEventHandler;

        [SetUp]
        public void SetUp()
        {
            HookProc callback = delegate { return 1; };

            IUser32 user32 = MockRepository.GenerateStub<IUser32>();
            user32.Stub(usr32 => usr32.SetWindowsHook(Arg<int>.Is.Anything, Arg<HookProc>.Is.Anything, Arg<IntPtr>.Is.Anything, Arg<int>.Is.Anything))
                .WhenCalled(invocation=>callback = (HookProc)invocation.Arguments[1])
                .Return(1);

            _keyUpEventHandler = MockRepository.GenerateStub<EventHandler<KeyboardEventArgs>>();

            Keyboard keyboard = new Keyboard(user32);
            keyboard.KeyUp += _keyUpEventHandler;

            KeyboardHookStruct keyboardData = new KeyboardHookStruct();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(keyboardData));
            Marshal.StructureToPtr(keyboardData, ptr, true);

            callback(Constants.HC_ACTION, Constants.WM_KEYUP, ptr);
        }

        [Test]
        public void should_publish_keyboard_event()
        {
            _keyUpEventHandler.AssertWasCalled(
                handler=>handler(null, null), options=>options.IgnoreArguments());
        }
    }

    [TestFixture]
    public class When_keyboard_event_is_unhandled
    {
        private const int KEYBOARD_HOOK_HANDLE = 1;
        private IUser32 _user32;
        private IntPtr _ptr;

        [SetUp]
        public void SetUp()
        {
            HookProc callback = delegate { return 1; };

            _user32 = MockRepository.GenerateStub<IUser32>();
            _user32.Stub(usr32 => usr32.SetWindowsHook(Arg<int>.Is.Anything, Arg<HookProc>.Is.Anything, Arg<IntPtr>.Is.Anything, Arg<int>.Is.Anything))
                .WhenCalled(invocation=>callback = (HookProc)invocation.Arguments[1])
                .Return(KEYBOARD_HOOK_HANDLE);

            EventHandler<KeyboardEventArgs> keyboardEventHandler = MockRepository.GenerateStub<EventHandler<KeyboardEventArgs>>();
            KeyboardEventArgs eventArgs = new KeyboardEventArgs {Handled = false};
            keyboardEventHandler.Stub(handler => handler(null, null)).IgnoreArguments().WhenCalled(invocation => invocation.Arguments[1] = eventArgs);

            Keyboard keyboard = new Keyboard(_user32);
            keyboard.KeyUp += keyboardEventHandler;

            KeyboardHookStruct keyboardData = new KeyboardHookStruct();
            _ptr = Marshal.AllocHGlobal(Marshal.SizeOf(keyboardData));
            Marshal.StructureToPtr(keyboardData, _ptr, true);

            callback(Constants.HC_ACTION, Constants.WM_KEYUP, _ptr);
        }

        [Test]
        public void should_publish_keyboard_event()
        {
            _user32.AssertWasCalled(user32=>user32.CallNextHook(KEYBOARD_HOOK_HANDLE, Constants.HC_ACTION, Constants.WM_KEYUP, _ptr));
        }
    }
 
    [TestFixture]
    public class When_keyboard_event_is_handled
    {
        private const int KEYBOARD_HOOK_HANDLE = 1;
        private IUser32 _user32;
        private IntPtr _ptr;
        HookProc _callback = delegate { return 1; };

        [SetUp]
        public void SetUp()
        {
            _user32 = MockRepository.GenerateStub<IUser32>();
            _user32.Stub(usr32 => usr32.SetWindowsHook(Arg<int>.Is.Anything, Arg<HookProc>.Is.Anything, Arg<IntPtr>.Is.Anything, Arg<int>.Is.Anything))
                .WhenCalled(invocation=>_callback = (HookProc)invocation.Arguments[1])
                .Return(KEYBOARD_HOOK_HANDLE);

            EventHandler<KeyboardEventArgs> keyboardEventHandler = MockRepository.GenerateStub<EventHandler<KeyboardEventArgs>>();
            keyboardEventHandler.Stub(handler => handler(null, null))
                .IgnoreArguments()
                .WhenCalled(invocation => ((KeyboardEventArgs)invocation.Arguments[1]).Handled = true);
            
            Keyboard keyboard = new Keyboard(_user32);
            keyboard.KeyUp += keyboardEventHandler;

            KeyboardHookStruct keyboardData = new KeyboardHookStruct();
            _ptr = Marshal.AllocHGlobal(Marshal.SizeOf(keyboardData));
            Marshal.StructureToPtr(keyboardData, _ptr, true);
        }

        [Test]
        public void callback_should_return_negative_one()
        {
            Assert.AreEqual(-1, _callback(Constants.HC_ACTION, Constants.WM_KEYUP, _ptr));
        }
    }

    [TestFixture]
    public class When_callback_is_called_with_nCode_other_HC_ACTION
    {
        private const int KEYBOARD_HOOK_HANDLE = 1;
        private IUser32 _user32;
        private IntPtr _ptr;

        [SetUp]
        public void SetUp()
        {
            HookProc callback = delegate { return 1; };

            _user32 = MockRepository.GenerateStub<IUser32>();
            _user32.Stub(usr32 => usr32.SetWindowsHook(Arg<int>.Is.Anything, Arg<HookProc>.Is.Anything, Arg<IntPtr>.Is.Anything, Arg<int>.Is.Anything))
                .WhenCalled(invocation=>callback = (HookProc)invocation.Arguments[1])
                .Return(KEYBOARD_HOOK_HANDLE);

            Keyboard keyboard = new Keyboard(_user32);
            keyboard.KeyUp += delegate { };

            KeyboardHookStruct keyboardData = new KeyboardHookStruct();
            _ptr = Marshal.AllocHGlobal(Marshal.SizeOf(keyboardData));
            Marshal.StructureToPtr(keyboardData, _ptr, true);

            callback(1, Constants.WM_KEYUP, _ptr);
        }

        [Test]
        public void should_call_next_handler()
        {
            _user32.Stub(user32=>user32.CallNextHook(KEYBOARD_HOOK_HANDLE, 1, Constants.WM_KEYUP, _ptr));
        }
    }

    [TestFixture]
    public class When_multiple_observers_are_registered_and_keyboard_event_occurs
    {
        private const int KEYBOARD_HOOK_HANDLE = 1;
        private IUser32 _user32;
        private IntPtr _ptr;
        private EventHandler<KeyboardEventArgs> _firstHandler;
        private EventHandler<KeyboardEventArgs> _secondHandler;

        [SetUp]
        public void SetUp()
        {
            HookProc callback = delegate { return 1; };

            _user32 = MockRepository.GenerateStub<IUser32>();
            _user32.Stub(usr32 => usr32.SetWindowsHook(Arg<int>.Is.Anything, Arg<HookProc>.Is.Anything, Arg<IntPtr>.Is.Anything, Arg<int>.Is.Anything))
                .WhenCalled(invocation=>callback = (HookProc)invocation.Arguments[1])
                .Return(KEYBOARD_HOOK_HANDLE);

            _firstHandler = MockRepository.GenerateMock<EventHandler<KeyboardEventArgs>>();
            _secondHandler = MockRepository.GenerateMock<EventHandler<KeyboardEventArgs>>();
            Keyboard keyboard = new Keyboard(_user32);

            keyboard.KeyUp += _firstHandler;
            keyboard.KeyUp += _secondHandler;

            KeyboardHookStruct keyboardData = new KeyboardHookStruct();
            _ptr = Marshal.AllocHGlobal(Marshal.SizeOf(keyboardData));
            Marshal.StructureToPtr(keyboardData, _ptr, true);

            callback(Constants.HC_ACTION, Constants.WM_KEYUP, _ptr);
        }

        [Test]
        public void should_publish_event_to_first_observer()
        {
            _firstHandler.AssertWasCalled(handler=>handler(null, null), options=>options.IgnoreArguments());
        }

        [Test]
        public void should_publish_event_to_second_observer()
        {
            _secondHandler.AssertWasCalled(handler => handler(null, null), options => options.IgnoreArguments());
        }
    }

    [TestFixture]
    public class When_only_observer_unregisters_from_keyboard_events
    {
        private const int KEYBOARD_HOOK_HANDLE = 1;
        private IUser32 _user32;

        [SetUp]
        public void SetUp()
        {
            _user32 = MockRepository.GenerateStub<IUser32>();
            _user32.Stub(usr32 => usr32.SetWindowsHook(Arg<int>.Is.Anything, Arg<HookProc>.Is.Anything, Arg<IntPtr>.Is.Anything, Arg<int>.Is.Anything))
                .Return(KEYBOARD_HOOK_HANDLE);
            _user32.Stub(user32=>user32.UnhookWindowsHook(KEYBOARD_HOOK_HANDLE)).Return(1);

            EventHandler<KeyboardEventArgs> handler = MockRepository.GenerateStub<EventHandler<KeyboardEventArgs>>();

            Keyboard keyboard = new Keyboard(_user32);
            keyboard.KeyUp += handler;
            keyboard.KeyUp -= handler;
        }

        [Test]
        public void should_unhook_keyboard_events()
        {
            _user32.UnhookWindowsHook(KEYBOARD_HOOK_HANDLE);
        }
    }
}