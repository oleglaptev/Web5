﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web5.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WEBEntities : DbContext
    {
        public WEBEntities()
            : base("name=WEBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Пользователь> Пользователь { get; set; }
        public virtual DbSet<Роль_пользователя> Роль_пользователя { get; set; }
        public virtual DbSet<Студент> Студент { get; set; }
        public virtual DbSet<Группы> Группы { get; set; }
        public virtual DbSet<Студенты_в_группах> Студенты_в_группах { get; set; }
    }
}
