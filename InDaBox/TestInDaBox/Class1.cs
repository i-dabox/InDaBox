using InDaBox.Models;
using InDaBox.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace TestInDaBox
{
    public class Class1
    {
        
        private readonly ApplicationDbContext _context;

        public Class1(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
