using System.ComponentModel;

namespace ERP.Domain.Enums
{
    public enum PermissionEnum
    {
        [Description("View Employee:Employees")]
        EmployeeView = 1,
        [Description("Edit Employee:Employees")]
        EmployeeEdit = 2,
        [Description("Add Employee:Employees")]
        EmployeeAdd = 3,

        [Description("View User:Users")]
        UserView = 4,
        [Description("Edit User:Users")]
        UserEdit = 5,
        [Description("Add User:Users")]
        UserAdd = 6,

        [Description("View Role:Roles")]
        RoleView = 7,
        [Description("Edit Role:Roles")]
        RoleEdit = 8,
        [Description("Add Role:Roles")]
        RoleAdd = 9,
        [Description("Delete Role:Roles")]
        RoleDelete = 10,

        [Description("View Designation:Designations")]
        DesignationView = 11,
        [Description("Edit Designation:Designations")]
        DesignationEdit = 12,
        [Description("Add Designation:Designations")]
        DesignationAdd = 13,
        [Description("Delete Designation:Designations")]
        DesignationDelete = 14,

        [Description("View LeaveType:LeaveTypes")]
        LeaveTypeView = 15,
        [Description("Edit LeaveType:LeaveTypes")]
        LeaveTypeEdit = 16,
        [Description("Add LeaveType:LeaveTypes")]
        LeaveTypeAdd = 17,
        [Description("Delete LeaveType:LeaveTypes")]
        LeaveTypeDelete = 18,

        [Description("View Department:Departments")]
        DepartmentView = 19,
        [Description("Edit Department:Departments")]
        DepartmentEdit = 20,
        [Description("Add Department:Departments")]
        DepartmentAdd = 21,
        [Description("Delete Department:Departments")]
        DepartmentDelete = 22,

        [Description("View Holiday:Holidays")]
        HolidayView = 23,
        [Description("Edit Holiday:Holidays")]
        HolidayEdit = 24,
        [Description("Add Holiday:Holidays")]
        HolidayAdd = 25,
        [Description("Delete Holiday:Holidays")]
        HolidayDelete = 26,

        [Description("View Leaves To Approve:Leaves To Approve")]
        LeaveToApproveView = 31,
        [Description("Edit Leaves To Approve:Leaves To Approve")]
        LeaveToApproveEdit = 32,

        [Description("View My Leave:My Leaves")]
        MyLeaveView = 33,
        [Description("Edit My Leave:My Leaves")]
        MyLeaveEdit = 34,
        [Description("Add My Leave:My Leaves")]
        MyLeaveAdd = 35,
        [Description("Delete My Leave:My Leaves")]
        MyLeaveDelete = 36,

        [Description("View Project:Projects")]
        ProjectView = 37,
        [Description("Edit Project:Projects")]
        ProjectEdit = 38,
        [Description("Add Project:Projects")]
        ProjectAdd = 39,
        [Description("Delete Project:Projects")]
        ProjectDelete = 40
    }
}