using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniCommerce.Domain.Security
{
    public enum Permission  
    {
        ProductNone,
        ProductRead,
        ProductWrite, 
        ProductDelete,
        UserAdminRead,
        UserAdminWrite,
        UserAdminDelete
    }


    //https://ardalis.com/enum-alternatives-in-c
    public abstract class PermissionClass
    {
        internal PermissionClass(int value, string name)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; internal set; }
        public int Value { get; internal set; }


        public static List<PermissionClass> List()
        {
            return new List<PermissionClass>();
        }

        public virtual PermissionClass FromString(string permissionString)
        {
            return List().Single(x => string.Equals(x.Name, permissionString, StringComparison.OrdinalIgnoreCase));
        }

        public virtual PermissionClass FromValue(int value)
        {
            return List().Single(x => x.Value == value);
        }
    }

    //PROBLEM
    // So, the values and names would have to be unique if we did it this way. How would we enforce that across permission classes?

    public class ProductPermission : PermissionClass
    {
        public static ProductPermission Read { get; } = new ProductPermission(0, "ProductRead");
        public static ProductPermission Write { get; } = new ProductPermission(1, "ProductWrite");
        public static ProductPermission Delete { get; } = new ProductPermission(2, "ProductDelete");

        private ProductPermission(int value, string name) : base(value, name)
        {
        }

        public static List<PermissionClass> List()
        {
            return new List<PermissionClass> { Read, Write, Delete };
        }
    }

    public class UserPermission : PermissionClass
    {
        public static UserPermission Read { get; } = new UserPermission(10, "UserRead");
        public static UserPermission Write { get; } = new UserPermission(11, "UserWrite");
        public static UserPermission Delete { get; } = new UserPermission(12, "UserDelete");

        private UserPermission(int value, string name) : base(value, name)
        {
        }

        public static List<PermissionClass> List()
        {
            return new List<PermissionClass> { Read, Write, Delete };
        }
    }
}
