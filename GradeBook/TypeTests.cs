using System;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage);
        
    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");
            //Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        //[Fact]
        //public void ValueTypesAlsoPassByValue()
        //{
            
        //    SetInt(ref x);

        //    //Assert.Equal(42, x);
        //}

        private void SetInt(ref Int32 z)
        {
            z = 42;
        }

        
    }

    internal class Assert: Attribute
    {
    }

    internal class FactAttribute : Attribute
    {
    }
}
