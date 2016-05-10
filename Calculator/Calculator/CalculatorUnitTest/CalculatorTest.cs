using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Model;

namespace CalculatorUnitTest
{
    [TestClass]
    public class CalculatorTest
    {
        [Description("Division of a negative number into other negative number")]
        [TestMethod]
        public void DivisionOfANegativeNumbers()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("65", calculator.Calculate("60-50/-10"));
        }

        [Description("Сheck the rules of mathematics.")]
        [TestMethod]
        public void SimpleExpression()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("6", calculator.Calculate("2+2*2"));
        }

        [TestMethod]
        public void SimpleParanthesesTest()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("8", calculator.Calculate("(2+2)*2"));
        }

        [Description("Test big expression")]
        [TestMethod]
        public void BigExpression()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("-520,5", calculator.Calculate("((2+3)*4+3+1+4*(0*3))+100*(20-5)/(10-((((20-10)-30)-40)-50))-20*(30+30)-50*(20-30)-(20-(20+93)-50)"));
        }


        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideByZeroExceptionTest()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            calculator.Calculate("1/0");
        }

        [Description("Сheck the rules of mathematics.")]
        [TestMethod]
        public void OperationsTest()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("-6", calculator.Calculate("-(-(5+5)/-10-25+-6)/-(10+5---10)"));
        }

        [Description("Ten divided by three and multiplied by three")]
        [TestMethod]
        public void DivideTest()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("10", calculator.Calculate("10/3*3"));
        }

        [TestMethod]
        public void BigNumbersTest()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("3,4028236692093846E+38", calculator.Calculate("(1,8446744073709552E+19)*(1,8446744073709552E+19)"));
        }

        [TestMethod]
        public void LittleNumbersTest()
        {
            ICalculator calculator = new Calculator.Model.Calculator();
            Assert.AreEqual("3,4028236692093844E-38", calculator.Calculate("(1,8446744073709552E-19)*(1,8446744073709552E-19)"));
        }
    }
}
