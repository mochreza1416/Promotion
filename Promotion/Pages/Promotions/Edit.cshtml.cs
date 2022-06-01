using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Promotion.Pages.Promotions.CreateModel;
using static Promotion.Pages.Promotions.IndexModel;

namespace Promotion.Pages.Promotions
{
    public class EditModel : PageModel
    {
        public PromotionInfo promotionInfo = new PromotionInfo();
        public List<StoreInfo> listStores = new List<StoreInfo>();
        public String errorMessage = string.Empty;
        public String successMessage = string.Empty;
        public void OnGet()
        {
            String id = Request.Query["id"];
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
            try
            {
                String connectionString = "Data Source=DESKTOP-0RKH9KJ;Initial Catalog=promotionDB;Persist Security Info=True;User ID=sa;Password=141186";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM promotions WHERE promo_id = @promo_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@promo_id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {   
                                promotionInfo.promo_id = reader.GetString(0);
                                promotionInfo.promo_type_id = reader.GetString(1);
                                promotionInfo.value_type_id = reader.GetString(2);
                                promotionInfo.amount = reader.GetDecimal(3);
                                promotionInfo.item_id = reader.GetString(4);
                                promotionInfo.promo_code = reader.GetString(5);
                                promotionInfo.promo_description = reader.GetString(6);
                                promotionInfo.startdate = reader.GetDateTime(7);
                                promotionInfo.enddate = reader.GetDateTime(8);
                                promotionInfo.created_at = reader.GetDateTime(9);
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
            promotionInfo.promo_id = Request.Form["promo_id"] == DBNull.Value ? "" : Request.Form["promo_id"];
            promotionInfo.promo_type_id = Request.Form["promo_type_id"] == DBNull.Value ? "" : Request.Form["promo_type_id"];
            promotionInfo.value_type_id = Request.Form["value_type_id"] == DBNull.Value ? "" : Request.Form["value_type_id"];
            promotionInfo.amount = Request.Form["amount"] == DBNull.Value ? 0 : Decimal.Parse(Request.Form["amount"]);
            promotionInfo.item_id = "IT40";
            promotionInfo.promo_code = "PR50";
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
                    String sql = "UPDATE promotions " +
                                 "SET promo_id=@promo_id,promo_type_id=@promo_type_id,value_type_id=@value_type_id,amount=@amount,item_id=@item_id,promo_code=@promo_code,promo_description=@promo_description,startdate=@startdate,enddate=@enddate " +
                                 "WHERE promo_id=@promo_id;";
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

            successMessage = "Promotion Edited Correctly";
            Response.Redirect("/Promotions/Index");
        }
    }
}
