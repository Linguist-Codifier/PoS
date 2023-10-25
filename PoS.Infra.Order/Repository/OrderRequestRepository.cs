using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.Order.Domain.Interfaces.Repository;
using PoS.Order.Domain.Filters.Attributes;
using PoS.Order.Domain.Models;

namespace PoS.Infra.Order.Repository
{
    /// <summary>
    /// Exposes the models implementations specified by <see cref="IOrderRequestRepository"/> interface.
    /// </summary>
    [DbContext(contextType: typeof(OrderRequestRepository))]
    public class OrderRequestRepository : DbContext, IOrderRequestRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OrderRequestRepository"/>.
        /// </summary>
        /// <param name="contextConfigs">
        /// The options to be used by a <see cref="DbContext" />. You normally override
        /// <see cref="DbContext.OnConfiguring(DbContextOptionsBuilder)" /> or use a <see cref="DbContextOptionsBuilder" />
        /// to create instances of this class and it is not designed to be directly constructed in your application code.
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-dbcontext-options">Using DbContextOptions</see> for more information and examples.
        /// </remarks>
        /// </param>
        public OrderRequestRepository([NotNull] DbContextOptions contextConfigs) : base(contextConfigs) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder.ApplyConfiguration<DefaultOrderRequest>(new OrderRequestEntity()));
        }

        private readonly struct OrderRequestEntity : IEntityTypeConfiguration<DefaultOrderRequest>
        {
            public OrderRequestEntity() { }

            public void Configure(EntityTypeBuilder<DefaultOrderRequest> entityTypeBuilder)
            {
                entityTypeBuilder.HasKey(column => column.Id).HasName("id");
                entityTypeBuilder.Property(column => column.Category).IsRequired().HasColumnType("int").HasColumnName("category");
                entityTypeBuilder.Property(column => column.Inquirer).IsRequired().HasColumnType("varchar(100)").HasColumnName("inquirer");
                entityTypeBuilder.Property(column => column.Instructions).IsRequired().HasColumnType("varchar(500)").HasColumnName("instructions");
            }
        }

        /// <inheritdoc/>
        [TableMapping(name: nameof(DefaultOrderRequest))]
        public DbSet<DefaultOrderRequest> Orders { get; set; }
    }
}