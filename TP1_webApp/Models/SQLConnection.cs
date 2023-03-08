using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;



namespace TP1_webApp.Models
{

    public class SQLConnection
    {
        // Attribute
        public List<ItemClass> ItemsList = new List<ItemClass>();
        public int ItemsListCount = 0;
        // ... DB credentials
        public String DBCredentials;
        public SqlConnection Connection;
        public SqlCommand SelectCommand;
        public SqlCommand InsertCommand;


        // Init
        // ... this inicialize the table
        public SQLConnection()
        {
            //public String DBCredentials = "Data Source=ec2-54-160-71-139.compute-1.amazonaws.com;Initial Catalog=TareaConcepto;Persist Security Info=True;User ID=sa;Password=Guachin321?";
            DBCredentials = "Data Source=ec2-18-225-7-10.us-east-2.compute.amazonaws.com;Initial Catalog=TP1;Persist Security Info=True;User ID=sa;Password=Admin1234";
            Connection = new SqlConnection(DBCredentials);
            SelectCommand = new SqlCommand("SELECT * FROM dbo.Articulo", Connection);
            InsertCommand = new SqlCommand("INSERT INTO dbo.Articulo (Id,Nombre,Precio) VALUES (@valID, @valName, @valPrice)", Connection);
            ItemClass articuloInfo = new ItemClass("NA1", "NA2", "NA3");
            ItemsList.Add(articuloInfo);
        }


        // Methods

        // ... Get info from DB
        public void Get()
        {
            try
            {
                // ... open connection, send request and read responce
                Connection.Open();
                SqlDataReader Reader = SelectCommand.ExecuteReader();
                
                // ... collect the items from the DB
                while (Reader.Read())
                {
                    String articuloID = Reader["Id"].ToString();
                    String articuloName = Reader["Nombre"].ToString();
                    String articuloPrice = Reader["Precio"].ToString();

                    ItemClass articuloInfo = new ItemClass(articuloID, articuloName, articuloPrice);
                    ItemsList.Add(articuloInfo);
                    ItemsListCount++;
                }
                
                // ... close connection
                var sortedList = ItemsList.OrderBy(p => p.Name).ToList();
                ItemsList = sortedList;
                Reader.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }



        // ... Add item to DB
        public void Add()
        {
            try
            {
                SqlConnection Connection = new SqlConnection(DBCredentials);
                InsertCommand.Parameters.AddWithValue("@valID", "newID");
                InsertCommand.Parameters.AddWithValue("@valName", "newNAme");
                InsertCommand.Parameters.AddWithValue("@valPrice", "newPrice");

                // ... open connection and send new item
                Connection.Open();
                InsertCommand.ExecuteNonQuery();
                Connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
