using CheckoutSimu;

namespace CheckoutSimuTest
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void GetTotalPrice_SingleItem_ReturnsCorrectPrice()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act
            checkout.Scan("A");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void GetTotalPrice_MultipleItems_ReturnsCorrectPrice()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(225));
        }

        [Test]
        public void GetTotalPrice_SpecialPrice_ReturnsCorrectPrice()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(95));
        }
    }
}