using CheckoutSimu;
using Moq;

namespace CheckoutSimuTest
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void GetTotalPrice_EmptyCart_ReturnsZero()
        {
            //Arrange
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice>());

            ICheckout checkout = new Checkout(pricingRulesMock.Object);

            //Act and Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
        }

        [Test]
        public void GetTotalPrice_UnknownItem_IgnoresItem()
        {
            //Arrange
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice>());

            ICheckout checkout = new Checkout(pricingRulesMock.Object);

            //Act
            checkout.Scan("X");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
        }

        [Test]
        public void GetTotalPrice_MultipleSpecialPrices_ReturnsCorrectPrice()
        {
            //Arrange
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice> { { "A", new SpecialPrice { quantity = 3, specialPrice = 130 } } });

            ICheckout checkout = new Checkout(pricingRulesMock.Object);

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
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 }, { "B", 30 }, { "C", 20 }, { "D", 15 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice>());
            ICheckout checkout = new Checkout(pricingRulesMock.Object);

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
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 }, { "B", 30 }, { "C", 20 }, { "D", 15 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice>());
            ICheckout checkout = new Checkout(pricingRulesMock.Object);

            //Act
            checkout.Scan("A");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void GetTotalPrice_MultipleItems_ReturnsCorrectPrice()
        {
            //Arrange
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 }, { "B", 30 }, { "C", 20 }, { "D", 15 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice> { { "A", new SpecialPrice { quantity = 3, specialPrice = 130 } }, { "B", new SpecialPrice { quantity = 2, specialPrice = 45 } } });
            ICheckout checkout = new Checkout(pricingRulesMock.Object);

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
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 }, { "B", 30 }, { "C", 20 }, { "D", 15 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice> { { "A", new SpecialPrice { quantity = 3, specialPrice = 130 } }, { "B", new SpecialPrice { quantity = 2, specialPrice = 45 } } });
            ICheckout checkout = new Checkout(pricingRulesMock.Object);

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
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 }, { "B", 30 }, { "C", 20 }, { "D", 15 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice> { { "A", new SpecialPrice { quantity = 3, specialPrice = 130 } }, { "B", new SpecialPrice { quantity = 2, specialPrice = 45 } } });
            ICheckout checkout1 = new Checkout(pricingRulesMock.Object);
            ICheckout checkout2 = new Checkout(pricingRulesMock.Object);

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
            var pricingRulesMock = new Mock<IPricingRules>();
            pricingRulesMock.Setup(p => p.ItemPrices).Returns(new Dictionary<string, int> { { "A", 50 }, { "B", 30 }, { "C", 20 }, { "D", 15 } });
            pricingRulesMock.Setup(p => p.SpecialPrices).Returns(new Dictionary<string, SpecialPrice> { { "A", new SpecialPrice { quantity = 3, specialPrice = 130 } }, { "B", new SpecialPrice { quantity = 2, specialPrice = 45 } } });
            ICheckout checkout = new Checkout(pricingRulesMock.Object);

            //Act
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");

            //Assert 
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(95));
        }
    }
}