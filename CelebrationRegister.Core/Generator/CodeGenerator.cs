using System;
using System.Collections.Generic;
using System.Text;

namespace CelebrationRegister.Core.Generator
{
    public class CodeGenerator
    {
        public static string GenerateActivateCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        
        public static string GenerateCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
