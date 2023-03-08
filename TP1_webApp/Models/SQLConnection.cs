using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;



namespace TP1_webApp.Models
{

    public class SQLConnection
    {
        // Attribute
        public List<ItemClass> ItemsList = new List<ItemClass>();
        public int ItemsListCount = 0;
        

        // Init
        // ... this inicialize the table
        public SQLConnection()
        {
            ItemClass articuloInfo = new ItemClass("NA1", "NA2", "NA3");
            ItemsList.Add(articuloInfo);
        }


        // Method
        // ... getting info from DB
        public void Get()
        {
            try
            {
                // ... DB credentials
                String DBCredentials = "Data Source=ec2-54-160-71-139.compute-1.amazonaws.com;Initial Catalog=TareaConcepto;Persist Security Info=True;User ID=sa;Password=Guachin321?";
                //String DBCredentials = "Data Source=ec2-18-225-7-10.us-east-2.compute.amazonaws.com;Initial Catalog=TP1;Persist Security Info=True;User ID=sa;Password=Admin1234";
                SqlConnection Connection = new SqlConnection(DBCredentials);
                
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
    }
}
