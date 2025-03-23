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
            Start_button = new Button();
            Stop_button = new Button();
            Send_button = new Button();
            numericUpDown = new NumericUpDown();
            listBoxEvents = new ListBox();
            messageBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // Start_button
            // 
            Start_button.Location = new Point(89, 88);
            Start_button.Name = "Start_button";
            Start_button.Size = new Size(94, 29);
            Start_button.TabIndex = 0;
            Start_button.Text = "Start";
            Start_button.UseVisualStyleBackColor = true;
            Start_button.Click += Start_button_Click;
            // 
            // Stop_button
            // 
            Stop_button.Location = new Point(89, 138);
            Stop_button.Name = "Stop_button";
            Stop_button.Size = new Size(94, 29);
            Stop_button.TabIndex = 1;
            Stop_button.Text = "Stop";
            Stop_button.UseVisualStyleBackColor = true;
            Stop_button.Click += Stop_button_Click;
            // 
            // Send_button
            // 
            Send_button.Location = new Point(89, 189);
            Send_button.Name = "Send_button";
            Send_button.Size = new Size(94, 29);
            Send_button.TabIndex = 2;
            Send_button.Text = "Send";
            Send_button.UseVisualStyleBackColor = true;
            Send_button.Click += Send_button_Click;
            // 
            // numericUpDown
            // 
            numericUpDown.Location = new Point(246, 88);
            numericUpDown.Name = "numericUpDown";
            numericUpDown.Size = new Size(150, 27);
            numericUpDown.TabIndex = 3;
            // 
            // listBoxEvents
            // 
            listBoxEvents.FormattingEnabled = true;
            listBoxEvents.Location = new Point(453, 50);
            listBoxEvents.Name = "listBoxEvents";
            listBoxEvents.Size = new Size(292, 264);
            listBoxEvents.TabIndex = 4;
            // 
            // messageBox
            // 
            messageBox.Location = new Point(224, 190);
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(201, 27);
            messageBox.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(messageBox);
            Controls.Add(listBoxEvents);
            Controls.Add(numericUpDown);
            Controls.Add(Send_button);
            Controls.Add(Stop_button);
            Controls.Add(Start_button);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            ((System.ComponentModel.ISupportInitialize)numericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Start_button;
        private Button Stop_button;
        private Button Send_button;
        private NumericUpDown numericUpDown;
        private ListBox listBoxEvents;
        private TextBox messageBox;
    }
}
