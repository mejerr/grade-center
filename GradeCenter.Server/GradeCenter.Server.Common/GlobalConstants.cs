namespace GradeCenter.Server.Common
{
    public static class GlobalConstants
    {
        public static class Data
        {
            public static class Roles
            {
                public const string AdministratorRoleName = "Administrator";
                public const string PrincipalRoleName = "Principal";
                public const string TeacherRoleName = "Teacher";
                public const string ParentRoleName = "Parent";
                public const string StudentRoleName = "Student";
            }

            public static class School
            {
                public const int NameMinLength = 5;
                public const int NameMaxLength = 150;
                public const int AddressMinLength = 5;
                public const int AddressMaxLength = 400;
            }

            public static class Class
            {
                public const int DivisionMaxLength = 2;
            }

            public static class Subject
            {
                public const int NameMinLength = 2;
                public const int NameMaxLength = 200;
            }

            public static class User
            {
                public const int FullNameMinLength = 5;
                public const int FullNameMaxLength = 150;
                public const int AddressMinLength = 5;
                public const int AddressMaxLength = 400;
            }
        }
    }
}
