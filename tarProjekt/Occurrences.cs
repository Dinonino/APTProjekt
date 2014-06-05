using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    public class Ocurrences
    {
        int numberOfOccurrences = 0;
        Dictionary<string, int> followers = new Dictionary<string, int>();
        public Ocurrences()
        {
            numberOfOccurrences = 1;
        }

        public void AddFollower(string follower)
        {
            if (followers.ContainsKey(follower))
                followers[follower]++;
            else
                followers[follower] = 1;
        }

        public void increaseCount()
        {
            numberOfOccurrences++;
        }

        public int GetNumberOfOccurrences()
        {
            return numberOfOccurrences;
        }

        public Dictionary<string, int> GetFollowers()
        {
            return followers;
        }
    }
}
