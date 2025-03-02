using System.Diagnostics;
using System.Windows.Forms.Design;

namespace Sharp_system_Kotkov
{
    public partial class Form1 : Form
    {
        Process? ChildProcess = null;
        EventWaitHandle StartEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "StartEvent");
        EventWaitHandle StopEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "StopEvent");
        EventWaitHandle ConfirmEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "ConfirmEvent");
        EventWaitHandle ExitEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "ExitEvent");
        public Form1()
        {
            InitializeComponent();
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            if (listBoxEvents.InvokeRequired)
            {
                listBoxEvents.Invoke(new Action(() => listBoxEvents.Items.Clear()));
            }
            else
            {
                listBoxEvents.Items.Clear();
            }
            
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            if (ChildProcess == null || ChildProcess.HasExited)
            {
                ChildProcess = Process.Start("System_programming_Kotkov.exe");
                ChildProcess.EnableRaisingEvents = true;
                ChildProcess.Exited += OnProcessExited;
                listBoxEvents.Items.Add("Все потоки");
                listBoxEvents.Items.Add("Главный поток");
            }
            else
            {
                int n = (int)numericUpDown.Value;
                int CountItems = listBoxEvents.Items.Count;
                for (int i = 0; i < n; ++i)
                {
                    StartEvent.Set();
                    ConfirmEvent.WaitOne();
                    listBoxEvents.Items.Add($"Thread {i + CountItems - 2}");
                }

            }
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            if (!(ChildProcess == null || ChildProcess.HasExited))
            {
                StopEvent.Set();
                ConfirmEvent.WaitOne();
                listBoxEvents.Items.RemoveAt(listBoxEvents.Items.Count - 1);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!(ChildProcess == null || ChildProcess.HasExited))
            {
                ExitEvent.Set();
                ConfirmEvent.WaitOne();
            }
        }
    }
}
