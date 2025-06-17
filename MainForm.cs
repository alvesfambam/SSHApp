using System;
using System.Windows.Forms;
using Renci.SshNet;
using System.Threading.Tasks;
using System.IO;



namespace SSHApp
{
    
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
            PopulateTreeView();
        }
        private void PopulateTreeView()
        {

            // Read all lines from the text file into an array
            string fileName = "Nodes.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
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


        // Updates all child tree nodes recursively.
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
                            textBox1.Invoke((Action)(() =>
                            {
                                textBox1.AppendText(line + Environment.NewLine);
                            }));
                        }
                        System.Threading.Thread.Sleep(100); // Prevent tight loop
                    }

                    // Read any remaining output
                    string remaining = cmd.Result;
                    if (!string.IsNullOrEmpty(remaining))
                    {
                        textBox1.Invoke((Action)(() =>
                        {
                            textBox1.AppendText(remaining + Environment.NewLine);
                        }));
                    }

                    // Check for errors
                    if (!string.IsNullOrEmpty(cmd.Error))
                    {
                        textBox1.Invoke((Action)(() =>
                        {
                            textBox1.AppendText("Error: " + cmd.Error + Environment.NewLine);
                        }));
                    }

                    client.Disconnect();
                }
                else
                {
                    textBox1.Invoke((Action)(() =>
                    {
                        textBox1.AppendText("Failed to connect to SSH server." + Environment.NewLine);
                    }));
                }
            }
            catch (Exception ex)
            {
                textBox1.Invoke((Action)(() =>
                {
                    textBox1.AppendText($"Exception: {ex.Message}" + Environment.NewLine);
                }));
            }
        }

        // Example button click to trigger SSH command
        private void button1_Click(object sender, EventArgs e)
        {
            // Example usage
            //string host = "localhost"; // Replace with your server IP
            string username = "ryan"; // Replace with your SSH username
            string pemFilePath = @"C:\Users\Ryan\.ssh\id_rsa"; // Replace with PEM file path
            string command = "tail -f /var/log/syslog"; // Replace with your command
            listBox1.Items.Clear();
            IterateCheckedNodes(treeView1.Nodes);

            foreach (string item in listBox1.Items)
            {
                // Run SSH command in a separate thread to avoid blocking UI
                System.Threading.Tasks.Task.Run(() =>
                    {
                        ExecuteSshCommand(item, username, pemFilePath, command);
                    });
            }
           
        }
        // Recursive method to iterate through all nodes
        private void IterateCheckedNodes(TreeNodeCollection nodes)
        {
            

            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    // 
                    if (node.Text.Contains(".com") == true) {
                        //textBox1.AppendText(node.Text); // Process checked node
                        listBox1.Items.Add(node.Text);
                    }
                }
                // Recursively check child nodes
                IterateCheckedNodes(node.Nodes);
            }
        }

    }
}
