using Bogus;
using TekusApi.Models;
using Xunit;

namespace TekusApi.Test.Mocks
{
    public static class ProviderMock
    {
        /// <summary>
        /// Creates a mock instance of CreateProvider.
        /// </summary>
        /// <returns>A populated CreateProvider instance.</returns>
        public static CreateProvider Create()
        {
            return new Faker<CreateProvider>()
                .RuleFor(p => p.Nit, f => f.Random.String2(10, "0123456789"))
                .RuleFor(p => p.Name, f => f.Company.CompanyName()) 
                .RuleFor(p => p.Email, f => f.Internet.Email()) 
                .RuleFor(p => p.PersonalizedFields, f => new Dictionary<string, string>
                {
                    { "Field1", f.Lorem.Word() },
                    { "Field2", f.Lorem.Word() },
                    { "Field3", f.Lorem.Word() }
                }); 
        }

        /// <summary>
        /// Asserts that two CreateProvider instances are equal.
        /// </summary>
        /// <param name="expected">The expected CreateProvider.</param>
        /// <param name="actual">The actual CreateProvider.</param>
        public static void Equal(CreateProvider expected, CreateProvider actual)
        {
            Assert.Equal(expected.Nit, actual.Nit);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.PersonalizedFields, actual.PersonalizedFields);
        }
    }
}
