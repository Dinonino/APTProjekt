using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data;

namespace TarProjekt
{
    class DbConnector
    {
        string connectionString = null;
        
        public DbConnector()
        {
            connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dino\Desktop\tarProjekt\tarProjekt\tarProjekt\TarDB.mdf;Integrated Security=True";
        }

  

        public void SaveToDB(Dictionary<string, Ocurrences> bigrams)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd;

                foreach (string word in bigrams.Keys)
                {
                    String cmdString = "IF EXISTS ( SELECT 1 FROM Words WHERE Word = @WORD) SELECT 1 ELSE SELECT 0";
                    cmd = new SqlCommand( cmdString, con);
                    cmd.Parameters.Add("@WORD", SqlDbType.NVarChar);
                    cmd.Parameters["@WORD"].Value = word;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 1)
                                cmd.CommandText = "UPDATE Words SET Occurrences = Occurrences + " + bigrams[word].GetNumberOfOccurrences()
                                + "where word = @WORD2";
                            else
                                cmd.CommandText = "INSERT INTO Words values( @WORD2 , " + bigrams[word].GetNumberOfOccurrences()
                                                + ")";
                        }                       
                    }
                    cmd.Parameters.Add("@WORD2", SqlDbType.NVarChar);
                    cmd.Parameters["@WORD2"].Value = word;
                    if(word.Length < 20)
                    {
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "SELECT 1 FROM Words WHERE Word = @WORD) SELECT 1 ELSE SELECT 0";



                        foreach (string follower in bigrams[word].GetFollowers().Keys)
                        {

                        }
                    }

                }
                con.Close();
            }


        }
    }
}


// SqlCommand myCommand = new SqlCommand("INSERT INTO Words " +
//  "Values ('string', 1)", myConnection);