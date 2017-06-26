using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tests
{
    public static class TestExtensions
    {
        public static T ShouldBe<T>(this T obj,int intsuam)
        {
            Assert.IsNull(obj);
            return obj;
        }
        public static T ShouldNotNull<T>(this T obj)
        {
            Assert.IsNull(obj);
            return obj;
        }

        public static T ShouldNotNull<T>(this T obj, string message)
        {
            Assert.IsNull(obj, message);
            return obj;
        }

        public static T ShouldNotBeNull<T>(this T obj)
        {
            Assert.IsNotNull(obj);
            return obj;
        }

        public static T ShouldNotBeNull<T>(this T obj, string message)
        {
            Assert.IsNotNull(obj, message);
            return obj;
        }

        public static T ShouldEqual<T>(this T actual, object expected)
        {
            Assert.AreEqual(expected, actual);
            return actual;
        }

        ///<summary>
        /// Asserts that two objects are equal.
        ///</summary>
        ///<param name="actual"></param>
        ///<param name="expected"></param>
        ///<param name="message"></param>
        ///<exception cref="AssertionException"></exception>
        public static void ShouldEqual(this object actual, object expected, string message)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldBeNull(this object actual)
        {
            Assert.IsNull(actual);
        }

        public static void ShouldBeTheSameAs(this object actual, object expected)
        {
            Assert.AreSame(expected, actual);
        }

        public static void ShouldBeNotBeTheSameAs(this object actual, object expected)
        {
            Assert.AreNotSame(expected, actual);
        }

        public static T CastTo<T>(this object source)
        {
            return (T)source;
        }

        public static void ShouldBeTrue(this bool source)
        {
            Assert.IsTrue(source);
        }

        public static void ShouldBeFalse(this bool source)
        {
            Assert.IsFalse(source);
        }
        public static void AssertSameStringAs(this string actual, string expected)
        {
            if (!string.Equals(actual, expected, StringComparison.InvariantCultureIgnoreCase))
            {
                var message = string.Format("Expected {0} but was {1}", expected, actual);
                throw new Exception(message);
            }
        }

        public static Exception Throws(Action action)
        {
            return Throws(action, null);
        }
        public static Exception Throws(Action action, string message)
        {

            try
            {
                action();
            }
            catch (Exception ex)
            {
                return ex;

            }
            throw new AssertFailedException(message ?? "Expected exception was not thrown.");
        }
        public static T Throws<T>(Action action) where T : Exception
        {
            return Throws<T>(action, null);
        }
        public static T Throws<T>(Action action, string message) where T : Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                T actual = ex as T;
                if (actual == null)
                {
                    throw new AssertFailedException(message ?? String.Format("Expected exception of type {0} not thrown. Actual exception type was {1}.", typeof(T), ex.GetType()));
                }
                return actual;
            }
            throw new AssertFailedException(message ?? String.Format("Expected exception of type {0} not thrown.", typeof(T)));
        }
    }
}
