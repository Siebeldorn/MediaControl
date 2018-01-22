using SlimDX.XInput;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MediaControl
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            // Gamepads
            {
                int firstConnectedIndex = -1;
                for ( var i = 0; i < 4; ++i )
                {
                    var controller = new Controller((UserIndex)i);
                    if (controller.IsConnected && firstConnectedIndex < 0 )
                        firstConnectedIndex = i;

                    GamepadsComboBox.Items.Add(BuildControllerDisplayName(controller, i));
                }

                // preselect first
                if ( firstConnectedIndex >= 0 )
                {
                    GamepadsComboBox.SelectedIndex = firstConnectedIndex;
                    OnDeviceChanged();
                }
            }

            // start update loop
            UpdateTimer.Interval = UpdateIntervalMs;
            UpdateTimer.Enabled = true;
        }

        private void Deinit()
        {
            Gamepad = null;
        }

        private string BuildControllerDisplayName(Controller controller, int index)
        {
            var displayName = string.Format("Gamepad #{0}, {1}", index.ToString(), controller.IsConnected ? "Connected" : "Disconnected");

            if ( controller.IsConnected )
            {
                var bat = controller.GetBatteryInformation(BatteryDeviceType.Gamepad);
                displayName += ", " + (bat.Type == BatteryType.Wired ? "Wired" : "Wireless");
                if ( bat.Type != BatteryType.Wired )
                    displayName += ", Battery " + bat.Level.ToString("");
            }

            return displayName;
        }

        private void OnDeviceChanged()
        {
            Deinit();

            var userIndex = (UserIndex)GamepadsComboBox.SelectedIndex;
            var controller = new Controller(userIndex);
            if ( controller.IsConnected )
            {
                Gamepad = controller;
                CurrButtonState = Gamepad.GetState().Gamepad.Buttons;
                LastButtonState = CurrButtonState;
            }

            CanProcessInput = Gamepad != null;
        }

        private void OnUpdate()
        {
            if ( Gamepad == null || !Gamepad.IsConnected )
                return;

            var state = Gamepad.GetState().Gamepad;

            // Mouse Cursor Movement
            if ( CanProcessInput )
            {
                // relative values in range [-1.0, 1.0]
                double deltaX = Math.Min(1.0f, Math.Max(-1.0f, state.LeftThumbX * 1.0f / short.MaxValue));
                double deltaY = Math.Min(1.0f, Math.Max(-1.0f, state.LeftThumbY * -1.0f / short.MaxValue));

                // treshold
                if ( Math.Abs(deltaX) < 0.1f )
                    deltaX = 0;
                if ( Math.Abs(deltaY) < 0.1f )
                    deltaY = 0;
                
                // exponential 
                double facX = deltaX < 0 ? -1.0 : 1.0;
                double facY = deltaY < 0 ? -1.0 : 1.0;
                deltaX = Math.Pow( deltaX, 4) * facX;
                deltaY = Math.Pow( deltaY, 4) * facY;

                int x = (int)(deltaX * UpdateIntervalMs * 3);
                int y = (int)(deltaY * UpdateIntervalMs * 3);
                MoveCursor(x, y);
            }

            // mouse buttons and keyboard input
            {
                CurrButtonState = state.Buttons;
                ButtonsPressedTextBox.Text = CurrButtonState.ToString("G");

                if ( IsUp(GamepadButtonFlags.Start) )
                    CanProcessInput = !CanProcessInput;
                
                if ( CanProcessInput )
                {
                    // toggle indepenent
                    {
                        // Arrow Up
                        if ( IsUp(GamepadButtonFlags.DPadUp) )
                            Native.InjectKeyboardInput(Keys.Up);

                        // Arrow Down
                        if ( IsUp(GamepadButtonFlags.DPadDown) )
                            Native.InjectKeyboardInput(Keys.Down);

                        // Arrow Left
                        if ( IsUp(GamepadButtonFlags.DPadLeft) )
                            Native.InjectKeyboardInput(Keys.Left);

                        // Arrow Right
                        if ( IsUp(GamepadButtonFlags.DPadRight) )
                            Native.InjectKeyboardInput(Keys.Right);
                    }

                    var isLeftToggle = IsDown(GamepadButtonFlags.LeftShoulder, true);
                    var isRightToggle = IsDown(GamepadButtonFlags.RightShoulder, true);
                    if ( isLeftToggle && isRightToggle )
                    {
                        // some more advanced commands
                    }
                    else if ( isLeftToggle ) // audio
                    {
                        // A => Volume Down
                        if ( IsDown(GamepadButtonFlags.A, true) )
                            Native.InjectKeyboardInput(Keys.VolumeDown);

                        // B => Volume Up
                        if ( IsDown(GamepadButtonFlags.B, true) )
                            Native.InjectKeyboardInput(Keys.VolumeUp);

                        // X => Mute
                        if ( IsUp(GamepadButtonFlags.X) )
                            Native.InjectKeyboardInput(Keys.VolumeMute);
                    }
                    else if ( isRightToggle ) // media
                    {
                        // A => Prev
                        if ( IsUp(GamepadButtonFlags.A) )
                            Native.InjectKeyboardInput(Keys.MediaPreviousTrack);

                        // B => Next
                        if ( IsUp(GamepadButtonFlags.B) )
                            Native.InjectKeyboardInput(Keys.MediaNextTrack);

                        // X => Pause
                        if ( IsUp(GamepadButtonFlags.X) )
                            Native.InjectKeyboardInput(Keys.MediaPlayPause);

                        // Y => Stop
                        if ( IsUp(GamepadButtonFlags.Y) )
                            Native.InjectKeyboardInput(Keys.MediaStop);
                    }
                    else // default interaction
                    {
                        // A => Left Mouse Up/Down
                        if ( IsDown(GamepadButtonFlags.A, false) )
                            Native.InjectMouseInput(Native.MouseButton.LeftDown);
                        if ( IsUp(GamepadButtonFlags.A) )
                            Native.InjectMouseInput(Native.MouseButton.LeftUp);

                        // B => Right Mouse Up/Down
                        if ( IsDown(GamepadButtonFlags.B, false) )
                            Native.InjectMouseInput(Native.MouseButton.RightDown);
                        if ( IsUp(GamepadButtonFlags.B) )
                            Native.InjectMouseInput(Native.MouseButton.RightUp);

                        // X => Enter
                        if ( IsUp(GamepadButtonFlags.X) )
                            Native.InjectKeyboardInput(Keys.Enter);

                        // Y => Escape
                        if ( IsUp(GamepadButtonFlags.Y) )
                            Native.InjectKeyboardInput(Keys.Escape);
                    }
                }

                LastButtonState = CurrButtonState;
            }
        }

        private bool IsDown(GamepadButtonFlags button, bool repeated)
        {
            if ( repeated )
                return (CurrButtonState & button) != 0;
            else
                return (CurrButtonState & button) != 0 && (LastButtonState & button) == 0;
        }

        private bool IsUp(GamepadButtonFlags button)
        {
            return (CurrButtonState & button) == 0 && (LastButtonState & button) != 0;
        }

        public void MoveCursor(int deltaX, int deltaY)
        {
            Cursor.Position = new Point(Cursor.Position.X + deltaX, Cursor.Position.Y + deltaY);
        }

        #region Members

        bool CanProcessInput;
        Controller Gamepad;
        GamepadButtonFlags CurrButtonState;     // for convenience usage in methods
        GamepadButtonFlags LastButtonState;
        int UpdateIntervalMs = 1000 / 60;

        #endregion Members

        #region Events

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            OnUpdate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Deinit();
        }

        private void GamepadsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnDeviceChanged();
        }

        #endregion Events
    }
}
