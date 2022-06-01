using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Promotion.Pages.Promotions.IndexModel;

namespace Promotion.Pages.Promotions
{
    public class CreateModel : PageModel
    {
        public PromotionInfo promotionInfo = new PromotionInfo();
        public List<StoreInfo> listStores = new List<StoreInfo>();
        public String errorMessage = string.Empty;
        public String successMessage = string.Empty;

        public class StoreInfo
        {
            public string store_id { get; set; }
            public string store_name { get; set; }
            public DateTime created_at { get; set; }
        }
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-0RKH9KJ;Initial Catalog=promotionDB;Persist Security Info=True;User ID=sa;Password=141186";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stores";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StoreInfo storeInfo = new StoreInfo();
                                storeInfo.store_id = reader.GetString(0);
                                storeInfo.store_name = reader.GetString(1);
                                listStores.Add(storeInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
        }
            
        public void OnPost()
        {
            string guid = Guid.NewGuid().ToString();
            string promoId = "P" + DateTime.Now.ToString("yyyymmdd")+"-"+ guid;
            promotionInfo.promo_id = promoId;
            promotionInfo.promo_type_id = Request.Form["promo_type_id"] == DBNull.Value ? "" : Request.Form["promo_type_id"];
            promotionInfo.value_type_id = Request.Form["value_type_id"] == DBNull.Value ? "" : Request.Form["value_type_id"];
            promotionInfo.amount = Request.Form["amount"] == DBNull.Value ? 0 : Decimal.Parse(Request.Form["amount"]);
            promotionInfo.item_id = "IT15";
            promotionInfo.promo_code = "PR21";
            promotionInfo.promo_description = Request.Form["promo_description"] == DBNull.Value ? "" : Request.Form["promo_description"];
            promotionInfo.startdate = Request.Form["startdate"] == DBNull.Value ? default(DateTime) : DateTime.Parse(Request.Form["startdate"]);
            promotionInfo.enddate = Request.Form["enddate"] == DBNull.Value ? default(DateTime) : DateTime.Parse(Request.Form["enddate"]);

            if (promotionInfo.promo_description.Length == 0)
            {
                errorMessage = "promo_description is required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-0RKH9KJ;Initial Catalog=promotionDB;Persist Security Info=True;User ID=sa;Password=141186";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO promotions (promo_id,promo_type_id,value_type_id,amount,item_id,promo_code,promo_description,startdate,enddate) "+
                                " VALUES (@promo_id,@promo_type_id,@value_type_id,@amount,@item_id,@promo_code,@promo_description,@startdate,@enddate);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@promo_id", promotionInfo.promo_id);
                        command.Parameters.AddWithValue("@promo_type_id", promotionInfo.promo_type_id);
                        command.Parameters.AddWithValue("@value_type_id", promotionInfo.value_type_id);
                        command.Parameters.AddWithValue("@amount", promotionInfo.amount);
                        command.Parameters.AddWithValue("@item_id", promotionInfo.item_id);
                        command.Parameters.AddWithValue("@promo_code", promotionInfo.promo_code);
                        command.Parameters.AddWithValue("@promo_description", promotionInfo.promo_description);
                        command.Parameters.AddWithValue("@startdate", promotionInfo.startdate);
                        command.Parameters.AddWithValue("@enddate", promotionInfo.enddate);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            promotionInfo.promo_type_id = "";
            promotionInfo.value_type_id = "";
            promotionInfo.amount = 0;
            promotionInfo.item_id = "";
            promotionInfo.promo_code = "";
            promotionInfo.promo_description = "";
            promotionInfo.startdate = default(DateTime);
            promotionInfo.enddate = default(DateTime);

            successMessage = "New Promotions Added Correctly";
            Response.Redirect("/Promotions/Index");
        }
    }
}
