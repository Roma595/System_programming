using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace Sharp_system_Kotkov
{
    public partial class Form1 : Form
    {
        private Socket socket;
        HashSet<int> OtherIDs = [];
        private volatile bool _running = true;
    
        void ProcessMessages()
        {
            while (_running)
            {
                var m = Message.send(socket, MessageRecipients.MR_BROKER, MessageTypes.MT_GETDATA);
                switch (m.header.type)
                {
                    case MessageTypes.MT_DATA:
                        messagesListBox.Invoke(new Action(() => {
                            messagesListBox.Items.Add($"[{m.header.from}>] {m.data}");
                        }));
                        break;
                    case MessageTypes.MT_INIT:
                        OtherIDs.Add((int)m.header.from);
                        RefreshUsersListBox();
                        break;
                    case MessageTypes.MT_EXIT:
                        if (m.header.from == MessageRecipients.MR_BROKER)
                            return;
                        else
                        {
                            OtherIDs.Remove((int)m.header.from);
                            RefreshUsersListBox();
                        }
                        break;
                    default:
                        Thread.Sleep(100);
                        break;
                }
            }
        }

        private void RefreshUsersListBox()
        {
            usersListBox.DataSource = null;
            usersListBox.DataSource = OtherIDs.Select(id => new DisplayUser { Id = id }).ToList();
        }
        public Form1()
        {
            InitializeComponent();

            int nPort = 12345;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), nPort);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);
            if (!socket.Connected)
            {
                throw new Exception("Connection error");
            }

            var m = Message.send(socket, MessageRecipients.MR_BROKER, MessageTypes.MT_INIT);

            if (m.header.type == MessageTypes.MT_INIT)
            {
                OtherIDs.Add(10);
                OtherIDs.Add(50);
                RefreshUsersListBox();
                messagesListBox.Items.Add($"Твой clientID: {Message.clientID}");
            }

            Thread t = new Thread(ProcessMessages);
            t.Start();
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            if (usersListBox.InvokeRequired)
            {
                usersListBox.Invoke(new Action(() => usersListBox.Items.Clear()));
            }
            else
            {
                usersListBox.Items.Clear();
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //var m = SendMsg((int)MessageRecipients.MR_BROKER, MessageTypes.MT_EXIT);
            _running = false;
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            string message = messageBox.Text;
            MessageRecipients to = new();
            to = (MessageRecipients)((DisplayUser)usersListBox.SelectedItem).Id;

            string to_name = string.Empty;
            switch (to)
            {
                case MessageRecipients.MR_ALL:
                    to_name = "всем";
                    break;
                case MessageRecipients.MR_BROKER:
                    to_name = "серверу";
                    break;
                default:
                    to_name = to.ToString();
                    break;
            }

            messagesListBox.Items.Add($"[{to_name}<] {message}");

            var m = Message.send(socket, to, MessageTypes.MT_DATA, message);

            if (m.header.type == MessageTypes.MT_CONFIRM)
            {
                messageBox.Text = string.Empty;
            }
        }
        public class DisplayUser
        {
            public int Id { get; set; }
            public string DisplayName
            {
                get
                {
                    return Id switch
                    {
                        (int)MessageRecipients.MR_BROKER => "Главный поток",
                        (int)MessageRecipients.MR_ALL => "Все потоки",
                        _ => $"Пользователь {Id}"
                    };
                }
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}

