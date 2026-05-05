using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SSHApp
{

    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            PopulateTreeView();
            TreeView_SSH_Commands_Populate();
        }
        private void PopulateTreeView()
        {

            // Read all lines from the text file into an array
            string fileName = "Nodes.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#")) continue;
                string[] parts = line.Trim().Split('\t');
                TreeNodeCollection currentNodes = treeView1.Nodes;
                foreach (string part in parts)
                {
                    TreeNode[] foundNodes = currentNodes.Find(part, false);
                    TreeNode currentNode;
                    if (foundNodes.Length == 0)
                    {
                        currentNode = currentNodes.Add(part, part);
                    }
                    else
                    {
                        currentNode = foundNodes[0];
                    }
                    currentNodes = currentNode.Nodes;
                }
            }
        }
        private void TreeView_SSH_Commands_Populate()
        {
            string fileName = "ssh_commands.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if ( string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#")) continue; 
                    string[] parts = line.Trim().Split('\t');
                    TreeNodeCollection currentNodes = treeView2.Nodes;
                    foreach (string part in parts)
                    {
                        TreeNode[] foundNodes = currentNodes.Find(part, false);
                        TreeNode currentNode;
                        if (foundNodes.Length == 0)
                        {
                            currentNode = currentNodes.Add(part, part);
                        }
                        else
                        {
                            currentNode = foundNodes[0];
                        }
                        currentNodes = currentNode.Nodes;
                    }
            }
        }
        
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void ExecuteSshCommand(string host, string username, string pemFilePath, string command)
        {
            try
            {
                // Load PEM private key
                using var privateKeyStream = new FileStream(pemFilePath, FileMode.Open, FileAccess.Read);
                var privateKey = new PrivateKeyFile(privateKeyStream);

                // Set up SSH connection info
                var connectionInfo = new ConnectionInfo(
                    host: host,
                    username: username,
                    new PrivateKeyAuthenticationMethod(username, privateKey)
                );

                // Create SSH client
                using var client = new SshClient(connectionInfo);
                client.Connect();

                if (client.IsConnected)
                {
                    // Run command and stream output
                    using var cmd = client.CreateCommand(command);
                    var asyncResult = cmd.BeginExecute();

                    // Read output in real-time
                    using var reader = new StreamReader(cmd.OutputStream);
                    while (!reader.EndOfStream || !asyncResult.IsCompleted)
                    {
                        string line = reader.ReadLine();
                        if (line != null)
                        {
                            // Update TextBox on UI thread
                            richTextBox1.Invoke((Action)(() =>
                            {
                                richTextBox1.AppendText(line + Environment.NewLine);
                            }));
                        }
                        System.Threading.Thread.Sleep(100); // Prevent tight loop
                    }

                    // Read any remaining output
                    string remaining = cmd.Result;
                    if (!string.IsNullOrEmpty(remaining))
                    {
                        richTextBox1.Invoke((Action)(() =>
                        {
                            richTextBox1.AppendText(remaining + Environment.NewLine);
                        }));
                    }

                    // Check for errors
                    if (!string.IsNullOrEmpty(cmd.Error))
                    {
                        richTextBox1.Invoke((Action)(() =>
                        {
                            richTextBox1.AppendText("Error:"  + cmd.Error + Environment.NewLine);
                        }));
                    }

                    client.Disconnect();
                }
                else
                {
                    richTextBox1.Invoke((Action)(() =>
                    {
                        richTextBox1.AppendText("Failed to connect to SSH server." + Environment.NewLine);
                    }));
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Invoke((Action)(() =>
                {
                    richTextBox1.AppendText($"Exception:{ex.Message}" + Environment.NewLine);
                }));
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            // Example usage
            //string host = localhost; // Replace with your server IP
            string username = "ralves"; // Replace with your SSH username
            string pemFilePath = @"C:\Users\Ryan\.ssh\id_rsa"; // Replace with PEM file path
            string command = "sudo tail -f /var/log/syslog"; // Replace with your command
            List<TreeNode> checked_nodes = new List<TreeNode>();
            List<TreeNode> checked_cmds = new List<TreeNode>();
            listBox1.Items.Clear();
            GetCheckedLevelNodes(treeView1.Nodes, 2, checked_nodes);
            GetCheckedLevelNodes(treeView2.Nodes, 2, checked_cmds);

            //IterateCheckedNodes(treeView1.Nodes, treeView1.Nodes);


            foreach (TreeNode node in checked_nodes)
            {
                foreach (TreeNode cmd in checked_cmds)
                {

                    listBox1.Items.Add(node.Text + "-"  + cmd.Text);
                    // Run SSH command in a separate thread to avoid blocking UI
                    System.Threading.Tasks.Task.Run(() =>
                        {
                            ExecuteSshCommand(node.Text, username, pemFilePath, cmd.Text);
                        });
                }
            }

        }

        private void GetCheckedLevelNodes(TreeNodeCollection nodes, int targetLevel, List<TreeNode> result)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Level == targetLevel && node.Checked)
                {
                    result.Add(node);
                }

                // Continue recursion for children
                if (node.Nodes.Count > 0)
                {
                    GetCheckedLevelNodes(node.Nodes, targetLevel, result);
                }
            }
        }

        // Recursive method to iterate through all nodes
        private void IterateCheckedNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                   if (node.Text.Contains(".com") == true)
                    {
                        foreach (TreeNode cmd in treeView1.Nodes)
                        {
                            if (cmd.Checked)
                            {
                                //add  checked items to node list
                                listBox1.Items.Add(node.Text + "-"  + cmd.Text);
                            }
                            // Recursively check child nodes
                            IterateCheckedNodes(cmd.Nodes);
                        }                        
                    }
                }
                // Recursively check child nodes
                IterateCheckedNodes (node.Nodes);
            }

            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
