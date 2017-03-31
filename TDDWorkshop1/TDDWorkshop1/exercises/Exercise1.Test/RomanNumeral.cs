using NUnit.Framework;

namespace Exercise1.Test
{
    public class RomanNumeral
    {
        [Test]
        public void RomanNumeral_should_transform_5_to_numberal_V()
        {
            // arrange
            var romanNumeral = new Exercise1.RomanNumeral(5);

            // act
            var textNumeral = romanNumeral.ToString();

            // assert
            Assert.AreEqual("V", textNumeral);
        }

        ///
        /// Exercise 1:1
        /// 
        [Test]
        public void RomanNumeral_should_transform_11_to_numeral_XI()
        {
            // arrange
            var romanNumeral = new Exercise1.RomanNumeral(11);

            // act
            var textNumeral = romanNumeral.ToString();

            // assert
            Assert.AreEqual("XI", textNumeral);

        }

        ///
        /// Exercise 1:2
        ///
        [Test]
        public void RomanNumeral_returns_empty_string_on_negative_number()
        {
            // arrange
            var romanNumeral = new Exercise1.RomanNumeral(-1);

            // act
            var textNumeral = romanNumeral.ToString();

            // assert
            Assert.AreEqual("", textNumeral);
            // Assert.Throws(() => romanNumeral.ToString());

        }
        ///
        /// Exercise 1:3
        ///
        [TestCase(5, "V")]
        [TestCase(11, "XI")]
        [TestCase(9, "IX")]
        [TestCase(20, "XX")]
        [TestCase(0, "")]
        [TestCase(int.MaxValue, "XX")]
        [TestCase(20.4, "XX")]
        [TestCase(25*3+20, "XX")]
        [TestCase(2000, "MM")]
        public void RomanNumeralTests(int value, string expectedResult)
        {
            // arrange
            var romanNumeral = new Exercise1.RomanNumeral(value);

            // act
            var textNumeral = romanNumeral.ToString();

            // assert
            Assert.AreEqual(expectedResult, textNumeral);
        }

       

    }
}
