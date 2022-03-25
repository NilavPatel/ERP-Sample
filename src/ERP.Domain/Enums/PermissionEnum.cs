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

        [Description("View User:User")]
        UserView = 4,
        [Description("Edit User:User")]
        UserEdit = 5,
        [Description("Add User:User")]
        UserAdd = 6,

        [Description("View Role:Role")]
        RoleView = 7,
        [Description("Edit Role:Role")]
        RoleEdit = 8,
        [Description("Add Role:Role")]
        RoleAdd = 9,
        [Description("Delete Role:Role")]
        RoleDelete = 10,

        [Description("View Designation:Designation")]
        DesignationView = 11,
        [Description("Edit Designation:Designation")]
        DesignationEdit = 12,
        [Description("Add Designation:Designation")]
        DesignationAdd = 13,
        [Description("Delete Designation:Designation")]
        DesignationDelete = 14,

        [Description("View Department:Department")]
        DepartmentView = 19,
        [Description("Edit Department:Department")]
        DepartmentEdit = 20,
        [Description("Add Department:Department")]
        DepartmentAdd = 21,
        [Description("Delete Department:Department")]
        DepartmentDelete = 22
    }
}