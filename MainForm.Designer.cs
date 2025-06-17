using System.Data;

namespace SSHApp
{
    partial class MainForm
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
            treeView1 = new TreeView();
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.BackColor = SystemColors.Window;
            treeView1.CheckBoxes = true;
            treeView1.ForeColor = Color.Black;
            treeView1.Location = new Point(1, 44);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(178, 364);
            treeView1.TabIndex = 0;
            treeView1.AfterCheck += treeView1_AfterCheck;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(185, 44);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(800, 757);
            textBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(218, 18);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(991, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(186, 409);
            listBox1.TabIndex = 5;
            // 
            // listBox2
            // 
            listBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(991, 415);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(186, 379);
            listBox2.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 801);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(treeView1);
            Name = "MainForm";
            Text = "SSHApp";
            ResumeLayout(false);
            PerformLayout();

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
          
            this.CheckAllChildNodes(e.Node, e.Node.Checked);
        }

        #endregion

        private TreeView treeView1;
        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private ListBox listBox1;
        private ListBox listBox2;
    }
}
