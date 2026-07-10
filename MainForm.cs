using Org.BouncyCastle.Asn1.X509;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
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
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#")) continue;
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

        private void ExecuteSshCommand(string host, string username, string pemFilePath, string command, string color = "White")
        {
            try
            {
                // Load PEM private key
                using var privateKeyStream = new FileStream(pemFilePath, FileMode.Open, FileAccess.Read);
                var privateKey = new PrivateKeyFile(privateKeyStream);

                // Relay (jump host) credentials                
                var connectionInfoRelay = new ConnectionInfo(
                    host: "jumpbox.telematics.com",
                    port: 22,
                    username: username,
                    new PrivateKeyAuthenticationMethod(username, privateKey)
                );

                // Step 1: Connect to the relay server
                    using (var relayClient = new SshClient(connectionInfoRelay))
                    {
                        relayClient.Connect();
                    richTextBox1.Invoke((Action)(() =>
                    {
                        richTextBox1.AppendText("Connected to relay server." + Environment.NewLine);
                    }));
                    // Step 2: Forward a local port through the relay to the target
                    using (var portForward = new ForwardedPortLocal("127.0.0.1", 0, host, (uint)22))
                        {
                            relayClient.AddForwardedPort(portForward);
                            portForward.Start();

                        //Console.WriteLine($"Forwarding local port {portForward.BoundPort} to {host}:22 via relay.");
                        richTextBox1.Invoke((Action)(() =>
                        {
                            richTextBox1.AppendText($"Forwarding local port {portForward.BoundPort} to {host}:22 via relay." + Environment.NewLine);
                        }));
                        // Step 3: Connect to the target through the forwarded port
                        using (var targetClient = new SshClient(
                                     new ConnectionInfo(
                                        "127.0.0.1",
                                        (int)portForward.BoundPort,
                                        username,
                                        new PrivateKeyAuthenticationMethod(username, privateKey)
                                        )
                                ))
                            {
                            targetClient.Connect();
                            richTextBox1.Invoke((Action)(() =>
                            {
                                richTextBox1.AppendText("Connected to target server through relay." + Environment.NewLine);
                            }));
                                                        
                            if (targetClient.IsConnected)
                            {
                                // Run command and stream output
                                var clientcmd = targetClient.CreateCommand(command);
                                var asyncResult = clientcmd.BeginExecute();

                                // Read output in real-time
                                using var reader = new StreamReader(clientcmd.OutputStream);
                                while (!reader.EndOfStream || !asyncResult.IsCompleted)
                                {
                                    string line = reader.ReadLine();
                                    if (line != null)
                                    {
                                        // Update TextBox on UI thread
                                        richTextBox1.Invoke((Action)(() =>
                                        {
                                            Color namedColor = Color.FromName(color);
                                            richTextBox1.SelectionBackColor = namedColor;
                                            richTextBox1.AppendText(host + ":    " + line + Environment.NewLine);
                                            //richTextBox1.SelectionBackColor = Color.White;
                                        }));
                                    }
                                    System.Threading.Thread.Sleep(100); // Prevent tight loop
                                }

                                // Read any remaining output
                                string remaining = clientcmd.Result;
                                if (!string.IsNullOrEmpty(remaining))
                                {
                                    richTextBox1.Invoke((Action)(() =>
                                    {
                                        richTextBox1.AppendText(remaining + Environment.NewLine);
                                    }));
                                }

                                // Check for errors
                                if (!string.IsNullOrEmpty(clientcmd.Error))
                                {
                                    richTextBox1.Invoke((Action)(() =>
                                    {
                                        richTextBox1.AppendText("Error:" + clientcmd.Error + Environment.NewLine);
                                    }));
                                }

                                //targetClient.Disconnect();
                            }
                            else
                            {
                                richTextBox1.Invoke((Action)(() =>
                                {
                                    richTextBox1.AppendText("Failed to connect to SSH server." + Environment.NewLine);
                                }));
                            }

                            portForward.Stop();
                            targetClient.Disconnect();
                        }
                    }

                    relayClient.Disconnect();
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Invoke((Action)(() =>
                {
                    richTextBox1.AppendText($"Exception:{ex.Message}:{ex.Source}" + Environment.NewLine);
                }));
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            // Example usage
            //string host = localhost; // Replace with your server IP
            string username = "ralves"; // Replace with your SSH username
            string pemFilePath = @"C:\Users\Ryan\.ssh\id_rsa"; // Replace with PEM file path
            bool rowexists = false;
            //string command = "sudo tail -f /var/log/syslog"; // Replace with your command
            List<TreeNode> checked_nodes = new List<TreeNode>();
            List<TreeNode> checked_cmds = new List<TreeNode>();
            GetCheckedLevelNodes(treeView1.Nodes, 2, checked_nodes);
            GetCheckedLevelNodes(treeView2.Nodes, 2, checked_cmds);
// var tasks = new List<Task<string>>;

            foreach (TreeNode node in checked_nodes)
            {
                foreach (TreeNode cmd in checked_cmds)
                {                    
                    rowexists = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(node.Text))
                        {
                            if (row.Cells[1].Value != null && row.Cells[1].Value.Equals(cmd.Text))
                            {
                                //Row Exists
                                rowexists = true;
                                richTextBox1.AppendText($"'{node.Text + "-" + cmd.Text}' exists in the list." + Environment.NewLine);
                                break;
                            }
                        }
                    }

                    if (rowexists != true)
                        {
                            Task task = Task.Factory.StartNew(() =>
                            {
                                ExecuteSshCommand(node.Text, username, pemFilePath, cmd.Text);
                            });

                            richTextBox1.AppendText($"'{node.Text + "-" + cmd.Text}' Does not exists in the list." + $" and is now running on thread" + task.Id + Environment.NewLine);
                            dataGridView1.Rows.Add(node.Text, cmd.Text, task.Id);                        
                        }

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
        
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void RemoveItem_Click(object sender, EventArgs e)
        {

        }
        
    }
}
