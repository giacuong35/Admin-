using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models;

public partial class SportPlusDbContext : DbContext
{
    public SportPlusDbContext()
    {
    }

    public SportPlusDbContext(DbContextOptions<SportPlusDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<BookingLog> BookingLogs { get; set; }

    public virtual DbSet<BookingService> BookingServices { get; set; }

    public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<DepositStatus> DepositStatuses { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<FieldMaintenanceLog> FieldMaintenanceLogs { get; set; }

    public virtual DbSet<FieldPriceHistory> FieldPriceHistories { get; set; }

    public virtual DbSet<FieldSlot> FieldSlots { get; set; }

    public virtual DbSet<FieldSlotStatus> FieldSlotStatuses { get; set; }

    public virtual DbSet<FieldStatus> FieldStatuses { get; set; }

    public virtual DbSet<FieldType> FieldTypes { get; set; }

    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<IncidentStatus> IncidentStatuses { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<PeakSchedule> PeakSchedules { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<PromotionType> PromotionTypes { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<SpecialDay> SpecialDays { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<VwBookingHistory> VwBookingHistories { get; set; }

    public virtual DbSet<VwDashboardSummary> VwDashboardSummaries { get; set; }

    public virtual DbSet<VwFieldOccupancyByMonth> VwFieldOccupancyByMonths { get; set; }

    public virtual DbSet<VwFieldRating> VwFieldRatings { get; set; }

    public virtual DbSet<VwFieldSchedule> VwFieldSchedules { get; set; }

    public virtual DbSet<VwLowStockProduct> VwLowStockProducts { get; set; }

    public virtual DbSet<VwPendingDeposit> VwPendingDeposits { get; set; }

    public virtual DbSet<VwRevenueByMonth> VwRevenueByMonths { get; set; }

    public virtual DbSet<VwRevenueByService> VwRevenueByServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED603602F5");

            entity.ToTable(tb => tb.HasTrigger("trg_Bookings_StatusLog"));

            entity.HasIndex(e => new { e.StatusId, e.CreatedAt }, "IX_Bookings_StatusCreated");

            entity.HasIndex(e => e.UserId, "IX_Bookings_UserId");

            entity.Property(e => e.CancelReason).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DepositAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.RescheduleCount).HasDefaultValue(0);
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.SubTotal).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TaxAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK_Bookings_Promotion");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Status__1AD3FDA4");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__UserId__19DFD96B");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailId).HasName("PK__BookingD__8136D45AEA34A1AA");

            entity.HasIndex(e => e.BookingId, "IX_BookingDetails_Booking");

            entity.HasIndex(e => e.FieldSlotId, "UQ_BookingDetail_Slot").IsUnique();

            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingDe__Booki__1F98B2C1");

            entity.HasOne(d => d.FieldSlot).WithOne(p => p.BookingDetail)
                .HasForeignKey<BookingDetail>(d => d.FieldSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingDe__Field__208CD6FA");
        });

        modelBuilder.Entity<BookingLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__BookingL__5E548648995E5BCB");

            entity.HasIndex(e => new { e.BookingId, e.ChangedAt }, "IX_BookingLogs_Booking");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Note).HasMaxLength(500);

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingLogs)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingLo__Booki__37703C52");

            entity.HasOne(d => d.ChangedByUser).WithMany(p => p.BookingLogs)
                .HasForeignKey(d => d.ChangedByUserId)
                .HasConstraintName("FK__BookingLo__Chang__3A4CA8FD");

