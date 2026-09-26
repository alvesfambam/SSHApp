using System.Data;
using System.Windows.Forms;
using System;
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
            components = new System.ComponentModel.Container();
            treeView1 = new TreeView();
            splitContainer1 = new SplitContainer();
            treeView2 = new TreeView();
            splitContainer2 = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            richTextBox2 = new RichTextBox();
            tabPage1 = new TabPage();
            richTextBox1 = new RichTextBox();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            txtbox_appsettings_pathtoprivatkey = new TextBox();
            txtbox_appsettings_SSHPort = new TextBox();
            txtbox_appsettings_SSHRelayPort = new TextBox();
            txtbox_appsettings_SSHRelay = new TextBox();
            txtbox_appsettings_Username = new TextBox();
            txtbox_appsettings_AppName = new TextBox();
            tabPage5 = new TabPage();
            dataGridView1 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            ID = new DataGridViewTextBoxColumn();
            Token = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            removeToolStripMenuItem = new ToolStripMenuItem();
            button1 = new Button();
            button2 = new Button();
            textBox_highlight = new TextBox();
            textBox_exclude = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btn_RefreshNodes = new Button();
            button3 = new Button();
            treeView3 = new TreeView();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
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
            splitContainer2.Panel1.Controls.Add(tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(dataGridView1);
            splitContainer2.Size = new Size(951, 601);
            splitContainer2.SplitterDistance = 749;
            splitContainer2.TabIndex = 8;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(749, 601);
            tabControl1.TabIndex = 5;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(richTextBox2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(741, 573);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Console";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            richTextBox2.BackColor = SystemColors.Window;
            richTextBox2.Dock = DockStyle.Fill;
            richTextBox2.ForeColor = SystemColors.InfoText;
            richTextBox2.Location = new Point(3, 3);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(735, 567);
            richTextBox2.TabIndex = 4;
            richTextBox2.Text = "";
            richTextBox2.WordWrap = false;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(741, 573);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "LogTail";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.Black;
            richTextBox1.ForeColor = Color.White;
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(735, 567);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            richTextBox1.WordWrap = false;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(741, 573);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Command Edit";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(txtbox_appsettings_pathtoprivatkey);
            tabPage4.Controls.Add(txtbox_appsettings_SSHPort);
            tabPage4.Controls.Add(txtbox_appsettings_SSHRelayPort);
            tabPage4.Controls.Add(txtbox_appsettings_SSHRelay);
            tabPage4.Controls.Add(txtbox_appsettings_Username);
            tabPage4.Controls.Add(txtbox_appsettings_AppName);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(741, 573);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "AppSettings";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtbox_appsettings_pathtoprivatkey
            // 
            txtbox_appsettings_pathtoprivatkey.Location = new Point(413, 238);
            txtbox_appsettings_pathtoprivatkey.Name = "txtbox_appsettings_pathtoprivatkey";
            txtbox_appsettings_pathtoprivatkey.Size = new Size(100, 23);
            txtbox_appsettings_pathtoprivatkey.TabIndex = 5;
            // 
            // txtbox_appsettings_SSHPort
            // 
            txtbox_appsettings_SSHPort.Location = new Point(413, 193);
            txtbox_appsettings_SSHPort.Name = "txtbox_appsettings_SSHPort";
            txtbox_appsettings_SSHPort.Size = new Size(100, 23);
            txtbox_appsettings_SSHPort.TabIndex = 4;
            // 
            // txtbox_appsettings_SSHRelayPort
            // 
            txtbox_appsettings_SSHRelayPort.Location = new Point(413, 147);
            txtbox_appsettings_SSHRelayPort.Name = "txtbox_appsettings_SSHRelayPort";
            txtbox_appsettings_SSHRelayPort.Size = new Size(100, 23);
            txtbox_appsettings_SSHRelayPort.TabIndex = 3;
            // 
            // txtbox_appsettings_SSHRelay
            // 
            txtbox_appsettings_SSHRelay.Location = new Point(413, 103);
            txtbox_appsettings_SSHRelay.Name = "txtbox_appsettings_SSHRelay";
            txtbox_appsettings_SSHRelay.Size = new Size(100, 23);
            txtbox_appsettings_SSHRelay.TabIndex = 2;
            // 
            // txtbox_appsettings_Username
            // 
            txtbox_appsettings_Username.Location = new Point(413, 64);
            txtbox_appsettings_Username.Name = "txtbox_appsettings_Username";
            txtbox_appsettings_Username.Size = new Size(100, 23);
            txtbox_appsettings_Username.TabIndex = 1;
            // 
            // txtbox_appsettings_AppName
            // 
            txtbox_appsettings_AppName.Location = new Point(413, 21);
            txtbox_appsettings_AppName.Name = "txtbox_appsettings_AppName";
            txtbox_appsettings_AppName.Size = new Size(100, 23);
            txtbox_appsettings_AppName.TabIndex = 0;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(treeView3);
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(741, 573);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Chef ENV's";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, ID, Token });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridView1.RowTemplate.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(198, 601);
            dataGridView1.TabIndex = 11;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Host";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 57;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Command";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 89;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "PID";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 50;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 43;
            // 
            // Token
            // 
            Token.HeaderText = "Token";
            Token.Name = "Token";
            Token.ReadOnly = true;
            Token.Width = 63;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { removeToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(117, 22);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += RemoveItem_Click;
            // 
            // button1
            // 
            button1.Location = new Point(226, 9);
            button1.Name = "button1";
            button1.Size = new Size(63, 23);
            button1.TabIndex = 9;
            button1.Text = "ADD";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btn_add_Click;
            // 
            // button2
            // 
            button2.Location = new Point(295, 9);
            button2.Name = "button2";
            button2.Size = new Size(52, 23);
            button2.TabIndex = 10;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox_highlight
            // 
            textBox_highlight.Location = new Point(466, 10);
            textBox_highlight.Name = "textBox_highlight";
            textBox_highlight.Size = new Size(272, 23);
            textBox_highlight.TabIndex = 11;
            // 
            // textBox_exclude
            // 
            textBox_exclude.Location = new Point(840, 10);
            textBox_exclude.Name = "textBox_exclude";
            textBox_exclude.Size = new Size(334, 23);
            textBox_exclude.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(403, 15);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 13;
            label1.Text = "Highlight";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(755, 15);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 14;
            label2.Text = "Exclude Rows";
            // 
            // btn_RefreshNodes
            // 
            btn_RefreshNodes.Location = new Point(11, 12);
            btn_RefreshNodes.Name = "btn_RefreshNodes";
            btn_RefreshNodes.Size = new Size(75, 23);
            btn_RefreshNodes.TabIndex = 15;
            btn_RefreshNodes.Text = "RefreshNodes";
            btn_RefreshNodes.UseVisualStyleBackColor = true;
            btn_RefreshNodes.Click += btn_RefreshNodes_Click;
            // 
            // button3
            // 
            button3.Location = new Point(89, 11);
            button3.Name = "button3";
            button3.Size = new Size(36, 23);
            button3.TabIndex = 16;
            button3.Text = "Load";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // treeView3
            // 
            treeView3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView3.Location = new Point(5, 3);
            treeView3.Name = "treeView3";
            treeView3.Size = new Size(738, 571);
            treeView3.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1177, 641);
            Controls.Add(button3);
            Controls.Add(btn_RefreshNodes);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_exclude);
            Controls.Add(textBox_highlight);
            Controls.Add(button2);
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
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

            //private TextBox txtbox_appsettings_SSHRelayPort;
            //private TextBox txtbox_appsettings_SSHRelay;
            //p/rivate TextBox txtbox_appsettings_Username;
            //private TextBox txtbox_appsettings_AppName;
            //private TextBox txtbox_appsettings_pathtoprivatkey;
            //private TextBox txtbox_appsettings_SSHPort;
            // Bind TextBox.Text to the application setting "UserName"
            //txtbox_appsettings_Username.DataBindings.Add(
            //         "Text",
            //        Properties.Settings.Default,
            //        "UserName",
            //        true,
            //        DataSourceUpdateMode.OnPropertyChanged
            //    );

            //checkBoxRemember.DataBindings.Add(
            //        "Checked", 
            //        Properties.Settings.Default, 
            //       "RememberMe", 
            //         true, 
            //        DataSourceUpdateMode.OnPropertyChanged
            //     );
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
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Button button1;
        private TreeView treeView2;
        private RichTextBox richTextBox1;
        private DataGridView dataGridView1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem removeToolStripMenuItem;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Token;
        private Button button2;
        private RichTextBox richTextBox2;
        private TextBox textBox_highlight;
        private TextBox textBox_exclude;
        private Label label1;
        private Label label2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TextBox txtbox_appsettings_SSHRelayPort;
        private TextBox txtbox_appsettings_SSHRelay;
        private TextBox txtbox_appsettings_Username;
        private TextBox txtbox_appsettings_AppName;
        private TextBox txtbox_appsettings_pathtoprivatkey;
        private TextBox txtbox_appsettings_SSHPort;
        private Button btn_RefreshNodes;
        private TabPage tabPage5;
        private Button button3;
        private TreeView treeView3;
    }
}
