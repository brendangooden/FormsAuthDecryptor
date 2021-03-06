using System;
using Xunit;

namespace FormsAuthentication.Tests
{
    public class Tests
    {
        public const string EncryptionKey = "539CF2E9E2F8ACE34F5CD2425885CEDC05C50481E978721A";
        public const string ValidationKey = "BDB04B2D098D8C6D00011196EAB9A40EA145D743AE909B489B3BEA29C440FF1EB28B689821E6CE130F08BFE61F411AD4458AC4FA28B314B9873DA26F27AB2E1D";

        [Fact]
        public void DemonstrateSuccessfulDecryptionOfValidFormsAuthCookie()
        {
            const string ValidFormsAuthCookie = "1C21F217EF878B56AD0861A0912EC19E3418665963EE41B937D622970DF529166637468E2370837A234C28D5E8C3BE66028BA61E9464190C6FF27AE84914992D1FB7F77403B457B6610906C42D18794608D9392809B2C55E9D8C68864FD07BB8662A153835F2089CC5D21F8F3F34270E7FD2FE60EE70FB22730EE6FDC3771F927EE1B811E3C9A6B9C804A7F94EE6D385A2D82F1AB7DDECDEBF3D36674D11423AE8BF850EFB71BEFC5F821E9BD31BDC2CA3BDB4EFB74DC7EBA83F1AE69CC622149A2513C0325935AF88F011EBF781200A9C1E0520CFB1B7EBCA8742EE8705EAE9FCB3720995306769332972F07838179F9C086E489DA7C4D4DF10F9112C30C73B86FEE12A1F07E3E6DB765559CFA751A9B94E9C052DE1F93D67D532AE8541ABB1C6D219F1454C86028EBD12E220E88396AD0A670BF7DF6E6BF8E77956A95E7CD69F696F4560F194F0B46DEFACC3910EC52FF5CD6415E483901877538A9F0F12F974300D7A0198BA71D580DB4387EF21FC1D5F042A3B9B92B1F5774952EDA51B45475AC54D6E6EB9B63F2A1A0F9CA4D4490C71407496B7E19D025F0DFE6D95DF89EE9DEC3EDED2A0BAE0F362E078BE2D3F3914842EB99081EB46F1DF08C8DC9921";

            var decryptor = new Decryptor(EncryptionKey, ValidationKey, ValidationAlgorithm.HmacSha256);

            var ticket = decryptor.Decrypt(ValidFormsAuthCookie);

            //"{ "Version":2,"Name":"3178092","Expiration":"2019-06-23T02:20:01.1928409+08:00","IssueDate":"2019-06-22T21:25:01.1928409+08:00","IsPersistent":false,"Expired":true,"UserData":"oEP01LNxCb7H3fUVmmUIcg0K6WrCEQwaW3tiLP0T0xXp/poYKvaqosEulCHaboDU21l1C6YoN1RSAs33uYgpHEGrecK1Ah4VFzdDWQs6WDdBEngjjWisT4+oEP7H0JgIvlI1td13VF8Oj+rkoWZm9+oMJ2tiU0dyZXn/ghEdAGE=","CookiePath":"/"}"
            Assert.NotNull(decryptor.SerializedTicket);
            Assert.Equal("3178092", ticket.Name);
            Assert.Equal(2, ticket.Version);
            Assert.Equal(DateTimeOffset.Parse("2019-06-23T02:20:01.1928409+08:00"), ticket.Expiration);
            Assert.Equal(DateTimeOffset.Parse("2019-06-22T21:25:01.1928409+08:00"), ticket.IssueDate);
            Assert.False(ticket.IsPersistent);
            Assert.True(ticket.Expired);
            Assert.Equal("oEP01LNxCb7H3fUVmmUIcg0K6WrCEQwaW3tiLP0T0xXp/poYKvaqosEulCHaboDU21l1C6YoN1RSAs33uYgpHEGrecK1Ah4VFzdDWQs6WDdBEngjjWisT4+oEP7H0JgIvlI1td13VF8Oj+rkoWZm9+oMJ2tiU0dyZXn/ghEdAGE=", ticket.UserData);
            Assert.Equal("/", ticket.CookiePath);
        }
    }
}
