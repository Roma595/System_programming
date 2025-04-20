using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace Sharp_system_Kotkov
{



    public partial class Form1 : Form
    {
        enum MessageType
        {
            INIT,
            EXIT,
            START,
            SEND,
            STOP,
            CONFIRM,
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct Header
        {
            [MarshalAs(UnmanagedType.I4)]
            public MessageType type;
            [MarshalAs(UnmanagedType.I4)]
            public int num;
            [MarshalAs(UnmanagedType.I4)]
            public int addr;
            [MarshalAs(UnmanagedType.I4)]
            public int size;
        };

        [DllImport("Kotkov_DLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        static extern Header sendClient(MessageType type, int num = 0, int addr = 0, string str = "");
        
        public Form1()
        {
            InitializeComponent();
            Header h = sendClient(MessageType.INIT);
            if(h.type == MessageType.CONFIRM)
            {
                listBoxEvents.Items.Add("Все потоки");
                listBoxEvents.Items.Add("Главный поток");
                for (int i = 0; i < h.num; ++i)
                {
                    listBoxEvents.Items.Add($"Thread {i}");
                }
            }
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

        private void Update_List(int m)
        {
            int n = listBoxEvents.Items.Count - 2;
            if (m >= n)
            {
                for (int i = n; i < m; ++i)
                {
                    listBoxEvents.Items.Add($"Thread {i}");
                }
            }
            else
            {
                for (int i = m; i < n; ++i)
                {
                    listBoxEvents.Items.RemoveAt(listBoxEvents.Items.Count - 1);
                }
            }
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            int n = (int)numericUpDown.Value;
            Header h = sendClient(MessageType.START, n);
            if (h.type == MessageType.CONFIRM)
            {
                Update_List(h.num);
            }
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            Header h = sendClient(MessageType.STOP);
            if (h.type == MessageType.CONFIRM)
            {
                Update_List(h.num);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                sendClient(MessageType.EXIT);
            }
            catch (Exception ex)
            {
                return;
            }
            

        }

        private void Send_button_Click(object sender, EventArgs e)
        {

            Header h = sendClient(MessageType.SEND, 0, listBoxEvents.SelectedIndex, messageBox.Text);
            if (h.type == MessageType.CONFIRM)
            {
                messageBox.Text = "";
            }
            
        }
    }
}

