namespace Sharp_system_Kotkov
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Send_button = new Button();
            usersListBox = new ListBox();
            messageBox = new TextBox();
            messagesListBox = new ListBox();
            SuspendLayout();
            // 
            // Send_button
            // 
            Send_button.Location = new Point(694, 387);
            Send_button.Name = "Send_button";
            Send_button.Size = new Size(94, 29);
            Send_button.TabIndex = 2;
            Send_button.Text = "Send";
            Send_button.UseVisualStyleBackColor = true;
            Send_button.Click += Send_button_Click;
            // 
            // usersListBox
            // 
            usersListBox.FormattingEnabled = true;
            usersListBox.Location = new Point(12, 12);
            usersListBox.Name = "usersListBox";
            usersListBox.Size = new Size(239, 404);
            usersListBox.TabIndex = 4;
            // 
            // messageBox
            // 
            messageBox.Location = new Point(257, 389);
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(430, 27);
            messageBox.TabIndex = 5;
            // 
            // messagesListBox
            // 
            messagesListBox.FormattingEnabled = true;
            messagesListBox.Location = new Point(257, 12);
            messagesListBox.Name = "messagesListBox";
            messagesListBox.Size = new Size(531, 364);
            messagesListBox.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(messagesListBox);
            Controls.Add(messageBox);
            Controls.Add(usersListBox);
            Controls.Add(Send_button);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Send_button;
        private ListBox usersListBox;
        private TextBox messageBox;
        private ListBox messagesListBox;
    }
}
