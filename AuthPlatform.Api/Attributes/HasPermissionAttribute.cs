namespace AuthPlatform.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    //Where this attribute is allowed to be used.
    public class HasPermissionAttribute : Attribute
    {
        public string PermissionCode { get; }

        public HasPermissionAttribute(string permissionCode)
        {
            PermissionCode = permissionCode;
        }
    }
}
