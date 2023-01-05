using Week01_1_Intro;

namespace Week01_2_Intro_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddTwoNumbers_SmallIntegers()
        {
            // Arrange
            int a = 5;
            int b = 10;
            int expectedResult = 15;

            // Act
            int result = SampleClass.AddTwoNumbers(a, b);

            //Assert
            Assert.AreEqual(expectedResult, result, "Incorrect sum returned.");

        }


        [TestMethod]
        public void TestMultiplyTwoNumbers_SmallIntegers()
        {
            // Arrange
            int a = 5;
            int b = 10;
            int expectedResult = 50;

            // Act
            int result = SampleClass.MultiplyTwoNumbers(a, b);

            //Assert
            Assert.AreEqual(expectedResult, result, "Incorrect product returned.");

        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestDivideTwoNumbers_DivideByZero()
        {
            // Arrange
            int a = 20;
            int b = 0;

            // Act
            decimal result = SampleClass.DivideTwoNumbers(a, b);

            //Assert


        }

    }
}