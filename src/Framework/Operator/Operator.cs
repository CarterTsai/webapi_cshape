using System;

namespace webapi.Framework.DAL
{
    class OperationTest : IOperationTest
    {
        public OperationTest()
        {
        }

        public string Get()
        {
            return "Hello";
        }
    }
}