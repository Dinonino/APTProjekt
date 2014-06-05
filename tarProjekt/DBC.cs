using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    class DBC
    {
        DataContext db = new DataContext(@"C:\Users\Dino\Desktop\tarProjekt\tarProjekt\tarProjekt\TestDBTar.sdf");
 

        public void SaveToDB(Dictionary<string, Ocurrences> bigrams)
        {


            foreach (string word in bigrams.Keys)
            {
                Table<Words> Words = db.GetTable<Words>();
                if (word.Length <= 20)
                {
                    
                    int already_in = (from w in Words
                                      where w.word_string == word
                                      select w).Count();

                    if (already_in > 0)
                    {
                        var word_row = (from w in Words where w.word_string == word select w).First();
                        word_row.num_occurrences = word_row.num_occurrences + bigrams[word].GetNumberOfOccurrences();
                    }
                    else
                    {
                        
                        w w = new Word { Word_string = word, Occurrences_num = bigrams[word].GetNumberOfOccurrences() };
                        db.Words.InsertOnSubmit(w);
                    }
                }
  
            }
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
            
        }
    }
}
