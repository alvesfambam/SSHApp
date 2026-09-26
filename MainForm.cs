using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.X509;
using Renci.SshNet;
using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace SSHApp
{
    public partial class MainForm : Form
    {
        // Dictionary to store each thread's cancellation token source
        private static Dictionary<int, CancellationTokenSource> threadTokens = new Dictionary<int, CancellationTokenSource>();
        static int lastId = 0;
        static int generateId()
        {
            return Interlocked.Increment(ref lastId);
        }

        public MainForm()
        {
            InitializeComponent();
            PopulateTreeView();
            TreeView_SSH_Commands_Populate();
            // Read all keys



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
            treeView1.TreeViewNodeSorter = null; // Use default alphabetical
            treeView1.Sort();
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
            treeView2.TreeViewNodeSorter = null; // Use default alphabetical
            treeView2.Sort();
        }
        private void RTB_Message(string msg)
        {
            string textBox_highlight_string = textBox_highlight.Text;
            string[] textBox_highlight_list = textBox_highlight_string.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string textBox_exclude_string = textBox_exclude.Text;
            string[] textBox_exclude_list = textBox_exclude_string.Split([' '], StringSplitOptions.RemoveEmptyEntries);

            if (string.IsNullOrEmpty(msg))
            {
                //Do nothing
            }
            else if (textBox_highlight_list == null || textBox_highlight_list.Length == 0)
            {
                if (textBox_exclude_list == null || textBox_exclude_list.Length == 0)
                {
                    //Apply nothing

                    richTextBox1.Invoke((Action)(() =>
                    {
                        richTextBox1.AppendText(msg + Environment.NewLine);
                    }));
                }
                else
                {
                    bool foundwords = false;
                    //Apply Exclude Only
                    foreach (string excludedword in textBox_exclude_list)
                    {
                        if (msg.IndexOf(excludedword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            //Excluded
                            RTB_Status("Row Excluded for:" + excludedword);
                            foundwords = true;
                        }
                    }
                    if (foundwords == false)
                    {
                        richTextBox1.Invoke((Action)(() =>
                        {
                            richTextBox1.AppendText(msg + Environment.NewLine);
                        }));
                    }
                }
            }
            else
            {
                if (textBox_exclude_list == null || textBox_exclude_list.Length == 0)
                {
                    richTextBox1.Invoke((Action)(() =>
                    {
                        richTextBox1.AppendText(msg + Environment.NewLine);
                    }));
                }
                else
                {
                    //Apply BOTH
                    bool foundwords = false;
                    //Apply Exclude Only
                    foreach (string excludedword in textBox_exclude_list)
                    {
                        if (msg.IndexOf(excludedword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            //Excluded
                            RTB_Status("Row Excluded for:" + excludedword);
                            foundwords = true;
                        }
                    }
                    if (foundwords == false)
                    {
                        richTextBox1.Invoke((Action)(() =>
                        {
                            richTextBox1.AppendText(msg + Environment.NewLine);
                        }));
                    }
                }

            }

        }
        private void RTB_Status(string msg)
        {
            richTextBox2.Invoke((Action)(() =>
            {
                richTextBox2.AppendText(msg + Environment.NewLine);
            }));
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
        private void ExecuteSshCommand_Relay(string host, string username, string pemFilePath, string command, int id, CancellationToken token)
        {

            try
            {

                // Load PEM private key
                using var privateKeyStream = new FileStream(pemFilePath, FileMode.Open, FileAccess.Read);
                var privateKey = new PrivateKeyFile(privateKeyStream);

                // Relay (jump host) credentials                
                var connectionInfoRelay = new ConnectionInfo(
                    host: (ConfigurationManager.AppSettings["SSHRelay"] ?? "jumpbox.telematics.com"),
                    port: 22,
                    username: (ConfigurationManager.AppSettings["SSHRelayUsername"] ?? "ubuntu"),
                    new PrivateKeyAuthenticationMethod(username, privateKey)
                );

                // Step 1: Connect to the relay server
                using (var relayClient = new SshClient(connectionInfoRelay))
                {

                    //IF Jumpbox Checked
                    relayClient.Connect();
                    RTB_Status("Connected to relay server.");

                    // Step 2: Forward a local port through the relay to the target
                    using (var portForward = new ForwardedPortLocal("127.0.0.1", 0, host, (uint)22))
                    {
                        relayClient.AddForwardedPort(portForward);
                        portForward.Start();

                        //Console.WriteLine($"Forwarding local port {portForward.BoundPort} to {host}:22 via relay.");
                        RTB_Status($"Forwarding local port {portForward.BoundPort} to {host}:22 via relay.");

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
                            RTB_Status("Connected to target server through relay.");


                            if (targetClient.IsConnected)
                            {
                                // Run command and stream output
                                var clientcmd = targetClient.CreateCommand(command);
                                var asyncResult = clientcmd.BeginExecute();

                                // Read output in real-time
                                using var reader = new StreamReader(clientcmd.OutputStream);
                                // while (!reader.EndOfStream || !asyncResult.IsCompleted)
                                string? line;
                                while ((line = reader.ReadLine()) != null || !asyncResult.IsCompleted)
                                {
                                    token.ThrowIfCancellationRequested();
                                    //string line = reader.ReadLine().;
                                    if (line != null)
                                    {
                                        RTB_Message(host + ":    " + line);
                                    }
                                    System.Threading.Thread.Sleep(100); // Prevent tight loop
                                }

                                // Read any remaining output
                                string remaining = clientcmd.Result;
                                if (!string.IsNullOrEmpty(remaining))
                                {
                                    RTB_Message(remaining);
                                }

                                // Check for errors
                                if (!string.IsNullOrEmpty(clientcmd.Error))
                                {
                                    RTB_Message("Error:" + clientcmd.Error);
                                }

                                //targetClient.Disconnect();
                            }
                            else
                            {
                                RTB_Status("Failed to connect to SSH server.");
                            }

                            portForward.Stop();
                            targetClient.Disconnect();
                        }
                    }

                    relayClient.Disconnect();
                }
            }
            catch (OperationCanceledException)
            {
                RTB_Status($"Thread {id} stopped.");
            }
            catch (Exception ex)
            {
                RTB_Status($"Exception:{ex.Message}:{ex.Source}");
            }
        }
        private void ExecuteSshCommand_Direct(string host, string username, string pemFilePath, string command, int id, CancellationToken token)
        {

            try
            {

                // Load PEM private key
                using var privateKeyStream = new FileStream(pemFilePath, FileMode.Open, FileAccess.Read);
                var privateKey = new PrivateKeyFile(privateKeyStream);

                // Relay (jump host) credentials                
                var connectionInfoRelay = new ConnectionInfo(
                    host: (System.Configuration.ConfigurationManager.AppSettings["SSHRelay"] ?? "jumpbox.telematics.com"),
                    port: 22,
                    username: (System.Configuration.ConfigurationManager.AppSettings["SSHRelayUsername"] ?? "ubuntu"),
                    new PrivateKeyAuthenticationMethod(username, privateKey)
                );

                // Step 1: Connect to the relay server
                using (var relayClient = new SshClient(connectionInfoRelay))
                {

                    //IF Jumpbox Checked
                    relayClient.Connect();
                    RTB_Status("Connected to relay server.");

                    // Step 2: Forward a local port through the relay to the target
                    using (var portForward = new ForwardedPortLocal("127.0.0.1", 0, host, (uint)22))
                    {
                        relayClient.AddForwardedPort(portForward);
                        portForward.Start();

                        //Console.WriteLine($"Forwarding local port {portForward.BoundPort} to {host}:22 via relay.");
                        RTB_Status($"Forwarding local port {portForward.BoundPort} to {host}:22 via relay.");

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
                            RTB_Status("Connected to target server through relay.");


                            if (targetClient.IsConnected)
                            {
                                // Run command and stream output
                                var clientcmd = targetClient.CreateCommand(command);
                                var asyncResult = clientcmd.BeginExecute();

                                // Read output in real-time
                                using var reader = new StreamReader(clientcmd.OutputStream);
                                // while (!reader.EndOfStream || !asyncResult.IsCompleted)
                                string? line;
                                while ((line = reader.ReadLine()) != null || !asyncResult.IsCompleted)
                                {
                                    token.ThrowIfCancellationRequested();
                                    //string line = reader.ReadLine().;
                                    if (line != null)
                                    {
                                        RTB_Message(host + ":    " + line);
                                    }
                                    System.Threading.Thread.Sleep(100); // Prevent tight loop
                                }

                                // Read any remaining output
                                string remaining = clientcmd.Result;
                                if (!string.IsNullOrEmpty(remaining))
                                {
                                    RTB_Message(remaining);
                                }

                                // Check for errors
                                if (!string.IsNullOrEmpty(clientcmd.Error))
                                {
                                    RTB_Message("Error:" + clientcmd.Error);
                                }

                                //targetClient.Disconnect();
                            }
                            else
                            {
                                RTB_Status("Failed to connect to SSH server.");
                            }

                            portForward.Stop();
                            targetClient.Disconnect();
                        }
                    }

                    relayClient.Disconnect();
                }
            }
            catch (OperationCanceledException)
            {
                RTB_Status($"Thread {id} stopped.");
            }
            catch (Exception ex)
            {
                RTB_Status($"Exception:{ex.Message}:{ex.Source}");
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            string username = (ConfigurationManager.AppSettings["SSHRelayUsername"] ?? "ubuntu");
            string pemFilePath = (ConfigurationManager.AppSettings["SSHRelayKeyPath"] ?? "/etc");
            bool rowexists = false;
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
                                RTB_Status($"'{node.Text + "-" + cmd.Text}' exists in the list.");
                                break;
                            }
                        }
                    }

                    if (rowexists != true)
                    {
                        // Thread-safe increment to avoid duplicate IDs in multi-threaded scenarios

                        var cts = new CancellationTokenSource();
                        int id = 0;
                        id = generateId();
                        threadTokens[id] = cts;
                        Task task = Task.Factory.StartNew(() =>
                        {
                            ExecuteSshCommand_Relay(node.Text, username, pemFilePath, cmd.Text, id, cts.Token);
                        });

                        RTB_Status($"'{node.Text + "-" + cmd.Text}' Does not exists in the list." + $" and is now running on thread" + task.Id);
                        dataGridView1.Rows.Add(node.Text, cmd.Text, task.Id, id, cts.Token);
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
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No rows selected.");
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // Example: Get value from first column
                    var taskid = row.Cells[3].Value;
                    // Safe conversion to int
                    if (taskid != null && int.TryParse(taskid.ToString(), out int intValue))
                    {
                        RTB_Status($"Terminating Thread ID:{intValue}");
                        StopWorker(intValue);
                    }
                    else
                    {
                        RTB_Status($"No Selection Made. Cell value is null or not a valid integer.");
                    }
                }
            }

            catch (Exception ex)
            {
                RTB_Status($"Exception:{ex.Message}:{ex.Source}");
            }
        }
        // Stop a specific worker thread
        private void StopWorker(int id)
        {
            try
            {
                if (threadTokens.TryGetValue(id, out var cts))
                {

                    RTB_Status($"Terminating Thread ID: {id} ");
                    cts.Cancel();
                }
                else
                {
                    RTB_Status($"Thread {id} not found.");
                }

                for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    if (!dataGridView1.SelectedRows[i].IsNewRow) // Avoid deleting new rows
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                RTB_Status($"Exception:{ex.Message}:{ex.Source}");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Focus();
        }

        private void btn_RefreshNodes_Click(object sender, EventArgs e)
        {
            string[] environments = ConfigurationManager.AppSettings["ChefEnvironments"].Split(' ');

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            KnifeRefreshNodes(environments);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        }

        public void LoadJsonToTreeView(string filePath, System.Windows.Forms.TreeView treeView, string ENV)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = doc.RootElement;

                    // Create a root node for the visual tree
                    TreeNode rootNode = new TreeNode(ENV);
                    treeView.Nodes.Add(rootNode);
                    AddJsonNodes(root, rootNode);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading JSON: {ex.Message}");
            }
            
        }

        private void AddJsonNodes(JsonElement element, TreeNode treeNode)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (JsonProperty property in element.EnumerateObject())
                    {
                        TreeNode propNode = new TreeNode(property.Name);
                        treeNode.Nodes.Add(propNode);
                        AddJsonNodes(property.Value, propNode); // Recursion
                    }
                    break;

                case JsonValueKind.Array:
                    int index = 0;
                    foreach (JsonElement arrayItem in element.EnumerateArray())
                    {
                        string itemTitle = GetBestTitleFromObject(arrayItem, index);
                        TreeNode itemNode = new TreeNode(itemTitle);
                        //TreeNode itemNode = new TreeNode($"[{index}]"); 
                        treeNode.Nodes.Add(itemNode);
                        AddJsonNodes(arrayItem, itemNode); // Recursion
                        index++;
                    }
                    break;

                // Leaf nodes (Values)
                default:
                    treeNode.Text += $": {element.ToString()}";
                    break;
            }
        }
        private  string GetBestTitleFromObject(JsonElement element, int index)
    {
        // Define priority properties to look for
        string[] titleKeys = { "name", "title", "id", "key" };

        foreach (string key in titleKeys)
        {
                RTB_Status($"Loading JSON for : " + element);
                if (element.TryGetProperty(key, out JsonElement prop) || element.TryGetProperty(key.ToUpper(), out prop)) // Case-insensitive check
            {
                return prop.ToString();
            }
        }

        // Fallback if no matching primitive child title property is found
        return $"[{index}]";
    }
        private void button3_Click(object sender, EventArgs e)
        {
            treeView3.Nodes.Clear();
            treeView3.Sorted = true;
            //treeView3.CollapseAll();
            string[] environments = (ConfigurationManager.AppSettings["ChefEnvironments"]?.Split(' ') ?? Array.Empty<string>());  
           
            foreach (string env in environments)
            {
                try
                {
                    treeView3.BeginUpdate(); 
                    RTB_Status($"Loading JSON for : " + env );
                    LoadJsonToTreeView(".\\" + env + ".json", treeView3, env);
                    treeView3.EndUpdate();
                }
                catch (Exception ex)
                {
                    RTB_Status($"Error parsing JSON for : " + env  + ex.Message);
                }
                finally
                {
                    
                }
                
            }
            
        }
        
        
        
        async Task KnifeRefreshNodes(string[] environments)
        {
            using RunspacePool pool = RunspaceFactory.CreateRunspacePool(1, 5);
            pool.Open();
            var tasks = new List<Task>();

            foreach (var env in environments)
            {
                tasks.Add(Task.Run(() => RunPowerShellCommand(pool, env)));
            }
            await Task.WhenAll(tasks);
            pool.Close();

        }

        private void RunPowerShellCommand(RunspacePool pool, string env)
        {
            using PowerShell ps = PowerShell.Create();
            ps.RunspacePool = pool;
            ps.AddScript($"$env:CHEF_ENV='" + env + "'; C:\\opscode\\chef-workstation\\bin\\knife.bat search '*:*' -F json | Out-String -Stream | Where-Object { -not $_.Contains(\"INFO:\")} | Out-File -FilePath .\\" + env + ".json");
            try
            {
                var results = ps.Invoke();
                foreach (var output in results)
                {
                    RTB_Status($"[Thread {Environment.CurrentManagedThreadId}] Success for {env}: {output}");
                }
            }
            catch (Exception ex)
            {
                RTB_Status($"Error on thread {Environment.CurrentManagedThreadId} for {env}: {ex.Message}");
            }
        }
        
    }
}
