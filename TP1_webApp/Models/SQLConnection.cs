using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        public String Name;
        public int Price;



        // Init
        // ... this inicialize the table
        public SQLConnection()
        {
            //public String DBCredentials = "Data Source=ec2-54-160-71-139.compute-1.amazonaws.com;Initial Catalog=TareaConcepto;Persist Security Info=True;User ID=sa;Password=Guachin321?";
            DBCredentials = "Data Source=ec2-18-225-7-10.us-east-2.compute.amazonaws.com;Initial Catalog=TP1;Persist Security Info=True;User ID=sa;Password=Admin1234";
            Connection = new SqlConnection(DBCredentials);
            Name = "";
            Price = 0;
            ItemClass articuloInfo = new ItemClass();
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
                SqlCommand SelectCommand = new SqlCommand("SELECT * FROM dbo.Articulo", Connection);
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
            Console.WriteLine("aqui");
            try
            {
                int i = 100;
                int p = 1;
                String valName = "newName3";

                SqlConnection Connection = new SqlConnection(DBCredentials);
                SqlCommand InsertCommand = new SqlCommand("INSERT INTO [TP1].[dbo].[Articulo]([Nombre], [Precio])VALUES(@valName,@valPrice)", Connection);
                InsertCommand.CommandType = CommandType.Text;
                InsertCommand.Parameters.Add("@valName", SqlDbType.VarChar).Value = valName;
                InsertCommand.Parameters.AddWithValue("@valPrice", SqlDbType.Int).Value = p;

                // ... open connection and send new item
                try
                {
                    Connection.Open();
                    InsertCommand.ExecuteNonQuery();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