            entity.HasOne(d => d.NewStatus).WithMany(p => p.BookingLogNewStatuses)
                .HasForeignKey(d => d.NewStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingLo__NewSt__395884C4");

            entity.HasOne(d => d.OldStatus).WithMany(p => p.BookingLogOldStatuses)
                .HasForeignKey(d => d.OldStatusId)
                .HasConstraintName("FK__BookingLo__OldSt__3864608B");
        });

        modelBuilder.Entity<BookingService>(entity =>
        {
            entity.HasKey(e => e.BookingServiceId).HasName("PK__BookingS__43F55CB171114F4E");

            entity.HasIndex(e => e.BookingId, "IX_BookingServices_Booking");

            entity.HasIndex(e => new { e.BookingId, e.ServiceId }, "UQ_BookingService").IsUnique();

            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingServices)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingSe__Booki__47A6A41B");

            entity.HasOne(d => d.Service).WithMany(p => p.BookingServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingSe__Servi__489AC854");
        });

        modelBuilder.Entity<BookingStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__BookingS__C8EE20638E59C273");

            entity.HasIndex(e => e.Name, "UQ__BookingS__737584F66ABECD1D").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("PK__Deposits__AB60DF71362C0268");

            entity.HasIndex(e => e.DeadlineAt, "IX_Deposits_Deadline").HasFilter("([StatusId]=(1))");

            entity.HasIndex(e => new { e.StatusId, e.DeadlineAt }, "IX_Deposits_Status");

            entity.HasIndex(e => e.BookingId, "UQ__Deposits__73951AEC3042657E").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.PaidAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.RequiredAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Booking).WithOne(p => p.Deposit)
                .HasForeignKey<Deposit>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Deposits__Bookin__31B762FC");

            entity.HasOne(d => d.Payment).WithMany(p => p.Deposits)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Deposits__Paymen__339FAB6E");

            entity.HasOne(d => d.Status).WithMany(p => p.Deposits)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Deposits__Status__32AB8735");
        });

        modelBuilder.Entity<DepositStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__DepositS__C8EE2063D474751B");

            entity.HasIndex(e => e.Name, "UQ__DepositS__737584F6ED7C4677").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK__Fields__C8B6FF076FE006DC");

            entity.ToTable(tb => tb.HasTrigger("trg_Fields_PriceHistory"));

            entity.HasIndex(e => new { e.TypeId, e.StatusId }, "IX_Fields_TypeStatus").HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PeakPrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Status).WithMany(p => p.Fields)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fields__StatusId__656C112C");

            entity.HasOne(d => d.Type).WithMany(p => p.Fields)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fields__TypeId__6477ECF3");
        });

        modelBuilder.Entity<FieldMaintenanceLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__FieldMai__5E54864801C3D303");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Reason).HasMaxLength(500);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FieldMaintenanceLogs)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FieldMain__Creat__7E37BEF6");

            entity.HasOne(d => d.Field).WithMany(p => p.FieldMaintenanceLogs)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FieldMain__Field__7D439ABD");
        });

        modelBuilder.Entity<FieldPriceHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__FieldPri__4D7B4ABDFDF3D8DE");

            entity.ToTable("FieldPriceHistory");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NewBasePrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.NewPeakPrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.OldBasePrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.OldPeakPrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Reason).HasMaxLength(255);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.FieldPriceHistories)
                .HasForeignKey(d => d.ChangedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FieldPric__Chang__6A30C649");

            entity.HasOne(d => d.Field).WithMany(p => p.FieldPriceHistories)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FieldPric__Field__693CA210");
        });

        modelBuilder.Entity<FieldSlot>(entity =>
        {
            entity.HasKey(e => e.FieldSlotId).HasName("PK__FieldSlo__38F0B09461933DE9");

            entity.HasIndex(e => e.HoldExpireAt, "IX_FieldSlots_HoldExpire").HasFilter("([StatusId]=(2))");

            entity.HasIndex(e => new { e.FieldId, e.SlotDate, e.StatusId }, "IX_FieldSlots_Search");

            entity.HasIndex(e => new { e.FieldId, e.SlotId, e.SlotDate }, "UQ_FieldSlot").IsUnique();

            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Field).WithMany(p => p.FieldSlots)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FieldSlot__Field__76969D2E");

            entity.HasOne(d => d.Slot).WithMany(p => p.FieldSlots)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FieldSlot__SlotI__778AC167");

            entity.HasOne(d => d.Status).WithMany(p => p.FieldSlots)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FieldSlot__Statu__787EE5A0");
        });

        modelBuilder.Entity<FieldSlotStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__FieldSlo__C8EE20630A66E13F");

            entity.HasIndex(e => e.Name, "UQ__FieldSlo__737584F6DB684BDA").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<FieldStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__FieldSta__C8EE2063F5B7AC5A");

            entity.HasIndex(e => e.Name, "UQ__FieldSta__737584F63EB7B7D5").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<FieldType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__FieldTyp__516F03B54BBCDAF8");

            entity.HasIndex(e => e.Name, "UQ__FieldTyp__737584F6B46850C2").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Incident>(entity =>
        {
            entity.HasKey(e => e.IncidentId).HasName("PK__Incident__3D8053B2A6DB5099");

            entity.HasIndex(e => new { e.FieldId, e.StatusId }, "IX_Incidents_Field");

            entity.HasIndex(e => new { e.StatusId, e.CreatedAt }, "IX_Incidents_Status");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.HandledNote).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Field).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Incidents__Field__72910220");

            entity.HasOne(d => d.HandledByUser).WithMany(p => p.IncidentHandledByUsers)
                .HasForeignKey(d => d.HandledByUserId)
                .HasConstraintName("FK__Incidents__Handl__74794A92");

            entity.HasOne(d => d.ReportedByUser).WithMany(p => p.IncidentReportedByUsers)
                .HasForeignKey(d => d.ReportedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Incidents__Repor__73852659");

            entity.HasOne(d => d.Status).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Incidents__Statu__756D6ECB");
        });

        modelBuilder.Entity<IncidentStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Incident__C8EE20635E7446A4");

            entity.HasIndex(e => e.Name, "UQ__Incident__737584F64FE43343").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E122DA84D33");

            entity.HasIndex(e => new { e.UserId, e.IsRead, e.CreatedAt }, "IX_Notifications_Unread").IsDescending(false, false, true);

            entity.Property(e => e.Body).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__03BB8E22");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A389B00323A");

            entity.HasIndex(e => e.BookingId, "IX_Payments_BookingId");

            entity.HasIndex(e => e.TransactionCode, "IX_Payments_TxCode").HasFilter("([TransactionCode] IS NOT NULL)");

            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.TransactionCode).HasMaxLength(100);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Bookin__2645B050");

            entity.HasOne(d => d.Method).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Method__282DF8C2");

            entity.HasOne(d => d.Status).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Status__2739D489");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__PaymentM__FC681851C03AC92E");

            entity.HasIndex(e => e.Name, "UQ__PaymentM__737584F6D9390B09").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__PaymentS__C8EE2063B4658AD6");

            entity.HasIndex(e => e.Name, "UQ__PaymentS__737584F6E5824677").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PeakSchedule>(entity =>
        {
            entity.HasKey(e => e.PeakScheduleId).HasName("PK__PeakSche__1100797520CC1D32");

            entity.HasIndex(e => new { e.DayOfWeek, e.SlotId }, "UQ_PeakSchedule").IsUnique();

            entity.Property(e => e.IsPeak).HasDefaultValue(true);

            entity.HasOne(d => d.Slot).WithMany(p => p.PeakSchedules)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PeakSched__SlotI__0B91BA14");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CDF2D72535");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MinQty).HasDefaultValue(5);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StockQty).HasDefaultValue(0);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profiles__290C88E429495699");

            entity.HasIndex(e => e.UserId, "UQ__Profiles__1788CC4D85B5F668").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AvatarUrl).HasMaxLength(500);

            entity.HasOne(d => d.User).WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profiles__UserId__5629CD9C");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42FCF98EBFE01");

            entity.HasIndex(e => new { e.IsActive, e.StartDate, e.EndDate }, "IX_Promotions_Active");

            entity.HasIndex(e => e.Code, "IX_Promotions_Code").HasFilter("([IsActive]=(1))");

            entity.HasIndex(e => e.Code, "UQ_Promotions_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxDiscount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.MinOrderAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UsageCount).HasDefaultValue(0);
            entity.Property(e => e.UsageLimit).HasDefaultValue(1);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__Creat__55009F39");

            entity.HasOne(d => d.Type).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__TypeI__540C7B00");
        });

        modelBuilder.Entity<PromotionType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Promotio__516F03B55513D4EA");

            entity.HasIndex(e => e.Name, "UQ__Promotio__737584F609D9248E").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK__Purchase__036BACA41E61ECD9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Creat__662B2B3B");

            entity.HasOne(d => d.Status).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Statu__671F4F74");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Suppl__65370702");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderDetailId).HasName("PK__Purchase__5026B6986D7A74FA");

            entity.HasIndex(e => new { e.PurchaseOrderId, e.ProductId }, "UQ_POD_Product").IsUnique();

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Produ__6DCC4D03");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Purch__6CD828CA");
        });

        modelBuilder.Entity<PurchaseOrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Purchase__C8EE2063239CB501");

            entity.HasIndex(e => e.Name, "UQ__Purchase__737584F69DFEFF95").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__RefreshT__658FEEEA052F78C3");

            entity.HasIndex(e => e.Token, "IX_RefreshTokens_Token");

            entity.HasIndex(e => new { e.UserId, e.IsRevoked }, "IX_RefreshTokens_User");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RefreshTo__UserI__5AEE82B9");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CE0A50A95B");

            entity.HasIndex(e => new { e.FieldId, e.IsVisible }, "IX_Reviews_Field");

            entity.HasIndex(e => e.BookingId, "UQ__Reviews__73951AECC58E72AA").IsUnique();

            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.IsVisible).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Booking).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Booking__7D0E9093");

            entity.HasOne(d => d.Field).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__FieldId__7EF6D905");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__UserId__7E02B4CC");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AD3BC7A37");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F662DA8277").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB00A8F9B08D1");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<SpecialDay>(entity =>
        {
            entity.HasKey(e => e.SpecialDayId).HasName("PK__SpecialD__58DACBCECA0E28C6");

            entity.HasIndex(e => e.SpecialDate, "UQ_SpecialDays_Date").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsFullDayPeak).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.PriceMultiplier)
                .HasDefaultValue(10m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SpecialDays)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SpecialDa__Creat__05D8E0BE");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666B4684FD33C");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.HasKey(e => e.ConfigKey).HasName("PK__SystemCo__4A306785272F1693");

            entity.ToTable("SystemConfig");

            entity.Property(e => e.ConfigKey).HasMaxLength(100);
            entity.Property(e => e.ConfigValue).HasMaxLength(500);
            entity.Property(e => e.DataType)
                .HasMaxLength(20)
                .HasDefaultValue("STRING");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.SystemConfigs)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_SystemConfig_UpdatedBy");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__TimeSlot__0A124AAF2D145574");

            entity.HasIndex(e => new { e.StartTime, e.EndTime }, "UQ_TimeSlots").IsUnique();

            entity.Property(e => e.IsPeakHour).HasDefaultValue(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C74D6A066");

            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId");

            entity.HasIndex(e => e.StatusId, "IX_Users_StatusId");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ_Users_Phone").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__5070F446");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__StatusId__5165187F");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__UserStat__C8EE20637C37373C");

            entity.HasIndex(e => e.Name, "UQ__UserStat__737584F6B019FAAA").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<VwBookingHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BookingHistory");

            entity.Property(e => e.BookingStatus).HasMaxLength(50);
            entity.Property(e => e.CancelReason).HasMaxLength(500);
            entity.Property(e => e.CustomerEmail).HasMaxLength(100);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.CustomerPhone).HasMaxLength(20);
            entity.Property(e => e.DepositAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.DepositPaid).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.DepositRequired).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.DepositStatus).HasMaxLength(50);
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.FieldType).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.PromotionCode).HasMaxLength(50);
            entity.Property(e => e.SlotPrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<VwDashboardSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DashboardSummary");

            entity.Property(e => e.TodayRevenue).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwFieldOccupancyByMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FieldOccupancyByMonth");

            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.FieldType).HasMaxLength(50);
            entity.Property(e => e.OccupancyRate).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<VwFieldRating>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FieldRatings");

            entity.Property(e => e.AvgRating).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.FieldType).HasMaxLength(50);
        });

        modelBuilder.Entity<VwFieldSchedule>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FieldSchedule");

            entity.Property(e => e.FieldImageUrl).HasMaxLength(500);
            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.FieldType).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.SlotStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<VwLowStockProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_LowStockProducts");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<VwPendingDeposit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PendingDeposits");

            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.CustomerPhone).HasMaxLength(20);
            entity.Property(e => e.DepositPaid).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.DepositRequired).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");
        });

        modelBuilder.Entity<VwRevenueByMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_RevenueByMonth");

            entity.Property(e => e.AvgBookingValue).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.TotalRevenue).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwRevenueByService>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_RevenueByService");

            entity.Property(e => e.ServiceName).HasMaxLength(100);
            entity.Property(e => e.TotalRevenue).HasColumnType("decimal(38, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
