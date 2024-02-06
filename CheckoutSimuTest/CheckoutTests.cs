using CheckoutSimu;

namespace CheckoutSimuTest
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void GetTotalPrice_EmptyCart_ReturnsZero()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act and Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
        }

        [Test]
        public void GetTotalPrice_UnknownItem_IgnoresItem()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act
            checkout.Scan("X");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
        }

        [Test]
        public void GetTotalPrice_MultipleSpecialPrices_ReturnsCorrectPrice()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(230));
        }

        [Test]
        public void GetTotalPrice_MixedItems_ReturnsCorrectPrice()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Scan("D");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(115));
        }

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
        public void GetTotalPrice_OrderOfScanning_ReturnsCorrectPrice()
        {
            //Arrange
            ICheckout checkout = new Checkout();

            //Act
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("D");
            checkout.Scan("C");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(115));
        }

        [Test]
        public void GetTotalPrice_MultipleCheckoutInstances_ReturnsCorrectPrice()
        {
            //Arrange
            ICheckout checkout1 = new Checkout();
            ICheckout checkout2 = new Checkout();

            //Act
            checkout1.Scan("A");
            checkout2.Scan("A");

            //Assert 
            Assert.That(checkout1.GetTotalPrice(), Is.EqualTo(50));
            Assert.That(checkout2.GetTotalPrice(), Is.EqualTo(50));
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