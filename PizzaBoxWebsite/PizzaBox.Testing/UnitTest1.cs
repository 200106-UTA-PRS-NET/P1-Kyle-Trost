using PizzaBox.Testing.Mocks;
using PizzaBoxWebsite;
using Xunit;

namespace PizzaBox.Testing
{
    public class UnitTest1
    {
        [Fact]
        // Make sure size repository is not empty
        public void Test1()
        {
            var sizeRepo = new MockRepositorySize();

            Assert.NotEmpty(sizeRepo.GetSizes());
        }

        [Fact]
        // Make sure crust type repository is not empty
        public void Test2()
        {
            var crustRepo = new MockRepositoryCrustType();

            Assert.NotEmpty(crustRepo.GetCrustTypes());
        }

        [Fact]
        // Make sure preset pizza repository is not empty
        public void Test3()
        {
            var presetRepo = new MockRepositoryPresetPizza();

            Assert.NotEmpty(presetRepo.GetPresetPizzas());
        }

        [Fact]
        // Make sure user pizza repository is not empty
        public void Test4()
        {
            var userRepo = new MockRepositoryUser();

            Assert.NotEmpty(userRepo.GetUsers());
        }

        [Fact]
        // Make sure preset pizza repository is not empty
        public void Test5()
        {
            var storeRepo = new MockRepositoryStore();

            Assert.NotEmpty(storeRepo.GetStores());
        }

        [Fact]
        // Make sure passing -1 will get all stores
        public void Test6()
        {
            var storeRepo = new MockRepositoryStore();

            Assert.NotEmpty(storeRepo.GetStores(-1));
        }

        [Fact]
        // Make sure passing -1 will get all sizes
        public void Test7()
        {
            var sizeRepo = new MockRepositorySize();

            Assert.NotEmpty(sizeRepo.GetSizes(-1));
        }

        [Fact]
        // Make sure passing -1 will get all crust types
        public void Test8()
        {
            var crustRepo = new MockRepositoryCrustType();

            Assert.NotEmpty(crustRepo.GetCrustTypes(-1));
        }

        [Fact]
        // Make sure passing -1 will get all preset pizzas
        public void Test9()
        {
            var presetRepo = new MockRepositoryPresetPizza();

            Assert.NotEmpty(presetRepo.GetPresetPizzas(-1));
        }

        [Fact]
        // Make sure the user Joseph with password qwerty is in the mock repo
        public void Test10()
        {
            var testUserRepo = new MockRepositoryUser();

            Assert.NotEmpty(testUserRepo.GetUsers("Joseph", "qwerty"));
        }
    }
}
