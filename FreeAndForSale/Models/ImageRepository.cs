using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace People_Search.Models
{
    public class ImageRepository
    {
        private static PeopleEntities1 dataContext = new PeopleEntities1();
        
        public static string updateImage(UserDetail ud)
        {
            try {
                UserDetail usr = dataContext.UserDetails.First(i => i.fname == ud.fname && i.address == ud.address);

                usr.pic = ud.pic;
              
                dataContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return "success";
        }
       

       
     
    }
}