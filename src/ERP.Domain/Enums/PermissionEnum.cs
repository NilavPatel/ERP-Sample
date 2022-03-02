using System.ComponentModel;

namespace ERP.Domain.Enums
{
    public enum PermissionEnum
    {
        [Description("View Employee:Employee")]
        EmployeeView = 1,
        [Description("Edit Employee:Employee")]
        EmployeeEdit = 2,
        [Description("Add Employee:Employee")]
        EmployeeAdd = 3,
        [Description("Delete Employee:Employee")]
        EmployeeDelete = 4,

        [Description("View User:User")]
        UserView = 5,
        [Description("Edit User:User")]
        UserEdit = 6,
        [Description("Add User:User")]
        UserAdd = 7,
        [Description("Delete User:User")]
        UserDelete = 8,

        [Description("View Role:Role")]
        RoleView = 9,
        [Description("Edit Role:Role")]
        RoleEdit = 10,
        [Description("Add Role:Role")]
        RoleAdd = 11,
        [Description("Delete Role:Role")]
        RoleDelete = 12,

        [Description("View Designation:Designation")]
        DesignationView = 13,
        [Description("Edit Designation:Designation")]
        DesignationEdit = 14,
        [Description("Add Designation:Designation")]
        DesignationAdd = 15,
        [Description("Delete Designation:Designation")]
        DesignationDelete = 16,
    }
}