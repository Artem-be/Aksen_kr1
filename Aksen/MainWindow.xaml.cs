using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Aksen
{
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;

        public MainWindow()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(@"Server=dbsrv\DUB2024;Database=Aksen;Trusted_Connection=true;TrustServerCertificate=true;encrypt=false");
            LoadData();
        }

        private void OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        private void LoadData()
        {
            try
            {
                OpenConnection();

                // Загрузка клиентов
                LoadClients();

                // Загрузка транспорта
                LoadTransport();

                // Загрузка поездок
                LoadTrips();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void LoadClients()
        {
            string clientsQuery = "SELECT * FROM Клиенты";
            SqlDataAdapter clientsAdapter = new SqlDataAdapter(clientsQuery, sqlConnection);
            DataTable clientsTable = new DataTable();
            clientsAdapter.Fill(clientsTable);
            ClientsGrid.ItemsSource = clientsTable.DefaultView;
        }

        private void LoadTransport()
        {
            string transportQuery = "SELECT * FROM Транспорт";
            SqlDataAdapter transportAdapter = new SqlDataAdapter(transportQuery, sqlConnection);
            DataTable transportTable = new DataTable();
            transportAdapter.Fill(transportTable);
            TransportGrid.ItemsSource = transportTable.DefaultView;
        }

        private void LoadTrips()
        {
            string tripsQuery = "SELECT * FROM Поездки";
            SqlDataAdapter tripsAdapter = new SqlDataAdapter(tripsQuery, sqlConnection);
            DataTable tripsTable = new DataTable();
            tripsAdapter.Fill(tripsTable);
            TripsGrid.ItemsSource = tripsTable.DefaultView;
        }


    }
}