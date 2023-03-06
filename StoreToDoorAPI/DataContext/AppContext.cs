﻿using Microsoft.EntityFrameworkCore;

namespace StoreToDoorAPI.DataContext
{
    public class AppContext: DbContext
    {
        public AppContext() 
        { 
        }
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
