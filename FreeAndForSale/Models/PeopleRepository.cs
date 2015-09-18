using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace People_Search.Models
{
    public class PeopleRepository
    {





        private static PeopleEntities1 dataContext = new PeopleEntities1();

        

        public static string InsertUser(UserDetail e)
        {
            dataContext.UserDetails.Add(e);
            try {
                dataContext.SaveChanges();
            }







            catch(Exception e1)
            {
                Console.WriteLine(e1);
            }
            return "success";
        }

       

        public List<UserDetail> SearchPeople(string keyword)
        {
            var query = from u in dataContext.UserDetails
                        where u.fname.Contains(keyword) || u.lname.Contains(keyword)
                        select u;
            return query.ToList();
        }

       
    }
}