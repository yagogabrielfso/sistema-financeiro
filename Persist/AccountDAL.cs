using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using Db;
using Model;


namespace Persist
{
    public class AccountDAL
    {
        private SqlConnection conn;
        private CategoryDAL category;

        public AccountDAL(SqlConnection conn)
        {
            this.conn = conn;
            string strConn = Db.Connection.GetStringConnection();
            this.category = new CategoryDAL(new SqlConnection(strConn));
        }

        public List<Account> List()
        {
            List<Account> accounts = new List<Account>();

            var command = new SqlCommand("select con.id, con.descricao, con.valor, con.tipo, con.data_vencimento, cat.nome, cat.id as categoria_id from contas con inner join categorias cat on con.categoria_id = cat.id  ", this.conn);
            this.conn.Open();

            using (SqlDataReader rd = command.ExecuteReader())
            {
                while (rd.Read())
                {
                    Account account = new Account()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Description = rd["descricao"].ToString(),
                        Type = Convert.ToChar(rd["tipo"].ToString()),
                        Value = Convert.ToDouble(rd["valor"].ToString())

                    };

                    int id_categoria = Convert.ToInt32(rd["id"].ToString());
                    account.Category = this.category.GetCategory(id_categoria);
                    accounts.Add(account);
                }

            }

            return accounts;
        }


        public void Save(Account account)
        {
            if (account.Id == null)
            {
                Register(account);
            }
            else
            {
                Edit(account);
            }
        }

        void Register(Account account)
        {
            this.conn.Open();
             SqlCommand command = this.conn.CreateCommand();
             command.CommandText = "insert into contas (descricao, tipo, valor, data_vencimento, categoria_id values (@description, @type, @value, @due_date, @category_id)";
             command.Parameters.AddWithValue("@description", account.Description);
             command.Parameters.AddWithValue("@type", account.Type);
             command.Parameters.AddWithValue("@value", account.Value);
             command.Parameters.AddWithValue("@due_date", account.DueDate);
             command.Parameters.AddWithValue("@category_id", account.Category.Id);
             command.ExecuteNonQuery();
            this.conn.Close();

        }

        void Edit(Account account)
        {

        }
    }
}
