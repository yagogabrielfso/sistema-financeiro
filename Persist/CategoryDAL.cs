using System;
using System.Data.SqlClient;
using CategoryModel = Model.Category;

namespace Persist
{
    public class CategoryDAL
    {
        private SqlConnection conn;

        public CategoryDAL(SqlConnection conn)
        {
            this.conn = conn;
        }

        public CategoryModel GetCategory(int id)
        {
            CategoryModel category = new CategoryModel();
            var command =
                new SqlCommand(
                    "select id, nome from categorias where id = @id",
                    this.conn);
            command.Parameters.AddWithValue("@id", id);
            this.conn.Open();

            using (SqlDataReader rd = command.ExecuteReader())
            {
                rd.Read();
                category.Id = Convert.ToInt32(rd["id"].ToString());
                category.Name = rd["nome"].ToString();
            }
            this.conn.Close();

            return category;

        }
    }
}
