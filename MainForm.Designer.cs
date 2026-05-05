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
        /// <param name=disposing>true if managed resources should be disposed; otherwise, false.</param>
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
            listBox1 = new ListBox();
            splitContainer1 = new SplitContainer();
            treeView2 = new TreeView();
            splitContainer2 = new SplitContainer();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            checkedListBox1 = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.BackColor = SystemColors.Window;
            treeView1.CheckBoxes = true;
            treeView1.Dock = DockStyle.Fill;
            treeView1.ForeColor = Color.Black;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(226, 298);
            treeView1.TabIndex = 0;
            treeView1.AfterCheck += treeView1_AfterCheck;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(198, 601);
            listBox1.TabIndex = 5;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Left;
            splitContainer1.Location = new Point(0, 40);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel1.RightToLeft = RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(treeView2);
            splitContainer1.Panel2.RightToLeft = RightToLeft.No;
            splitContainer1.RightToLeft = RightToLeft.No;
            splitContainer1.Size = new Size(226, 601);
            splitContainer1.SplitterDistance = 298;
            splitContainer1.TabIndex = 7;
            // 
            // treeView2
            // 
            treeView2.BackColor = SystemColors.Window;
            treeView2.CheckBoxes = true;
            treeView2.Dock = DockStyle.Fill;
            treeView2.ForeColor = Color.Black;
            treeView2.Location = new Point(0, 0);
            treeView2.Name = "treeView2";
            treeView2.Size = new Size(226, 299);
            treeView2.TabIndex = 1;
            treeView2.AfterCheck += treeView2_AfterCheck;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(226, 40);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(richTextBox1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(checkedListBox1);
            splitContainer2.Panel2.Controls.Add(listBox1);
            splitContainer2.Size = new Size(951, 601);
            splitContainer2.SplitterDistance = 749;
            splitContainer2.TabIndex = 8;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(0, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(749, 595);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            richTextBox1.WordWrap = false;
            // 
            // button1
            // 
            button1.Location = new Point(536, 11);
            button1.Name = "button1";
            button1.Size = new Size(146, 23);
            button1.TabIndex = 9;
            button1.Text = "ADD";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btn_add_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(0, 204);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(195, 76);
            checkedListBox1.TabIndex = 10;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1177, 641);
            Controls.Add(button1);
            Controls.Add(splitContainer2);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Padding = new Padding(0, 40, 0, 0);
            Text = "SSHApp";
            Load += MainForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
          
            this.CheckAllChildNodes(e.Node, e.Node.Checked);
        }
        private void treeView2_AfterCheck(object sender, TreeViewEventArgs e)
        {

            this.CheckAllChildNodes(e.Node, e.Node.Checked);
        }
        #endregion

        private TreeView treeView1;
        private ListBox listBox1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Button button1;
        private TreeView treeView2;
        private RichTextBox richTextBox1;
        private CheckedListBox checkedListBox1;
    }
}
