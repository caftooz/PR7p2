using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class MatrixCipherUnitTest
    {
        // TC_FUNC_01
        [TestMethod]
        public void Encrypt_CorrectText_3x6_ReturnsExpectedCipher()
        {
            string result = MatrixCipher.Encrypt("ПРОГРАММИРОВАНИЕ", 3, 6);
            Assert.AreEqual(18, result.Length);
            Assert.AreEqual("ПМАРМНОИИГРЕРО АВ ", result);
        }

        // TC_FUNC_02
        [TestMethod]
        public void Decrypt_CorrectCipher_3x6_ReturnsOriginalText()
        {
            string result = MatrixCipher.Decrypt("ПМАРМНОИИГРЕРО АВ ", 3, 6);
            Assert.AreEqual("ПРОГРАММИРОВАНИЕ  ", result);
        }

        // TC_FUNC_03
        [TestMethod]
        public void EncryptThenDecrypt_ReturnsOriginalText()
        {
            string original = "ТЕСТ";
            string cipher = MatrixCipher.Encrypt(original, 2, 3);
            string decrypted = MatrixCipher.Decrypt(cipher, 2, 3);
            Assert.AreEqual(original, decrypted.TrimEnd());
        }

        // TC_FUNC_04
        [TestMethod]
        public void Encrypt_MinimalMatrix_2x2_ReturnsCorrectResult()
        {
            string result = MatrixCipher.Encrypt("АБ", 2, 2);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("А Б ", result);
        }

        // TC_FUNC_05
        [TestMethod]
        public void Encrypt_SingleRow_1xN_TextUnchanged()
        {
            string result = MatrixCipher.Encrypt("ПРИВЕТ", 1, 6);
            Assert.AreEqual("ПРИВЕТ", result);
        }

        // TC_FUNC_06
        [TestMethod]
        public void Encrypt_SingleColumn_Nx1_TextUnchanged()
        {
            string result = MatrixCipher.Encrypt("ПРИВЕТ", 6, 1);
            Assert.AreEqual("ПРИВЕТ", result);
        }

        // TC_FUNC_07
        [TestMethod]
        public void Encrypt_ShortText_PaddedWithSpaces()
        {
            string result = MatrixCipher.Encrypt("АБВ", 2, 3);
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual("А Б В ", result);
        }

        // TC_FUNC_08
        [TestMethod]
        public void Encrypt_TextFillsMatrixExactly_NoExtraSpaces()
        {
            string result = MatrixCipher.Encrypt("АБВГДЕЖЗ", 2, 4);
            Assert.AreEqual(8, result.Length);
            Assert.AreEqual("АДБЕВЖГЗ", result);
            Assert.IsFalse(result.Contains(" "));
        }

        // TC_FUNC_09
        [TestMethod]
        public void Encrypt_TextWithSpacesAndPunctuation_PreservedAfterRoundTrip()
        {
            string original = "ПРИВЕТ МИР!";
            string cipher = MatrixCipher.Encrypt(original, 3, 4);
            string decrypted = MatrixCipher.Decrypt(cipher, 3, 4);
            Assert.AreEqual(original, decrypted.TrimEnd());
        }

        // TC_FUNC_10
        [TestMethod]
        public void Encrypt_SingleCharacter_ReturnsItself()
        {
            string result = MatrixCipher.Encrypt("А", 1, 1);
            Assert.AreEqual("А", result);
        }

        // TC_FUNC_11
        [TestMethod]
        public void Decrypt_WrongMatrixSize_DoesNotMatchOriginal()
        {
            string cipher = MatrixCipher.Encrypt("ПРОГРАММИРОВАНИЕ", 3, 6);

            string wrongDecrypt = MatrixCipher.Decrypt(cipher, 2, 9);
            Assert.AreNotEqual("ПРОГРАММИРОВАНИЕ", wrongDecrypt.TrimEnd());
        }

        // TC_NEG_01
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_EmptyString_ThrowsArgumentException()
        {
            MatrixCipher.Encrypt("", 2, 3);
        }

        // TC_NEG_02
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_NullText_ThrowsArgumentNullException()
        {
            MatrixCipher.Encrypt(null, 2, 3);
        }

        // TC_NEG_03
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Encrypt_RowsZero_ThrowsArgumentOutOfRangeException()
        {
            MatrixCipher.Encrypt("ПРИВЕТ", 0, 3);
        }

        // TC_NEG_04
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Encrypt_ColsZero_ThrowsArgumentOutOfRangeException()
        {
            MatrixCipher.Encrypt("ПРИВЕТ", 3, 0);
        }

        // TC_NEG_05
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Encrypt_NegativeRows_ThrowsArgumentOutOfRangeException()
        {
            MatrixCipher.Encrypt("ПРИВЕТ", -1, 3);
        }

        // TC_NEG_06
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Encrypt_NegativeCols_ThrowsArgumentOutOfRangeException()
        {
            MatrixCipher.Encrypt("ПРИВЕТ", 3, -2);
        }

        // TC_NEG_07
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_MatrixSmallerThanText_ThrowsArgumentException()
        {
            MatrixCipher.Encrypt("ПРОГРАММИРОВАНИЕ", 2, 3);
        }

        // TC_NEG_02 для Decrypt
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_NullText_ThrowsArgumentNullException()
        {
            MatrixCipher.Decrypt(null, 2, 3);
        }
    }
}