namespace OwinModulesHelloWorld
{
    using Superscribe.Owin;

    public class HelloWorldModule : SuperscribeOwinModule
    {
        public HelloWorldModule()
        {
            this.Get["/"] = _ => "Hello world - OWIN style";

            this.Get["Messages"] = _ =>
                new[]
                    {
                        new { From = "Bill", Content = "Fancy a pint later?" },
                        new { From = "Kathryn", Content = "You've been coding instead of doing the washing up again haven't you" }
                    };

            this.Post["Products"] = _ =>
            {
                var product = _.Bind<Product>();

                _.StatusCode = 201;
                return new { Message = string.Format("Received product {0}", product.Name) };
            };
        }
    }
}