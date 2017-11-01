using System;
using Shouldly;
using Valit.Tests.HelperExtensions;
using Xunit;

namespace Valit.Tests.DateTimeOffset_
{
    public class DateTimeOffset_IsSameAs_Tests
    {
        [Fact]
        public void DateTimeOffset_IsSameAs_For_Not_Nullable_Values_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, DateTimeOffset>)null)
                    .IsSameAs(DateTimeOffset.Now);
            });
            
            exception.ShouldBeOfType(typeof(ValitException));
        }

        [Fact]
        public void DateTimeOffset_IsSameAs_For_Not_Nullable_Value_And_Nullable_Value_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, DateTimeOffset>)null)
                    .IsSameAs((DateTimeOffset?)DateTimeOffset.Now);
            });
            
            exception.ShouldBeOfType(typeof(ValitException));
        }

        [Fact]
        public void DateTimeOffset_IsSameAs_For_Nullable_Value_And_Not_Nullable_Value_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, DateTimeOffset?>)null)
                    .IsSameAs(DateTimeOffset.Now);
            });
            
            exception.ShouldBeOfType(typeof(ValitException));
        }    

        [Fact]
        public void DateTimeOffset_IsSameAs_For_Nullable_Values_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, DateTimeOffset?>)null)
                    .IsSameAs((DateTimeOffset?) DateTimeOffset.Now);
            });
            
            exception.ShouldBeOfType(typeof(ValitException));
        }

        [Theory]
        [InlineData("2017-06-10", true)]
        [InlineData("2017-06-09", false)]
        [InlineData("2017-06-11", false)]     
        public void DateTimeOffset_IsSameAs_Returns_Proper_Results_For_Not_Nullable_Values(string stringValue,  bool expected)
        {            
            var value = stringValue.AsDateTimeOffset();

            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => m.Value, _=>_
                    .IsSameAs(value))
                .For(_model)
                .Validate();

            Assert.Equal(result.Succeeded, expected);
        }   

        [Theory]
        [InlineData("2017-06-10", true)]
        [InlineData("2017-06-09", false)]
        [InlineData("2017-06-11", false)]     
        [InlineData(null, false)]     
        public void DateTimeOffset_IsSameAs_Returns_Proper_Results_For_Not_Nullable_Value_And_Nullable_Value(string stringValue,  bool expected)
        {            
            DateTimeOffset? value = stringValue.AsNullableDateTimeOffset();

            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => m.Value, _=>_
                    .IsSameAs(value))
                .For(_model)
                .Validate();

            Assert.Equal(result.Succeeded, expected);
        }

        [Theory]
        [InlineData(false, "2017-06-10", true)]
        [InlineData(false, "2017-06-09", false)]        
        [InlineData(false, "2017-06-11", false)]     
        [InlineData(true, "2017-06-10", false)]     
        public void DateTimeOffset_IsSameAs_Returns_Proper_Results_For_Nullable_Value_And_Not_Nullable_Value(bool useNullValue, string stringValue,  bool expected)
        {            
            var value = stringValue.AsDateTimeOffset();

            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => useNullValue? m.NullValue : m.NullableValue, _=>_
                    .IsSameAs(value))
                .For(_model)
                .Validate();

            Assert.Equal(result.Succeeded, expected);
        }

        [Theory]
        [InlineData(false, "2017-06-10", true)] 
        [InlineData(false, "2017-06-09", false)]    
        [InlineData(false, "2017-06-11", false)]
        [InlineData(false, null, false)]     
        [InlineData(true, "2017-06-10", false)]     
        [InlineData(true, null, false)]     
        public void DateTimeOffset_IsSameAs_Returns_Proper_Results_For_Nullable_Values(bool useNullValue, string stringValue,  bool expected)
        {    
            DateTimeOffset? value = stringValue.AsNullableDateTimeOffset();

            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => useNullValue? m.NullValue : m.NullableValue, _=>_
                    .IsSameAs(value))
                .For(_model)
                .Validate();

            Assert.Equal(result.Succeeded, expected);
        }

#region ARRANGE
        public DateTimeOffset_IsSameAs_Tests()
        {
            _model = new Model();
        }

        private readonly Model _model;

        class Model
        {
            public DateTimeOffset Value => new DateTimeOffset(new DateTime(2017, 6, 10));
            public DateTimeOffset? NullableValue => new DateTimeOffset(new DateTime(2017, 6, 10));
            public DateTimeOffset? NullValue => null;
        }
#endregion 
    }
}