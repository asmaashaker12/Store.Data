﻿using Microsoft.AspNetCore.Identity;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                  DisplayName="Asmaa Shaker",
                  Email="en.asmaashaker@gmailcom",
                  UserName="asmaaShaker",
                  Address=new Address
                  {
                      FirstName="Asmaa",
                      LastName="Shaker",
                      City="Bni Suef",
                      State="Cairo",
                      PostalCode="1111"
                  }
                };
                await userManager.CreateAsync(user,"123");
            }
        }
    }
}
