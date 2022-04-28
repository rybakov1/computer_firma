using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace app_computer.Models
{
    public partial class mydbContext : DbContext
    {
        public mydbContext()
        {
        }

        public mydbContext(DbContextOptions<mydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Component> Components { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderComponent> OrderComponents { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Preset> Presets { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Storage> Storages { get; set; } = null!;
        public virtual DbSet<StorageComp> StorageComps { get; set; } = null!;
        public virtual DbSet<Typeofcomponent> Typeofcomponents { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VendorComponent> VendorComponents { get; set; } = null!;
        public virtual DbSet<WarranityTalon> WarranityTalons { get; set; } = null!;
        public virtual DbSet<МоделиПоКатегориям> МоделиПоКатегориямs { get; set; } = null!;
        public virtual DbSet<ФиИЗарплатуРаботниковПоПолу> ФиИЗарплатуРаботниковПоПолуs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=mydb;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasKey(e => e.IdComp)
                    .HasName("PRIMARY");

                entity.ToTable("components");

                entity.HasIndex(e => e.IdType, "Components_idtype_idx");

                entity.Property(e => e.IdComp).HasColumnName("id_comp");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .HasColumnName("company");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .HasColumnName("model");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.Specifications)
                    .HasMaxLength(50)
                    .HasColumnName("specifications");

                entity.Property(e => e.Warranty)
                    .HasColumnName("warranty")
                    .HasComment("month");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("Components_idtype");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PRIMARY");

                entity.ToTable("customers");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(45)
                    .HasColumnName("address");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(40)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(40)
                    .HasColumnName("lastname");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(40)
                    .HasColumnName("middlename");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(12)
                    .HasColumnName("mobile_number")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PRIMARY");

                entity.ToTable("employees");

                entity.HasIndex(e => e.IdPos, "Employees_idpos_idx");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.BirthdayDate).HasColumnName("birthday_date");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(40)
                    .HasColumnName("firstname");

                entity.Property(e => e.IdPos).HasColumnName("id_pos");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(40)
                    .HasColumnName("lastname");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(40)
                    .HasColumnName("middlename");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(12)
                    .HasColumnName("mobile_number")
                    .IsFixedLength();

                entity.Property(e => e.Passport)
                    .HasMaxLength(10)
                    .HasColumnName("passport")
                    .IsFixedLength();

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .HasColumnName("sex")
                    .IsFixedLength();

                entity.HasOne(d => d.IdPosNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdPos)
                    .HasConstraintName("Employees_idpos");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("orders");

                entity.HasIndex(e => e.IdCustomer, "Orders_idcustomer_idx");

                entity.HasIndex(e => e.IdEmp, "Orders_idemployees_idx");

                entity.HasIndex(e => e.IdTalon, "talon_idx");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.IdEmp).HasColumnName("id_emp");

                entity.Property(e => e.IdTalon).HasColumnName("id_talon");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.PaymentMark).HasColumnName("payment_mark");

                entity.Property(e => e.Prepayment).HasColumnName("prepayment");

                entity.Property(e => e.RealizationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("realization_date");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("orders_customer");

                entity.HasOne(d => d.IdEmpNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdEmp)
                    .HasConstraintName("orders_employees");

                entity.HasOne(d => d.IdTalonNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdTalon)
                    .HasConstraintName("orders_talon");

                entity.HasMany(d => d.IdServices)
                    .WithMany(p => p.IdOrders)
                    .UsingEntity<Dictionary<string, object>>(
                        "OrderService",
                        l => l.HasOne<Service>().WithMany().HasForeignKey("IdService").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Order_services_idserv"),
                        r => r.HasOne<Order>().WithMany().HasForeignKey("IdOrder").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("id_order"),
                        j =>
                        {
                            j.HasKey("IdOrder", "IdService").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("order_services");

                            j.HasIndex(new[] { "IdService" }, "Order_services_idserv_idx");

                            j.IndexerProperty<int>("IdOrder").ValueGeneratedOnAdd().HasColumnName("id_order");

                            j.IndexerProperty<int>("IdService").HasColumnName("id_service");
                        });
            });

            modelBuilder.Entity<OrderComponent>(entity =>
            {
                entity.HasKey(e => new { e.IdOrder, e.IdComp })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("order_components");

                entity.HasIndex(e => e.IdComp, "Order_components_idcomp_idx");

                entity.Property(e => e.IdOrder)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_order");

                entity.Property(e => e.IdComp).HasColumnName("id_comp");

                entity.Property(e => e.CompCount)
                    .HasColumnName("comp_count")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.IdCompNavigation)
                    .WithMany(p => p.OrderComponents)
                    .HasForeignKey(d => d.IdComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_components_idcomp");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderComponents)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_orderss");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPos)
                    .HasName("PRIMARY");

                entity.ToTable("position");

                entity.Property(e => e.IdPos).HasColumnName("id_pos");

                entity.Property(e => e.PostName)
                    .HasMaxLength(20)
                    .HasColumnName("post_name");

                entity.Property(e => e.Requirements)
                    .HasMaxLength(45)
                    .HasColumnName("requirements");

                entity.Property(e => e.Responsibilities)
                    .HasMaxLength(45)
                    .HasColumnName("responsibilities");

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<Preset>(entity =>
            {
                entity.HasKey(e => e.IdPreset)
                    .HasName("PRIMARY");

                entity.ToTable("presets");

                entity.Property(e => e.IdPreset)
                    .ValueGeneratedNever()
                    .HasColumnName("id_preset");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.HasMany(d => d.IdComps)
                    .WithMany(p => p.IdPresets)
                    .UsingEntity<Dictionary<string, object>>(
                        "PresestComponent",
                        l => l.HasOne<Component>().WithMany().HasForeignKey("IdComp").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("id_comp"),
                        r => r.HasOne<Preset>().WithMany().HasForeignKey("IdPreset").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("id_preset"),
                        j =>
                        {
                            j.HasKey("IdPreset", "IdComp").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("presest_components");

                            j.HasIndex(new[] { "IdComp" }, "id_comp_idx");

                            j.IndexerProperty<int>("IdPreset").HasColumnName("id_preset");

                            j.IndexerProperty<int>("IdComp").HasColumnName("id_comp");
                        });
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdService)
                    .HasName("PRIMARY");

                entity.ToTable("services");

                entity.Property(e => e.IdService).HasColumnName("id_service");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Title)
                    .HasMaxLength(40)
                    .HasColumnName("title");

                entity.HasMany(d => d.IdEmployees)
                    .WithMany(p => p.IdServices)
                    .UsingEntity<Dictionary<string, object>>(
                        "ServiceEmployee",
                        l => l.HasOne<Employee>().WithMany().HasForeignKey("IdEmployee").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("service_emloyee"),
                        r => r.HasOne<Service>().WithMany().HasForeignKey("IdService").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("service_service"),
                        j =>
                        {
                            j.HasKey("IdService", "IdEmployee").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("service_employee");

                            j.HasIndex(new[] { "IdEmployee" }, "id_emloyee_idx");

                            j.HasIndex(new[] { "IdService" }, "id_service_idx");

                            j.IndexerProperty<int>("IdService").HasColumnName("id_service");

                            j.IndexerProperty<int>("IdEmployee").HasColumnName("id_employee");
                        });
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.IdStorage)
                    .HasName("PRIMARY");

                entity.ToTable("storage");

                entity.Property(e => e.IdStorage)
                    .ValueGeneratedNever()
                    .HasColumnName("id_storage");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");
            });

            modelBuilder.Entity<StorageComp>(entity =>
            {
                entity.HasKey(e => new { e.IdStorage, e.IdComp })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("storage_comp");

                entity.HasIndex(e => e.IdComp, "comp__idx");

                entity.Property(e => e.IdStorage).HasColumnName("id_storage");

                entity.Property(e => e.IdComp).HasColumnName("id_comp");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.HasOne(d => d.IdCompNavigation)
                    .WithMany(p => p.StorageComps)
                    .HasForeignKey(d => d.IdComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comp_");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.StorageComps)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("storage_");
            });

            modelBuilder.Entity<Typeofcomponent>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PRIMARY");

                entity.ToTable("typeofcomponents");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.IdVendor)
                    .HasName("PRIMARY");

                entity.ToTable("vendor");

                entity.Property(e => e.IdVendor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_vendor");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<VendorComponent>(entity =>
            {
                entity.HasKey(e => new { e.IdVendor, e.IdComp, e.OrderTime, e.Count })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("vendor_components");

                entity.HasComment("						");

                entity.HasIndex(e => e.IdComp, "comp_idx");

                entity.Property(e => e.IdVendor).HasColumnName("id_vendor");

                entity.Property(e => e.IdComp).HasColumnName("id_comp");

                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_time");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.IdCompNavigation)
                    .WithMany(p => p.VendorComponents)
                    .HasForeignKey(d => d.IdComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comp");

                entity.HasOne(d => d.IdVendorNavigation)
                    .WithMany(p => p.VendorComponents)
                    .HasForeignKey(d => d.IdVendor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vendor_");
            });

            modelBuilder.Entity<WarranityTalon>(entity =>
            {
                entity.HasKey(e => e.IdTalon)
                    .HasName("PRIMARY");

                entity.ToTable("warranity_talon");

                entity.HasIndex(e => e.IdEmployee, "id_employee_idx");

                entity.Property(e => e.IdTalon)
                    .ValueGeneratedNever()
                    .HasColumnName("id_talon");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.WarranityTalons)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("talon_employee");
            });

            modelBuilder.Entity<МоделиПоКатегориям>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("модели по категориям");

                entity.Property(e => e.МодельВидеокарты)
                    .HasMaxLength(50)
                    .HasColumnName("Модель_видеокарты");

                entity.Property(e => e.Характеристики).HasMaxLength(50);
            });

            modelBuilder.Entity<ФиИЗарплатуРаботниковПоПолу>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("фи и зарплату работников по полу");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.ФамилияИмя)
                    .HasMaxLength(81)
                    .HasColumnName("Фамилия, имя");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
