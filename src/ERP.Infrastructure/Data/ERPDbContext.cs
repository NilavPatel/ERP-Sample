using System.Linq.Expressions;
using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Departments;
using ERP.Domain.Modules.Designations;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Modules.Leaves;
using ERP.Domain.Modules.Roles;
using ERP.Domain.Modules.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ERP.Infrastructure.Data
{
    public class ERPDbContext : DbContext
    {
        public ERPDbContext(DbContextOptions<ERPDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Expression<Func<BaseAuditableEntity, bool>> filterExpr = x => !x.IsDeleted;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseEntity
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(BaseAuditableEntity)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePersonalDetail> EmployeePersonalDetails { get; set; }
        public DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public DbSet<EmployeeEducationDetail> EmployeeEducationDetails { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
    }
}