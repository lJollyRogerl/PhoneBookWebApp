﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneBook
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext()
            : base("name=PhoneBookDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Citizen> tblCitizens { get; set; }
    
        public virtual ObjectResult<Citizen> spGetContacts(Nullable<int> pageNumber)
        {
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Citizen>("spGetContacts", pageNumberParameter);
        }
    
        public virtual ObjectResult<Citizen> spGetContacts(Nullable<int> pageNumber, MergeOption mergeOption)
        {
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Citizen>("spGetContacts", mergeOption, pageNumberParameter);
        }
    }
}
