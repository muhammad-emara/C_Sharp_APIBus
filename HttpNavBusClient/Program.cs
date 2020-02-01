using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using log4net;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using NAVCompanies_210;

namespace HttpNavBusClient
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
            datatable_Navcustomer_tosend.Columns.Add("No_", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("Name", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("SearchName", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("GlobalDimension1Code", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("CustomerPostingGroup", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("InvoiceDisc_Code", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("LocationCode", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("Gen_Bus_PostingGroup", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("VATBus_PostingGroup", typeof(string));
            datatable_Navcustomer_tosend.Columns.Add("country", typeof(string));

        }



    }

    public class NAVCompanies_210
    {

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        static string OTD_connectionString = ConfigurationManager.ConnectionStrings["erp_otd"].ConnectionString;
        static string UAE_connectionString = ConfigurationManager.ConnectionStrings["erp_uae"].ConnectionString;
        static string Callcenter_85_connectionString = ConfigurationManager.ConnectionStrings["callCenter_85"].ConnectionString;
        static string logFileName = "Log_" + DateTime.Now.ToString("dd_MM_yyyy_HH") + ".txt";

        public List<string> Companies()
        {
            List<string> CompaniesList = new List<string>();

            string sqlstm = @" select Name from Company where [Hide from Reports] =0 ";


            try
            {

                using (SqlConnection con = new SqlConnection(OTD_connectionString))
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
                                    //  Console.WriteLine("We Found Compnay Called " + mySqlDataReader["Name"].ToString());

                                    CompaniesList.Add(mySqlDataReader["Name"].ToString());


                                }
                            }
                        }












                    }
                }




            }
            catch (Exception ex)
            {
                Logger.Fatal(Program.ReportError("getCradDicounts :  " + ex.Message.ToString() + " Nav OTD Customer WS 285  "));
                //  return ex.Message.ToString();

            }
            return CompaniesList;

        }


    }

    public class PaymentMehods
    {


        public string Code { get; set; }
        public string Description { get; set; }


        public DataTable datatable_Navdata_tosend = new DataTable();//we using data table to fill with data and sending it to NAV


        public PaymentMehods()
        {
            datatable_Navdata_tosend.Columns.Add("Code", typeof(string));
            datatable_Navdata_tosend.Columns.Add("Description", typeof(string));
            datatable_Navdata_tosend.Columns.Add("country", typeof(string));

        }



    }

    public class ShippingAgents
    {


        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public DataTable datatable_Navdata_tosend = new DataTable();//we using data table to fill with data and sending it to NAV


        public ShippingAgents()
        {
            datatable_Navdata_tosend.Columns.Add("Code", typeof(string));
            datatable_Navdata_tosend.Columns.Add("Name", typeof(string));
            datatable_Navdata_tosend.Columns.Add("InternetAddress", typeof(string));
            datatable_Navdata_tosend.Columns.Add("country", typeof(string));

        }



    }


    /// <summary>
    /// MemberShip Calling 
    /// </summary>


    // Nav Contacts
    public class ContactInfo
    {




        public DataTable datatable_Navdata_tosend = new DataTable();//we using data table to fill with data and sending it to NAV


        public ContactInfo()
        {


            datatable_Navdata_tosend.Columns.Add("accountno", typeof(string));
            datatable_Navdata_tosend.Columns.Add("ContactNo", typeof(string));
            datatable_Navdata_tosend.Columns.Add("clubcode", typeof(string));
            datatable_Navdata_tosend.Columns.Add("SchemeCode", typeof(string));
            datatable_Navdata_tosend.Columns.Add("ContactType", typeof(string));
            datatable_Navdata_tosend.Columns.Add("Name", typeof(string));
            datatable_Navdata_tosend.Columns.Add("PhoneNo_", typeof(string));
            datatable_Navdata_tosend.Columns.Add("MobilePhoneNo_", typeof(string));
            datatable_Navdata_tosend.Columns.Add("Country", typeof(string));
            datatable_Navdata_tosend.Columns.Add("Blocked", typeof(string));
            datatable_Navdata_tosend.Columns.Add("BlockFromDate", typeof(string));
            datatable_Navdata_tosend.Columns.Add("BlockToDate", typeof(string));


        }



    }

    // Nav Cards
    public class CardInfo
    {




        public DataTable datatable_Navdata_tosend = new DataTable();//we using data table to fill with data and sending it to NAV


        public CardInfo()
        {


            datatable_Navdata_tosend.Columns.Add("cardno_", typeof(string));
            datatable_Navdata_tosend.Columns.Add("status", typeof(string));//we don not need the name 
            datatable_Navdata_tosend.Columns.Add("Actualstatus", typeof(string));//we don not need the name 
            datatable_Navdata_tosend.Columns.Add("clubcode", typeof(string));//we don not need the name 
            datatable_Navdata_tosend.Columns.Add("schemecode", typeof(string));
            datatable_Navdata_tosend.Columns.Add("accountno_", typeof(string));
            datatable_Navdata_tosend.Columns.Add("contactno_", typeof(string));
            datatable_Navdata_tosend.Columns.Add("firstdateused", typeof(string));
            datatable_Navdata_tosend.Columns.Add("lastvaliddate", typeof(string));
            datatable_Navdata_tosend.Columns.Add("membershiptype", typeof(string));



        }



    }



    /*********************************************** END OF MemberShip **************************************************/
    class Program
    {

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        static string OTD_connectionString = ConfigurationManager.ConnectionStrings["erp_otd"].ConnectionString;
        static string UAE_connectionString = ConfigurationManager.ConnectionStrings["erp_uae"].ConnectionString;
        static string Callcenter_85_connectionString = ConfigurationManager.ConnectionStrings["callCenter_85"].ConnectionString;
        static string logFileName = "Log_" + DateTime.Now.ToString("dd_MM_yyyy_HH") + ".txt";

        static HttpClient client = new HttpClient();


        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tEmail: " +
                $"{product.Email}\t");
        }

        static async Task<Uri> CreateProductAsync(Product product)
        {
            Console.WriteLine($" Create Name: {product.Name}\tEmail: " +
               $"{product.Email}\t");

            var byteArray = Encoding.ASCII.GetBytes("admin:1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/ErpBus/users/", product);
            Console.WriteLine($" Create response.StatusCode: {response.StatusCode}\tHearser is : " +
               $"{ response.Headers}\t");
            response.EnsureSuccessStatusCode();



            // return URI of the created resource. // i donet neet it now
            return response.Headers.Location;
        }

        static async Task<Product> GetProductAsync(string path = "api/ErpBus/users/id/1")
        {
            Product product = null;
            string prodct_str = "";
            HttpResponseMessage response = await client.GetAsync(path);

            Console.WriteLine($" Get response.StatusCode: {response.StatusCode}\t\n ");

            if (response.IsSuccessStatusCode)
            {
                // product = await response.Content.ReadAsAsync<Product>();
                prodct_str = await response.Content.ReadAsAsync<string>();
                Console.WriteLine($" \n\t  response: {prodct_str}\t\n ");
            }

         
            return product;
        }

        static async Task<Product> UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }

        //getPaymentDBAsync get Payment methods from nav
        public static string getPaymentDBAsync()
        {
            string resultjson = "";

            PaymentMehods PaymentMehod_var = new PaymentMehods();

            string sqlstm = @" SELECT Code
      ,Description      
  FROM [DNP2016-HO].[dbo].[Dr_ Nutrition HO - 2016$Payment Method]";

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
                                    item_sampleDataRow = PaymentMehod_var.datatable_Navdata_tosend.NewRow();


                                    item_sampleDataRow["Code"] = mySqlDataReader["Code"].ToString();
                                    item_sampleDataRow["Description"] = mySqlDataReader["Description"].ToString();
                                    item_sampleDataRow["country"] = "UAE";
                                    // datatable_Navcustomer_tosend.Columns.Add("country", typeof(string));



                                    PaymentMehod_var.datatable_Navdata_tosend.Rows.Add(item_sampleDataRow);


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



            resultjson = DataTableToJSONWithStringBuilder(PaymentMehod_var.datatable_Navdata_tosend);

            return resultjson;
        }



        //getPaymentDBAsync get Payment methods from nav
        public static string getOTDPaymentDBAsync()
        {

            List<string> companies = new NAVCompanies_210().Companies();

            string resultjson = "";
            PaymentMehods PaymentMehod_var = new PaymentMehods();

            foreach (string company in companies)
            {
                string country = company.ToString();
                

                string sqlstm = @" SELECT Code
      ,Description      
  FROM [OTD2016NEW].[dbo].[" + company.ToString() + @"$Payment Method]";

                try
                {

                    using (SqlConnection con = new SqlConnection(OTD_connectionString))
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
                                        item_sampleDataRow = PaymentMehod_var.datatable_Navdata_tosend.NewRow();


                                        item_sampleDataRow["Code"] = mySqlDataReader["Code"].ToString();
                                        item_sampleDataRow["Description"] = mySqlDataReader["Description"].ToString();
                                        item_sampleDataRow["country"] =country.ToString();
                                        // datatable_Navcustomer_tosend.Columns.Add("country", typeof(string));



                                        PaymentMehod_var.datatable_Navdata_tosend.Rows.Add(item_sampleDataRow);


                                    }
                                }
                            }












                        }
                    }




                }
                catch (Exception ex)
                {
                    Logger.Fatal(ReportError("getOTDPaymentDBAsync :  " + ex.Message.ToString() + "   "));
                    return ex.Message.ToString();

                }



            }




            resultjson = DataTableToJSONWithStringBuilder(PaymentMehod_var.datatable_Navdata_tosend);

            return resultjson;
        }


        //get customer from DB and convert it to Json

        public static string getCustomers_bulkDBAsync()
        {
            string resultjson = "";
            string country = "UAE";
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

                                    /*  DataRow item_sampleDataRow;
                                      item_sampleDataRow = customer_var.datatable_Navcustomer_tosend.NewRow();


                                      item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                      item_sampleDataRow["SearchName"] = mySqlDataReader["Search Name"].ToString();
                                      item_sampleDataRow["Name"] = mySqlDataReader["Name"].ToString();
                                      item_sampleDataRow["GlobalDimension1Code"] = mySqlDataReader["Global Dimension 1 Code"].ToString();
                                      item_sampleDataRow["CustomerPostingGroup"] = mySqlDataReader["Customer Posting Group"].ToString();
                                      item_sampleDataRow["InvoiceDisc_Code"] = mySqlDataReader["Invoice Disc_ Code"].ToString();
                                      item_sampleDataRow["LocationCode"] = mySqlDataReader["Location Code"].ToString();
                                      item_sampleDataRow["Gen_Bus_PostingGroup"] = mySqlDataReader["Gen_ Bus_ Posting Group"].ToString();
                                      item_sampleDataRow["VATBus_PostingGroup"] = mySqlDataReader["VAT Bus_ Posting Group"].ToString();
                                      item_sampleDataRow["country"] = "UAE";


                                      customer_var.datatable_Navcustomer_tosend.Rows.Add(item_sampleDataRow);*/

                                    //`No_`, `Name`, `SearchName`, `GlobalDimension1Code`, `CustomerPostingGroup`, `InvoiceDisc_Code`, `LocationCode`, `Gen_Bus_PostingGroup`, `VATBus_PostingGroup`, `country`
                                    resultjson += @"('" + mySqlDataReader["No_"].ToString() + "','" + mySqlDataReader["Name"].ToString() + "','" + mySqlDataReader["Search Name"].ToString() + "','" + mySqlDataReader["Global Dimension 1 Code"].ToString() + "','" + mySqlDataReader["Customer Posting Group"].ToString() + "','" + mySqlDataReader["Invoice Disc_ Code"].ToString() + "','" + mySqlDataReader["Location Code"].ToString() + "','" + mySqlDataReader["Gen_ Bus_ Posting Group"].ToString() + "','" + mySqlDataReader["VAT Bus_ Posting Group"].ToString() + "','" + country.ToString() + "'),";


                                }
                                resultjson += "('','','','','','','','','','','','','','')";
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



            //  resultjson = DataTableToJSONWithStringBuilder(customer_var.datatable_Navcustomer_tosend);

            return resultjson;
        }



        public static string GetCustomersBulk_OTDAsync()
        {

            //  Console.WriteLine("InSide  GetCustomersBulk_OTDAsync");

            List<string> companies = new NAVCompanies_210().Companies();

            string resultjson = "";

            foreach (string company in companies)
            {
                string country = company.ToString();

                string sqlstm = @" SELECT [No_]
      ,[Name]
      ,[Search Name]
      ,[Global Dimension 1 Code]
      ,[Customer Posting Group]
      ,[Invoice Disc_ Code]
      ,[Location Code]
      ,[Gen_ Bus_ Posting Group]

      ,[VAT Bus_ Posting Group]
   
     
   
    
  FROM [OTD2016NEW].[dbo].[" + company.ToString() + @"$Customer]

 where [Customer Posting Group] like'%ONLINE%' ";

                //Console.WriteLine("SQL For Company  " + company.ToString() + @" is " + sqlstm.ToString());

                try
                {

                    using (SqlConnection con = new SqlConnection(OTD_connectionString))
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

                                        /*  DataRow item_sampleDataRow;
                                          item_sampleDataRow = customer_var.datatable_Navcustomer_tosend.NewRow();


                                          item_sampleDataRow["No_"] = mySqlDataReader["No_"].ToString();
                                          item_sampleDataRow["SearchName"] = mySqlDataReader["Search Name"].ToString();
                                          item_sampleDataRow["Name"] = mySqlDataReader["Name"].ToString();
                                          item_sampleDataRow["GlobalDimension1Code"] = mySqlDataReader["Global Dimension 1 Code"].ToString();
                                          item_sampleDataRow["CustomerPostingGroup"] = mySqlDataReader["Customer Posting Group"].ToString();
                                          item_sampleDataRow["InvoiceDisc_Code"] = mySqlDataReader["Invoice Disc_ Code"].ToString();
                                          item_sampleDataRow["LocationCode"] = mySqlDataReader["Location Code"].ToString();
                                          item_sampleDataRow["Gen_Bus_PostingGroup"] = mySqlDataReader["Gen_ Bus_ Posting Group"].ToString();
                                          item_sampleDataRow["VATBus_PostingGroup"] = mySqlDataReader["VAT Bus_ Posting Group"].ToString();
                                          item_sampleDataRow["country"] = "UAE";


                                          customer_var.datatable_Navcustomer_tosend.Rows.Add(item_sampleDataRow);*/

                                        //`No_`, `Name`, `SearchName`, `GlobalDimension1Code`, `CustomerPostingGroup`, `InvoiceDisc_Code`, `LocationCode`, `Gen_Bus_PostingGroup`, `VATBus_PostingGroup`, `country`
                                        resultjson += @"('" + mySqlDataReader["No_"].ToString() + "','" + mySqlDataReader["Name"].ToString() + "','" + mySqlDataReader["Search Name"].ToString() + "','" + mySqlDataReader["Global Dimension 1 Code"].ToString() + "','" + mySqlDataReader["Customer Posting Group"].ToString() + "','" + mySqlDataReader["Invoice Disc_ Code"].ToString() + "','" + mySqlDataReader["Location Code"].ToString() + "','" + mySqlDataReader["Gen_ Bus_ Posting Group"].ToString() + "','" + mySqlDataReader["VAT Bus_ Posting Group"].ToString() + "','" + country.ToString() + "'),";


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

            }
            resultjson += "('','','','','','','','','','','','','','')";
            return resultjson;

        }


        public static string getCustomersDBAsync()
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
                                    item_sampleDataRow["SearchName"] = mySqlDataReader["Search Name"].ToString();
                                    item_sampleDataRow["Name"] = mySqlDataReader["Name"].ToString();
                                    item_sampleDataRow["GlobalDimension1Code"] = mySqlDataReader["Global Dimension 1 Code"].ToString();
                                    item_sampleDataRow["CustomerPostingGroup"] = mySqlDataReader["Customer Posting Group"].ToString();
                                    item_sampleDataRow["InvoiceDisc_Code"] = mySqlDataReader["Invoice Disc_ Code"].ToString();
                                    item_sampleDataRow["LocationCode"] = mySqlDataReader["Location Code"].ToString();
                                    item_sampleDataRow["Gen_Bus_PostingGroup"] = mySqlDataReader["Gen_ Bus_ Posting Group"].ToString();
                                    item_sampleDataRow["VATBus_PostingGroup"] = mySqlDataReader["VAT Bus_ Posting Group"].ToString();
                                    item_sampleDataRow["country"] = "UAE";


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



        //get PaymentMethods from DB and convert it to Json
        public static string getshippingagentsDBAsync()
        {
            string resultjson = "";
            ShippingAgents customer_var = new ShippingAgents();

            string sqlstm = @" SELECT [Code]
      ,[Name]
      ,[Internet Address]
     
  FROM [DNP2016-HO].[dbo].[Dr_ Nutrition HO - 2016$Shipping Agent] ";


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
                                    item_sampleDataRow = customer_var.datatable_Navdata_tosend.NewRow();


                                    item_sampleDataRow["Code"] = mySqlDataReader["Code"].ToString();
                                    item_sampleDataRow["InternetAddress"] = mySqlDataReader["Internet Address"].ToString();
                                    item_sampleDataRow["Name"] = mySqlDataReader["Name"].ToString();
                                    item_sampleDataRow["country"] ="UAE";
                                    



                                    customer_var.datatable_Navdata_tosend.Rows.Add(item_sampleDataRow);


                                }
                            }
                        }












                    }
                }




            }
            catch (Exception ex)
            {
                Logger.Fatal(ReportError("getshippingagentsDBAsync :  " + ex.Message.ToString() + " Nav API  "));
                return ex.Message.ToString();

            }



            resultjson = DataTableToJSONWithStringBuilder(customer_var.datatable_Navdata_tosend);

            return resultjson;

        }


        //get PaymentMethods from DB and convert it to Json
        public static string getOTDshippingagentsDBAsync()
        {
            List<string> companies = new NAVCompanies_210().Companies();
            string resultjson = "";
            ShippingAgents customer_var = new ShippingAgents();

            foreach (string company in companies)
            {
                string country = company.ToString();
                string sqlstm = @" SELECT [Code]
      ,[Name]
      ,[Internet Address]
     
  FROM [OTD2016NEW].[dbo].[" + company.ToString() + @"$Shipping Agent] ";


                try
                {

                    using (SqlConnection con = new SqlConnection(OTD_connectionString))
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
                                        item_sampleDataRow = customer_var.datatable_Navdata_tosend.NewRow();


                                        item_sampleDataRow["Code"] = mySqlDataReader["Code"].ToString();
                                        item_sampleDataRow["InternetAddress"] = mySqlDataReader["Internet Address"].ToString();
                                        item_sampleDataRow["Name"] = mySqlDataReader["Name"].ToString();
                                        item_sampleDataRow["country"] = country.ToString();



                                        customer_var.datatable_Navdata_tosend.Rows.Add(item_sampleDataRow);


                                    }
                                }
                            }












                        }
                    }




                }
                catch (Exception ex)
                {
                    Logger.Fatal(ReportError("OTD getshippingagentsDBAsync :  " + ex.Message.ToString() + " Nav API  "));
                    return ex.Message.ToString();

                }

            }

               



            resultjson = DataTableToJSONWithStringBuilder(customer_var.datatable_Navdata_tosend);

            return resultjson;

        }


        //get Nav Contact from DB and convert it to Json
        public static string getContactsDBAsync(int offsetvar = 0, int chuncksize = 1)
        {
            string resultjson = "";
            ContactInfo customer_var = new ContactInfo();



            string sqlstm = @" select Distinct   
  MCO.[Account No_] [mc account no]
   ,MCO.[Contact No_] [mc Contact No]
    ,MCO.[Club Code] [mc Club Code]
	
	  ,MCO.[Scheme Code] [mc Scheme Code]
	   ,MCO.[Contact Type] [mc Contact Type]
	    ,MCO.[Name] [mc Name]
		 ,MCO.[Phone No_] [mc Phone No_]
		  ,MCO.[Mobile Phone No_] [mc Mobile Phone No_]
		   ,MCO.[Country] [mc Country]
		    ,MCO.[Blocked] [mc Blocked]
			 ,MCO.[Block From Date] [mc Block From Date]
			 ,MCO.[Block To Date] [mc Block To Date] 
  
   from   [DNP2016-HO].[dbo].[Dr_ Nutrition HO - 2016$Member Contact] MCO
   where MCO.Blocked =0
    order by MCO.[Account No_] desc

	  OFFSET @offsetvar ROWS 
FETCH NEXT @chuncksize ROWS ONLY;
";



            try
            {

                using (SqlConnection con = new SqlConnection(UAE_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = sqlstm;


                        cmd.Connection = con;
                        con.Open();

                        cmd.Parameters.AddWithValue("@offsetvar", Convert.ToInt32(offsetvar));
                        cmd.Parameters.AddWithValue("@chuncksize", Convert.ToInt32(chuncksize));




                        using (SqlDataReader mySqlDataReader = cmd.ExecuteReader())
                        {
                            if (mySqlDataReader.HasRows)
                            {
                                while (mySqlDataReader.Read())
                                {

                                    /*  DataRow item_sampleDataRow;
                                      item_sampleDataRow = customer_var.datatable_Navdata_tosend.NewRow();


                                      item_sampleDataRow["accountno"] = mySqlDataReader["mc account no"].ToString();
                                      item_sampleDataRow["ContactNo"] = mySqlDataReader["mc Contact No"].ToString();
                                      //item_sampleDataRow["mc account no2"] = mySqlDataReader["mc account no2"].ToString();
                                      item_sampleDataRow["ClubCode"] = mySqlDataReader["mc Club Code"].ToString();
                                      item_sampleDataRow["SchemeCode"] = mySqlDataReader["mc Scheme Code"].ToString();
                                      item_sampleDataRow["ContactType"] = mySqlDataReader["mc Contact Type"].ToString();
                                      item_sampleDataRow["Name"] = mySqlDataReader["mc Name"].ToString();
                                      item_sampleDataRow["PhoneNo_"] = mySqlDataReader["mc Phone No_"].ToString();
                                      item_sampleDataRow["MobilePhoneNo_"] = mySqlDataReader["mc Mobile Phone No_"].ToString();
                                      item_sampleDataRow["Country"] = mySqlDataReader["mc Country"].ToString();
                                      item_sampleDataRow["Blocked"] = mySqlDataReader["mc Blocked"].ToString();
                                      item_sampleDataRow["BlockFromDate"] = mySqlDataReader["mc Block From Date"].ToString();
                                      // item_sampleDataRow["mc Block From Date"] = mySqlDataReader["SerialNo"].ToString();
                                      item_sampleDataRow["BlockToDate"] = mySqlDataReader["mc Block To Date"].ToString();




                                      customer_var.datatable_Navdata_tosend.Rows.Add(item_sampleDataRow);*/
                                    resultjson += @"('" + mySqlDataReader["mc account no"].ToString() + "','" + mySqlDataReader["mc Contact No"].ToString() + "','" + mySqlDataReader["mc Club Code"].ToString() + "','" + mySqlDataReader["mc Scheme Code"].ToString() + "','" + mySqlDataReader["mc Name"].ToString() + "','" + mySqlDataReader["mc Phone No_"].ToString() + "','" + mySqlDataReader["mc Mobile Phone No_"].ToString() + "','" + mySqlDataReader["mc Country"].ToString() + "','" + mySqlDataReader["mc Blocked"].ToString() + "','" + mySqlDataReader["mc Block From Date"].ToString() + "','" + mySqlDataReader["mc Block To Date"].ToString() + "','" + mySqlDataReader["mc Contact Type"].ToString() + "'),";


                                }
                                resultjson += "('','','','','','','','','','','','','','')";
                            }
                            else
                            {
                                return "false";
                            }
                        }












                    }
                }




            }
            catch (Exception ex)
            {
                Logger.Fatal(ReportError("getContactsDBAsync :  " + ex.Message.ToString() + " Nav API  "));
                return ex.Message.ToString();

            }



            // resultjson = DataTableToJSONWithStringBuilder(customer_var.datatable_Navdata_tosend);// if we need it json uncomment that 

            return resultjson;

        }
        /***********************************************************setting functions  *****************************************************/
        static string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
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
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public static string ReportError(string Message)
        {
            StackFrame CallStack = new StackFrame(1, true);
            return ("API : " + Message + ", File: " + CallStack.GetFileName() + ", Method Name " + CallStack.GetMethod().ToString() + ", Line: " + CallStack.GetFileLineNumber()).ToString();
        }

        // Retrieves a connection string by name.
        // Returns null if the name is not found.


        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        /***********************************************************End setting functions  *****************************************************/




        //return string of status code
        static async Task<string> CreateCustomerAsync(string ValuesJson, string url = "api/ErpBus/customers")
        {
            Console.WriteLine($" Create Customer from data: {ValuesJson}\t ");

            var byteArray = Encoding.ASCII.GetBytes("admin:1234");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

           HttpResponseMessage response = await client.PostAsJsonAsync(url, ValuesJson);
          
            // HttpResponseMessage response = await client.PostAsJsonAsync(String.Empty, ValuesJson);

            Console.WriteLine($" Create response.StatusCode: {response.StatusCode}\tHeader is : " +
               $"{ response.Headers}\t");
            response.EnsureSuccessStatusCode();



            // return URI of the created resource. // i donet neet it now
            return response.StatusCode.ToString();
        }

        //return string of status code
        static async Task<string> CreatePaymentAsync(string ValuesJson, string url = "api/ErpBus/payments")
        {
            Console.WriteLine($" Create payments from data: {ValuesJson}\t ");

            var byteArray = Encoding.ASCII.GetBytes("admin:1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpResponseMessage response = await client.PostAsJsonAsync(url, ValuesJson);

            Console.WriteLine($" CreatePaymentAsync response.StatusCode: {response.StatusCode}\tHeader is : " +
               $"{ response.Headers}\t");
            response.EnsureSuccessStatusCode();



            // return URI of the created resource. // i donet neet it now
            return response.StatusCode.ToString();
        }


        //return string of status code
        static async Task<string> CreateshippingagentAsync(string ValuesJson, string url = "api/ErpBus/shippingagent")
        {
            Console.WriteLine($" Create shippingagent from data: {ValuesJson}\t ");

            var byteArray = Encoding.ASCII.GetBytes("admin:1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.PostAsJsonAsync(url, ValuesJson);

            Console.WriteLine($" CreateshippingagentAsync response.StatusCode: {response.StatusCode}\tHeader is : " +
               $"{ response.Headers}\t");
            response.EnsureSuccessStatusCode();



            // return URI of the created resource. // i donet neet it now
            return response.StatusCode.ToString();
        }

        //return string of status code
        static async Task<string> CreateMembershipContactAsync(string ValuesJson, string url = "api/ErpBus/membershipcontact")
        {
            // Console.WriteLine($" Create MembershipContact from data: {ValuesJson}\t ");

            var byteArray = Encoding.ASCII.GetBytes("admin:1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.PostAsJsonAsync(url, ValuesJson);

            Console.WriteLine($" Creat MembershipContact response.status code IS \n\r\t: {response.StatusCode}\tHeader is : " +
               $"{ response.Headers}\t \n\r ***********************the response is************************** \n\r\t {response}\n\r0000000000000000000000000000000********************\n\r");
            response.EnsureSuccessStatusCode();



            // return URI of the created resource. // i donet neet it now
            return response.ToString();
        }



        static void Main()
        {
            // RunAsync().GetAwaiter().GetResult();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

           // NavCustomers_RunAsync().GetAwaiter().GetResult();
            RunAsync().GetAwaiter().GetResult();









            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Total RunTime " + elapsedTime);
        }

        static async Task NavCustomers_RunAsync()
        {

            client.BaseAddress = new Uri("http://localhost/drnutrition03082019/en/ae/");
           // client.BaseAddress = new Uri("https://www.drnutrition.com/en/ae/");//$this->post()[0]
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var byteArray = Encoding.ASCII.GetBytes("admin:1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            client.Timeout = TimeSpan.FromMinutes(1440);


            using (StreamWriter w = File.AppendText(logFileName))
            {
                Log($" Client time out is {client.Timeout.ToString()}", w);

            }

            Console.WriteLine($"Connection To drnutrition.com  With Data {client.BaseAddress} \t\r\n");

            try
            {
                Product product = new Product
                {
                    Name = "Gizmo",
                    Email = "Email@rmail.comn",

                };
/*
                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created Fininshed at {url}");
                */

               // product = await GetProductAsync();
               // ShowProduct(product);
                
                // Create a new Customer
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                //  string CustomersJson = Convert.ToString(getCustomersDBAsync());
                string CustomersJson = Convert.ToString(getCustomers_bulkDBAsync());

                var CreateStatus = await CreateCustomerAsync(CustomersJson, "api/ErpBus/customers");
                Console.WriteLine($"CreateCustomerAsync Fininshed with {CreateStatus}");

                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("Customer Total RunTime " + elapsedTime);


                // Create OTD Customers
                Stopwatch stopwatch1 = new Stopwatch();

                string OTDCustomersJson = Convert.ToString(GetCustomersBulk_OTDAsync());

                var OTDCustomer_CreateStatus = await CreateCustomerAsync(OTDCustomersJson, "api/ErpBus/customers");

                Console.WriteLine($"OTDCustomer_CreateStatus Fininshed with {OTDCustomer_CreateStatus}");

                stopwatch1.Stop();
                TimeSpan ts1 = stopwatch1.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts1.Hours, ts1.Minutes, ts1.Seconds,
                    ts1.Milliseconds / 10);
                Console.WriteLine("Customer Total RunTime " + elapsedTime1);




                //Create New Payment Methods 

                Stopwatch stopWatch2 = new Stopwatch();
                stopWatch2.Start();

                string PaymentMethodsJson = Convert.ToString(getPaymentDBAsync());

                var CreatePaymentMethodsJsonStatus = await CreatePaymentAsync(PaymentMethodsJson, "api/ErpBus/payments");
                Console.WriteLine($"CreatePaymentAsync Fininshed with {CreatePaymentMethodsJsonStatus}");


                stopWatch2.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts2 = stopWatch2.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts2.Hours, ts2.Minutes, ts2.Seconds,
                    ts2.Milliseconds / 10);
                Console.WriteLine("PaymentMethods Total RunTime " + elapsedTime2);

                //Create New Payment Methods OTD 

                Stopwatch OTDstopWatch2 = new Stopwatch();
                OTDstopWatch2.Start();

                string OTDPaymentMethodsJson = Convert.ToString(getOTDPaymentDBAsync());

                var OTDCreatePaymentMethodsJsonStatus = await CreatePaymentAsync(OTDPaymentMethodsJson, "api/ErpBus/payments");
                Console.WriteLine($"CreateOTD PAYMENT Async Fininshed with {OTDCreatePaymentMethodsJsonStatus}");


                stopWatch2.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan OTDts2 = OTDstopWatch2.Elapsed;

                // Format and display the TimeSpan value.
                string OTDelapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    OTDts2.Hours, OTDts2.Minutes, OTDts2.Seconds,
                    OTDts2.Milliseconds / 10);
                Console.WriteLine("OTD PaymentMethods Total RunTime " + OTDelapsedTime2);

                //Create new Shippment Agent

                Stopwatch stopWatch3 = new Stopwatch();
                stopWatch3.Start();
                string shippingagentJson = Convert.ToString(getshippingagentsDBAsync());

                var CreateshippingagentStatus = await CreateshippingagentAsync(shippingagentJson, "api/ErpBus/shippingagent");
                Console.WriteLine($"CreateshippingagentAsync Fininshed with {CreateshippingagentStatus}");

                stopWatch3.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts3 = stopWatch3.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime3 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts3.Hours, ts3.Minutes, ts3.Seconds,
                    ts3.Milliseconds / 10);
                Console.WriteLine("Shippingagent Total RunTime " + elapsedTime3);


                //Create new OTD Shippment Agent

                Stopwatch OTDstopWatch3 = new Stopwatch();
                OTDstopWatch3.Start();
                string OTDshippingagentJson = Convert.ToString(getOTDshippingagentsDBAsync());

                var OTDCreateshippingagentStatus = await CreateshippingagentAsync(OTDshippingagentJson, "api/ErpBus/shippingagent");
                Console.WriteLine($"OTD CreateshippingagentAsync Fininshed with {OTDCreateshippingagentStatus}");

                stopWatch3.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan OTDts3 = OTDstopWatch3.Elapsed;

                // Format and display the TimeSpan value.
                string OTDelapsedTime3 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    OTDts3.Hours, OTDts3.Minutes, OTDts3.Seconds,
                    OTDts3.Milliseconds / 10);
                Console.WriteLine("OTD Shippingagent Total RunTime " + OTDelapsedTime3);





                /*   //Create new  CreateMembership Contact CreateMembershipContactAsync

                   Stopwatch stopWatch4 = new Stopwatch();
                   stopWatch4.Start();
                   string MembershipContact = "";
                   int MembershipContact_offset = 0;
                   int MovingSteps = 1000;
                  // int MembershipContact_Cheunk = 1000;
                   var CreateMembershipContactStatus =string.Empty;

                   do
                   {
                       MembershipContact = "";
                       using (StreamWriter w = File.AppendText(logFileName))
                       {
                           Log($"Call getContactsDBAsync with Offset {MembershipContact_offset} and chunk is  {MovingSteps}", w);

                       }
                       MembershipContact = Convert.ToString(getContactsDBAsync(MembershipContact_offset, MovingSteps));

                        CreateMembershipContactStatus = await CreateMembershipContactAsync(MembershipContact, "api/ErpBus/membershipcontact");

                       Console.WriteLine($"CreateshippingagentAsync Offset is {MembershipContact_offset}  Chunks is {MovingSteps} Fininshed with {CreateMembershipContactStatus}");

                       MembershipContact_offset = MembershipContact_offset + MovingSteps;
                    //   MembershipContact_Cheunk =  MovingSteps;


                       // Console.WriteLine($"MembershipContact is \t {MembershipContact} ");
                       if (MembershipContact == "false") {
                           using (StreamWriter w = File.AppendText(logFileName))
                           {
                               Log($"  response is Call getContactsDBAsync  {MembershipContact}", w);

                           }
                       }
                     //  await Task.Delay(2000);


                   } while (MembershipContact != "false");




                   stopWatch4.Stop();
                   // Get the elapsed time as a TimeSpan value.
                   TimeSpan ts4 = stopWatch4.Elapsed;

                   // Format and display the TimeSpan value.
                   string elapsedTime4 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                       ts4.Hours, ts4.Minutes, ts4.Seconds,
                       ts4.Milliseconds / 10);
                   Console.WriteLine("Shippingagent Total RunTime " + elapsedTime4);*/



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Console.ReadLine();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
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
                Console.WriteLine($"Created at {url}");

                // Get the product
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





