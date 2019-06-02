using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PhoneApi.Model;


namespace PhoneApi.DAO
{
    public static class PhoneDao
    {
        private static readonly MySqlConnection cnx = new MySqlConnection();

        public static void OpenConnection() {
            cnx.ConnectionString = "Server=127.0.0.1;Uid=root;Pwd=root;Database=catalogue;";
            cnx.Open();
        }

        public static CatalogPhone GetAll() {
            OpenConnection();
            var phones = new CatalogPhone();
            var cmd = new MySqlCommand();
            //DB request
            cmd.CommandText = "SELECT * FROM phone";
            cmd.Connection = cnx;
            //execute and read request
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                var phone = new Phone();
                phone.ID = dr.GetString("id");
                phone.Name = dr.GetString("name");
                phone.Price = dr.GetFloat("price");
                if(dr["image"] != DBNull.Value)
                    phone.Image = dr.GetString("image");
                if (dr["description"] != DBNull.Value)
                    phone.Description = dr.GetString("description");
                phones.Phones.Add(phone);
            }
            CloseConnection();

            return phones;
        }

        public static Phone GetById(string id) {
            OpenConnection();
            var phone = new Phone();
            var cmd = new MySqlCommand();
            // DB request
            cmd.Connection = cnx;
            cmd.CommandText = "SELECT * FROM phone WHERE id=@pid";
            cmd.Parameters.AddWithValue("@pid",id);
            cmd.Prepare();
            // execute and read DB request
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                phone.ID = dr.GetString("id");
                phone.Name = dr.GetString("name");
                phone.Price = dr.GetFloat("price");
                if (dr["image"] != DBNull.Value)
                    phone.Image = dr.GetString("image");
                if (dr["description"] != DBNull.Value)
                    phone.Description = dr.GetString("description");
            }
            CloseConnection();
            return phone;
        }

        public static int Create(Phone phone) {
            OpenConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "INSERT INTO phone VALUES(@pid,@pname,@pprice,@pimage,@pdescription)";
            cmd.Parameters.AddWithValue("@pid", phone.ID);
            cmd.Parameters.AddWithValue("@pname", phone.Name);
            cmd.Parameters.AddWithValue("@pprice", phone.Price);
            cmd.Parameters.AddWithValue("@pimage", phone.Image);
            cmd.Parameters.AddWithValue("@pdescription", phone.Description);
            cmd.Prepare();
            var result = cmd.ExecuteNonQuery();
            CloseConnection();
            return result;
        }

        public static int Update(Phone phone) {
            OpenConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "UPDATE phone SET name=@pname,price=@pprice,image=@pimage,description=@pdescription WHERE id=@pid";
            cmd.Parameters.AddWithValue("@pid", phone.ID);
            cmd.Parameters.AddWithValue("@pname", phone.Name);
            cmd.Parameters.AddWithValue("@pprice", phone.Price);
            cmd.Parameters.AddWithValue("@pimage", phone.Image);
            cmd.Parameters.AddWithValue("@pdescription", phone.Description);
            cmd.Prepare();
            var result = cmd.ExecuteNonQuery();
            CloseConnection();
            return result;
        }

        public static int Delete(string id) {
            OpenConnection();
            var cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "DELETE FROM phone  WHERE id=@pid";
            cmd.Parameters.AddWithValue("@pid", id);
            cmd.Prepare();
            var result = cmd.ExecuteNonQuery();
            CloseConnection();
            return result;
        }

        private static void CloseConnection() {
            cnx.Close();
        }
    }
}
