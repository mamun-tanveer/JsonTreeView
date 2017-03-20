using System;
using System.Windows.Forms;

namespace JsonTreeView
{
	public partial class MainForm : Form
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		public MainForm()
		{
			InitializeComponent();

			JsonTreeView.LoadJsonToTreeView(@"{
	'id': '0001',
	'type': 'donut',
	'name': 'Cake',
	'ppu': 0.55,
	'batters':
		{
			'batter':
				[
					{ 'id': '1001', 'type': 'Regular' },
					{ 'id': '1002', 'type': 'Chocolate' },
					{ 'id': '1003', 'type': 'Blueberry' },
					{ 'id': '1004', 'type': 'Devil\'s Food' }
				]
		},
	'topping':
		[
			{ 'id': '5001', 'type': 'None' },
			{ 'id': '5002', 'type': 'Glazed' },
			{ 'id': '5005', 'type': 'Sugar' },
			{ 'id': '5007', 'type': 'Powdered Sugar' },
			{ 'id': '5006', 'type': 'Chocolate with Sprinkles' },
			{ 'id': '5003', 'type': 'Chocolate' },
			{ 'id': '5004', 'type': 'Maple' }
		]
}");
			JsonTreeView.ExpandAll();
		}

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                var reader = new System.IO.StreamReader(openFileDialog1.OpenFile());
                string json = reader.ReadToEnd();
                JsonTreeView.Nodes.Clear();
                JsonTreeView.LoadJsonToTreeView(json);
                foreach(TreeNode node in JsonTreeView.Nodes)
                {
                    node.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to open", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                JsonTreeView.SelectedNode.Remove();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to delete node", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.ShowDialog();
                string outputPath = saveFileDialog1.FileName;
                TreeNode parentNode = JsonTreeView.TopNode;
                parentNode.SaveToFile(outputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
    }
}