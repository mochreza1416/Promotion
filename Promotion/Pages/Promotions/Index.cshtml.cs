using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Promotion.Pages.Promotions
{
    public class IndexModel : PageModel
    {
        public List<PromotionInfo> listPromotions = new List<PromotionInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-0RKH9KJ;Initial Catalog=promotionDB;Persist Security Info=True;User ID=sa;Password=141186";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM promotions";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PromotionInfo promotionInfo = new PromotionInfo();
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
                                listPromotions.Add(promotionInfo);
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
        }

        public class PromotionInfo
        {
            public string promo_id { get; set; }
            public string promo_type_id { get; set; }
            public string value_type_id { get; set; }
            public decimal amount { get; set; }
            public string item_id { get; set; }
            public string promo_code { get; set; }
            public string promo_description { get; set; }
            public DateTime startdate { get; set; }
            public DateTime enddate { get; set; }
            public DateTime created_at { get; set; }
        }
    }
}
