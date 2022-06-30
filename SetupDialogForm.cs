using ASCOM.MAK127;
using ASCOM.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace ASCOM.MAK127
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        TraceLogger tl; // Holder for a reference to the driver's trace logger

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Focuser.comPort = (string)comboBoxComPort.SelectedItem;
            Focuser.speed = Int32.Parse(textBoxSpeed.Text);
            Focuser.numberTurns = Int32.Parse(textBoxTurns.Text);

            SerialPort _serialPort;
            _serialPort = new SerialPort();
            _serialPort.PortName = Focuser.comPort;
            _serialPort.BaudRate = 57600;
            _serialPort.Open();
            _serialPort.Write("SETSPEED1#" + textBoxSpeed.Text + "#");
            _serialPort.Write("SETTURNS1#" + textBoxTurns.Text + "#");
            _serialPort.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            
            // set the list of com ports to those that are currently available
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            // select the current port if possible
            if (comboBoxComPort.Items.Contains(Focuser.comPort))
            {
                comboBoxComPort.SelectedItem = Focuser.comPort;
            }
        }

        private void comboBoxComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {

            textBoxSpeed.Text = Focuser.speed.ToString();
            textBoxTurns.Text = Focuser.numberTurns.ToString();

            SerialPort _serialPort;
            _serialPort = new SerialPort();
            _serialPort.PortName = Focuser.comPort;
            _serialPort.BaudRate = 57600;
            _serialPort.Open();
            _serialPort.Write("SETSPEED1#" + textBoxSpeed.Text + "#");
            _serialPort.Write("SETTURNS1#" + textBoxTurns.Text + "#");
            _serialPort.Close();
        }
    }
}