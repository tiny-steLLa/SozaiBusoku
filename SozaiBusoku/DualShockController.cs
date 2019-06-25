using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    static class DualShockController
    {
        private static asd.Joystick joystick = asd.Engine.JoystickContainer.GetJoystickAt(0);
        public static bool IsJoystickPush(int t) => asd.Engine.JoystickContainer.GetIsPresentAt(0) && joystick.GetButtonState(t) == asd.JoystickButtonState.Push;
        public static bool IsJoystickHold(int t) => asd.Engine.JoystickContainer.GetIsPresentAt(0) && joystick.GetButtonState(t) == asd.JoystickButtonState.Hold;
    }
}
