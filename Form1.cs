using Newtonsoft.Json;
using System.Data;
using WinFormsApp4.Models;
using WinFormsApp4.Repositories;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ReadClients();
            //ReadData();
        }

        private async Task ReadData()
        {
            DataTable dataTable = new DataTable();

            var repo = new ClientRepository();
            var clients = await repo.GetData();

            Cursor.Current = Cursors.WaitCursor;

            var clientList = JsonConvert.DeserializeObject<List<PostModel>>(clients);

            List<PostModel> data = new List<PostModel>();

            foreach (var client in clientList)
            {
                data.Add(client);
            }

            DataTable dataTable2 = new DataTable();

            dataTable2.Columns.Add("userId");
            dataTable2.Columns.Add("id");
            dataTable2.Columns.Add("title");
            dataTable2.Columns.Add("body");

            foreach (var c in clientList)
            {
                var r = dataTable2.NewRow();

                r["userId"] = c.userId;
                r["id"] = c.id;
                r["title"] = c.title;
                r["body"] = c.body;

                dataTable2.Rows.Add(r);
            }

            this.clientsTable.DataSource = dataTable2;
        }

        private void ReadClients()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Phone");
            dataTable.Columns.Add("Date");

            var repo = new ClientRepository();
            var clients = repo.GetClients();

            foreach (var client in clients)
            {
                var row = dataTable.NewRow();

                row["ID"] = client.id;
                row["Name"] = client.firstname + " " + client.lastname;
                row["Email"] = client.email;
                row["Phone"] = client.phone;
                row["Date"] = client.created_at;

                dataTable.Rows.Add(row);
            }

            this.clientsTable.DataSource = dataTable;

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var val = this.clientsTable.SelectedRows[0].Cells[0].Value.ToString();
            if (val == null || val.Length == 0) return;

            int clientId = int.Parse(val);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this client?", "Delete Client", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            var repo = new ClientRepository();
            repo.DeleteClient(clientId);

            ReadClients();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            CreateEditForm form = new CreateEditForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadClients();
            }
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            var val = this.clientsTable.SelectedRows[0].Cells[0].Value.ToString();

            if (val == null || val.Length == 0) return;

            int clientId = int.Parse(val);

            var repo = new ClientRepository();
            var client = repo.GetClient(clientId);

            if (client == null) { return; }

            CreateEditForm form = new CreateEditForm();
            form.EditClient(client);

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadClients();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
