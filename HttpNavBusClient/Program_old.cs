using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



using System.Configuration;


using System.Data.SqlClient;
using log4net;
using System.Diagnostics;

namespace HttpNavBusClient_old
{

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
    public class Customer
    {


        public string No_ { get; set; }
        public string Name { get; set; }
        public string SearchName { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string CustomerPostingGroup { get; set; }
        public string InvoiceDisc_Code { get; set; }
        public string LocationCode { get; set; }
        public string Gen_Bus_PostingGroup { get; set; }
        public string VATBus_PostingGroup { get; set; }

        public DataTable datatable_Navcustomer_tosend = new DataTable();//we using data table to fill with data and sending it to NAV
        public Customer()
        {
            datatable_Navcustomer_tosend.Columns.Add("No_", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("Name", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("SearchName", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("GlobalDimension1Code", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("CustomerPostingGroup", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("InvoiceDisc_Code", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("LocationCode", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("Gen_Bus_PostingGroup", typeof(string));//we do not need the store name 
            datatable_Navcustomer_tosend.Columns.Add("VATBus_PostingGroup", typeof(string));//we do not need the store name 

        }



    }

    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName,"Machine Name");
        static string OTD_connectionString = ConfigurationManager.ConnectionStrings["erp_otd"].ConnectionString;
        static string UAE_connectionString = ConfigurationManager.ConnectionStrings["erp_uae"].ConnectionString;
        static string Callcenter_85_connectionString = ConfigurationManager.ConnectionStrings["callCenter_85"].ConnectionString;

        static HttpClient client = new HttpClient();
        /*static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }*/


       /* static void Main()
        {
            // call set the customers fistrly
            NavCustomers_RunAsync().GetAwaiter().GetResult();
        }*/


        static async Task NavCustomers_RunAsync()
        {
  
            client.BaseAddress = new Uri("http://localhost/drnutrition03082019/en/ae/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var byteArray = Encoding.ASCII.GetBytes("admin:1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            try
            {
                // Create a new product
                Product product = new Product
                {
                    Name = "Gizmo",
                    Email = "Email@rmail.comn",

                };

                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created Fininshed at {url}");

                // Get the product
                // product = await GetProductAsync(url.PathAndQuery);
                product = await GetProductAsync();
                ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.Email = "NewEmail@cc.gmail.com";
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }


        //return string of status code
        static async Task<string> CreateAsync(string ValuesJson, string url = "api/example/customers")
        {
            Console.WriteLine($" Create Customer from data: {ValuesJson}\t ");

            HttpResponseMessage response = await client.PostAsJsonAsync("api/example/users", ValuesJson);
            Console.WriteLine($" Create response.StatusCode: {response.StatusCode}\tHeader is : " +
               $"{ response.Headers}\t");
            response.EnsureSuccessStatusCode();



            // return URI of the created resource. // i donet neet it now
            return response.StatusCode.ToString();
        }

        public string getCustomersDBAsync()
        {
            string resultjson = "";
            Customer customer_var = new Customer();

            string sqlstm = @" SELECT [No_]
      ,[Name]
      ,[Search Name]
      ,[Global Dimension 1 Code]
      ,[Customer Posting Group]
      ,[Invoice Disc_ Code]
      ,[Location Code]
      ,[Gen_ Bus_ Posting Group]

      ,[VAT Bus_ Posting Group]
   
     
   
    
  FROM [DNP2016-HO].[dbo].[Dr_ Nutrition HO - 2016$Customer]

 where [Customer Posting Group] like'%ONLINE%' ";


            try
            {

                using (SqlConnection con = new SqlConnection(UAE_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = sqlstm;//where [SerialNo]=@serialno
                        cmd.Connection = con;
                        con.Open();
                        //  return resultjson + " 3";

                        // return cmd.CommandText.ToString();
                        /*cmd.CommandText += @"   and  MC.Status = 2 and

      (CONVERT(datetime,CONVERT(VARCHAR,[Last Valid Date], 101)) >
      CONVERT(datetime,CONVERT(VARCHAR, DATEADD(DAY, -1, GETDATE()), 101))  or CONVERT(datetime,CONVERT(VARCHAR,[Last Valid Date], 101))=
      CONVERT(datetime,'1753-01-01 00:00:00.000', 101))  ";*/

                       //cmd.Parameters.AddWithValue("@PhoneNo", DicountNo.ToString());

                      

                        using (SqlDataReader mySqlDataReader = cmd.ExecuteReader())
                        {
                            if (mySqlDataReader.HasRows)
                            {
                                while (mySqlDataReader.Read())
                                {

                                    DataRow item_sampleDataRow;
                                    item_sampleDataRow = customer_var.datatable_Navcustomer_tosend.NewRow();


                                    item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                    item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                    item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                    item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                    item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                    item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                    item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();


                                    customer_var.datatable_Navcustomer_tosend.Rows.Add(item_sampleDataRow);


                                }
                            }
                        }












                    }
                }




            }
            catch (Exception ex)
            {
                Logger.Fatal(ReportError("getCradDicounts :  " + ex.Message.ToString() + " Nav MemberShip WS 85  "));
                return ex.Message.ToString();

            }



            resultjson = DataTableToJSONWithStringBuilder(customer_var.datatable_Navcustomer_tosend);

            return resultjson;

        }

       /* static async Task<string> GetjsonfromDatatabel(DataTable datatable)
        {
            string resultjson = "";

            return resultjson;

        }*/

/***********************************************************setting functions  *****************************************************/
        static string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
              //  JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",\n");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"\n");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
               // JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        static private string ReportError(string Message)
        {
            StackFrame CallStack = new StackFrame(1, true);
            return ("API : " + Message + ", File: " + CallStack.GetFileName() + ", Method Name " + CallStack.GetMethod().ToString() + ", Line: " + CallStack.GetFileLineNumber() ).ToString();
        }

        // Retrieves a connection string by name.
        // Returns null if the name is not found.
       
        /***********************************************************End setting functions  *****************************************************/





        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tEmail: " +
                $"{product.Email}\t");
        }




        static async Task<Uri> CreateProductAsync(Product product)
        {
            Console.WriteLine($" Create Name: {product.Name}\tEmail: " +
               $"{product.Email}\t");
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/example/users", product);
            Console.WriteLine($" Create response.StatusCode: {response.StatusCode}\tHearser is : " +
               $"{ response.Headers}\t");
            response.EnsureSuccessStatusCode();



            // return URI of the created resource. // i donet neet it now
            return response.Headers.Location;
        }


        static async Task<Product> GetProductAsync(string path = "api/example/users/id/2")
        {
            Product product = null;


            HttpResponseMessage response = await client.GetAsync(path);

            Console.WriteLine($" Get response.StatusCode: {response}\t ");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        static async Task<Product> UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/example/users/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/example/users/{id}");
            return response.StatusCode;
        }


        static void Main_old()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Product product = new Product
                {
                    Name = "Gizmo",
                    Email = "Email@rmail.comn",

                };

                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created Fininshed at {url}");

                // Get the product
                // product = await GetProductAsync(url.PathAndQuery);
                product = await GetProductAsync();
                ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.Email = "NewEmail@cc.gmail.com";
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }


    }
}
